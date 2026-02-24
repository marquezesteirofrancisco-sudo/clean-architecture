using CA_ApplicationLayer;
using CA_interfaceAdapterData;
using CA_InterfaceAdapters_Models;
using CL_EnterpriseLayer;
using Microsoft.EntityFrameworkCore;

namespace CA_InterfaceAdapters_Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private readonly AppDbContext _DbContext;

        public BeerRepository(AppDbContext dbcontext)
        {
            _DbContext = dbcontext;
        }

        public async Task AddAsync(Beer beer)
        {
            var beerModel = new BeerModel
            {
                Id = beer.Id,
                Name = beer.Name,
                Style = beer.Style,
                Alcohol = beer.Alcohol
            };

            await _DbContext.Beers.AddAsync(beerModel);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Beer>> GetAllAsync()
        {
            return await _DbContext.Beers
                .Select(b => new Beer
                {
                    Id = b.Id,
                    Name = b.Name,
                    Style = b.Style,
                    Alcohol = b.Alcohol
                })
                .ToListAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            var beerModel = await _DbContext.Beers
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

            // Si no existe, devolvemos null inmediatamente
            if (beerModel == null) return null;

            return new Beer
            {
                Id = beerModel.Id,
                Name = beerModel.Name,
                Style = beerModel.Style,
                Alcohol = beerModel.Alcohol
            };

        }
    
        public async Task DeleteAsync(int id)
        {
            var beerModel = await _DbContext.Beers.FindAsync(id);

            _DbContext.Beers.Remove(beerModel);

            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Beer beer)
        {
            var beerModel = new BeerModel
            {
                Id = beer.Id,
                Name = beer.Name,
                Style = beer.Style,
                Alcohol = beer.Alcohol
            };

            _DbContext.Beers.Update(beerModel);

            await _DbContext.SaveChangesAsync();

        }
    }
}
