using BlazorAssemblyBasic.Components;
using BlazorAssemblyBasic.Services;
using BlazorAssemblyBasic.Shared;
using Blazorbasic.Models;
using Blazorbasic.Models.SeedWork;
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

        public MetaData MetaData { get; set; } = new MetaData();

        [CascadingParameter]
        private Error Error { set; get; }

        protected override async Task OnInitializedAsync()
        {
            await GetTasks();
        }


        public async Task SearchTask(TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            await GetTasks();
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
                await GetTasks();
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
                await GetTasks();
            }
        }


        private async Task GetTasks()
        {
            try
            {
                var pagingResponse = await TaskApiClient.GetTaskList(TaskListSearch);
                taskList = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
            }
            catch(Exception ex)
            {
                Error.ProcessError(ex);
            }
            
        }

        private async Task SelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await GetTasks();
        }

    }
}
