using System;
using System.Collections.Generic;
using System.Text;

namespace R4Quadrant {
    public partial class QuadrantBox {
        public void DeleteTask(TaskRecord task) {
            Source.Remove(task);
            MainDataManager.DoSave();
        }
    }
}
