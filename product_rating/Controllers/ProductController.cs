using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product_rating.Repositories.Interfaces;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using systemrating.Data.EntityModels;
using Newtonsoft.Json.Linq;
using systemrating.Data.Dtos;

namespace product_rating.Controllers
{
    public class ProductController: Controller  //why ContorllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        public ProductController(ApplicationDbContext context,IProductRepository productRepository)
        {
            _context = context; 
            _productRepository = productRepository;
        }
        //[HttpGet("api/getallproducts")]
        public async Task< IActionResult> Index()
        {
            var Product = _productRepository.GetAll();  
            return View(Product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   _productRepository.AddProduct(product);  
                    return RedirectToAction("Index");
                } catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty,$"something went wrong {ex.Message}");
                }
                
            }
            
                ModelState.AddModelError(String.Empty, "something went wrong");
                return View(product);
            
            
        }

        [HttpGet]
        public  IActionResult Edit(int id)
        {
            var product =  _productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productRepository.EditProduct(product);
                    return RedirectToAction("Index"); 
                 
                }catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"something went wrong {ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, "something went wrong");
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product =  _productRepository.GetById(id); 
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productRepository.DeleteProduct(product);
                     return RedirectToAction("Index");
                    
                }catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, "something went wrong");
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ViewDetails(int id)
        {
            ViewBag.ProductId = id;
            ViewBag.Productdetails = _productRepository.ViewAllDetails(id);
            return View();
        }
     
        [HttpGet]
        public IActionResult FilterProduct()
        {
            var product = _productRepository.GetAllReviewByRating();
            return View(product);
        }

        //[HttpGet]
        //public IActionResult ViewAllProductOrderByRating()
        //{
        //    var product = _productRepository.GetAllReviewByRating();
        //    return View()     
        //}


        [HttpGet]
        public IActionResult BestReviewOnProduct()
        {
            var bestReview = _productRepository.GetBestReview();
            return new JsonResult(bestReview);
        }


        [HttpGet]
        public IActionResult BadReviewProduct()
        {
            var product = _productRepository.GetBadReview();
            return new JsonResult(product);
        }


        [HttpGet]
        public IActionResult ExportAllReviewInCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("All Product Review order by Rating");
            sb.AppendLine("Product Name, Description, Price, Quantity, AverageRating");
            foreach (var item in _productRepository.GetAllReviewByRating())
            {
                sb.AppendLine($"{item.Name}, {item.Description} ,{item.Price} ,{item.Quantity},{item.AverageRating}");
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "product.csv");
        }

        [HttpGet]
        public IActionResult ExportBestReviewInCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Product Name, Description, Price, Quantity, AverageRating");
            foreach (var item in _productRepository.GetBestReview())
            {
                sb.AppendLine($"{item.Name}, {item.Description} ,{item.Price} ,{item.Quantity},{item.AverageRating}");
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "product.csv");
        }

        [HttpGet]
        public IActionResult ExportBadReviewInCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("All Bad Reivew on Product Details");
            sb.AppendLine("Product Name, Description, Price, Quantity, AverageRating");
            foreach (var item in _productRepository.GetBadReview())
            {
                sb.AppendLine($"{item.Name}, {item.Description} ,{item.Price} ,{item.Quantity},{item.AverageRating}");
            }
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "product.csv");
        }

    }
}
