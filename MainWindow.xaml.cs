using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfApp1.ViewModel;
using WpfApp1.Models;
using System.Collections;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        MorceauServices morceauServices;

        public MainWindow()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            if (Verification())
            {
                Morceau obj = GetData();
                obj.Id = CreateId();
                morceauServices.InsertJSON(obj);
                RefreshGrid();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int select = getSelect();

            if (select >= 0 && select < MorceauxListBox.Items.Count)
            {
                if (Verification())
                {
                    Morceau obj = GetData();
                    obj.Id = getId(select);
                    morceauServices.UpdateJSON(obj);
                    RefreshGrid();
                }
            } else MessageBox.Show("Invalid Select");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int select = getSelect();

            if (select >= 0 && select < MorceauxListBox.Items.Count)
            {
                Morceau obj = new Morceau();
                obj.Id = getId(select);
                morceauServices.DeleteJSON(obj);
                RefreshGrid();
            } else MessageBox.Show("Invalid Select");
        }

        private void BtnSearchTitre_Click(object sender, RoutedEventArgs e) { RefreshGridResearch("titre", TbTitre.Text.Trim()); }
        private void BtnSearchArtiste_Click(object sender, RoutedEventArgs e) { RefreshGridResearch("artiste", TbArtiste.Text.Trim()); }
        private void BtnSearchDuree_Click(object sender, RoutedEventArgs e) { RefreshGridResearch("duree", TbDuree.Text.Trim()); }
        private void BtnSearchGenre_Click(object sender, RoutedEventArgs e) { RefreshGridResearch("genre", TbGenre.Text.Trim()); }
        private void BtnSearchAnnee_Click(object sender, RoutedEventArgs e) { RefreshGridResearch("annee", TbAnnee.Text.Trim()); }
        private void BtnReset_Click(object sender, RoutedEventArgs e) { RefreshGrid(); }

        private void RefreshGrid()
        {
            morceauServices = new MorceauServices();
            MorceauxListBox.ItemsSource = null;
            MorceauxListBox.ItemsSource = morceauServices.MorceauxList;
        }

        private void RefreshGridResearch(string type, string input)
        {
            morceauServices = new MorceauServices();
            MorceauxListBox.ItemsSource = null;
            MorceauxListBox.ItemsSource = morceauServices.SelectResearch(type, input);
        }

        private bool Verification()
        {
            if (TbTitre.Text.Length == 0)
            {
                MessageBox.Show("Titre Missing");
                return false;
            }
            if (TbArtiste.Text.Length == 0)
            {
                MessageBox.Show("Artiste Missing");
                return false;
            }
            if (TbDuree.Text.Length == 0)
            {
                MessageBox.Show("Durée Missing");
                return false;
            }
            if (TbGenre.Text.Length == 0)
            {
                MessageBox.Show("Genre Missing");
                return false;
            }
            if (TbAnnee.Text.Length == 0)
            {
                MessageBox.Show("Année Missing");
                return false;
            }

            return true;
        }

        private Morceau GetData()
        {
            var obj = new Morceau();
            obj.Titre = TbTitre.Text.Trim();
            obj.Artiste = TbArtiste.Text.Trim();
            obj.Duree = TbDuree.Text.Trim();
            obj.Genre = TbGenre.Text.Trim();
            obj.Annee = TbAnnee.Text.Trim();

            return (obj);
        }

        private int CreateId()
        {
            int result = 0;
            IEnumerable<Morceau> items = MorceauxListBox.Items.OfType<Morceau>();
            IEnumerator<Morceau> itemsEnum = items.GetEnumerator();

            while (itemsEnum.MoveNext())
            {
                result += itemsEnum.Current.Id;
            }

            if (result == 0) result = 1;
            if (result == 1) result = 2;

            return result;
        }

        private int getId(int select)
        {
            IEnumerable<Morceau> items = MorceauxListBox.Items.OfType<Morceau>();
            Morceau[] results = items.ToArray();
            return results[select].Id;
        }

        private int getSelect()
        {
            int select;
            try { select = int.Parse(TbSelect.Text.Trim()); }
            catch { select = -1; }
            return select;
        }
    }
}
