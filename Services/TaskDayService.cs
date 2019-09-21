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

        public async Task<List<TaskDay>> GetList(int taskWeekId)
        {
            var query = from taskDay in _context.TaskDaySet
                        where taskDay.TaskWeekId == taskWeekId
                        select taskDay;

            List<TaskDay> result = null;
            try
            {
                result = await query.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new DataException(
                    $"Error trying to retrieve a list of TaskDays with taskWeekId: {taskWeekId}.  {exception.Message}", 
                    exception);
            }
            return result;
        }

        public async Task<List<TaskDay>> GetOrCreateList(TaskWeek taskWeek)
        {
            var taskDayList = await GetList(taskWeek.Id.Value);

            if (taskDayList == null || taskDayList.Count() == 0)
            {
                taskDayList = new List<TaskDay>();

                for (int day = 0; day < 7; day++)
                {
                    DateTime date = taskWeek.WeekStartDate.AddDays(day);
                    var taskDay = new TaskDay()
                    {
                        UserIdentifier = taskWeek.UserIdentifier,
                        TaskWeekId = taskWeek.Id.Value,
                        Date = date,
                        StatusId = (int)Constants.Status.Open
                    };
                    taskDayList.Add(taskDay);
                }

                await CreateList(taskDayList);
            }

            return taskDayList;
        }

        public async Task<List<TaskDay>> GetByTaskWeek(int taskWeekId)
        {
            var query = from taskday in _context.TaskDaySet
                        where taskday.TaskWeekId == taskWeekId 
                        orderby taskday.Date
                        select taskday;

            return await query.ToListAsync();
            
        }
    }
}
