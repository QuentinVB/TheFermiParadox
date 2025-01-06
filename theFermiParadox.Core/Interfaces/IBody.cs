using Newtonsoft.Json;

namespace theFermiParadox.Core.Interfaces
{
    [JsonObject(IsReference = false)]
    public interface IBody : IUnique,INode
    {
    }
}
