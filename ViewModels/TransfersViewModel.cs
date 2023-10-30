using EasySave.Helpers;
using EasySave.Models;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class TransfersViewModel : BaseViewModel
    {
        private ObservableCollection<Transfer> _transfers;
        public ObservableCollection<Transfer> Transfers { get => _transfers; set {  _transfers = value; OnPropertyChanged(); } }
        public ICommand StartTransferCommand { get; }
        public ICommand CancelTransferCommand { get; }
        public ICommand DeleteTransferCommand { get; }
        public ICommand PauseTransferCommand { get; }
        public ICommand ResumeTransferCommand { get; }
        private void StartTransfer(object? parameter)
        {
            if(parameter is Transfer transfer)
            {
                Thread t = new Thread(transfer.Start);
                t.Start();
            }
        }
        private void PauseTransfer(object? parameter)
        {
            if(parameter is Transfer transfer)
                transfer.Pause();
        }
        private void ResumeTransfer(object? parameter)
        {
            if (parameter is Transfer transfer)
                transfer.Resume();
        }
        private void DeleteTransfer(object? parameter)
        {
            if(parameter is Transfer transfer)
            {
                Transfers.Remove(transfer);
            }
        }
        private void CancelTransfer(object? parameter) {
            if (parameter is Transfer transfer)
                transfer.Cancel();
        }
        public TransfersViewModel()
        {
            _transfers = new ObservableCollection<Transfer>()
            {
                new Transfer("Transfer 1"),
                new Transfer("Transfer 2")
            };
            StartTransferCommand = new RelayCommand(StartTransfer);
            CancelTransferCommand = new RelayCommand(CancelTransfer);
            DeleteTransferCommand = new RelayCommand(DeleteTransfer);
            PauseTransferCommand = new RelayCommand(PauseTransfer);
            ResumeTransferCommand = new RelayCommand(ResumeTransfer);   
        }

    }
}
