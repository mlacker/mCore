using System;
using System.Linq;
using AutoMapper;
using mCore.Application;
using mCore.Application.ViewModels;
using mCore.Exceptions;
using mCore.Services.Process.Application.Runtime.ViewModels;
using mCore.Services.Process.Core.Definition;
using mCore.Services.Process.Core.Engine;
using mCore.Services.Process.Core.Runtime;

namespace mCore.Services.Process.Application.Runtime
{
    public class RuntimeAppService : ApplicationService
    {
        private readonly RuntimeService runtimeService;
        private readonly TaskService taskService;
        private readonly IProcessRepository processRepository;
        private readonly IProcessInstanceRepository processInstanceRepository;
        private readonly ITaskRepository taskRepository;

        public RuntimeAppService(
            RuntimeService runtimeService,
            TaskService taskService,
            IProcessRepository processRepository,
            IProcessInstanceRepository processInstanceRepository,
            ITaskRepository taskRepository)
        {
            this.runtimeService = runtimeService;
            this.taskService = taskService;
            this.processRepository = processRepository;
            this.processInstanceRepository = processInstanceRepository;
            this.taskRepository = taskRepository;
        }

        /// <summary>
        /// 待办任务
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="currentUserId">当前用户</param>
        /// <returns></returns>
        public PagedListViewModel<DoingTaskViewModel> GetDoingTasks(PagedFilterViewModel filter, Guid currentUserId)
        {
            var tasks = taskRepository.GetAll(m => m.Status == ActivityStatusEnum.Running
                    && m.AssigneeId == currentUserId);

            var models = Mapper.Map<PagedListViewModel<DoingTaskViewModel>>(tasks);

            return models;
        }

        /// <summary>
        /// 已办任务
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="currentUserId">当前用户</param>
        /// <returns></returns>
        public PagedListViewModel<DoingTaskViewModel> GetDoneTasks(PagedFilterViewModel filter, Guid currentUserId)
        {
            var tasks = taskRepository.GetAll(m => m.Status == ActivityStatusEnum.Completed
                    && m.AssigneeId == currentUserId);

            var models = Mapper.Map<PagedListViewModel<DoingTaskViewModel>>(tasks);

            return models;
        }

        /// <summary>
        /// 获取已发流程实例
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public PagedListViewModel<ProcessInstanceViewModel> GetStartedInstances(PagedFilterViewModel filter, Guid currentUserId)
        {
            filter["currentUserId"] = currentUserId.ToString();

            return GetInstances(filter);
        }

        /// <summary>
        /// 获取所有流程实例
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns></returns>
        public PagedListViewModel<ProcessInstanceViewModel> GetInstances(PagedFilterViewModel filter)
        {
            var instances = processInstanceRepository.GetAll()
                .Skip(filter.Start).Take(filter.Size);

            return Mapper.Map<PagedListViewModel<ProcessInstanceViewModel>>(instances);
        }

        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="processDefinitionId">流程定义标识</param>
        /// <param name="currentUserId">当前用户</param>
        /// <returns></returns>
        public Guid StartProcessInstance(Guid processDefinitionId, Guid currentUserId)
        {
            var processDefinition = processRepository.Get(processDefinitionId)
                ?? throw new ArgumentAppException($"No process definition found for id = '{processDefinitionId}'.", nameof(processDefinitionId));

            var processInstance = runtimeService.StartProcessInstance(processDefinition, currentUserId);

            processInstanceRepository.Insert(processInstance);
            UnitOfWork.Commit();

            // 获取当前任务标识.
            throw new NotImplementedException();
        }

        public void CompleteTask(Guid taskId, Guid actionId, Guid currentUserId, dynamic data)
        {
            var currentTask = taskRepository.Get(taskId)
                ?? throw new ArgumentAppException($"No task instance found for id = '{taskId}'", nameof(taskId));

            // Save data

            taskService.Complete(currentTask, currentUserId);

            var nextTasks = taskRepository.GetAll(m => m.ProcessInstance.Id == currentTask.ProcessInstance.Id
                && m.Status == ActivityStatusEnum.Running);

            // Set Assignee

            // Task Complate Event, set EndTime, Comment
            // End Event, set EndTime, EndActivity

            taskRepository.Update(currentTask);
            foreach (var nextTask in nextTasks)
            {
                taskRepository.Update(nextTask);
            }
            UnitOfWork.Commit();
        }

        public void Revoke(Guid taskId, Guid currentUserId)
        {
            var currentTask = taskRepository.Get(taskId);

            var tasks = taskRepository.GetAll(m => m.ProcessInstance.Id == currentTask.ProcessInstance.Id);

            throw new NotImplementedException();
        }

        public void Terminate(Guid instanceId)
        {
            throw new NotImplementedException();
        }
    }
}
