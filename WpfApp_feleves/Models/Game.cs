using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_feleves.Models
{
    public class Game : INotifyPropertyChanged
    {
        private string _nev;
        public string Nev
        {
            get => _nev;
            set { _nev = value; OnPropertyChanged(nameof(Nev)); }
        }

        private string _tipus;
        public string Tipus
        {
            get => _tipus;
            set { _tipus = value; OnPropertyChanged(nameof(Tipus)); }
        }

        private string _jatekosSzam;
        public string JatekosSzam
        {
            get => _jatekosSzam;
            set { _jatekosSzam = value; OnPropertyChanged(nameof(JatekosSzam)); }
        }

        private GameStatus _status;
        public GameStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _kolcsonvevo;
        public string Kolcsonvevo
        {
            get => _kolcsonvevo;
            set
            {
                _kolcsonvevo = value;
                OnPropertyChanged(nameof(Kolcsonvevo));
            }
        }

        // Kölcsönzési előzmények
        private List<string> _kolcsonzesiElőzmények = new List<string>();
        public List<string> KolcsonzésiElőzmények
        {
            get => _kolcsonzesiElőzmények;
            set { _kolcsonzesiElőzmények = value; OnPropertyChanged(nameof(KolcsonzésiElőzmények)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return Nev;
        }

        public Game Clone()
        {
            return new Game
            {
                Nev = this.Nev,
                Tipus = this.Tipus,
                JatekosSzam = this.JatekosSzam,
                Status = this.Status,
                Kolcsonvevo = this.Kolcsonvevo,
                KolcsonzésiElőzmények = new List<string>(this.KolcsonzésiElőzmények)
            };
        }
    }
}