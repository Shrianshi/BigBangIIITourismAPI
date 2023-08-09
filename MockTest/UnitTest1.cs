using Moq;
using BigBangIII_Api.Controllers;
using BigBangIII_Api.Models;
using BigBangIII_Api.Repository;
using System.Numerics;

namespace MockTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var doc = new Bookings
            {
                Book_Id = 2,
                Tname = "Shivani",
                P_id = 2
            };
            var docRepo = new Mock<IBookingRepository>();
            docRepo.Setup(x => x.GetBookingsById(It.IsAny<int>())).Returns(doc);
            var docController = new BookingController(docRepo.Object);
            var getDoc = docController.Get(1);
            Assert.IsNotNull(getDoc);
        }
    }
}