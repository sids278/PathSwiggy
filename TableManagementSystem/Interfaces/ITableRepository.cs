using TableManagementSystem.Interfaces;

namespace TableManagement_Repository.Interfaces
{
    public interface ITableRepository
    {
        List<ITable> AvailableTables { get; set; }
        List<ITable> BookedTables { get; set; }
        int Reserve(string userName);
        void Cancel(string userName);
        void List();
        void Add(int tableNumber);
        void Remove(int tableNumber);
    }
}
