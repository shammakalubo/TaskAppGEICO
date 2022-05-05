using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAppGEICO.Helpers;
using TaskAppGEICO.Models;

namespace TaskAppGEICO.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly IOptions<AppSettingsParamHelperModel> appSettings;
        private readonly GEICOTESTContext dbContext;

        public TaskServices(IOptions<AppSettingsParamHelperModel> _options, GEICOTESTContext _db)
        {
            this.appSettings = _options;
            this.dbContext = _db;
        }

        public async Task AddTask(TaskTable task, ReturnMessage message)
        {
            try
            {
                //As this is a new task, we may need to make the status as "New"
                //get statusList
                var statusList = dbContext.TaskStatuses.ToList();
                var newTaskStatus = statusList.Where(x => x.Stype == appSettings.Value.NewTasksStatus).Select(x => x.Sid).FirstOrDefault();
                task.TstatusId = newTaskStatus;

                //create a new Task to keep the task Id null as it auto-generated while insert
                TaskTable newTask = new TaskTable
                {
                    Tname = task.Tname,
                    Description = task.Description,
                    TdueDate = task.TdueDate,
                    TpriorityId = task.TpriorityId,
                    TstatusId = task.TstatusId
                };

                //When creating a Task, the DueDate cannot be in the past
                if (await CheckDueDate(newTask, message))
                {
                    //duedate cannot be in the pst, but I guess it can be saved as empty and no need to check any other conditions as the date is empty
                    if (task.TdueDate == null)
                    {
                        await AddTaskToDB(newTask, message);
                    }
                    else
                    {
                        if (await CheckPriorityAndCount(newTask, message))
                        {
                            await AddTaskToDB(newTask, message);
                        }
                        else
                        {
                            message.MessageDescription = ($"Failed| The system have more than 100 High Priority tasks which have the same due date and are not finished yet at any time");
                            message.MessageType = "Error";
                        }
                    }
                }
                else
                {
                    message.MessageDescription = ($"Failed|The due date cannot be in the past. Please check the date");
                    message.MessageType = "Error";
                }

            }
            catch (Exception ex)
            {
                message.MessageDescription = ($"Failed|{ ex.Message}");
                message.MessageType = "Error";
            }

        }

        public async Task UpdateTask(TaskTable task, ReturnMessage message)
        {
            try
            {
                //When creating a Task, the Due Date cannot be in the past
                //No need of checking on the due date as this is an update
                //It can be saved as empty and no need to check any other conditions if the date is empty
                if (task.TdueDate == null)
                {
                    await UpdateTaskInDB(task, message);
                }
                else
                {
                    if (await CheckPriorityAndCount(task, message))
                    {
                        await UpdateTaskInDB(task, message);
                    }
                    else
                    {
                        message.MessageDescription = ($"Failed| The system have more than 100 High Priority tasks which have the same due date and are not finished yet at any time");
                        message.MessageType = "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                message.MessageDescription = ($"Failed|{ ex.Message}");
                message.MessageType = "Error";
            }
        }

        public async Task DeleteTask(TaskTable task, ReturnMessage message)
        {
            try
            {
                //When creating a Task, the Due Date cannot be in the past
                //No need of checking on the due date as this is an update
                //It can be saved as empty and no need to check any other conditions if the date is empty
                if (task.TdueDate == null)
                {
                    await UpdateTaskInDB(task, message);
                }
                else
                {
                    if (await CheckPriorityAndCount(task, message))
                    {
                        await UpdateTaskInDB(task, message);
                    }
                    else
                    {
                        message.MessageDescription = ($"Failed| The system have more than 100 High Priority tasks which have the same due date and are not finished yet at any time");
                        message.MessageType = "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                message.MessageDescription = ($"Failed|{ ex.Message}");
                message.MessageType = "Error";
            }
        }

        public async Task<List<TaskTable>> GetTasks(ReturnMessage message)
        {
            List<TaskTable> task = new List<TaskTable>();
            try
            {
                var taskList = dbContext.TaskTables.ToList();
                await dbContext.DisposeAsync();
                return taskList;
            }
            catch (Exception ex)
            {
                message.MessageDescription = ($"Failed|{ ex.Message}");
                message.MessageType = "Error";
            }
            return task;
        }

        private async Task UpdateTaskInDB(TaskTable task, ReturnMessage message)
        {
            try
            {
                //var updateTask = dbContext.TaskTables.Update(task);
                // dbContext.Entry(task).State = EntityState.Detached;
                //await dbContext.SaveChangesAsync();


                var updateTask = dbContext.Entry(task).State=EntityState.Modified;
                await dbContext.SaveChangesAsync();

                message.MessageDescription = ($"Task {task.Tname} updated");
            }
            catch (Exception e)
            {
                message.MessageDescription = ($"An error campe up while saving a task. {task.Description} ");
                message.MessageType = $"Error, {e.Message}";
            }
        }
        private async Task DeleteTaskInDB(TaskTable task, ReturnMessage message)
        {
            try
            {
                var taskToDelete = dbContext.TaskTables.FirstOrDefault(x => x.Tid == task.Tid);
                //var updateTask = dbContext.TaskTables.Update(task);
                var deleteTask = dbContext.Entry(taskToDelete).State = EntityState.Deleted;
                await dbContext.SaveChangesAsync();

                message.MessageDescription = ($"Task {task.Tname} updated");
            }
            catch (Exception e)
            {
                message.MessageDescription = ($"An error campe up while saving a task. {task.Description} ");
                message.MessageType = $"Error, {e.Message}";
            }
        }

        private async Task AddTaskToDB(TaskTable task, ReturnMessage message)
        {
            try
            {
                var newTask = await dbContext.TaskTables.AddAsync(task);
                await dbContext.SaveChangesAsync();
                await dbContext.DisposeAsync();

                message.MessageDescription = ($"Task {task.Tname} saved");
            }
            catch (Exception e)
            {
                message.MessageDescription = ($"An error campe up while saving a task. {task.Description} ");
                message.MessageType = $"Error, {e.Message}";
            }
        }

        private async Task<bool> CheckPriorityAndCount(TaskTable task, ReturnMessage message)
        {
            //The system should not have more than 100 High Priority tasks which have the same due date and are not finished yet at any time
            try
            {
                //get priorities
                var priorityList = dbContext.TaskPriorities.ToList();
                var HighPriorityId = priorityList.Where(x => x.Ptype == appSettings.Value.PriorityConcern).Select(x => x.Pid).FirstOrDefault();

                var statusList = dbContext.TaskStatuses.ToList();
                var FinishedStatusId = statusList.Where(x => x.Stype.Trim() == appSettings.Value.StatusConcern.Trim()).Select(x => x.Sid).FirstOrDefault();

                //High Priority, I'm not sure but I'm saving the task with priority as empty
                if (task.TpriorityId != null)
                {
                    if (task.TpriorityId == HighPriorityId)//checks if the priority is == High
                    {
                        if (task.TstatusId != null)//Checking if the status is Finished or not
                        {
                            if (task.TstatusId == FinishedStatusId)//Checking if the status is Finished or not
                            {
                                var highPriorityUnfinishedCount = await getHighPriorityUnfinishedCount(task, message);
                                if (highPriorityUnfinishedCount >= Convert.ToInt32(appSettings.Value.HighPriorityTaskCount))
                                {
                                    message.MessageDescription = "High priority unfinished tasks for the same DueDate are almost full(100).";
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {

                message.MessageDescription = ($"An error campe up while the high priority, count than 100 for same duedate checking. {e.InnerException} ");
                return false;
            }
        }

        private async Task<int> getHighPriorityUnfinishedCount(TaskTable task, ReturnMessage message)
        {
            try
            {
                //get priorities List
                var priorityList = dbContext.TaskPriorities.ToList();
                var HighPriorityId = priorityList.Where(x => x.Ptype == appSettings.Value.PriorityConcern).Select(x => x.Pid).FirstOrDefault();

                //get statuses List
                var statusList = dbContext.TaskStatuses.ToList();
                var FinishedStatusId = statusList.Where(x => x.Stype.Trim() == appSettings.Value.StatusConcern.Trim()).Select(x => x.Sid).FirstOrDefault();

                //get task count
                var tasks = dbContext.TaskTables.ToList();

                if (tasks != null)
                {
                    //check if the same due date, high priority and not finished status on a task
                    var count = tasks.Where(x => x.TdueDate.Value.ToShortDateString() == task.TdueDate.Value.ToShortDateString()).Where(x => x.TpriorityId == HighPriorityId).Where(x => x.TstatusId != FinishedStatusId).Count();
                    return count;
                }
                else return 0;
            }
            catch (Exception e)
            {

                message.MessageDescription = ($"An error campe up while the get data from DB for high priority count than 100 for same duedate checking. {e.InnerException} ");
                return 0;
            }
            return 0;
        }

        private async Task<bool> CheckDueDate(TaskTable taskModel, ReturnMessage message)
        {
            try
            {
                //check for the duedate
                if (taskModel.TdueDate == null)
                {
                    //duedate cannot be in the pst, but I guess it can be saved empty
                    return true;
                }
                else
                {
                    try//if the datetime conversion went wrong I'm just saving the task as empty and prompting a message
                    {
                        var duedate = taskModel.TdueDate;
                        if (duedate < DateTime.Now)
                        {
                            message.MessageDescription = "The duedate is in past";
                            message.MessageType = "Error";

                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {

                        message.MessageDescription = ($"The datetime conversion went with an error. Task was saved with an empty duedate /n {e.InnerException} ");
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                message.MessageDescription = ($"Failed|{ ex.Message}");
                message.MessageType = "Error";

                return false;
            }
        }
    }
}
