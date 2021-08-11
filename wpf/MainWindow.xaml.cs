using System.Windows;
using MahApps.Metro.Controls;

// TODO: 减少依赖
namespace R4Quadrant {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public static QuadrantBox[] QBs;

        public MainWindow() {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            QBs = new QuadrantBox[] { QB0, QB1, QB2, QB3 };
            MainDataManager.DoLoad();
            TopToggle.IsOn = true;
        }

        private void TopToggle_Toggled(object sender, RoutedEventArgs e) {
            this.Topmost = (sender as ToggleSwitch).IsOn;
        }
    }
}
