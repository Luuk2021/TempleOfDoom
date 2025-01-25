using System.Collections.ObjectModel;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic.Models
{
    public class Room : Observable<(bool isAdded,ILocatable)>
    {
        public int Id { get; set; }
        private List<ILocatable> _locatables;

        public Room(int id)
        {
            Id = id;
            _locatables = new List<ILocatable>();
        }

        public void AddLocatable(ILocatable locatable)
        {
            _locatables.Add(locatable);
            Notify((true, locatable));
        }

        public void RemoveLocatable(ILocatable locatable)
        {
            _locatables.Remove(locatable);
            Notify((false, locatable));
        }

        public IEnumerable<ILocatable> GetLocatables()
        {
            return new ReadOnlyCollection<ILocatable>(_locatables);
        }

        public bool CanMoveTo((int x, int y) position)
        {
            return !_locatables.Any(l => l.Position == position && l is not ICollidable);
        }

        public IEnumerable<ICollidable> CheckCollisions(ICollidable collidable)
        {
            return _locatables.OfType<ICollidable>().Where(l => l != collidable && l.Position == collidable.Position);
        }
    }
}
