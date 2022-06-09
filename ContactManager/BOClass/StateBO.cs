using ContactManager.Data;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.BOClass
{
	public class StateBO
	{
		private readonly DataContext context;

		public StateBO(DataContext _context)
		{
			context = _context;
		}
		public IEnumerable<State> GetAll()
		{
			var dataContext = context.States.Include(s => s.Fkcountry);
			return dataContext.ToList();
		}
		public State GetById(int id)
		{
			var state = context.States
				.Include(s => s.Fkcountry)
				.FirstOrDefault(m => m.PkstateId == id);
			return state;
		}
		public State Add(State state)
		{
			context.States.Add(state);
			context.SaveChanges();
			return state;
		}
		public void Update(State state)
		{
			context.Entry<State>(state).State=EntityState.Modified;
			context.SaveChanges();
		}
		public void Delete(int id)
		{
			var state = context.States.Find(id);
			context.States.Remove(state);
			context.SaveChanges();
		}
		public bool StateExists(int id)
		{
			return context.States.Any(e => e.PkstateId == id);
		}
		public IEnumerable<State> GetAllonlyselectedstates(string sk)
		{
			return context.States.Where(a => a.FkcountryId.ToString() == sk);
		}
		public IEnumerable<State> sort(string? countryname)
		{
			var states = context.States.Include(s => s.Fkcountry).Where(a => a.Fkcountry.CountryName.Contains(countryname));
			return states.ToList();
		}
	}
}
