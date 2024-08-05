using System.ComponentModel;

namespace WPF_TeilnehmerModel
{
    public class Teilnehmer : INotifyPropertyChanged
    {
        private int? _platznummer;
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(DisplayText));  // Notify when Name changes
                }
            }
        }

        public int? Platznummer
        {
            get => _platznummer;
            set
            {
                if (_platznummer != value)
                {
                    _platznummer = value;
                    OnPropertyChanged(nameof(Platznummer));
                    OnPropertyChanged(nameof(DisplayText));  // Notify when Platznummer changes
                }
            }
        }

        public string DisplayText => Platznummer.HasValue ? $"{Platznummer:00}: {Name}" : Name;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
