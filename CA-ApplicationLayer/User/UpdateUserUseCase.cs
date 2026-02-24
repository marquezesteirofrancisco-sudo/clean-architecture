using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class UpdateUserUseCase<TDTO>
    {
 
        private readonly IRepository<User> _repository;
        private readonly IMapper<TDTO, User> _mapper;

        public UpdateUserUseCase(IRepository<User> repository, IMapper<TDTO, User> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task ExecuteAsync(TDTO dto)
        {
            // lo convierto con el mapper
            var userToUpdate = _mapper.ToEntity(dto);

            // pimero veo si lo que quiere actualizar exite
            var user = await _repository.GetByIdAsync(userToUpdate.Id);

            // si no lo encuentra lanzo una exception
            if (user == null)
                throw new KeyNotFoundException($"No se encontró ninguna usuario : {userToUpdate}");


            // si lo encuentra lo actualiza
            await _repository.UpdateAsync(userToUpdate);

        }
       


    }
}
