using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.UI.Views.TileViews;

namespace TempleOfDoom.UI.Services
{
    public class TileViewFactory
    {
        private Dictionary<string, ConstructorInfo> _tileViewsTypes = [];
        public TileViewFactory()
        {
            var abstractClassType = typeof(TileView);
            var concreteTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => abstractClassType.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
            foreach (var type in concreteTypes)
            {
                var name = type.Name.Replace("View", "").ToLower();
                var constructor = type.GetConstructors().MaxBy(c => c.GetParameters().Length);
                _tileViewsTypes.Add(name, constructor);
            }
        }

        public TileView GetTileView(string tileViewName, object?[]? args)
        {
            if (_tileViewsTypes.ContainsKey(tileViewName))
            {
                var constructor = _tileViewsTypes[tileViewName];
                if (args != null && args.Length == constructor.GetParameters().Length)
                {
                    return (TileView)constructor.Invoke(args);
                }
                return (TileView)constructor.Invoke(null);
            }
            throw new ArgumentException("Invalid tile view name: " + tileViewName);
        }
    }
}
