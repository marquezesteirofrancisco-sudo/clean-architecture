namespace CL_EnterpriseLayer
{
    public class Beer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Style { get; set; }

        public decimal Alcohol { get; set; }

        public bool isStrongBeer() => Alcohol > 7.5m;

        public string GetBeerInfo() => $"{Name} is a {Style} beer with {Alcohol}% alcohol.";
    }
}
