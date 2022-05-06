using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAppGEICO.Helpers;
using TaskAppGEICO.Models;
using TaskAppGEICO.Services;

namespace TaskAppGEICO.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices taskServices;

        public TaskController(
         ITaskServices taskServices

           )
        {

            this.taskServices = taskServices;
        }


        [HttpPost("addTask")]
        public async Task<ReturnMessage> AddTask([FromBody] TaskTable request)
        {
            ReturnMessage erm = new ReturnMessage
            {
                MessageDescription = "Successful",
                MessageType = "Success"
            };

            
            {
                try
                {
                    await taskServices.AddTask(request,erm);

                }
                catch (Exception ex)
                {

                    erm.MessageDescription = ($"Failed|{ ex.Message}");
                    erm.MessageType = "Error";

                }

            }
           
            return erm;

        }

        [HttpPut("updateTask")]
        public async Task<ReturnMessage> UpdateTask([FromBody] TaskTable request)
        {
            ReturnMessage erm = new ReturnMessage
            {
                MessageDescription = "Successful",
                MessageType = "Success"
            };

            if (request.Tid!=null)
            {
                try
                {
                    await taskServices.UpdateTask(request, erm);

                }
                catch (Exception ex)
                {

                    erm.MessageDescription = ($"Failed|{ ex.Message}");
                    erm.MessageType = "Error";

                }

            }
            else
            {

                erm.MessageDescription = "Task ID was null";
                erm.MessageType = "Error";

                return erm;
            }
            return erm;

        }

        [HttpGet("getTasks")]
        public async Task<object> GetTasks()

        {
            List<TaskTable> taskTable = new List<TaskTable>();  

            ReturnMessage erm = new ReturnMessage
            {
                MessageDescription = "Successful",
                MessageType = "Success"
            };


            {
                try
                {
                  var taskList=  await taskServices.GetTasks(erm);
                    if (erm.MessageType == "Error")
                    {
                        return erm;
                    }
                    else
                    {
                        return taskList;
                    }

                }
                catch (Exception ex)
                {

                    erm.MessageDescription = ($"Failed|{ ex.Message}");
                    erm.MessageType = "Error";

                }

            }
            return taskTable;
        }

    }
}
