using CA_ApplicationLayer.Exceptions;
using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class UpdateSaleUseCase<TDTO>
    {
        private readonly IRepository<Sale> _repository;
        private readonly IMapper<TDTO, Sale> _mapper;

        public UpdateSaleUseCase(IRepository<Sale> repository, IMapper<TDTO, Sale> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO saleDto)
        {
            // Map the DTO to the Sale entity
            var updatedSale = _mapper.ToEntity(saleDto);

            // Validate the updated sale entity
            if (updatedSale.Concepts.Count == 0)
                throw new ValidatorException("A sale must have at least one concept.");

            if (updatedSale.Total <= 0)
                throw new ValidatorException("The total of the sale must be greater than zero.");

            // Additional business rules can be added here
            // Update the sale in the repository
            await _repository.UpdateAsync(updatedSale);
        }
    }
}
