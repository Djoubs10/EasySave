using EasySave.Helpers;
using EasySave.Models;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Windows;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class TransfersViewModel : BaseViewModel
    {
        private ObservableCollection<Transfer> _transfers;
        private ObservableCollection<Transfer> _filteredTransfers;
        private string _newName = "", _newSource = "", _newTarget = "";
        public string NewName { get => _newName; set { _newName = value; OnPropertyChanged(); } }
        public string NewSource { get => _newSource; set { _newSource = value; OnPropertyChanged(); } }
        public string NewTarget { get => _newTarget; set { _newTarget = value; OnPropertyChanged(); } }
        private bool _add = false;
        public bool Add { get => _add; set { _add = value; OnPropertyChanged(); } }
        private string _search = "";
        public string Search { get => _search; set { _search = value; OnPropertyChanged(); LoadTransfers(); } }
        public ObservableCollection<Transfer> FilteredTransfers
        {
            get => _filteredTransfers;
            set { _filteredTransfers = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Transfer> Transfers { get => _transfers; set {  _transfers = value; OnPropertyChanged(); LoadTransfers(); }
        }
        public ICommand StartTransferCommand { get; }
        public ICommand CancelTransferCommand { get; }
        public ICommand DeleteTransferCommand { get; }
        public ICommand PauseTransferCommand { get; }
        public ICommand ResumeTransferCommand { get; }
        public ICommand ModifyTransferCommand { get; }
        public ICommand SaveTransferCommand { get; }    
        public ICommand StartAllTransfersCommand { get; }    
        public ICommand CancelAllTransfersCommand { get; }    
        public ICommand AddTransferCommand { get; }    
        public ICommand SaveNewTransferCommand { get; }
        public ICommand CancelAddTransferCommand { get; }
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
                LoadTransfers();
            }
        }
        private void CancelTransfer(object? parameter) {
            if (parameter is Transfer transfer)
                transfer.Cancel();
        }
        private void ModifyTransfer(object? parameter)
        {
            if (parameter is Transfer transfer)
            {
                transfer.State = States.Modifying;
                LoadTransfers();
            }
        }
        private void SaveTransfer(object? parameter)
        {
            if (parameter is Transfer transfer)
                transfer.State = States.Ready;
        }

        private bool CanSaveTransfer(object? parameter)
        {
            if(parameter is Transfer transfer)
            {
                Transfer? tr = Transfers.FirstOrDefault(t => transfer.Name == t.Name && t.Id != transfer.Id);
                if (tr is null)
                    return true;
            }
            return false;
        }

        private void LoadTransfers()
        {
            if(Search == "")
            {
                FilteredTransfers = Transfers;
                return;
            }
            FilteredTransfers = new ObservableCollection<Transfer>(Transfers.Where((t) =>
                t.Name.ToLower().Contains(Search.ToLower())
            ));
        }
        public void StartAllTransfers(object? _)
        {
            foreach(Transfer t in FilteredTransfers)
            {
                if(t.State == States.Ready)
                {                        
                    new Thread(t.Start).Start();
                }
            }
        }

        public void CancelAllTransfers(object? _)
        {
            foreach(Transfer t in FilteredTransfers)
            {
                if (t.State != States.Ready && t.State != States.Modifying)
                    t.Cancel();
            }
        }
        private void SaveNewTransfer(object? _)
        {
            if(NewName != "" && Transfers.FirstOrDefault(t => t.Name == NewName) == null)
            {
                Transfers.Insert(0,new Transfer(NewName, NewSource, NewTarget));
                NewName = ""; NewSource = ""; NewTarget = "";
                Add = false;
                LoadTransfers();
            }
        }
        private bool CanSaveNewTransfer(object? _)
            => (NewName != "" && Transfers.FirstOrDefault(t => t.Name == NewName) == null);

        public TransfersViewModel()
        {
            _transfers = new ObservableCollection<Transfer>() { new Transfer("Transfer 1", "C:\\Users\\ndjoubri\\Desktop\\dev_p\\EasySave", "C:\\Users\\ndjoubri\\Desktop\\Copy\\EasySaveCopy"), new Transfer("Transfer 2","","") };
            _filteredTransfers = _transfers;
            StartTransferCommand = new RelayCommand(StartTransfer);
            CancelTransferCommand = new RelayCommand(CancelTransfer);
            DeleteTransferCommand = new RelayCommand(DeleteTransfer);
            PauseTransferCommand = new RelayCommand(PauseTransfer);
            ResumeTransferCommand = new RelayCommand(ResumeTransfer);
            ModifyTransferCommand = new RelayCommand(ModifyTransfer);
            SaveTransferCommand = new RelayCommand(SaveTransfer, CanSaveTransfer);
            StartAllTransfersCommand = new RelayCommand(StartAllTransfers);
            CancelAllTransfersCommand = new RelayCommand(CancelAllTransfers);
            AddTransferCommand = new RelayCommand((object? _) => { Add = true; Search = ""; }, (object? _) => { return !Add; });
            CancelAddTransferCommand = new RelayCommand((object? _) => { Add = false; Search = ""; NewName = ""; NewSource = ""; NewTarget = ""; }, (object? _) => { return  Add; });
            SaveNewTransferCommand = new RelayCommand(SaveNewTransfer, CanSaveNewTransfer);
        }

    }
}
