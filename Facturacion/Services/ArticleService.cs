using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Data;
using Facturacion.Domain;

namespace Facturacion.Services
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository repository)
        {
            _articleRepository = repository;
        }

        public List<Article> GetAll()
        {
            return _articleRepository.GetAll();
        }

        public Article GetById(int id)
        {
            return _articleRepository.GetById(id);
        }

        public bool Save(Article article)
        {
            try
            {
                return _articleRepository.Save(article);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ArticleService.Save", ex);
            }

        }

        public bool Delete(int id)
        {
            return _articleRepository.Delete(id);
        }
    }
}
