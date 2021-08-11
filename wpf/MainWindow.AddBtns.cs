using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace R4Quadrant {
    public partial class MainWindow : MetroWindow {
        public Visibility AddBtnsVisibility { get; set; } = Visibility.Hidden;

        private void UpdateAddBtns(double size, double opacity, Visibility visibility) {
            AddBtns.Width = AddBtns.Height = size;
            AddBtns.Background.Opacity = opacity;
            AddBtnsContent.Visibility = visibility;
        }

        private void AddBtns_MouseEnter(object sender, MouseEventArgs e) {
            UpdateAddBtns(80, 1.0, Visibility.Visible);
        }

        private void AddBtns_MouseLeave(object sender, MouseEventArgs e) {
            UpdateAddBtns(17, 0.4, Visibility.Hidden);
        }
    }
}
