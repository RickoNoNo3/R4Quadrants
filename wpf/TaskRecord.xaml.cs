using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace R4Quadrant {
    public partial class TaskRecord : UserControl {
        public string Title { get; set; }
        public Color SignColor { get; set; } // 目前作废

        public TaskRecord(string text = "") {
            InitializeComponent();
            this.Title = text;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            this.DataContext = this;
        }

        public enum ChangeType { SourceToTarget, TargetToSource, }

        /// <summary>
        /// 改变本任务的标题(显示栏 -- 后台 双向)
        /// </summary>
        /// <param name="title">要改变的标题</param>
        /// <param name="type">改变方向</param>
        public void ChangeTitle(string title = "", ChangeType type = ChangeType.SourceToTarget) {
            this.Title = title;
            switch (type) {
                case ChangeType.SourceToTarget:
                    Text.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
                    Text.GetBindingExpression(TextBlock.ToolTipProperty).UpdateTarget();
                    break;
                case ChangeType.TargetToSource:
                    Text.GetBindingExpression(TextBlock.TextProperty).UpdateSource();
                    break;
                default:
                    break;
            }
            MainDataManager.DoSave();
        }

        /// <summary>
        /// 显示栏双击, 视为开始编辑
        /// </summary>
        private void DisplayControl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            DoEdit();
        }

        /// <summary>
        /// 编辑栏丢失焦点, 视为结束编辑
        /// </summary>
        private void TextEdit_LostFocus(object sender, RoutedEventArgs e) {
            EndEdit();
        }

        /// <summary>
        /// 编辑栏中按下回车键, 视为结束编辑
        /// </summary>
        private void TextEdit_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                EndEdit();
            }
        }

        /// <summary>
        /// 显示编辑栏
        /// </summary>
        public void DoEdit() {
            QuadrantBox.InEdit = true;
            TextEdit.Text = Title;
            TextEdit.Visibility = Visibility.Visible;
            DisplayControl.Visibility = Visibility.Hidden;
            this.Dispatcher.BeginInvoke(new Action(() => {
                TextEdit.CaretIndex = TextEdit.Text.Length;
                if (TextEdit.Text == "新任务")
                    TextEdit.SelectAll();
                TextEdit.Focus();
            }));
        }

        /// <summary>
        /// 隐藏编辑栏并显示修改后的内容
        /// </summary>
        public void EndEdit() {
            QuadrantBox.InEdit = false;
            ChangeTitle(TextEdit.Text);
            TextEdit.Visibility = Visibility.Hidden;
            DisplayControl.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 鼠标进入, 显示按钮栏
        /// </summary>
        private void TopGrid_MouseEnter(object sender, MouseEventArgs e) {
            BtnDock.Visibility = Visibility.Visible;
            BtnDock.Width = 26;
        }

        /// <summary>
        /// 鼠标退出, 隐藏按钮栏
        /// </summary>
        private void TopGrid_MouseLeave(object sender, MouseEventArgs e) {
            BtnDock.Visibility = Visibility.Visible;
            BtnDock.Width = 0;
        }

        /// <summary>
        /// 按下删除按钮, 删除本任务
        /// </summary>
        private void DeleteTaskBtn_Click(object sender, RoutedEventArgs e) {
            QuadrantBox parent = Util.FindVisualParent<QuadrantBox>(this);
            parent.DeleteTask(this);
        }

        /// <summary>
        /// 拖动事件入口, 实际的执行是在QuadrantBox
        /// </summary>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            QuadrantBox parent = Util.FindVisualParent<QuadrantBox>(this);
            MainWindow win = Util.FindVisualParent<MainWindow>(this);
            parent.DoDrag(this, e.GetPosition(win));
        }
    }
}
