﻿using EasySave.Helpers;
using EasySave.Models;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class TransfersViewModel : BaseViewModel
    {
        private ObservableCollection<Transfer> _transfers;
        private ObservableCollection<Transfer> _filteredTransfers;
        private string _search = "";
        public string Search { get => _search; set { _search = value; OnPropertyChanged(); LoadTransfers(); } }
        public ObservableCollection<Transfer> FilteredTransfers
        {
            get => _filteredTransfers;
            set { _filteredTransfers = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Transfer> Transfers { get => _transfers; set {  _transfers = value; OnPropertyChanged(); LoadTransfers();            }
        }
        public ICommand StartTransferCommand { get; }
        public ICommand CancelTransferCommand { get; }
        public ICommand DeleteTransferCommand { get; }
        public ICommand PauseTransferCommand { get; }
        public ICommand ResumeTransferCommand { get; }
        public ICommand ModifyTransferCommand { get; }
        public ICommand SaveTransferCommand { get; }    
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
        private void ModifyTransfer(object? parameter)
        {
            if (parameter is Transfer transfer)
                transfer.State = States.Modifying;
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
                Transfer? tr = Transfers.FirstOrDefault(t => transfer.Name == t.Name);
                if (tr is null)
                    return true;
                if(transfer.Id == tr.Id) return true;
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
                t.Name.Contains(Search)
            ));
        }

        public TransfersViewModel()
        {
            _transfers = new ObservableCollection<Transfer>()
            {
                new Transfer("Transfer 1","",""),
                new Transfer("Transfer 2","","")
            };
            _filteredTransfers = _transfers;
            StartTransferCommand = new RelayCommand(StartTransfer);
            CancelTransferCommand = new RelayCommand(CancelTransfer);
            DeleteTransferCommand = new RelayCommand(DeleteTransfer);
            PauseTransferCommand = new RelayCommand(PauseTransfer);
            ResumeTransferCommand = new RelayCommand(ResumeTransfer);
            ModifyTransferCommand = new RelayCommand(ModifyTransfer);
            SaveTransferCommand = new RelayCommand(SaveTransfer, CanSaveTransfer);
        }

    }
}
