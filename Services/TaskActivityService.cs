using AllowanceFunctions.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data;
using AllowanceFunctions.Common;

namespace AllowanceFunctions.Services
{
    public class TaskActivityService : EntityService<TaskActivity>
    {
        public TaskActivityService(DatabaseContext context) : base(context) { }

        public async Task<List<TaskActivity>> GetList(int accountId, int taskWeekId)
        {
            var query = from TaskActivity in _context.TaskActivitySet
                        where TaskActivity.AccountId == accountId && TaskActivity.TaskWeekId == taskWeekId
                        select TaskActivity;

            List<TaskActivity> result = null;
            try
            {
                result = await query.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new DataException(
                    $"Error trying to retrieve a list of TaskActivitys with accountId: {accountId}, taskWeekId: {taskWeekId}.  {exception.Message}", 
                    exception);
            }
            return result;
        }

        public async Task<List<TaskActivity>> CreateList(int accountId, List<TaskDay> taskDayList, List<TaskDefinition> taskDefinitionList)
        {

            var taskActivityList = new List<TaskActivity>();

            foreach (var taskDay in taskDayList)
            {
                foreach (var taskDefinition in taskDefinitionList)
                {
                    var taskActivity = new TaskActivity()
                    {
                        AccountId = accountId,
                        TaskDayId = taskDay.Id.Value,
                        TaskWeekId = taskDay.TaskWeekId,
                        TaskGroupId = taskDefinition.TaskGroupId,
                        Description = taskDefinition.Description,
                        Sequence = taskDefinition.Sequence,
                        Value = taskDefinition.Value,
                        
                    };
                    taskActivityList.Add(taskActivity);
                }
            }

            await CreateOrUpdateList(taskActivityList);


            return taskActivityList;
        }
    }
}
