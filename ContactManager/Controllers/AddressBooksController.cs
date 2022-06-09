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
	public class AddressBooksController : Controller
	{
		AddressBookBO objABO;
		StateBO objSBO;
		UserDetailsBO objUBO;
		CountryBO objCBO;
		public AddressBooksController(DataContext context)
		{
			objABO = new AddressBookBO(context);
			objSBO = new StateBO(context);
			objUBO = new UserDetailsBO(context);
			objCBO = new CountryBO(context);
		}
		public async Task<IActionResult> Index(int? countryid, string? statename, string? isactive, string? reset)
		{
			ViewData["FkcountryId"] = new SelectList(objCBO.GetAll(), "PkcountryId", "CountryName");
			ViewData["FkstateId"] = new SelectList(objSBO.GetAllonlyselectedstates(countryid.ToString()), "StateName", "StateName");
			if (countryid == null & statename == null & isactive == null)
			{
				return View(objABO.GetAll());
			}
			else if (reset == "reset")
			{
				return View(objABO.GetAll());
			}
			return View(objABO.Sort(countryid, statename, isactive));
		}
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var addressBook = objABO.GetById(id.Value);
			if (addressBook == null)
			{
				return NotFound();
			}
			return View(addressBook);
		}
		public IActionResult Create()
		{
			ViewData["FkstateId"] = new SelectList(objSBO.GetAll(), "PkstateId", "StateName");
			ViewData["FkuserId"] = new SelectList(objUBO.GetAll(), "PkuserId", "UserName");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PkaddressId,FkstateId,FkuserId,FirstName,LastName,EmailId,PhoneNo,Address1,Address2,Street,City,ZipCode,IsActive")] AddressBook addressBook)
		{
			if (ModelState.IsValid)
			{
				objABO.Add(addressBook);
				return RedirectToAction(nameof(Index));
			}
			ViewData["FkstateId"] = new SelectList(objSBO.GetAll(), "PkstateId", "StateName", addressBook.FkstateId);
			ViewData["FkuserId"] = new SelectList(objUBO.GetAll(), "PkuserId", "UserName", addressBook.FkuserId);
			return View(addressBook);
		}
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var addressBook = objABO.GetById(id.Value);
			if (addressBook == null)
			{
				return NotFound();
			}
			ViewData["FkstateId"] = new SelectList(objSBO.GetAll(), "PkstateId", "StateName", addressBook.FkstateId);
			ViewData["FkuserId"] = new SelectList(objUBO.GetAll(), "PkuserId", "UserName", addressBook.FkuserId);
			return View(addressBook);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PkaddressId,FkstateId,FkuserId,FirstName,LastName,EmailId,PhoneNo,Address1,Address2,Street,City,ZipCode,IsActive")] AddressBook addressBook)
		{
			if (id != addressBook.PkaddressId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					objABO.Update(addressBook);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AddressBookExists(addressBook.PkaddressId))
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
			ViewData["FkstateId"] = new SelectList(objSBO.GetAll(), "PkstateId", "StateName", addressBook.FkstateId);
			ViewData["FkuserId"] = new SelectList(objUBO.GetAll(), "PkuserId", "UserName", addressBook.FkuserId);
			return View(addressBook);
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var addressBook = objABO.GetById(id.Value);
			if (addressBook == null)
			{
				return NotFound();
			}
			return View(addressBook);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			objABO.Delete(id);
			return RedirectToAction(nameof(Index));
		}
		private bool AddressBookExists(int id)
		{
			return objABO.AddressBookExists(id);
		}
	}
}
