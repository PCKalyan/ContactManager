using ContactManager.Data;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.BOClass
{
	public class UserDetailsBO
	{
		DataContext context;

		public UserDetailsBO(DataContext _context)
		{
			context = _context;
		}
		public IEnumerable<UserDetail> GetAll()
		{
			var details = context.UserDetails;
			return details.ToList();
		}
		public UserDetail GetById(int id)
		{
			return context.UserDetails.Find(id);
		}
		public UserDetail Add(UserDetail entity)
		{
			context.UserDetails.Add(entity);
			context.SaveChanges();
			return entity;
		}
		public void Update(UserDetail entity)
		{
			context.Entry<UserDetail>(entity).State = EntityState.Modified;
			context.SaveChanges();
		}
		public void Delete(int id)
		{
			UserDetail user = context.UserDetails.Find(id);
			context.UserDetails.Remove(user);
			context.SaveChanges();
		}
		public bool UserExits(int id)
		{
			return context.UserDetails.Any(e => e.PkuserId == id);
		}
		public UserDetail login(string name)
		{
			return context.UserDetails.Where(p => p.UserName == name).FirstOrDefault();
		}
	}
}

