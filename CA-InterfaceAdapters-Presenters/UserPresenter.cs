using CA_ApplicationLayer;
using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{
    public class UserPresenter : IPresenter<User, UserViewModel>
    {
        public IEnumerable<UserViewModel> Present(IEnumerable<User> beers)
        {
            return beers.Select(b => new UserViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Rol = b.Rol,
                Departament = b.Departament
            });
        }
    }
}
