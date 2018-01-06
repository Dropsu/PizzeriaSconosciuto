using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzeriaSconosciuto
{
    /// <summary>
    /// Logika interakcji dla klasy ProduktyPage.xaml
    /// </summary>
    public partial class ProduktyPage : Page
    {
        LinqToSQLDataContext db;
        SqlConnection conn;
        GenericsHelper<Produkty> genericsHelper;



        public ProduktyPage()
        {
            InitializeDatabazeConnection();

            InitializeComponent();

            Loaded += Window_Loaded;

        }

        private void InitializeDatabazeConnection()
        {
            conn = new SqlConnection(
                        "Data Source = .\\SQLEXPRESS; Initial Catalog=Pizzeria;User ID=Pizzus;Password=pizza");
            db = new LinqToSQLDataContext(conn);

            genericsHelper = new GenericsHelper<Produkty>();

        }

        public void dodajProdukt(LinqToSQLDataContext db, string nazwa, int Ilosc, double cena)
        {

            try
            {
                db.InsertProdukt(nazwa, Ilosc, cena);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dodajProdukt(db, nazwa_box.Text, 999, Convert.ToDouble(cena_box.Text));
                genericsHelper.fillTablie(db.Produkties, db, conn, dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                usunProdukt(Int32.Parse(produktid_box.Text));
                genericsHelper.fillTablie(db.Produkties, db, conn, dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }

        }

        private void usunProdukt(int id)
        {
            try
            {
                db.DeleteProduct(id);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            genericsHelper.fillTablie(db.Produkties, db, conn, dataGrid);
        }

        private void zmien_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                db.AlterProduct(Int32.Parse(produktid_box.Text), nazwa_box.Text, Convert.ToDouble(cena_box.Text), 999);
                db.SubmitChanges();
                genericsHelper.fillTablie(db.Produkties, db, conn, dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }

        }
    }
}
