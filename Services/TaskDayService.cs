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
    public class TaskDayService : EntityService<TaskDay>
    {
        public TaskDayService(DatabaseContext context) : base(context) { }

        public async Task<List<TaskDay>> GetList(int accountId, int taskWeekId)
        {
            var query = from taskDay in _context.TaskDaySet
                        where taskDay.AccountId == accountId && taskDay.TaskWeekId == taskWeekId
                        select taskDay;

            List<TaskDay> result = null;
            try
            {
                result = await query.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new DataException(
                    $"Error trying to retrieve a list of TaskDays with accountId: {accountId}, taskWeekId: {taskWeekId}.  {exception.Message}", 
                    exception);
            }
            return result;
        }

        public async Task<List<TaskDay>> GetOrCreateList(int accountId, TaskWeek taskWeek)
        {
            var taskDayList = await GetList(accountId, taskWeek.Id.Value);

            if (taskDayList == null || taskDayList.Count() == 0)
            {
                taskDayList = new List<TaskDay>();

                for (int day = 0; day < 7; day++)
                {
                    DateTime date = taskWeek.WeekStartDate.AddDays(day);
                    var taskDay = new TaskDay()
                    {
                        AccountId = accountId,
                        TaskWeekId = taskWeek.Id.Value,
                        Date = date,
                        StatusId = (int)Constants.Status.Open
                    };
                    taskDayList.Add(taskDay);
                }

                await CreateOrUpdateList(taskDayList);
            }

            return taskDayList;
        }
    }
}
