using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CL_EnterpriseLayer;


namespace CA_ApplicationLayer
{
    public class GenerateSaleUseCase<TDTO>
    {

        private readonly IRepository<Sale> _repository;
        private readonly IMapper<TDTO, Sale> _mapper;

        public GenerateSaleUseCase(IRepository<Sale> repository, IMapper<TDTO, Sale> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO saleDto)
        {
            // Map the DTO to the Sale entity
            var sale = _mapper.ToEntity(saleDto);

            // Validate the sale entity
            if (sale.Concepts.Count == 0)
                throw new ValidationException("A sale must have at least one concept.");

            if (sale.Total <= 0)
                throw new ValidationException("The total of the sale must be greater than zero.");

            // Additional business rules can be added here
            await _repository.AddAsync(sale);
        }
    }
}
