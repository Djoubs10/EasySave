using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasySave.Customs
{
    public class AddTransferControl : Control
    {


        public string NewName
        {
            get { return (string)GetValue(NewNameProperty); }
            set { SetValue(NewNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewNameProperty =
            DependencyProperty.Register("NewName", typeof(string), typeof(AddTransferControl), new PropertyMetadata(""));



        public string NewSource
        {
            get { return (string)GetValue(NewSourceProperty); }
            set { SetValue(NewSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewSourceProperty =
            DependencyProperty.Register("NewSource", typeof(string), typeof(AddTransferControl), new PropertyMetadata(""));


        public string NewTarget
        {
            get { return (string)GetValue(NewTargetProperty); }
            set { SetValue(NewTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewTargetProperty =
            DependencyProperty.Register("NewTarget", typeof(string), typeof(AddTransferControl), new PropertyMetadata(""));





        public ICommand AddCommand
        {
            get { return (ICommand)GetValue(AddCommandProperty); }
            set { SetValue(AddCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddCommandProperty =
            DependencyProperty.Register("AddCommand", typeof(ICommand), typeof(AddTransferControl), new PropertyMetadata(null));





        static AddTransferControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AddTransferControl), new FrameworkPropertyMetadata(typeof(AddTransferControl)));
        }
    }
}
