#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactManager.Data;
using ContactManager.Models;
using ContactManager.BOClass;
using Microsoft.AspNetCore.Authorization;

namespace ContactManager.Controllers
{
    [Authorize]
    public class CountriesController : Controller
    {
        CountryBO objBO;
        public CountriesController(DataContext context)
        {
            objBO = new CountryBO(context);
        }
        public IActionResult Index()
        {
            return View(objBO.GetAll());
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = objBO.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkcountryId,CountryName,ZipCodeStart,ZipCodeEnd,IsActive")] Country country)
        {
            if (ModelState.IsValid)
            {
                objBO.Add(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = objBO.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkcountryId,CountryName,ZipCodeStart,ZipCodeEnd,IsActive")] Country country)
        {
            if (id != country.PkcountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    objBO.Update(country);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.PkcountryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = objBO.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            objBO.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return objBO.CountryExits(id);
        }
    }
}
