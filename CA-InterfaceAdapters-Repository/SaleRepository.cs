using CA_ApplicationLayer;
using CA_interfaceAdapterData;
using CA_InterfaceAdapters_Models;
using CL_EnterpriseLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CA_InterfaceAdapters_Repository
{
    public class SaleRepository : IRepository<Sale>, IRepositorySearch<SaleModel, Sale>
    {

        private readonly AppDbContext _DbContext;

        public SaleRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task AddAsync(Sale dto)
        {
            var sale = new SaleModel
            {
                Total = dto.Total,
                CreationDate = dto.Date,
                Concepts = dto.Concepts.Select(c => new ConceptModel
                {
                    UnitPrice = c.UnitPrice,
                    IdBeer = c.IdBeer,
                    Quantity = c.Quantity
                }).ToList()

            };

            await _DbContext.AddAsync(sale);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var saleModel = await _DbContext.Sales.FindAsync(id);

            _DbContext.Sales.Remove(saleModel);

            await _DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _DbContext.Sales

                .Select(s => new Sale(s.Id, s.CreationDate,
                                _DbContext.Concepts
                                    .Where(c => c.IdSale == s.Id)
                                    .Select(c => new Concept(c.Id, c.Quantity, c.IdBeer, c.UnitPrice))
                                    .ToList()
                                )
                       ).ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            var saleModel = await _DbContext.Sales
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == id);

            // Si no existe, devolvemos null inmediatamente
            if (saleModel == null) return null;

            return new Sale(saleModel.Id, saleModel.CreationDate,
                                _DbContext.Concepts
                                    .Where(c => c.IdSale == saleModel.Id)
                                    .Select(c => new Concept(c.Id, c.Quantity, c.IdBeer, c.UnitPrice))
                                    .ToList()
                                );
        }

        public async Task UpdateAsync(Sale sale)
        {
            var updatedSale = new SaleModel
            {
                Id = sale.Id,
                CreationDate = sale.Date,
                Total= sale.Total,
                Concepts = sale.Concepts.Select(c => new ConceptModel
                {
                    Id = c.Id,
                    UnitPrice = c.UnitPrice,
                    IdBeer = c.IdBeer,
                    Quantity = c.Quantity,
                    IdSale = sale.Id
                }).ToList()

            };



            // 1. Buscamos la entidad actual en DB incluyendo el detalle
            var existingSale = await _DbContext.Sales
                .Include(s => s.Concepts)
                .FirstOrDefaultAsync(s => s.Id == updatedSale.Id);

            if (existingSale == null) return;

            // 2. Actualizamos valores básicos de la cabecera
            _DbContext.Entry(existingSale).CurrentValues.SetValues(updatedSale);

            // 3. Sincronizamos el detalle (Concepts)

            // ELIMINAR: Conceptos que están en DB pero no en el objeto actualizado
            foreach (var existingConcept in existingSale.Concepts.ToList())
            {
                if (!updatedSale.Concepts.Any(c => c.Id == existingConcept.Id))
                    _DbContext.Concepts.Remove(existingConcept);
            }

            // ACTUALIZAR O AÑADIR:
            foreach (var updatedConcept in updatedSale.Concepts)
            {
                var existingConcept = existingSale.Concepts
                    .FirstOrDefault(c => c.Id == updatedConcept.Id);

                if (existingConcept != null)
                {
                    // Si existe, actualizamos sus valores
                    _DbContext.Entry(existingConcept).CurrentValues.SetValues(updatedConcept);
                }
                else
                {
                    // Si es nuevo (Id vacío o no encontrado), lo añadimos a la colección
                    existingSale.Concepts.Add(updatedConcept);
                }
            }

            await _DbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Sale>> GetAsync(Expression<Func<SaleModel, bool>> predicate)
        {
            
            var saleModels = await _DbContext.Sales.Include("Concepts")
                                        .Where(predicate)
                                        .AsNoTracking()
                                        .ToListAsync();

            var sales = new List<Sale>();

            foreach (var saleModel in saleModels)
            {
                var sale = new Sale(saleModel.Id, saleModel.CreationDate,
                                _DbContext.Concepts
                                    .Where(c => c.IdSale == saleModel.Id)
                                    .Select(c => new Concept(c.Id, c.Quantity, c.IdBeer, c.UnitPrice))
                                    .ToList()
                                );
                sales.Add(sale);
            }

            return  sales;
        }
    }
}
