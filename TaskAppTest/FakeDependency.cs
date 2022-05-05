using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskAppGEICO.Models;
using TaskAppGEICO.Services;

namespace TaskAppTest
{
    public class FakeDependency : ITaskServices
    {
        //     public void DoSomething(string value)
        //{
        //    InputValue = value;
        //    ExecutionCount++;
        //}

        //Task AddTask(TaskTable task, ReturnMessage message);
        //Task UpdateTask(TaskTable request, ReturnMessage erm);
        //Task<List<TaskTable>> GetTasks(ReturnMessage erm);


        public async Task AddTask(TaskTable task, ReturnMessage message)
        {
            taskTable = task;
            ExecutionCount++;
        }

        public async Task UpdateTask(TaskTable task, ReturnMessage message)
        {
            taskTable = task;
            ExecutionCount++;
        }
        public async Task<List<TaskTable>> GetTasks(ReturnMessage message)
        {


            ExecutionCount++;

            List <TaskTable > tasks = new List <TaskTable> ();

            return tasks;
        }


        public int ExecutionCount { get; private set; }
        public TaskTable taskTable { get; private set; }


    }
}
