using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace R4Quadrant {
    public partial class QuadrantBox {
        public void AddTask(TaskRecord task = null) {
            task ??= new TaskRecord("新任务");
            Source.Add(task);
            Util.FindVisualChild<ScrollViewer>(this)?.ScrollToBottom();
            MainDataManager.DoSave();
        }
    }
}
