using Obst.ølCatalog.Interfaces;
using Obst.ølCatalog.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Obst.ølCatalog.UI.ViewModels
{
    public class PiwoViewModel : ViewModelBase
    {
        private IPiwo _piwo;
        public IPiwo Piwo
        {
            get => _piwo;
        }

        private ObservableCollection<IProducent> _dostepniProducenci;
        public ObservableCollection<IProducent> DostepniProducenci
        {
            get => _dostepniProducenci;
        }

        public PiwoViewModel(IPiwo piwo, List<IProducent> producenci)
        {
            _piwo = piwo;
            _dostepniProducenci = new ObservableCollection<IProducent>(producenci);
        }

        [Required(ErrorMessage = "Piwo musi mieć określoną zawartość alkoholu!")]
        [Range(0,100,ErrorMessage = "Zawartość alkoholu nie może być mniejsza niż 0% lub większa niż 100%.")]
        public double ProcentAlkoholu
        {
            get => _piwo.ProcentAlkoholu;
            set
            {
                _piwo.ProcentAlkoholu = value;
                Validate();
                OnPropertyChanged("ProcentAlkoholu");
            }
        }

        [Required(ErrorMessage = "Piwo musi mieć nazwę!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Długość nazwy musi być w przedziale <3,50>")]
        public string Nazwa
        {
            get => _piwo.Nazwa;
            set
            {
                _piwo.Nazwa = value;
                Validate();
                OnPropertyChanged("Nazwa");
            }
        }

        [Required(ErrorMessage = "Piwo musi mieć cenę!")]
        [Range(0, double.MaxValue, ErrorMessage = "Cena musi być dodatnią liczbą!")]
        public double Cena
        {
            get => _piwo.Cena;
            set
            {
                _piwo.Cena = value;
                Validate();
                OnPropertyChanged("Cena");
            }
        }

        [Required(ErrorMessage = "Piwo musi mieć producenta!")]
        public IProducent Producent
        {
            get => _piwo.ProducentPiwa;
            set
            {
                _piwo.ProducentPiwa = value;
                Validate();
                OnPropertyChanged("ProducentPiwa");
            }
        }

        public Kolor KolorPiwa
        {
            get => _piwo.KolorPiwa;
            set
            {
                _piwo.KolorPiwa = value;
                Validate();
                OnPropertyChanged("KolorPiwa");
            }
        }

        public void Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            foreach (var kv in _errors.ToList())
            {
                if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                {
                    _errors.Remove(kv.Key);
                    RaiseErrorChanged(kv.Key);
                }
            }

            var q = from r in validationResults
                    from m in r.MemberNames
                    group r by m into g
                    select g;

            foreach (var prop in q)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();

                if (_errors.ContainsKey(prop.Key))
                {
                    _errors.Remove(prop.Key);
                }
                _errors.Add(prop.Key, messages);

                RaiseErrorChanged(prop.Key);
            }
        }

        /*public void Validate()
        {
            RemoveErrors(nameof(Nazwa));
            RemoveErrors(nameof(Cena));
            RemoveErrors(nameof(ProcentAlkoholu));
            RemoveErrors(nameof(KolorPiwa));

            if (Nazwa.Length < 3)
            {
                AddError(nameof(Nazwa), "Nazwa musi być dłuższa niż 2 znaki");
            }
            if (Cena < 0)
            {
                AddError(nameof(Cena), "Cena nie może być ujemna!");
            }
            if((ProcentAlkoholu<0)||(ProcentAlkoholu>100))
            {
                AddError(nameof(ProcentAlkoholu), "Wartość musi być prawidłowym procentem zawartości alkoholu!");
            }
            if((KolorPiwa!=Kolor.Ciemne)&&(KolorPiwa!=Kolor.Jasne))
            {
                AddError(nameof(KolorPiwa), "Podany kolor nie istnieje w świecie przedstawionym");
            }
        }*/
    }
}
