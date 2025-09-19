using FacturacionAPI_EF.Models;

namespace FacturacionAPI_EF.Data.Repositories
{
    public interface IArticleRepository
    {
        List<Article> GetAll();
        Article? GetById(int id);
        void Create(Article article);
        void Update(Article article);
        void Delete(int id);
    }
}
