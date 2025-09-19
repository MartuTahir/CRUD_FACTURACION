using FacturacionAPI_EF.Models;

namespace FacturacionAPI_EF.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private LibraryContext _context;

        public ArticleRepository(LibraryContext context)
        {
            _context = context;
        }

        public void Create(Article article)
        {
            _context.Articulos.Add(article);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var articleDeleted = GetById(id);
            if (articleDeleted != null)
            {
                _context.Articulos.Remove(articleDeleted);
                _context.SaveChanges();
            }   
        }

        public List<Article> GetAll()
        {
            return _context.Articulos.ToList();
        }

        public Article? GetById(int id)
        {
            return _context.Articulos.Find(id);
        }

        public void Update(Article article)
        {
            if(article != null)
            {
                _context.Articulos.Update(article);
                _context.SaveChanges();
            }
        }
    }
}
