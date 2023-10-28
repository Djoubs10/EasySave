using EasySave.Models;
using System.CodeDom;
using System.Collections.ObjectModel;

namespace EasySave.ViewModels
{
    public class TransfersViewModel : BaseViewModel
    {
        private ObservableCollection<Transfer> _transfers;
        public ObservableCollection<Transfer> Transfers { get => _transfers; set {  _transfers = value; OnPropertyChanged(); } }
        public TransfersViewModel()
        {
            _transfers = new ObservableCollection<Transfer>();
        }
    }
}
