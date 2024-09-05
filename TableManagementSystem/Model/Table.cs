using TableManagement_Repository.Interfaces;

namespace TableManagement_Repository.Model
{
    public class Table : ITable
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public string UserName { get; set; }

        public Table(int id)
        {
            Id = id;
            IsAvailable = true;
            UserName = string.Empty;
        }

        public void AllotTable(string userName)
        {
            IsAvailable = false;
            UserName = userName;
        }

        public void DeallocateTable()
        {
            IsAvailable = true;
            UserName = string.Empty;
        }
    }
}
