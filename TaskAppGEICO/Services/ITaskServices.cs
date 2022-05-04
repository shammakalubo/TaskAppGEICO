using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAppGEICO.Models;

namespace TaskAppGEICO.Services
{
    public interface ITaskServices
    {
        Task AddTask(TaskTable task, ReturnMessage message);
        Task UpdateTask(TaskTable request, ReturnMessage erm);
        Task<List<TaskTable>> GetTasks(ReturnMessage erm);
    }
}
