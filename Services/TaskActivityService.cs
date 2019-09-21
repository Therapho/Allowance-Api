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
        private TaskDayService _taskDayService;
        private TaskDefinitionService _taskDefinitonService;

        public TaskActivityService(DatabaseContext context, 
            TaskDayService taskDayService, 
            TaskDefinitionService  taskDefinitonService) : base(context)
        {
            _taskDayService = taskDayService;
            _taskDefinitonService = taskDefinitonService;
        }

        public async Task<List<TaskActivity>> GetList(Guid userIdentifier, int taskWeekId)
        {
          

            List<TaskActivity> result = null;
            try
            {
                var query = from TaskActivity in _context.TaskActivitySet
                            where TaskActivity.UserIdentifier == userIdentifier && TaskActivity.TaskWeekId == taskWeekId
                            orderby TaskActivity.Sequence
                            select TaskActivity;
                result = await query.ToListAsync();
            }
            catch (Exception exception)
            {
                throw new DataException(
                    $"Error trying to retrieve a list of TaskActivitys with userIdentifier: {userIdentifier}, taskWeekId: {taskWeekId}.  {exception.Message}", 
                    exception);
            }
            return result;
        }

        public async Task<List<TaskActivity>> CreateList(List<TaskDay> taskDayList, List<TaskDefinition> taskDefinitionList)
        {

            var taskActivityList = new List<TaskActivity>();
            int day = 1;
            foreach (var taskDay in taskDayList)
            {
                foreach (var taskDefinition in taskDefinitionList)
                {
                    var taskActivity = new TaskActivity()
                    {
                        UserIdentifier = taskDay.UserIdentifier,
                        TaskDayId = taskDay.Id.Value,
                        TaskWeekId = taskDay.TaskWeekId,
                        TaskGroupId = taskDefinition.TaskGroupId,
                        Sequence = taskDefinition.Sequence,
                        StatusId = (int)Constants.ActivityStatus.Incomplete,
                        TaskDefinitionId = taskDefinition.Id.Value,
                        DaySequence = day
                        
                    };
                    taskActivityList.Add(taskActivity);
                }
                day++;
            }

            await CreateList(taskActivityList);


            return taskActivityList;
        }

        public async Task<List<TaskActivity>> GetOrCreate(TaskWeek taskWeek)
        {
            var taskActivityList = await GetList(taskWeek.UserIdentifier, taskWeek.Id.Value);

            if (taskActivityList == null) taskActivityList = new List<TaskActivity>();

            if (taskActivityList.Count() == 0)
            {

                var taskDayList = await _taskDayService.GetOrCreateList(taskWeek);
                var taskDefinitionList = await _taskDefinitonService.GetList();
                taskActivityList = await CreateList(taskDayList, taskDefinitionList);
            }

            return taskActivityList;
        }
    }
}
