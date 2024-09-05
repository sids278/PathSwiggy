using System;
using System.Collections.Generic;
using TableManagement_Repository.Interfaces;
using TableManagement_Repository.Model;
using TableManagementSystem.Factory;
using TableManagementSystem.Interfaces;

namespace TableManagementSystem.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly ITableFactory _tableFactory;
        public List<ITable> AvailableTables { get; set; }
        public List<ITable> BookedTables { get; set; }

        public TableRepository(ITableFactory tableFactory)
        {
            AvailableTables = new List<ITable>();
            BookedTables = new List<ITable>();
            _tableFactory = tableFactory;
        }

        public int Reserve(string userName)
        {
            if (AvailableTables.Count == 0)
            {
                Console.WriteLine("No Tables Available!");
                return -1;
            }
            else
            {
                ITable table = AvailableTables[^1];
                table.AllotTable(userName);
                AvailableTables.RemoveAt(AvailableTables.Count - 1);
                BookedTables.Add(table);
                Console.WriteLine($"User {userName} booked table {table.Id}");
                return table.Id;
            }
        }

        public void Cancel(string userName)
        {
            if (BookedTables.Count == 0)
            {
                Console.WriteLine("No Tables booked");
            }
            ITable? tableToCancel = BookedTables.Find(table => !table.IsAvailable && table.UserName == userName);
            if (tableToCancel != null)
            {
                BookedTables.Remove(tableToCancel);
                tableToCancel.DeallocateTable();
                AvailableTables.Add(tableToCancel);
                Console.WriteLine($"User {userName} cancelled table {tableToCancel.Id}");
            }
            else
            {
                Console.WriteLine("No Bookings found for user: " + userName);
            }
        }

        public void List()
        {
            Console.WriteLine("Table No.\t\tAvailability\t\tUserBooked");
            foreach (var table in AvailableTables)
                Console.WriteLine($"{table.Id}\t\t\t{table.IsAvailable}\t\t\t{table.UserName}");
            foreach (var table in BookedTables)
                Console.WriteLine($"{table.Id}\t\t\t{table.IsAvailable}\t\t\t{table.UserName}");
        }

        public void Add(int tableNumber)
        {
            ITable table = _tableFactory.CreateTable(tableNumber);
            AvailableTables.Add(table);
            Console.WriteLine("Table " + table.Id + " Added to Restaurant!");
        }

        public void Remove(int tableNumber)
        {
            ITable? targetTable = BookedTables.Find(table => table.Id == tableNumber);
            if (targetTable != null)
            {
                Console.WriteLine($"Cannot remove {targetTable.Id} as it is booked!");
                return;
            }
            targetTable = AvailableTables.Find(table => table.Id == tableNumber);
            if (targetTable != null)
            {
                AvailableTables.Remove(targetTable);
                Console.WriteLine($"Table {tableNumber} Removed!");
            }
            else
            {
                Console.WriteLine("No such table exists");
            }
        }
    }
}
