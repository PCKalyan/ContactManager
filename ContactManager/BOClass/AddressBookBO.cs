using ContactManager.Data;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.BOClass
{
	public class AddressBookBO
	{
		private readonly DataContext context;
		
		public AddressBookBO(DataContext _context)
		{
			context = _context;
		}
		public IEnumerable<AddressBook> GetAll()
		{
			var dataContext = context.AddressBooks.Include(a => a.Fkstate).ThenInclude(a => a.Fkcountry).Include(a => a.Fkuser);
			return dataContext.ToList();
		}
		public AddressBook GetById(int id)
		{
			var address = context.AddressBooks
				.Include(a => a.Fkstate)
				.ThenInclude(a=>a.Fkcountry  )
				.Include(a => a.Fkuser)
				.FirstOrDefault(m => m.PkaddressId == id);
			return address;
		}
		public IEnumerable<AddressBook> Sort(int? countryid, string? statename, string? isactive)
		{
			if(isactive == "null")
			{
				var data = context.AddressBooks.Include(a => a.Fkstate).ThenInclude(a => a.Fkcountry).Include(a => a.Fkuser)
				.Where(a => a.Fkstate.Fkcountry.PkcountryId == countryid & a.Fkstate.StateName == statename);
				return data.ToList();
			}
			else
			{
				var data = context.AddressBooks.Include(a => a.Fkstate).ThenInclude(a => a.Fkcountry).Include(a => a.Fkuser)
		        .Where(a => a.Fkstate.Fkcountry.PkcountryId == countryid & a.Fkstate.StateName == statename & a.IsActive.ToString() == isactive);
				return data.ToList();
			}
			
			
		}	
		public AddressBook Add(AddressBook addressBook)
		{
			context.AddressBooks.Add(addressBook);
			context.SaveChanges();
			return addressBook;
		}
		public void Update(AddressBook addressBook)
		{
			context.Update(addressBook);
			context.SaveChanges();
		}
		public void Delete(int id)
		{
			var addressBook = context.AddressBooks.Find(id);
			context.AddressBooks.Remove(addressBook);
			context.SaveChanges();
		}
		public bool AddressBookExists(int id)
		{
			return context.AddressBooks.Any(e => e.PkaddressId == id);
		}
	}
}
