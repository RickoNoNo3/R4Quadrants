using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace R4Quadrant {
    /// <summary>
    /// 管理数据的类, 包含了存储和加载两大功能
    /// </summary>
    public class MainDataManager {
        private static readonly string _DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string DataFolder {
            get {
                string path = Path.Combine(_DataFolder, "R4Quadrant");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public static string MainData { get { return Path.Combine(DataFolder, "data.json"); } }

        public static void DoSave() {
            try {
                JObject rootObj = new JObject();
                JArray boxListArr = new JArray();
                // 构造每一个box
                foreach (QuadrantBox box in MainWindow.QBs) {
                    JObject boxObj = new JObject() { { "title", box.Title } };
                    JArray taskListArr = new JArray();
                    // 构造每一个task
                    foreach (TaskRecord task in box.Source) {
                        JObject taskObj = new JObject { { "title", task.Title } };
                        taskListArr.Add(taskObj);
                    }
                    boxObj.Add("taskList", taskListArr);
                    boxListArr.Add(boxObj);
                }
                rootObj.Add("boxList", boxListArr);
                // 写入文件
                File.WriteAllText(MainData, rootObj.ToString(), Encoding.UTF8);
            } catch { }
        }

        public static void DoLoad() {
            try {
                string rootJson = File.ReadAllText(MainData);
                JObject rootObj = JObject.Parse(rootJson);
                JArray boxListArr = rootObj.Value<JArray>("boxList");
                foreach (JObject boxObj in boxListArr) {
                    var source = Array.Find<QuadrantBox>(MainWindow.QBs, (box) => {
                        return box.Title == boxObj.Value<String>("title");
                    })?.Source;
                    JArray taskListArr = boxObj.Value<JArray>("taskList");
                    foreach (JObject taskObj in taskListArr) {
                        source.Add(new TaskRecord(taskObj.Value<String>("title")));
                    }
                }
            } catch { }
        }
    }
}
