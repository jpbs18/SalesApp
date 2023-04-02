using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

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
            if (id == null) RedirectToAction(nameof(Error), new { message = "Id not provided" });

            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(seller);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null) RedirectToAction(nameof(Error), new { message = "Id not provided" });

            Seller seller = _sellerService.FindById(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

            var departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, Seller seller)
        {
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            try
            {
                _sellerService.Update(seller);
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch(DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }

    }
}
