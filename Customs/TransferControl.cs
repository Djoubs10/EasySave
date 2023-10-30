using System.Windows;
using System.Windows.Controls;
using EasySave.Models;
namespace EasySave.Customs
{
 
    public class TransferControl : Control
    {




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
