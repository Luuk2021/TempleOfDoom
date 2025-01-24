using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.UI.Views.TileViews;

namespace TempleOfDoom.UI.Services
{
    public class TileViewFactory
    {
        private Dictionary<string, Type> _tileViewsTypes = [];
        public TileViewFactory()
        {
            var abstractClassType = typeof(TileView);
            var concreteTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => abstractClassType.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
            foreach (var type in concreteTypes)
            {
                string name = type.Name.Replace("View", "").ToLower();
                _tileViewsTypes.Add(name, type);
            }
        }

        public TileView GetTileView(string tileViewName)
        {
            if (_tileViewsTypes.ContainsKey(tileViewName))
            {
                return (TileView)Activator.CreateInstance(_tileViewsTypes[tileViewName]);
            }
            throw new ArgumentException("Invalid tile view name: " + tileViewName);
        }
    }
}
