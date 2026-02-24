using CA_ApplicationLayer;
using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{
    public class BeerDetailPresenter : IPresenter<Beer, BeerDetailViewModel>
    {
        public IEnumerable<BeerDetailViewModel> Present(IEnumerable<Beer> beers)
        {
            return beers.Select(b => new BeerDetailViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Style = b.Style,
                Alcohol = b.Alcohol + " %",
                Color = b.isStrongBeer() ? "ROJO" : "VERDE",
                Message = b.isStrongBeer() ? "Cerveza Fuerte" : "",
            });
        }
    }
}
