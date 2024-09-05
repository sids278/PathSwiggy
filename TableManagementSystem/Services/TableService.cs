using System;
using TableManagement_Repository.Interfaces;

namespace TableManagement_Repository.Services
{
    public class TableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public int ReserveTable(string userName)
        {
            return _tableRepository.Reserve(userName);
        }

        public void CancelTable(string userName)
        {
            _tableRepository.Cancel(userName);
        }

        public void ListTables()
        {
            _tableRepository.List();
        }

        public void AddTable(int tableNumber)
        {
            _tableRepository.Add(tableNumber);
        }

        public void RemoveTable(int tableNumber)
        {
            _tableRepository.Remove(tableNumber);
        }
    }
}
