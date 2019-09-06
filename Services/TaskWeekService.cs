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
    public class TaskWeekService : EntityService<TaskWeek>
    {
        public TaskWeekService(DatabaseContext context) : base(context) { }

        public async Task<TaskWeek> Get(int accountId, DateTime dateStart)
        {
            TaskWeek result = null;

            try
            {
                var query = from taskWeek in _context.TaskWeekSet
                            where taskWeek.WeekStartDate == dateStart && taskWeek.AccountId == accountId
                            select taskWeek;
                result = await query.FirstOrDefaultAsync();
            }
            catch (Exception exception)
            {
                throw new DataException(
                    $"Error trying to retrieve a taskweek with accountId: {accountId}, dateStart: {dateStart}.  {exception.Message}", 
                    exception);
            }
            return result;
           
        }

        public async Task<TaskWeek> GetOrCreate(int accountId, DateTime dateStart)
        {
            var taskWeek = await Get(accountId, dateStart);

            if(taskWeek == null)
            {
                taskWeek = new TaskWeek()
                {
                    AccountId = accountId,
                    WeekStartDate = dateStart,
                    StatusId = (int)Constants.Status.Open
                };
                await CreateOrUpdate(taskWeek);
            }

            return taskWeek;
        }
    }
}
