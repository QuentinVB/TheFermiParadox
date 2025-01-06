using Newtonsoft.Json;
using System;

namespace theFermiParadox.Core.Interfaces
{
    [JsonObject(IsReference = true)]
    public interface IUnique
    {
        Guid Uuid { get; }
        string Name { get; }
    }
}
