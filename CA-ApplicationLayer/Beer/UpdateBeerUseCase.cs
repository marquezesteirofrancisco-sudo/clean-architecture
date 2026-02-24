using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class UpdateBeerUseCase<TDTO>
    {

        private readonly IRepository<Beer> _repository;
        private readonly IMapper<TDTO, Beer> _mapper;

        public UpdateBeerUseCase(IRepository<Beer> repository, IMapper<TDTO, Beer> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task ExecuteAsync(TDTO dto)
        {
            // lo convierto con el mapper
            var beerToUpdate = _mapper.ToEntity(dto);

            // pimero veo si lo que quiere actualizar exite
            var beer = await _repository.GetByIdAsync(beerToUpdate.Id);

            // si no lo encuentra lanzo una exception
            if (beer == null)
                throw new KeyNotFoundException($"No se encontró ninguna cerveza : {beerToUpdate}");


            // si lo encuentra lo actualiza
            await _repository.UpdateAsync(beerToUpdate);

        }
    }
}
