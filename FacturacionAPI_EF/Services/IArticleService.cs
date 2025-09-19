using FacturacionAPI_EF.Models;

namespace FacturacionAPI_EF.Services
{
    public interface IArticleService
    {
        List<Article> GetAll();
        Article? GetById(int id);
        void Create(Article article);
        void Update(Article article);
        void Delete(int id);
    }
}
