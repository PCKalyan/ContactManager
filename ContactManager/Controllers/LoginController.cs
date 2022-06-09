using ContactManager.BOClass;
using ContactManager.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactManager.Controllers
{
	public class LoginController : Controller
	{
		DataContext context;
		UserDetailsBO objBO;

		public LoginController(DataContext _context)
		{
			context = _context;
			objBO = new UserDetailsBO(_context);

		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(Models.Login loginusers, string? returnUrl)
		{
			if (ModelState.IsValid)
			{
				string name = loginusers.UserName;
				string password = loginusers.Password;
				TempData["UserName"] = name;

				var details = objBO.login(name);
				if (details == null || details.Password != password)
				{
					ViewData["Error"] = "Access is declained, Plase enter the correct credentials";
				}
				else
				{
					List<Claim> claims;
					if ((details.UserName == "Chinni") && (details.Password == "Chinni@423"))
					{
						claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name,details.UserName),
						new Claim("Admin","true")
					};
					}
					else
					{
						claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name,details.UserName),
						new Claim("User","true")
					};
					}
					var identity = new ClaimsIdentity(claims, "MyCookieAuth");
					ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
					if (!string.IsNullOrEmpty(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}
			return View();
		}
		public IActionResult GetbyUser()
		{
			TempData.Keep();
			string name = TempData["UserName"].ToString();
			ViewData["Name"] = name;
			TempData["UserName"] = name;
			var listofitems = context.AddressBooks.Include(a => a.Fkstate).ThenInclude(a => a.Fkcountry).Include(a => a.Fkuser).
			Where(b => b.Fkuser.UserName == name);
			return View("Views/Shared/_AddressBook.cshtml", listofitems);
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("MyCookieAuth");
			return RedirectToAction("Index", "Home");
		}
	}
}
