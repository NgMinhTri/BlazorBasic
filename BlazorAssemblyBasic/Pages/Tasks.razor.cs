using BlazorAssemblyBasic.Components;
using BlazorAssemblyBasic.Services;
using Blazorbasic.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAssemblyBasic.Pages
{
    public partial class Tasks
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }
        protected Confirmation DeleteConfirmation { set; get; }

        private Guid DeleteId { set; get; }

        private List<TaskDto> taskList;
        private TaskListSearch TaskListSearch = new TaskListSearch();

        protected AssignTask AssignTaskDialog { set; get; }

        protected override async Task OnInitializedAsync()
        {
            taskList = await TaskApiClient.GetTaskList(TaskListSearch);
        }


        public async Task SearchTask(TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            taskList = await TaskApiClient.GetTaskList(TaskListSearch);
        }


        public void OnDeleteTask(Guid deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }

        public async Task OnConfirmDeleteTask(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await TaskApiClient.DeleteTask(DeleteId);
                taskList = await TaskApiClient.GetTaskList(TaskListSearch);
            }
        }

        //Show khung danh sach User
        public void OpenAssignPopup(Guid id)
        {
            AssignTaskDialog.Show(id);
        }

        //Get lại danh sách sau khi gán assign user cho task
        public async Task AssignTaskSuccess(bool result)
        {
            if (result)
            {
                taskList = await TaskApiClient.GetTaskList(TaskListSearch);
            }
        }

    }
}
