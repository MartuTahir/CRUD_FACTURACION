using FacturacionAPI_EF.Data.Repositories;
using FacturacionAPI_EF.Models;

namespace FacturacionAPI_EF.Services
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository repository)
        {
            _articleRepository = repository;
        }
        public void Create(Article article)
        {
            try
            {
                var existingArticle = _articleRepository.GetById(article.IdArticulo);
                if (existingArticle != null) //validar que no exista un articulo con mismo id
                {
                    throw new Exception("El artículo ya existe.");
                }
                else
                {
                    _articleRepository.Create(article);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el artículo: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var existingArticle = _articleRepository.GetById(id);
                if (existingArticle == null) //validar que exista un articulo con mismo id
                {
                    throw new Exception("El artículo no existe.");
                }
                else
                {
                    _articleRepository.Delete(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el artículo: " + ex.Message);
            }
        }

        public List<Article> GetAll()
        {
            try
            {
                return _articleRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los artículos: " + ex.Message);
            }
        }

        public Article? GetById(int id)
        {
            try
            {
                var article = _articleRepository.GetById(id);
                if (article == null)
                {
                    throw new Exception("El artículo no existe.");
                }
                return article;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el artículo: " + ex.Message);
            }
        }

        public void Update(Article article)
        {
            try
            {
                var existingArticle = _articleRepository.GetById(article.IdArticulo);
                if (existingArticle == null) //validar que exista un articulo con mismo id
                {
                    throw new Exception("El artículo no existe.");
                }
                else
                {
                    _articleRepository.Update(article);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el artículo: " + ex.Message);
            }
        }
    }
}
