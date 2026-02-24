using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class DeleteBeerUseCase
    {
        private readonly IRepository<Beer> _repository;

        public DeleteBeerUseCase(IRepository<Beer> repository)
        {
            _repository = repository;
        }


        public async Task ExecuteAsync(int id)
        {

            var beer = await _repository.GetByIdAsync(id);

            if (beer == null)
                throw new KeyNotFoundException($"No se encontró ninguna cerveza con el ID: {id}");


            await _repository.DeleteAsync(beer.Id);
            
        }
    }
}
