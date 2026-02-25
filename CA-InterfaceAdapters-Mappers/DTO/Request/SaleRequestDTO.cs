using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.DTO.Request
{
    public class SaleRequestDTO
    {
        public int Id { get; set; }

        public List<ConceptRequestDTO> Concepts { get; set; }

    }

    public class ConceptRequestDTO
    {
        public int Id { get; set; }

        public int idBeer { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
