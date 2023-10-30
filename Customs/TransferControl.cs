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

        // Using a DependencyProperty as the backing store for StartCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartCommandProperty =
            DependencyProperty.Register("StartCommand", typeof(ICommand), typeof(TransferControl), new PropertyMetadata(null));




        public Transfer Transfer
        {
            get { return (Transfer)GetValue(TransferProperty); }
            set { SetValue(TransferProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Transfer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransferProperty =
            DependencyProperty.Register("Transfer", typeof(Transfer), typeof(TransferControl), new PropertyMetadata(null));


        static TransferControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransferControl), new FrameworkPropertyMetadata(typeof(TransferControl)));
        }
    }
}
