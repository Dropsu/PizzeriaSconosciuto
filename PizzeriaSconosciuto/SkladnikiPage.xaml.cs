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
    /// Logika interakcji dla klasy SkladnikiPage.xaml
    /// </summary>
    public partial class SkladnikiPage : Page
    {
        LinqToSQLDataContext db;
        SqlConnection conn;
        GenericsHelper<Skladniki> genericsHelper;



        public SkladnikiPage()
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

            genericsHelper = new GenericsHelper<Skladniki>();

        }

        public void dodajProdukt(LinqToSQLDataContext db, int EAN, string nazwa, int ilosc, string opis, bool wege, bool gluten)
        {

            try
            {
                Skladniki skladniki = new Skladniki();

                skladniki.EAN = EAN;
                skladniki.Nazwa = nazwa;
                skladniki.Ilosc = ilosc;
                skladniki.Opis = opis;
                skladniki.Wege = wege;
                skladniki.Gluten = gluten;

                db.Skladniki.InsertOnSubmit(skladniki);
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
                dodajProdukt(db, Int32.Parse(EAN_box.Text), nazwa_box.Text, Int32.Parse(ilosc_box.Text),opis_box.Text,wege_check.IsChecked.Value,gluten_check.IsChecked.Value);
                genericsHelper.fillTablie(db.Skladniki, db, conn, dataGrid);
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
                usunProdukt(Int32.Parse(EAN_box.Text));
                genericsHelper.fillTablie(db.Skladniki, db, conn, dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }

        }

        private void usunProdukt(int EAN)
        {
            var delete =
            from Skladniki in db.Skladniki
            where Skladniki.EAN == EAN
            select Skladniki;
            foreach (var skladnik in delete)
            {
                db.Skladniki.DeleteOnSubmit(skladnik);
            }
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            genericsHelper.fillTablie(db.Skladniki, db, conn, dataGrid);
        }

        private void zmien_Click(object sender, RoutedEventArgs e)
        {
            if (EAN_box.Text == "" | nazwa_box.Text == "" || ilosc_box.Text == "" || opis_box.Text == "")
            {
                MessageBox.Show("Wprowadz EAN skladnika do zmiany i uzupelnij wszystkie pola", "Error",
                MessageBoxButton.OK);
            }
            else
            {
                Skladniki result = (from s in db.Skladniki
                                   where s.EAN == Int32.Parse(EAN_box.Text)
                                   select s).SingleOrDefault();
                result.Ilosc = Int32.Parse(ilosc_box.Text);
                result.Nazwa = nazwa_box.Text;
                result.Opis = opis_box.Text;
                result.Wege = wege_check.IsChecked.Value;
                result.Gluten = gluten_check.IsChecked.Value;
                db.SubmitChanges();
            }
        }
    }
}
