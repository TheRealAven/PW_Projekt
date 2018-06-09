using Obst.ølCatalog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Obst.ølCatalog.UI.ViewModels
{
    class ProducentViewModel : ViewModelBase
    {
        private IProducent _producent;
        public IProducent Producent
        {
            get => _producent;
        }

        public ProducentViewModel(IProducent producent)
        {
            _producent = producent;
        }

        [Required(ErrorMessage = "Producent musi mieć nazwę!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Długość nazwy musi być w przedziale <3,50>")]
        public string Nazwa
        {
            get => _producent.Nazwa;
            set
            {
                _producent.Nazwa = value;
                Validate();
                OnPropertyChanged("Nazwa");
            }
        }

        [Required(ErrorMessage = "Producent musi mieć przypisane miasto!")]
        [RegularExpression(@"^[a-zA-z\ ]*", ErrorMessage = "Nazwa miasta musi składać się z liter!")]
        public string Miasto
        {
            get => _producent.Miasto;
            set
            {
                _producent.Miasto = value;
                Validate();
                OnPropertyChanged("Miasto");
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
            RemoveErrors(nameof(Miasto));
            if (Nazwa.Length < 3)
            {
                AddError(nameof(Nazwa), "Nazwa musi być dłuższa niż 2 znaki");
            }
            if (Nazwa.Length < 3)
            {
                AddError(nameof(Miasto), "Nazwa miasta musi być dłuższa niż 2 znaki");
            }
            if (Miasto.Any(char.IsDigit))
            {
                AddError(nameof(Miasto), "Nazwa miasta nie może zawierać cyfr!");
            }
        }*/
    }
}
