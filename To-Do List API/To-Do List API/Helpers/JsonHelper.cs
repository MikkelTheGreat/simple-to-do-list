using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace To_Do_List_API.Helpers
{
    public class JsonHelper
    {
        private readonly string jsonFilePath = "./Data/tasks.json";

        public JsonHelper()
        {
        }

        public void CreateTaskInFile(string taskName)
        {
            var taskList = GetTaskList();

            var latestTask = taskList.LastOrDefault();

            taskList.Add(new Task
            {
                Id = latestTask != null ? latestTask.Id + 1 : 0,
                Name = taskName,
                IsCompleted = false
            });

            WriteToFile(taskList);
        }

        public List<Task> GetTaskList()
        {
            if (File.Exists(jsonFilePath))
            {
                List<Task> tasks = new List<Task>();

                using (StreamReader sr = new StreamReader(jsonFilePath))
                {
                    var json = sr.ReadToEnd();
                    tasks = JsonConvert.DeserializeObject<List<Task>>(json);
                }

                return tasks;
            }
            else
            {
                return new List<Task>();
            }
        }

        public void UpdateStatusForId(int id)
        {
            var taskList = GetTaskList();
            var task = taskList.FirstOrDefault(x => x.Id == id);

            if (task != null) { task.IsCompleted = !task.IsCompleted; }

            WriteToFile(taskList);
        }

        public void DeleteForId(int id)
        {
            var taskList = GetTaskList();
            var task = taskList.FirstOrDefault(x => x.Id == id);

            if (task != null) { taskList.Remove(task); }

            WriteToFile(taskList);
        }

        public string GetJsonStringFromList(List<Task> tasks)
        {
            return JsonConvert.SerializeObject(tasks);
        }

        private void WriteToFile(List<Task> taskList)
        {
            using (StreamWriter sw = new StreamWriter(jsonFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(taskList));
            }
        }
    }
}
