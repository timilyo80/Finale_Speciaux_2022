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

        private void RefreshGrid()
        {
            morceauServices = new MorceauServices();
            MorceauxListBox.ItemsSource = null;
            MorceauxListBox.ItemsSource = morceauServices.MorceauxList;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
