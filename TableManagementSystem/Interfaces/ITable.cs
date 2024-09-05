namespace TableManagement_Repository.Interfaces
{
    public interface ITable
    {
        int Id { get; set; }
        bool IsAvailable { get; set; }
        string UserName { get; set; }
        void AllotTable(string userName);
        void DeallocateTable();
    }
}
