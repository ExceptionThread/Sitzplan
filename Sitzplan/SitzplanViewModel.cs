using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WPF_MVWMSitzplan;
using WPF_TeilnehmerModel;

namespace Sitzplan
{
    public class SitzplanViewModel : INotifyPropertyChanged
    {
        private Teilnehmer _selectedFreeParticipant;
        private Teilnehmer _selectedSeatedParticipant;
        private int? _selectedFreeSeat;

        public SitzplanViewModel()
        {
            FreeParticipants = new ObservableCollection<Teilnehmer>
            {
                new Teilnehmer { Name = "Berg, Bruno" },
                new Teilnehmer { Name = "Berg, Xaver" },
                new Teilnehmer { Name = "Dachs, Devin" },
                new Teilnehmer { Name = "Dachs, Thomas" },
                new Teilnehmer { Name = "Müller, Max" },
                new Teilnehmer { Name = "Wächter, Waltraud" }
            };

            FreeSeats = new ObservableCollection<int>(Enumerable.Range(1, 20));

            SeatedParticipants = new ObservableCollection<Teilnehmer>
            {
                new Teilnehmer { Name = "Krause, Karl", Platznummer = 3 },
                new Teilnehmer { Name = "Schmidt, Susanne", Platznummer = 15 }
            };

            SetCommand = new RelayCommand(SetParticipant, CanSetParticipant);
            ReleaseCommand = new RelayCommand(ReleaseParticipant, CanReleaseParticipant);
        }

        public ObservableCollection<Teilnehmer> FreeParticipants { get; }
        public ObservableCollection<int> FreeSeats { get; }
        public ObservableCollection<Teilnehmer> SeatedParticipants { get; }

        public Teilnehmer SelectedFreeParticipant
        {
            get => _selectedFreeParticipant;
            set
            {
                if (_selectedFreeParticipant != value)
                {
                    _selectedFreeParticipant = value;
                    OnPropertyChanged(nameof(SelectedFreeParticipant));
                }
            }
        }

        public Teilnehmer SelectedSeatedParticipant
        {
            get => _selectedSeatedParticipant;
            set
            {
                if (_selectedSeatedParticipant != value)
                {
                    _selectedSeatedParticipant = value;
                    OnPropertyChanged(nameof(SelectedSeatedParticipant));
                }
            }
        }

        public int? SelectedFreeSeat
        {
            get => _selectedFreeSeat;
            set
            {
                if (_selectedFreeSeat != value)
                {
                    _selectedFreeSeat = value;
                    OnPropertyChanged(nameof(SelectedFreeSeat));
                }
            }
        }

        public ICommand SetCommand { get; }
        public ICommand ReleaseCommand { get; }

        private bool CanSetParticipant(object parameter)
        {
            return SelectedFreeParticipant != null && SelectedFreeSeat.HasValue;
        }

        private void SetParticipant(object parameter)
        {
            SelectedFreeParticipant.Platznummer = SelectedFreeSeat;
            SeatedParticipants.Add(SelectedFreeParticipant);
            FreeParticipants.Remove(SelectedFreeParticipant);
            FreeSeats.Remove(SelectedFreeSeat.Value);
        }

        private bool CanReleaseParticipant(object parameter)
        {
            return SelectedSeatedParticipant != null;
        }

        private void ReleaseParticipant(object parameter)
        {
            FreeParticipants.Add(SelectedSeatedParticipant);
            FreeSeats.Add(SelectedSeatedParticipant.Platznummer.Value);
            SeatedParticipants.Remove(SelectedSeatedParticipant);
            SelectedSeatedParticipant.Platznummer = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
