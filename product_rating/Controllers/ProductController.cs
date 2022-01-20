using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product_rating.Repositories.Interfaces;
using systemrating.Data.EntityModels;


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

        //[HttpGet("getproductid")]

        //public async Task<IActionResult> Getp()
        //{
        //    var q = _context.Reviews;
           
        //    return Ok(q);

        //}

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
                   // await _context.AddAsync(product);
                   // await _context.SaveChangesAsync();
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
                    
                    ModelState.AddModelError(string.Empty, "something went worng");
                    return View(product);
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
                    

                    //ModelState.AddModelError(String.Empty, "something went wrong");
                    //return View(product);
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

            ViewBag.Product = _context.Products.Where(x => x.Id == id).ToList();
            ViewBag.ProductId = id;
            ViewBag.Productdetails = _productRepository.ViewAllDetails(id);
      
            return View();
          
        }
     

        [HttpGet("pr")]
        public PartialViewResult ViewAllProduct()
        {

            var product = _context.Products.ToList();
            var review = _context.Reviews.ToList();

            var Productquery = (from p in product
                        select new
                        {
                            Name = p.Name,
                            Description = p.Description,
                            Quantity = p.Quantity,
                            Price = p.Price,
                            Image = p.Photo,
                            AverageRating = review.Where(x => x.ProductId == p.Id).Average(x => x.Rating),

                        }).OrderByDescending(x=> x.AverageRating).ToList();
            if(Productquery != null)
            {
                return PartialView("FilterProduct",Productquery);
            }

            return PartialView();
        }


        [HttpGet("pr1")]
        public PartialViewResult BestReviewProduct()
        {

            var product = _context.Products.ToList();
            var review = _context.Reviews.ToList();

            var Productquery = (from p in product
                                select new
                                {
                                    Name = p.Name,
                                    Description = p.Description,
                                    Quantity = p.Quantity,
                                    Price = p.Price,
                                    Image = p.Photo,
                                    AverageRating = review.Where(x => x.ProductId == p.Id).Average(x => x.Rating),

                                }).Where(x => x.AverageRating >= 3).OrderByDescending(x => x.AverageRating).ToList();
            if (Productquery != null)
            {
                return PartialView("FilterProduct", Productquery);
            }

            return PartialView();
        }



        [HttpGet("pr2")]
        public PartialViewResult BadReviewProduct()
        {

            var product = _context.Products.ToList();
            var review = _context.Reviews.ToList();

            var Productquery = (from p in product
                                select new
                                {
                                    Name = p.Name,
                                    Description = p.Description,
                                    Quantity = p.Quantity,
                                    Price = p.Price,
                                    Image = p.Photo,
                                    AverageRating = review.Where(x => x.ProductId == p.Id).Average(x => x.Rating),

                                }).Where(x => x.AverageRating <  3).OrderByDescending(x => x.AverageRating).ToList();
            if (Productquery != null)
            {
                return PartialView("FilterProduct", Productquery);
            }

            return PartialView();
        }
    }
}
