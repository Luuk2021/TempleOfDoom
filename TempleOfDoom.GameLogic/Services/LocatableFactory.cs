using System.Reflection;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.GameLogic.Services
{
    public class LocatableFactory
    {
        private Dictionary<string, Type> _supportedFormats;
        public LocatableFactory()
        {
            var type = typeof(ILocatable);
            _supportedFormats = [];
            var implementingTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface);
            foreach (var implementingType in implementingTypes)
            {
                _supportedFormats.Add(implementingType.Name.ToLower(), implementingType);
            }
        }
        public ILocatable CreateLocatable(string format, object[]? args)
        {
            var type = _supportedFormats[format];
            var o = Activator.CreateInstance(type, args);
            return (ILocatable)o;
        }
    }
}
