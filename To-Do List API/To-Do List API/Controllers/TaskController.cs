using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using To_Do_List_API.Helpers;

namespace To_Do_List_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private JsonHelper _jsonHelper;
        public TaskController()
        {
            _jsonHelper = new JsonHelper();
        }

        [HttpGet("Get", Name = "GetTasks")]
        public string Get()
        {
            var taskList = _jsonHelper.GetTaskList();

            return _jsonHelper.GetJsonStringFromList(taskList);
        }

        [HttpPost("Create", Name = "AddTask")]
        public string Create(string taskName)
        {
            _jsonHelper.CreateTaskInFile(taskName);

            return _jsonHelper.GetJsonStringFromList(_jsonHelper.GetTaskList());
        }

        [HttpPatch("Update/{id}", Name = "UpdateStatus")]
        public string UpdateStatus(int id)
        {
            _jsonHelper.UpdateStatusForId(id);

            return _jsonHelper.GetJsonStringFromList(_jsonHelper.GetTaskList());
        }

        [HttpDelete("Delete/{id}", Name = "Delete")]
        public string Delete(int id)
        {
            _jsonHelper.DeleteForId(id);

            return _jsonHelper.GetJsonStringFromList(_jsonHelper.GetTaskList());
        }
    }
}
