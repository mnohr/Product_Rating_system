using System.Collections;
using systemrating.Data.EntityModels;

namespace product_rating.Repositories.Interfaces
{
    public interface IProductRepository
    {
         void AddProduct(Product input);
        List<Product> GetAll();

        Product GetById(int id);
        void EditProduct(Product product);

        void DeleteProduct(Product product);

        IEnumerable ViewAllDetails(int id);
    }
}
