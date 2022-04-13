using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Utils.Enums;

namespace Utils.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null;
        }

        public static string GetNickname<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var nicknameAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(NicknameAttribute), false)
                            .FirstOrDefault() as NicknameAttribute;

                        if (nicknameAttribute != null)
                        {
                            return nicknameAttribute.Nickname;
                        }
                    }
                }
            }

            return null;
        }

        public static int[] GetEquipes<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var equipesAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(EquipesAttribute), false)
                            .FirstOrDefault() as EquipesAttribute;

                        if (equipesAttribute != null)
                        {
                            return equipesAttribute.Equipes;
                        }
                    }
                }
            }

            return null;
        }

        public static string GetEmailPagseguro<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var emailPagSeguroAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(EmailPagSeguroAttribute), false)
                            .FirstOrDefault() as EmailPagSeguroAttribute;

                        if (emailPagSeguroAttribute != null)
                        {
                            return emailPagSeguroAttribute.EmailPagSeguro;
                        }
                    }
                }
            }

            return null;
        }

        public static string GetTokenPagseguro<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var tokenPagSeguroAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(TokenPagSeguroAttribute), false)
                            .FirstOrDefault() as TokenPagSeguroAttribute;

                        if (tokenPagSeguroAttribute != null)
                        {
                            return tokenPagSeguroAttribute.TokenPagSeguro;
                        }
                    }
                }
            }

            return null;
        }

        public static T GetEnumValueFromDescription<T>(string description)
        {
            if (description == null)
                return default(T);

            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                .SelectMany(f => f.GetCustomAttributes(
                    typeof(DescriptionAttribute), false), (
                    f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                                                                              .Description.ToLower() == description.ToLower());
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<EnumModel> GetDescriptions<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            List<EnumModel> descriptions = new List<EnumModel>();
            var type = typeof(TEnum);
            foreach (int val in System.Enum.GetValues(type))
            {
                var memInfo = type.GetMember(type.GetEnumName(val));
                var descriptionAttribute = memInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null)
                {
                    descriptions.Add(new EnumModel { Id = val, Description = descriptionAttribute.Description });
                }
            }
            return descriptions;
        }

        public class EnumModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
    }
}
