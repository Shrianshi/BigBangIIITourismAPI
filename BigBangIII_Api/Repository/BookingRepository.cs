using BigBangIII_Api.Models;
using System.Numerics;

namespace BigBangIII_Api.Repository
{
    public class BookingRepository:IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBookings(Bookings doc)
        {
            _context.bookings.Add(doc);
            _context.SaveChanges();
        }

        public void DeleteBookings(int id)
        {
            var Book_Id = _context.bookings.Find(id);
            _context.bookings.Remove(Book_Id);
            _context.SaveChanges();
        }

        public Bookings GetBookingsById(int id)
        {
            var doc = _context.bookings.Find(id);
            return doc;
        }
        public void UpdateBookings(int id, Bookings b)
        {
            var d = _context.bookings.Find(b);
            d.Tname = b.Tname;
            _context.bookings.Update(d);
            _context.SaveChanges();
        }

        public IEnumerable<Bookings> GetBookings()
        {
            return _context.bookings.ToList();
        }
    }
}
