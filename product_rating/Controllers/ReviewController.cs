using Microsoft.AspNetCore.Mvc;
using systemrating.Data.EntityModels;

namespace product_rating.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> ReviewProduct(Review Reviewproduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    await _context.AddAsync(Reviewproduct);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(actionName: "ViewDetails", controllerName: "Product");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, $"something went wrong {ex.Message}");
                }

            }

            ModelState.AddModelError(String.Empty, "something went wrong");
            return View(Reviewproduct);


        }
    }
}
