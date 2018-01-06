using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PizzeriaSconosciuto
{
    class GenericsHelper <T> where T:class
    {
        public void fillTablie (Table<T> table, LinqToSQLDataContext db, SqlConnection conn, DataGrid dataGrid)
        {
            db = new LinqToSQLDataContext(conn);
            var q =
               from p in table
               select p;

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = q.ToList();
        }
    }
}
