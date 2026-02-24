using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class DeleteUserUseCase
    {

        private readonly IRepository<User> _repository;

        public DeleteUserUseCase(IRepository<User> repository)
        {
            _repository = repository;
        }


        public async Task ExecuteAsync(int id)
        {

            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException($"No se encontró ningun usuario con el ID: {id}");


            await _repository.DeleteAsync(user.Id);

        }


    }
}
