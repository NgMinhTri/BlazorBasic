using BlazorAssemblyBasic.Services;
using Blazorbasic.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Pages
{
    public partial class Tasks
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }
        [Inject] private IUserApiClient UserApiClient { set; get; }
        [Inject] private IToastService ToastService { set; get; }

        private List<TaskDto> taskList;
        private List<AssignerDto> Assigners;
        private TaskListSearch TaskListSearch = new TaskListSearch();

        protected override async Task OnInitializedAsync()
        {
            taskList = await TaskApiClient.GetTaskList(TaskListSearch);
            Assigners = await UserApiClient.GetAssigners();
        }


        private async Task SearchForm(EditContext context)
        {
            ToastService.ShowInfo("Search completed", "Info");
            taskList = await TaskApiClient.GetTaskList(TaskListSearch);
        }

    }
}
