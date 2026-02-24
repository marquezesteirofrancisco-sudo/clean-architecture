
using CA_InterfaceAdapters_Mappers.DTO.Request;
using FluentValidation;

namespace CA_FrameworksDrivers_API.Validator
{
    public class BeerValidator:AbstractValidator<BeerRequestDTO>
    {

        public BeerValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("La cerveza debe tener nombre");

            RuleFor(dto => dto.Style).NotEmpty().WithMessage("La cerveza debe tener Estilo");

            RuleFor(dto => dto.Alcohol).GreaterThan(0).WithMessage("La cerveza debe tener Alcohol mayor de 0.");

        }

    }
}
