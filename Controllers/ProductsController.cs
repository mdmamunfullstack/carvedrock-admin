using carvedrock_admin.Data;
using carvedrock_admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carvedrock_admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductLogic _logic;

        //public List<ProductModel> Products {get;set;}
        public ProductsController(IProductLogic logic)
        {
            //Products = GetSampleProducts();
            _logic = logic;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _logic.GetAllProducts();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _logic.GetProductById(id);
            return product == null ? NotFound() : View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                await _logic.AddNewProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductsData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _logic.GetProductById(id.Value);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: ProductsData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductModel product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _logic.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: ProductsData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productModel = await _logic.GetProductById(id.Value);
            if (productModel == null) return NotFound();

            return View(productModel);
        }

        // POST: ProductsData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _logic.RemoveProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
