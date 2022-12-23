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
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        MorceauServices morceauServices;
        public DispatcherTimer timer;
        public int MaxProgress => 60;
        private int currentProgress;
        MainViewModel vm = new MainViewModel();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CurrentProgress
        {
            get { return currentProgress; }
            private set
            {
                if (currentProgress != value)
                {
                    currentProgress = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
            RefreshGrid();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            if (Verification())
            {
                Morceau obj = GetData();
                obj.Id = CreateId();
                obj.Apprecie = false;
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
                    obj.Apprecie = getAprecie(select);
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

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            CheckBox c = (CheckBox)sender;

            Morceau selected = morceauServices.SelectResearch("id", c.Tag.ToString())[0];
            selected.Apprecie = c.IsChecked ?? false;
            morceauServices.UpdateJSON(selected);
        }

        private void BtnPlay_Click(object Sender, RoutedEventArgs e)
        {
            Button b = (Button)Sender;

            Morceau selected = morceauServices.SelectResearch("id", b.Tag.ToString())[0];

            vm.CurrentProgress = 0;
            vm.MaxProgress = int.Parse(selected.Duree);
            vm.timer.Start();
        }

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
                if (result < itemsEnum.Current.Id)
                    result = itemsEnum.Current.Id;
            }

            return result + 1;
        }

        private int getId(int select)
        {
            IEnumerable<Morceau> items = MorceauxListBox.Items.OfType<Morceau>();
            Morceau[] results = items.ToArray();
            return results[select].Id;
        }

        private bool getAprecie(int select)
        {
            IEnumerable<Morceau> items = MorceauxListBox.Items.OfType<Morceau>();
            Morceau[] results = items.ToArray();
            return results[select].Apprecie;
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
