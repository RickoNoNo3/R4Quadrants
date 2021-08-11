using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace R4Quadrant {
    public partial class QuadrantBox : UserControl {
        public MainWindow Win;
        public string Title { get; set; }
        public Color BackgroundColor { get; set; }
        public ObservableCollection<TaskRecord> Source = new ObservableCollection<TaskRecord>();

        public QuadrantBox() {
            InitializeComponent();
            this.DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            this.Win = Util.FindVisualParent<MainWindow>(this);
            this.DataList.ItemsSource = Source;
        }

        /* 这里的DoubleClick会在各种内部双击(比如修改文本时)触发, 有BUG. 暂时屏蔽了 */
        private void DataList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            //AddTask();
        }
    }
}
