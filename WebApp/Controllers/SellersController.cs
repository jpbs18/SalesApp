using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Services;
using WebApp.Services.Exceptions;

namespace WebApp.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var sellers = _sellerService.FindAll();
            return View(sellers);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(long? id)
        {
            if (id == null) return NotFound();

            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null) return NotFound();

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            _sellerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(long? id)
        {
            if (id == null) return NotFound();

            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null) return NotFound();

            return View(seller);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null) return NotFound();

            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null) return NotFound();

            var departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, Seller seller)
        {
            if (id != seller.Id) return BadRequest();

            try
            {
                _sellerService.Update(seller);
            }
            catch(NotFoundException ex)
            {
                return NotFound();
            }
            catch(DbConcurrencyException ex)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
