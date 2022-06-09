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

	public class StatesController : Controller
	{
		StateBO objSBO;
		CountryBO objCBO;
		public StatesController(DataContext context)
		{
			objSBO = new StateBO(context);
			objCBO = new CountryBO(context);
		}
		public async Task<IActionResult> Index(string? countryname, string? search, string? reset)
		{
			ViewData["FkcountryId"] = new SelectList(objCBO.GetAll(), "CountryName", "CountryName");
			if (search == "search" && reset == null)
			{
				return View(objSBO.sort(countryname));
			}
			else if (reset == "reset")
			{
				return View(objSBO.GetAll());
			}
			return View(objSBO.GetAll());
		}
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var state = objSBO.GetById(id.Value);
			if (state == null)
			{
				return NotFound();
			}

			return View(state);
		}
		public IActionResult Create()
		{
			ViewData["FkcountryId"] = new SelectList(objCBO.GetAll(), "PkcountryId", "CountryName");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PkstateId,FkcountryId,StateName,IsActive")] State state)
		{
			if (ModelState.IsValid)
			{
				objSBO.Add(state);
				return RedirectToAction(nameof(Index));
			}
			ViewData["FkcountryId"] = new SelectList(objCBO.GetAll(), "PkcountryId", "CountryName", state.FkcountryId);
			return View(state);
		}
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var state = objSBO.GetById(id.Value);
			if (state == null)
			{
				return NotFound();
			}
			ViewData["FkcountryId"] = new SelectList(objCBO.GetAll(), "PkcountryId", "CountryName", state.FkcountryId);
			return View(state);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PkstateId,FkcountryId,StateName,IsActive")] State state)
		{
			if (id != state.PkstateId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					objSBO.Update(state);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StateExists(state.PkstateId))
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
			ViewData["FkcountryId"] = new SelectList(objCBO.GetAll(), "PkcountryId", "CountryName", state.FkcountryId);
			return View(state);
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var state = objSBO.GetById(id.Value);
			if (state == null)
			{
				return NotFound();
			}

			return View(state);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			objSBO.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		private bool StateExists(int id)
		{
			return objSBO.StateExists(id);
		}
	}
}
