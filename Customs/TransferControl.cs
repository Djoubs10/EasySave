using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EasySave.Models;
namespace EasySave.Customs
{
 
    public class TransferControl : Control
    {
        public ICommand StartCommand
        {
            get { return (ICommand)GetValue(StartCommandProperty); }
            set { SetValue(StartCommandProperty, value); }
        }

        public static readonly DependencyProperty StartCommandProperty =
            DependencyProperty.Register("StartCommand", typeof(ICommand), typeof(TransferControl), new PropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(TransferControl), new PropertyMetadata(null));


        public ICommand PauseCommand
        {
            get { return (ICommand)GetValue(PauseCommandProperty); }
            set { SetValue(PauseCommandProperty, value); }
        }

        public static readonly DependencyProperty PauseCommandProperty =
            DependencyProperty.Register("PauseCommand", typeof(ICommand), typeof(TransferControl), new PropertyMetadata(null));

        public ICommand ResumeCommand
        {
            get { return (ICommand)GetValue(ResumeCommandProperty); }
            set { SetValue(ResumeCommandProperty, value); }
        }

        public static readonly DependencyProperty ResumeCommandProperty =
            DependencyProperty.Register("ResumeCommand", typeof(ICommand), typeof(TransferControl), new PropertyMetadata(null));


        public Transfer Transfer
        {
            get { return (Transfer)GetValue(TransferProperty); }
            set { SetValue(TransferProperty, value); }
        }

        public static readonly DependencyProperty TransferProperty =
            DependencyProperty.Register("Transfer", typeof(Transfer), typeof(TransferControl), new PropertyMetadata(null));


        static TransferControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransferControl), new FrameworkPropertyMetadata(typeof(TransferControl)));
        }
    }
}
