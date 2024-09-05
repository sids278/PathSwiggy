using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableManagement_Repository.Interfaces;
using TableManagement_Repository.Model;
using TableManagementSystem.Interfaces;

namespace TableManagementSystem.Factory
{
    public class TableFactory : ITableFactory
    {
        public ITable CreateTable(int tableNumber)
        {
            return new Table(tableNumber);
        }

    }
}
