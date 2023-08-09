using BigBangIII_Api.Models;

namespace BigBangIII_Api.Repository
{
    public interface IBookingRepository
    {
        IEnumerable<Bookings> GetBookings();
        void AddBookings(Bookings doc);
        Bookings GetBookingsById(int id);
        void DeleteBookings(int id);
        void UpdateBookings(int id, Bookings doc);
    }
}
