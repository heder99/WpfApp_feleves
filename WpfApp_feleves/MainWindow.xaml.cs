using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp_feleves.Models;
using WpfApp_feleves.Views;

namespace WpfApp_feleves;
public partial class MainWindow : Window
{
    public ObservableCollection<Game> OsszesJatek { get; set; }
    public ObservableCollection<string> JatekTipusok { get; set; }
    public ObservableCollection<string> JatekosSzamok { get; set; }

    private ICollectionView raktaronNezet;
    private ICollectionView kolcsonzottNezet;

    public MainWindow()
    {
        InitializeComponent();
        InitializeData();
        this.DataContext = this;
    }

    private void InitializeData()
    {
        OsszesJatek = new ObservableCollection<Game>();
        JatekTipusok = new ObservableCollection<string>
    {
        "Stratégiai",
        "Partijáték",
        "Kooperatív",
        "Logikai",
        "Családi"
    };
        JatekosSzamok = new ObservableCollection<string>
    {
        "1 fő",
        "2 fő",
        "2-4 fő",
        "3-5 fő",
        "4+ fő"
    };

        var raktaronCVS = FindResource("RaktaronNezet") as CollectionViewSource;
        var kolcsonzottCVS = FindResource("KolcsonzottNezet") as CollectionViewSource;

        raktaronCVS.Source = OsszesJatek;
        kolcsonzottCVS.Source = OsszesJatek;

        raktaronNezet = raktaronCVS.View;
        kolcsonzottNezet = kolcsonzottCVS.View;

        // Adatok inicializálása
        OsszesJatek.Add(new Game
        {
            Nev = "Ticket to Ride",
            Tipus = "Családi",
            JatekosSzam = "2-5",
            Status = GameStatus.Raktáron
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Azul",
            Tipus = "Logikai",
            JatekosSzam = "2-4",
            Status = GameStatus.Kölcsönadva,
            Kolcsonvevo = "Kovács Anna"
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Meow",
            Tipus = "Partijáték",
            JatekosSzam = "3-6",
            Status = GameStatus.Raktáron
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Avalon",
            Tipus = "Partijáték",
            JatekosSzam = "5-10",
            Status = GameStatus.Kölcsönadva,
            Kolcsonvevo = "Nagy Péter"
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Dixit",
            Tipus = "Partijáték",
            JatekosSzam = "3-6",
            Status = GameStatus.Kölcsönadva,
            Kolcsonvevo = "Nagy Péter"
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Monopoly",
            Tipus = "Családi",
            JatekosSzam = "2-6",
            Status = GameStatus.Raktáron
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Bang!",
            Tipus = "Partijáték",
            JatekosSzam = "4-7",
            Status = GameStatus.Kölcsönadva,
            Kolcsonvevo = "Kovács Anna"
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Bohnanza",
            Tipus = "Stratégiai",
            JatekosSzam = "3-5",
            Status = GameStatus.Raktáron
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Fedőnevek",
            Tipus = "Partijáték",
            JatekosSzam = "4-8",
            Status = GameStatus.Kölcsönadva,
            Kolcsonvevo = "Szabó Gábor"
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Fesztáv",
            Tipus = "Stratégiai",
            JatekosSzam = "1-5",
            Status = GameStatus.Raktáron
        });
        OsszesJatek.Add(new Game
        {
            Nev = "Lángművesek",
            Tipus = "Stratégiai",
            JatekosSzam = "2-5",
            Status = GameStatus.Raktáron
        });
    }

    private void Kolcsonzes_Click(object sender, RoutedEventArgs e)
    {
        if (raktaronGrid.SelectedItem is Game kivalasztottJatek)
        {
            InputWindow inputWindow = new InputWindow("Kölcsönvevő neve", "Kérem, adja meg a kölcsönvevő nevét:");

            if (inputWindow.ShowDialog() == true)
            {
                string kolcsonvevoNeve = inputWindow.InputText.Trim();

                if (string.IsNullOrWhiteSpace(kolcsonvevoNeve))
                {
                    MessageBox.Show("A név nem lehet üres!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                kivalasztottJatek.KolcsonzésiElőzmények.Add($"{DateTime.Now.ToShortDateString()}: Kölcsönözve: {kolcsonvevoNeve}");

                kivalasztottJatek.Status = GameStatus.Kölcsönadva;
                kivalasztottJatek.Kolcsonvevo = kolcsonvevoNeve;

                raktaronNezet.Refresh();
                kolcsonzottNezet.Refresh();
            }
        }
        else
        {
            MessageBox.Show("Kérlek, válassz ki egy játékot a 'Raktáron' listából!", "Kölcsönzés");
        }
    }

    private void Visszahoz_Click(object sender, RoutedEventArgs e)
    {
        if (kolcsonzottGrid.SelectedItem is Game kivalasztottJatek)
        {
            if (!string.IsNullOrWhiteSpace(kivalasztottJatek.Kolcsonvevo))
            {
                kivalasztottJatek.KolcsonzésiElőzmények.Add($"{DateTime.Now.ToShortDateString()}: Visszahozva: {kivalasztottJatek.Kolcsonvevo}");
            }

            kivalasztottJatek.Status = GameStatus.Raktáron;
            kivalasztottJatek.Kolcsonvevo = string.Empty;

            raktaronNezet.Refresh();
            kolcsonzottNezet.Refresh();
        }
        else
        {
            MessageBox.Show("Kérlek, válassz ki egy játékot a 'Kölcsönadott' listából!", "Visszahozás");
        }
    }

    private void Hozzaad_Click(object sender, RoutedEventArgs e)
    {
        Game ujJatek = new Game { Status = GameStatus.Raktáron };
        EditWindow editor = new EditWindow(ujJatek, JatekTipusok, JatekosSzamok);
        if (editor.ShowDialog() == true)
        {
            AddNewValuesToMasterLists(ujJatek);
            OsszesJatek.Add(ujJatek);
        }
    }

    private void Szerkeszt_Click(object sender, RoutedEventArgs e)
    {
        Game kivalasztottJatek = null;

        if (raktaronGrid.SelectedItem is Game raktaronJatek)
        {
            kivalasztottJatek = raktaronJatek;
        }
        else if (kolcsonzottGrid.SelectedItem is Game kolcsonzottJatek)
        {
            kivalasztottJatek = kolcsonzottJatek;
        }

        if (kivalasztottJatek != null)
        {
            Game jatekKlon = kivalasztottJatek.Clone();

            EditWindow editor = new EditWindow(jatekKlon, JatekTipusok, JatekosSzamok);

            if (editor.ShowDialog() == true)
            {
                AddNewValuesToMasterLists(jatekKlon);

                kivalasztottJatek.Nev = jatekKlon.Nev;
                kivalasztottJatek.Tipus = jatekKlon.Tipus;
                kivalasztottJatek.JatekosSzam = jatekKlon.JatekosSzam;
                kivalasztottJatek.Status = jatekKlon.Status;
                kivalasztottJatek.Kolcsonvevo = jatekKlon.Kolcsonvevo;
                kivalasztottJatek.KolcsonzésiElőzmények = jatekKlon.KolcsonzésiElőzmények;

                raktaronNezet.Refresh();
                kolcsonzottNezet.Refresh();
            }
        }
        else
        {
            MessageBox.Show("Nincs kijélt játék a szerkesztéshez!", "Hiba");
        }
    }

    private void AddNewValuesToMasterLists(Game game)
    {
        if (!string.IsNullOrWhiteSpace(game.Tipus) && !JatekTipusok.Contains(game.Tipus))
        {
            JatekTipusok.Add(game.Tipus);
        }

        if (!string.IsNullOrWhiteSpace(game.JatekosSzam) && !JatekosSzamok.Contains(game.JatekosSzam))
        {
            JatekosSzamok.Add(game.JatekosSzam);
        }
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        Game kivalasztottJatek = null;
        if (raktaronGrid.SelectedItem is Game r) kivalasztottJatek = r;
        else if (kolcsonzottGrid.SelectedItem is Game k) kivalasztottJatek = k;

        if (kivalasztottJatek != null)
        {
            if (MessageBox.Show($"Biztosan törölni szeretnéd a(z) '{kivalasztottJatek.Nev}' játékot?",
                                 "Törlés megerősítése",
                                 MessageBoxButton.YesNo,
                                 MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                OsszesJatek.Remove(kivalasztottJatek);
            }
        }
        else
        {
            MessageBox.Show("Nincs kijelölt játék a törléshez!", "Hiba");
        }
    }

    private void txtKereses_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (raktaronNezet != null)
        {
            raktaronNezet.Refresh();
        }
        if (kolcsonzottNezet != null)
        {
            kolcsonzottNezet.Refresh();
        }
    }

    private void Raktaron_Filter(object sender, FilterEventArgs e)
    {
        if (e.Item is Game game)
        {
            bool statusMegfelel = game.Status == GameStatus.Raktáron;

            string keresettSzoveg = txtKereses?.Text.ToLower() ?? "";

            bool szovegMegfelel = string.IsNullOrWhiteSpace(keresettSzoveg) ||
                                  game.Nev.ToLower().Contains(keresettSzoveg) ||
                                  game.Tipus.ToLower().Contains(keresettSzoveg) ||
                                  game.JatekosSzam.ToLower().Contains(keresettSzoveg);

            e.Accepted = statusMegfelel && szovegMegfelel;
        }
    }

    private void Kolcsonzott_Filter(object sender, FilterEventArgs e)
    {
        if (e.Item is Game game)
        {
            bool statusMegfelel = game.Status == GameStatus.Kölcsönadva;

            string keresettSzoveg = txtKereses?.Text.ToLower() ?? "";

            bool szovegMegfelel = string.IsNullOrWhiteSpace(keresettSzoveg) ||
                                  game.Nev.ToLower().Contains(keresettSzoveg) ||
                                  game.Tipus.ToLower().Contains(keresettSzoveg) ||
                                  game.JatekosSzam.ToLower().Contains(keresettSzoveg);

            e.Accepted = statusMegfelel && szovegMegfelel;
        }
    }

    private void Frissit_Statisztika_Click(object sender, RoutedEventArgs e)
    {
        var tipusStatisztika = OsszesJatek
            .GroupBy(g => g.Tipus)
            .Select(g => new { Tipus = g.Key, Darabszam = g.Count() })
            .OrderByDescending(s => s.Darabszam)
            .ToList();

        var raktaronDarabszam = OsszesJatek
            .Where(g => g.Status == GameStatus.Raktáron)
            .GroupBy(g => g.Nev)
            .Select(g => new { Nev = g.Key, RaktaronDarab = g.Count() })
            .OrderByDescending(s => s.RaktaronDarab)
            .ToList();

        StringBuilder sb = new StringBuilder();

        // Összes játék
        sb.AppendLine($"Összes nyilvántartott játékpéldány: {OsszesJatek.Count} db\n");
        sb.AppendLine($"Jelenleg raktáron: {OsszesJatek.Count(g => g.Status == GameStatus.Raktáron)} db");
        sb.AppendLine($"Jelenleg kölcsönadva: {OsszesJatek.Count(g => g.Status == GameStatus.Kölcsönadva)} db");

        sb.AppendLine("\n--- Játék Típusok Eloszlása ---\n");
        foreach (var item in tipusStatisztika)
        {
            sb.AppendLine($"- {item.Tipus}: {item.Darabszam} db");
        }

        sb.AppendLine("\n--- Raktáron Lévő Darabszámok ---\n");
        foreach (var item in raktaronDarabszam)
        {
            sb.AppendLine($"- {item.Nev}: {item.RaktaronDarab} db");
        }

        lblStatisztika.Text = sb.ToString();
    }
}