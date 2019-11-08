using Prism.Regions;
using Swizzer.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Swizzer.Client.Windows.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(async () =>
            {
                var RC = ContentRegion;
                var vm = DataContext as MainViewModel;
                await vm.InitializeAsync();
            }));
        }


    }
}
