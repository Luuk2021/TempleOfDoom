namespace TempleOfDoom.GameLogic.Models.Interfaces
{
    public interface IInventory : IObservable<(bool isAdded, IHoldable)>
    {
        void AddItem(IHoldable item);
        void RemoveItem(IHoldable item);
        IEnumerable<IHoldable> GetItems();
    }
}
