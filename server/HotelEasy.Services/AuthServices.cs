using HotelEasy.Data;

namespace HotelEasy.Services
{
    public class AuthServices
    {
        private readonly Diplomna21180105Context _context;

        public AuthServices(Diplomna21180105Context context)
        {
            _context = context;
        }
    }
}