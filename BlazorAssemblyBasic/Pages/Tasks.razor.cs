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
        protected override async Task OnInitializedAsync()
        {
            taskList = await TaskApiClient.GetTaskList();
        }

    }
}
