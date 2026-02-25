using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CA_InterfaceAdapters_Mappers
{
    public class SaleMapper : IMapper<SaleRequestDTO, Sale>
    {
        public Sale ToEntity(SaleRequestDTO dto)
        {
            // Convert the list of ConceptRequestDTO to a list of Concept
            var concepts = new List<Concept>();

            // Iterate through each ConceptRequestDTO in the DTO and create a corresponding Concept entity
            foreach (var conceptDTO in dto.Concepts)
            {
                concepts.Add(new Concept(conceptDTO.Id, conceptDTO.Quantity, conceptDTO.idBeer, conceptDTO.UnitPrice));
            }

            // Create a new Sale entity using the current date and the list of Concept entities
            return new Sale(dto.Id, DateTime.Now , concepts);
        }

    }
}
