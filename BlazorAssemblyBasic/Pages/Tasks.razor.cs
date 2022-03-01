using BlazorAssemblyBasic.Services;
using Blazorbasic.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Pages
{
    public partial class Tasks
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }

        private List<TaskDto> taskList;
        private TaskListSearch TaskListSearch = new TaskListSearch();

        protected override async Task OnInitializedAsync()
        {
            taskList = await TaskApiClient.GetTaskList(TaskListSearch);
        }


        public async Task SearchTask(TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            taskList = await TaskApiClient.GetTaskList(TaskListSearch);
        }

    }
}
