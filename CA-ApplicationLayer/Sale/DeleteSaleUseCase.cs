using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class DeleteSaleUseCase
    {

        private readonly IRepository<Sale> _repository;
    
        public DeleteSaleUseCase(IRepository<Sale> repository)
        {
            _repository = repository;
        }


        public async Task ExecuteAsync(int id)
        {
            var sale = await _repository.GetByIdAsync(id);

            if (sale == null)
                throw new KeyNotFoundException($"No se encontró ninguna venta con el ID: {id}");

            await _repository.DeleteAsync(id);    
        }

    }
}
