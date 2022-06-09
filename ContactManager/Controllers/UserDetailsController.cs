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
    public class UserDetailsController : Controller
    {
        UserDetailsBO objBO;
        public UserDetailsController(DataContext context)
        {
            objBO = new UserDetailsBO(context);
        }
        [Authorize]
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

            var userDetail =objBO.GetById(id.Value);
                
            if (userDetail == null)
            {
                return NotFound();
            }

            return View(userDetail);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkuserId,UserName,Password,FirstName,LastName,EmailId,PhoneNo,IsActive")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                objBO.Add(userDetail);
                return RedirectToAction(nameof(Index));
            }
            return View(userDetail);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetail = objBO.GetById(id.Value);
            if (userDetail == null)
            {
                return NotFound();
            }
            return View(userDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkuserId,UserName,Password,FirstName,LastName,EmailId,PhoneNo,IsActive")] UserDetail userDetail)
        {
            if (id != userDetail.PkuserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
				try
				{
                     objBO.Update(userDetail);
                }
				catch (DbUpdateConcurrencyException)
				{
					if (!UserDetailExists(userDetail.PkuserId))
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
            return View(userDetail);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetail = objBO.GetById(id.Value);
            if (userDetail == null)
            {
                return NotFound();
            }

            return View(userDetail);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            objBO.Delete(id);
            return RedirectToAction(nameof(Index));
        }

		private bool UserDetailExists(int id)
		{
           return objBO.UserExits(id);
		}
	}
}
