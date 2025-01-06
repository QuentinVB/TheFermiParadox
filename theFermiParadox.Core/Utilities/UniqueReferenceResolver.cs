using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using theFermiParadox.Core.Interfaces;

namespace theFermiParadox.Core.Utilities
{

    public class UniqueReferenceResolver : IReferenceResolver
    {
        private readonly IDictionary<Guid, IUnique> _uniques = new Dictionary<Guid, IUnique>();

        public object ResolveReference(object context, string reference)
        {
            Guid id = new Guid(reference);

            IUnique p;
            _uniques.TryGetValue(id, out p);

            return p;
        }

        public string GetReference(object context, object value)
        {
            IUnique p = (IUnique)value;
            _uniques[p.Uuid] = p;

            return p.Uuid.ToString();
        }

        public bool IsReferenced(object context, object value)
        {
            IUnique p = (IUnique)value;

            return _uniques.ContainsKey(p.Uuid);
        }

        public void AddReference(object context, string reference, object value)
        {
            Guid id = new Guid(reference);

            _uniques[id] = (IUnique)value;
        }
    }
}
