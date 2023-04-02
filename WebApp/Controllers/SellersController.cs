﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.FindAllAsync();
            return View(sellers);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            Seller seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _sellerService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            Seller seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(seller);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() };
                return View(viewModel);
            }

            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch(DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }

    }
}
