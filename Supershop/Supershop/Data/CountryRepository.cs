using Supershop.Data.Entities;

namespace Supershop.Data
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context) : base (context)
        {
                
        }
    }
}
