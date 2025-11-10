using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using WpfApp_feleves.Models;

namespace WpfApp_feleves.Views
{
    public partial class EditWindow : Window
    {
        public Game SzerkesztettJatek { get; set; }
        public ObservableCollection<string> ElérhetőTípusok { get; set; }
        public ObservableCollection<string> ElérhetőJátékosszámok { get; set; }
        public EditWindow(Game game, ObservableCollection<string> tipusok, ObservableCollection<string> jatekosSzamok)
        {
            InitializeComponent();
            SzerkesztettJatek = game;

            ElérhetőTípusok = tipusok;
            ElérhetőJátékosszámok = jatekosSzamok;

            this.DataContext = this;
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

