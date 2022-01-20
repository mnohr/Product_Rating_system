using product_rating.Repositories.Interfaces;
using System.Collections;
using systemrating.Data.EntityModels;

namespace product_rating.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
  
        public void AddProduct(Product input)
        {
            _context.Products.Add(input);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            var productexit =  _context.Products.Where(x => x.Id == product.Id).FirstOrDefault();
            if (productexit != null)
            {
                _context.Remove(productexit);
                 _context.SaveChanges();
            }
        }

        public void EditProduct(Product getproduct)
        {
            var ProductExit = _context.Products.Where(x => x.Id == getproduct.Id).FirstOrDefault();
            if (ProductExit != null)
            {
                ProductExit.Name = getproduct.Name;
                ProductExit.Description = getproduct.Description;
                ProductExit.Quantity = getproduct.Quantity;
                ProductExit.Price = getproduct.Price;
                ProductExit.Photo = getproduct.Photo;

                _context.SaveChangesAsync();
            }
        }

        public List<Product> GetAll()
        {
            var productList = _context.Products.ToList();
            return productList;
        }

        public Product GetById(int id)
        {
            var GetId = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            return GetId;
        }

        public IEnumerable ViewAllDetails(int id)
        {
            var review = _context.Reviews.ToList();
            var product = _context.Products.Where(x => x.Id == id).ToList();

            var productdetails = product.Select(y => new
            {
                Id = y.Id,
                PReivewDetails = review.Where(x => x.ProductId == id).FirstOrDefault() != null ? review.Where(x => x.ProductId == id).Select(x => new { PRating = x.Rating, PComment = x.Comments, PReviewDate = x.CreatedDate, PUserName = _context.Users.Where(y => y.Id == x.UserId).Select(x => x.Name).ToList() }).ToList() : null,
                PaverageRating = review.Where(x => x.ProductId == id).FirstOrDefault() != null ? review.Where(x => x.ProductId == id).Average(x => x.Rating) : 0,
                TotalRating = review.Where(x => x.ProductId == id).FirstOrDefault() != null ? review.Where(x => x.ProductId == id).Select(x => x.Rating).Count() : 0
            }).ToList();

             return productdetails;
            
        }
    }
}
