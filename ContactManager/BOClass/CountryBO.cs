using ContactManager.Data;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.BOClass
{
	public class CountryBO
	{
		DataContext context;

		public CountryBO(DataContext _context)
		{
			context = _context;
		}
		public IEnumerable<Country> GetAll()
		{
			var countries = context.Countries;
			return countries.ToList();
		}
		public Country GetById(int id)
		{
			return context.Countries.Find(id);
		}
		public Country Add(Country entity)
		{
			context.Countries.Add(entity);
			context.SaveChanges();
			return entity;
		}
		public void Update(Country entity)
		{
			context.Entry<Country>(entity).State = EntityState.Modified;
			context.SaveChanges();
		}
		public void Delete(int id)
		{
			Country country = context.Countries.Find(id);
			context.Countries.Remove(country);
			context.SaveChanges();
		}
		public bool CountryExits(int id)
		{
			return context.Countries.Any(e => e.PkcountryId == id);
		}
	}
}
