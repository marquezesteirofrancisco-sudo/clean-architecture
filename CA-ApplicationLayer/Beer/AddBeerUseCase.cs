using CA_ApplicationLayer.Exceptions;
using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class AddBeerUseCase<TDTO>
    {

        private readonly IRepository<Beer> _beerRepository;
        private readonly IMapper<TDTO, Beer> _mapper;

        public AddBeerUseCase(IRepository<Beer> beerRepository, IMapper<TDTO, Beer> mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO dto)
        {
            var beer = _mapper.ToEntity(dto);

            if (string.IsNullOrEmpty(beer.Name))
                throw new ValidatorException("El nombre de la cerveza es obligatorio");

            await _beerRepository.AddAsync(beer);

        }
    }
}
    