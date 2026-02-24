using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.DTO.Request;
using CA_InterfaceAdapters_Presenters;
using CL_EnterpriseLayer;
using System.Windows.Forms;

using System.Text.Json;

namespace Proyecto_Pruebas
{
    public partial class Form1 : Form
    {
        private readonly GetBeerUseCase<Beer, BeerViewModel> _getBeerUseCase;
        private readonly AddBeerUseCase<BeerRequestDTO> _addBeerUseCase;

        public Form1(GetBeerUseCase<Beer, BeerViewModel> getBeerUseCase,
                 AddBeerUseCase<BeerRequestDTO> addBeerUseCase)
        {
            InitializeComponent();

            _getBeerUseCase = getBeerUseCase;
            _addBeerUseCase = addBeerUseCase;
        }

        private async void btnListar_Click(object sender, EventArgs e)
        {
            // Uso del caso de uso
            var beers = await _getBeerUseCase.ExecuteAsync();


            // Serialización con formato "bonito" (identado)
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonFormat = JsonSerializer.Serialize(beers, options);

            MessageBox.Show(jsonFormat);

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var beer = new BeerRequestDTO();
                //beer.Name = "Nombre Cerveza";
                beer.Alcohol = 6;
                beer.Style = "Lager";

                await _addBeerUseCase.ExecuteAsync(beer);

                MessageBox.Show("Creada con exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
