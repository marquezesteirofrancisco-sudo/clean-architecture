using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CL_EnterpriseLayer;

namespace CA_InterfaceAdapters_Mappers
{
    public class BeerMapper : IMapper<BeerRequestDTO, Beer>
    {
        public Beer ToEntity(BeerRequestDTO dto) 
            => new Beer()
            { 
                Id = dto.Id,
                Name = dto.Name,
                Style = dto.Style,
                Alcohol = dto.Alcohol
            };
         
    }
}
