using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException() : base("Error de Validacion.") { }

        public ValidatorException(string error) : base(error) { }

    }
}
