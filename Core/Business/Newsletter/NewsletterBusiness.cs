using Data.Repository;
using System.Linq;

namespace Core.Business.Newsletter
{
    public class NewsletterBusiness : INewsletterBusiness
    {
        private readonly IGenericRepository<Data.Entities.Newsletter> newsletterRepository;

        public NewsletterBusiness(IGenericRepository<Data.Entities.Newsletter> newsletterRepository)
        {
            this.newsletterRepository = newsletterRepository;
        }

        public IQueryable<Data.Entities.Newsletter> GetFones(string whatsapp)
        {
            if (whatsapp != null)
            {
                return newsletterRepository.GetAll(n => n.Whatsapp.Equals(whatsapp));
            }

            return newsletterRepository.GetAll();
        }

        public void InsertWhatsapp(string whatsapp)
        {
            if (!GetFones(whatsapp).Any())
            {
                newsletterRepository.Insert(new Data.Entities.Newsletter { Whatsapp = whatsapp });
                newsletterRepository.Save();
            }
        }
    }
}