using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CL_EnterpriseLayer;
using CA_ApplicationLayer.Exceptions;


namespace CA_ApplicationLayer
{
    public class AddUserUseCase<TDTO>
    {

        public readonly IRepository<User> _repository;
        public readonly IMapper<TDTO, User> _mapper;

        public AddUserUseCase(IRepository<User> repository, IMapper<TDTO, User> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task ExecuteAsync (TDTO dto)
        {

            var user = _mapper.ToEntity(dto);

            if (String.IsNullOrEmpty(user.Name))
                throw new ValidatorException("Es obligatorio establecer el nombre");


            await _repository.AddAsync(user);

        }
    }
}
