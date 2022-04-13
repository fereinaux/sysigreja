using Data.Entities;
using System.Linq;

namespace Core.Business.Newsletter
{
    public interface INewsletterBusiness
    {
        IQueryable<Data.Entities.Newsletter> GetFones(string whatsapp);

        void InsertWhatsapp(string whatsapp);
    }
}
