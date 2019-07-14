using Helpers;

namespace theFermiParadox.Core.Abstracts
{
    public interface IOrbit
    {
        Vector3 Position { get; }
        ABody MainBody { get ; }
        ABody Body { get ; }
    }
}