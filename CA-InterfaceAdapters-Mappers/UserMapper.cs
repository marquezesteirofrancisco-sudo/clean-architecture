using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CL_EnterpriseLayer;

namespace CA_InterfaceAdapters_Mappers
{
    public class UserMapper : IMapper<UserRequestDTO, User>
    {
        public User ToEntity(UserRequestDTO dto) 
            => new User()
            { 
                Id = dto.Id,
                Name = dto.Name,
                Departament = dto.Departament,
                Rol = dto.Rol
            };
         
    }
}
