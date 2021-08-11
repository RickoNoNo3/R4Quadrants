using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace R4Quadrant {
    public partial class QuadrantBox {
        public class DragTargetStruct {
            public QuadrantBox qb;
            public int index;
        }

        public static bool InEdit = false; // 编辑时屏蔽拖动(指的是文本框上, 其实现在无用了)
        public static TaskRecord DragSource = null; // 当前拖动的元素
        public static DragTargetStruct DragTarget = null; // 当前指针下预计拖动到的目标

        // public static Window DragFollowWindow = null; // 视觉效果(预览窗口)

        /// <summary>
        /// 拖动的入口函数, 由某一条TaskRecord调用!!! 不是QuadrantBox调用!!!
        /// </summary>
        public void DoDrag(TaskRecord task, Point mouse) {
            try {
                if (DragSource == null && !InEdit) {
                    DragSource = task;
                    // --- 预览窗口现已弃用 ---
                    // // 进行视觉效果处理
                    // // 生成预览窗口
                    // DragFollowWindow = new Window {
                    // 	WindowStyle = WindowStyle.None,
                    // 	AllowsTransparency = true,
                    // 	AllowDrop = false,
                    // 	Background = null,
                    // 	IsHitTestVisible = false,
                    // 	SizeToContent = SizeToContent.WidthAndHeight,
                    // 	Topmost = true,
                    // 	ShowInTaskbar = false,
                    // };
                    // // 生成预览窗口内容
                    // ChangeDragFollowWindow(mouse, null);
                    // DragFollowWindow.Show();
                    // 开始拖动
                    ChangeDragTarget(new DragTargetStruct() {
                        qb = this,
                        index = Source.IndexOf(DragSource)
                    });
                    DragDrop.DoDragDrop(DataList, new DataObject(DragSource), DragDropEffects.All);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                StopDrag(); // 因为DoDragDrop是阻塞的, 所以到这步不论如何都应该停止拖动了
            }
        }

        /// <summary>
        /// 接受拖动结果
        /// </summary>
        private void DataList_Drop(object sender, DragEventArgs e) {
            try {
                // do nothing...
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                StopDrag();
            }
        }

        /// <summary>
        /// 处理拖动中途事件, 包括:
        ///   1. 鼠标到达Box上下边缘时, 让Box自动滚动.
        ///   2. 计算出如果松开鼠标, 会添加到的位置.
        ///   3. 更新拖动项的位置(实际上线程不安全).
        /// </summary>
        private void DataList_DragOver(object sender, DragEventArgs e) {
            // -----------------------------------------------------
            // 让ListBox(ContentControl)里的ScrollViewer上下自动滚动
            ScrollViewer viewer = Util.FindVisualChild<ScrollViewer>(DataList);
            double tolerance = 15, verticalPos = e.GetPosition(DataList).Y, offset = 3;
            if (verticalPos < tolerance) {
                viewer.ScrollToVerticalOffset(viewer.VerticalOffset - offset);
            } else if (verticalPos > DataList.ActualHeight - tolerance) {
                viewer.ScrollToVerticalOffset(viewer.VerticalOffset + offset);
            }
            // -----------------------------------------------------
            // 取得新DragTarget
            try {
                var mouseTop = e.GetPosition(DataList).Y + viewer.VerticalOffset;
                var index = 0;
                // 遍历当前数据列表, 找到合适的高度
                for (int i = 0; i < Source.Count; i++) {
                    var taskTop = Source[i].ActualHeight * (i + 0.6);
                    if (mouseTop >= taskTop) {
                        index = i + 1;
                    }
                }
                // 改变当前添加位置
                ChangeDragTarget(new DragTargetStruct() {
                    qb = this,
                    index = index,
                });
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            // --- 预览窗口现已弃用 ---
            // DragFollowWindow?.Show();
            // -----------------------------------------------------
            // 根据新DragTarget位置进行移动
            // 进行移动
            try {
                // 取得DragSource当前所在的Box
                var box = Util.FindVisualParent<QuadrantBox>(DragSource);
                if (box == null)
                    throw new Exception("无效的原元素盒子");
                // 从原Box中删除, 在新Box中插入或追加
                if (DragTarget.index >= DragTarget.qb.Source.Count) {
                    box.Source.Remove(DragSource);
                    Source.Add(DragSource);
                } else {
                    var target = DragTarget.qb.Source[DragTarget.index];
                    if (!ReferenceEquals(DragSource, target)) {
                        // 如果是同一个盒子内的移动
                        // 向后移动时, 目标下标-1  (因为会先删除前面的一个)
                        // 向前移动时, 目标下标不变(因为删除和它无关)
                        if (ReferenceEquals(box, this) && Source.IndexOf(DragSource) <= DragTarget.index)
                            DragTarget.index--;
                        box.Source.Remove(DragSource);
                        Source.Insert(DragTarget.index, DragSource);
                    }
                }
                MainDataManager.DoSave();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 拖动中途事件, 只处理鼠标样式, 别的都在DragOver里
        /// </summary>
        private void DataList_GiveFeedback(object sender, GiveFeedbackEventArgs e) {
            Mouse.SetCursor(Cursors.SizeAll);
            e.Handled = true;
        }

        /// <summary>
        /// 结束拖动, 清除所有中间变量
        /// </summary>
        private void StopDrag() {
            DragSource = null;
            if (DragTarget != null) {
                ChangeDragTarget(null);
            }
            // --- 预览窗口现已弃用 ---
            //if (DragFollowWindow != null) {
            //	DragFollowWindow.Close();
            //	DragFollowWindow = null;
            //}
        }

        /// <summary>
        /// 改变预览窗体位置
        /// </summary>

        // --- 预览窗口现已弃用 ---
        //private void ChangeDragFollowWindow(Point mouse, Object content) {
        //	if (DragFollowWindow == null)
        //		return;
        //	if (content != null) {
        //		DragFollowWindow.Content = content;
        //	}
        //	DragFollowWindow.Left = mouse.X + Win.Left + 10;
        //	DragFollowWindow.Top = mouse.Y + Win.Top + 10;
        //}

        /// <summary>
        /// 改变目标位置
        /// </summary>
        private void ChangeDragTarget(DragTargetStruct dragTarget = null) {
            // 1.皆不为空, 2.QB是同一个, 3.下标相等
            // 此时意味着target没变
            if (dragTarget != null && DragTarget != null &&
                ReferenceEquals(dragTarget.qb, DragTarget.qb) &&
                dragTarget.index == DragTarget.index) {
                return;
            }
            // 图形处理比较困难, 暂时先放弃了
            // 这种往listbox里添加分割线元素的方法存在很多问题, 不建议使用
            //// 清除旧数据
            //if (NowDragTarget != null) {
            //	foreach (Object item in NowDragTarget.qb.Source) {
            //		if (!(item is TaskRecord)) {
            //			NowDragTarget.qb.Source.Remove(item);
            //		}
            //	}
            //}
            //// 添加新数据
            //if (dragTarget != null) {
            //Win.Dispatcher.BeginInvoke(new Action(() => {
            //	Console.WriteLine(dragTarget.index);
            //}));
            //	dragTarget.qb.Source.Insert(dragTarget.index, new Border() {
            //		BorderThickness = new Thickness(0, 1, 0, 0),
            //		BorderBrush = Brushes.Black,
            //		Child = new Grid() {
            //			Background = Brushes.Black
            //		}
            //	});
            //}
            DragTarget = dragTarget;
        }
    }
}
