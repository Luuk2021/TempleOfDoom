using System.Collections.ObjectModel;
using TempleOfDoom.GameLogic.Services;

namespace TempleOfDoom.GameLogic.Models
{
    public class Room : Observable<List<ILocatable>>
    {
        public int Id { get; set; }
        public IList<ILocatable> Locatables { get ; private set; }

        public Room(int id)
        {
            Id = id;
            Locatables = new ObservableCollection<ILocatable>();
            ((ObservableCollection<ILocatable>)Locatables).CollectionChanged += (sender, e) => Notify([.. Locatables]);
        }

        public bool CanMoveTo((int x, int y) position)
        {
            return !Locatables.Any(l => l.Position == position && l is not ICollidable);
        }

        public IEnumerable<ICollidable> CheckCollisions(ICollidable collidable)
        {
            return Locatables.OfType<ICollidable>().Where(l => l != collidable && l.Position == collidable.Position);
        }
    }
}
