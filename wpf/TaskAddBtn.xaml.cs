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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace R4Quadrant {
    public partial class TaskAddBtn : UserControl {
        public uint QBIndex { get; set; } // 这个添加按钮对应的Box的下标
        public QuadrantBox QB {
            get {
                return MainWindow.QBs[QBIndex];
            }
        }

        public TaskAddBtn() {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            Background = QB.Background;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            QB.AddTask();
        }
    }
}
