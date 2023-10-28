using EasySave.Helpers;
using EasySave.Models;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class TransfersViewModel : BaseViewModel
    {
        private ObservableCollection<Transfer> _transfers;
        public ObservableCollection<Transfer> Transfers { get => _transfers; set {  _transfers = value; OnPropertyChanged(); } }
        public ICommand StartTransferCommand { get; }
        private void StartTransfer(object? parameter)
        {
            if(parameter is Transfer transfer)
            {
                Thread t = new Thread(transfer.Start);
                t.Start();
            }
        }

        public TransfersViewModel()
        {
            _transfers = new ObservableCollection<Transfer>()
            {
                new Transfer("Transfer 1"),
                new Transfer("Transfer 2")
            };
            StartTransferCommand = new RelayCommand(StartTransfer);
        }

    }
}
