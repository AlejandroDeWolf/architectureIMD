using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyProject.API.Domain;
using MyProject.API.Ports;


namespace MyProject.API.Infra
{
    public class SqliteDatabase : IDatabase
    {
        private EventContext _context;

        public SqliteDatabase(EventContext context)
        {
            _context = context;
        }
        /* public async Task DeleteMovie(Guid parsedId)
         {
             var movie = await _context.Movies.FindAsync(parsedId);
             _context.Movies.Remove(movie);
             await _context.SaveChangesAsync();
         }*/

        /*   public async Task<ReadOnlyCollection<Movie>> GetAllMovies(string titleStartsWith)
         {

             var movies = await _context.Movies.Where(x => EF.Functions.Like(x.Title, $"{titleStartsWith}%")).ToArrayAsync();
             return Array.AsReadOnly(movies);
         }*/


        public async Task<ReadOnlyCollection<Event>> GetAllEvents()
        {
            var events = await _context.Event.ToArrayAsync();
            return Array.AsReadOnly(events);
        }

        public async Task<Event> GetEventByAge(int eventage)
        {
            return await _context.Event.FindAsync(eventage);
        }


        public async Task<Event> GetEventById(Guid id)
        {
            return await _context.Event.FindAsync(id);
        }

        public async Task<Event> PersistEvent(Event Event)
        {
            if (Event.Id == null)
            {
                await _context.Event.AddAsync(Event);
            }
            else
            {
                _context.Event.Update(Event);
            }
            await _context.SaveChangesAsync();
            return Event;
        }

        public async Task<User> PersistUser(User User)
        {
            if (User.Id == null)
            {
                await _context.User.AddAsync(User);
            }
            else
            {
                _context.User.Update(User);
            }
            await _context.SaveChangesAsync();
            return User;
        }
    }
}