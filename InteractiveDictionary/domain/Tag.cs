using System.Collections.Generic;

namespace InteractiveDictionary.domain
{
    public record Tag(string Name)
    {
        public string Name { get; } = Name;

    }
}