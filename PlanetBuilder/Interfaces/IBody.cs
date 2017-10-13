namespace PlanetBuilder.Interfaces
{
    public interface IBody : IOrbitalObject
    {
        double Mass { get; set; }
    }
}