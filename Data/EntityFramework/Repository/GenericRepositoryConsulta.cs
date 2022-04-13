using Data.Context;
using Data.Entities.Base;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Utils.Enums;

namespace Data.Repository
{
    public class GenericRepositoryConsulta<T> : IGenericRepositoryConsulta<T> where T : class
    {
        private ConsultaDbContext _context = null;
        private DbSet<T> dbSet = null;
        private DbConnection conexao = null;

        public GenericRepositoryConsulta(ConsultaDbContext context)
        {
            this._context = context;
            dbSet = _context.Set<T>();
            this.conexao = _context.Database.Connection;
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> where = null)
        {
            var query = dbSet.AsQueryable();

            if (IsClassBase(typeof(StatusBase)))
                query = query.Where(StatusCondition());

            if (where == null)
            {
                return query;
            }

            return query.Where(where);
        }

        private Expression<Func<T, bool>> StatusCondition()
        {
            var param = Expression.Parameter(typeof(T));
            var condition =
                Expression.Lambda<Func<T, bool>>(
                    Expression.NotEqual(
                        Expression.Property(param, "Status"),
                        Expression.Constant(StatusEnum.Deletado, typeof(StatusEnum))
                    ),
                    param
                );
            return condition;
        }

        private Expression<Func<T, bool>> IdCondition(object id)
        {
            var param = Expression.Parameter(typeof(T));
            if (Expression.Property(param, "Id").Type == typeof(string))
            {
                var condition =
               Expression.Lambda<Func<T, bool>>(
                   Expression.Equal(
                       Expression.Property(param, "Id"),
                       Expression.Constant(id, typeof(object))
                   ),
                   param
               );
                return condition;
            }
            else
            {
                var condition =
                    Expression.Lambda<Func<T, bool>>(
                        Expression.Equal(
                            Expression.Property(param, "Id"),
                            Expression.Constant(id, typeof(int))
                        ),
                        param
                    );
                return condition;
            }
        }


        private IQueryable<T> GetIncludes(IQueryable<T> query)
        {            
            var properties = getProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod()?.IsVirtual ?? false;
                if (isVirtual && ((properties.FirstOrDefault(c => c.Name == property.Name + "Id") != null) || (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && typeof(string) != property.PropertyType)))
                {
                    query = query.Include(property.Name);
                    var secondProperties = property.PropertyType.GetProperties();
                    foreach (var secondProperty in secondProperties)
                    {
                        if (secondProperty.GetGetMethod().IsVirtual &&
                            secondProperties.FirstOrDefault(c => c.Name == secondProperty.Name + "Id") != null)
                        {
                            query = query.Include($"{property.Name}.{secondProperty.Name}");
                        }
                    }
                }
            }

            return query;
        }

        public T GetById(object id)
        {
            return GetAll(IdCondition(id)).First();
        }

        public virtual void InsertOrUpdate(T obj)
        {
            dbSet.AddOrUpdate(obj);
        }

        public void Insert(T obj)
        {
            if (IsClassBase(typeof(UsuarioBase)))
            {
                obj.GetType().GetProperty("UsuarioCadastroId").SetValue(obj, GetUserId());
                obj.GetType().GetProperty("DataCadastro").SetValue(obj, DateTime.Now.AddHours(4));
            }

            dbSet.Add(obj);
        }
        public void Update(T obj)
        {
            if (IsClassBase(typeof(UsuarioBase)))
            {
                obj.GetType().GetProperty("UsuarioEdicaoId").SetValue(obj, GetUserId());
                obj.GetType().GetProperty("DataEdicao").SetValue(obj, DateTime.Now.AddHours(4));
            }

            dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        private string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }

        public void Delete(object id)
        {
            T existing = dbSet.Find(id);

            if (IsClassBase(typeof(StatusBase)))
            {
                existing.GetType().GetProperty("Status").SetValue(existing, StatusEnum.Deletado);
                existing.GetType().GetProperty("UsuarioDelecaoId").SetValue(existing, GetUserId());
                existing.GetType().GetProperty("DataDelecao").SetValue(existing, DateTime.Now.AddHours(4).AddHours(4));
                Update(existing);
            }
            else
            {
                dbSet.Remove(existing);
            }
        }

        private bool IsClassBase(Type baseClass)
        {
            var type = typeof(T);

            while (type.BaseType != typeof(Object))
            {
                type = type.BaseType;

                if (type == baseClass)
                {
                    return true;
                }
            }

            return false;
        }

        private PropertyInfo[] getProperties()
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            return properties;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
