using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.DTO.Request
{
    public class UserRequestDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Rol { get; set; }

        public string Departament { get; set; }
    }
}
