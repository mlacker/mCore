using System;
using System.Linq;
using AutoMapper;
using mCore.Application;
using mCore.Domain.Entities;
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
        public PaginatedItems<DoingTaskViewModel> GetDoingTasks(PaginatedFilter filter, Guid currentUserId)
        {
            var tasks = taskRepository.GetAll(m => m.Status == ActivityStatusEnum.Running
                    && m.AssigneeId == currentUserId);

            var models = Mapper.Map<PaginatedItems<DoingTaskViewModel>>(tasks);

            return models;
        }

        /// <summary>
        /// 已办任务
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="currentUserId">当前用户</param>
        /// <returns></returns>
        public PaginatedItems<DoingTaskViewModel> GetDoneTasks(PaginatedFilter filter, Guid currentUserId)
        {
            var tasks = taskRepository.GetAll(m => m.Status == ActivityStatusEnum.Completed
                    && m.AssigneeId == currentUserId);

            var models = Mapper.Map<PaginatedItems<DoingTaskViewModel>>(tasks);

            return models;
        }

        /// <summary>
        /// 获取已发流程实例
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public PaginatedItems<ProcessInstanceViewModel> GetStartedInstances(PaginatedFilter filter, Guid currentUserId)
        {
            filter["currentUserId"] = currentUserId.ToString();

            return GetInstances(filter);
        }

        /// <summary>
        /// 获取所有流程实例
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns></returns>
        public PaginatedItems<ProcessInstanceViewModel> GetInstances(PaginatedFilter filter)
        {
            var instances = processInstanceRepository.GetAll()
                .Skip(filter.Start).Take(filter.Size);

            return Mapper.Map<PaginatedItems<ProcessInstanceViewModel>>(instances);
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

        /// <summary>
        /// 终止流程实例
        /// </summary>
        /// <param name="instanceId">流程实例标识</param>
        public void TerminateProcessInstance(Guid instanceId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="actionId">执行标识</param>
        /// <param name="currentUserId">当前用户</param>
        /// <param name="data">业务数据</param>
        public void CompleteTask(Guid taskId, Guid actionId, Guid currentUserId, dynamic data)
        {
            var currentTask = taskRepository.Get(taskId)
                ?? throw new ArgumentAppException($"No task instance found for id = '{taskId}'", nameof(taskId));

            var processDefinition = processRepository.Get(currentTask.ProcessInstance.ProcessDefinitionId);

            // Save data

            taskService.Complete(currentTask, processDefinition, currentUserId);

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

        /// <summary>
        /// 撤回任务
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="currentUserId">当前用户</param>
        public void RevokeTask(Guid taskId, Guid currentUserId)
        {
            var currentTask = taskRepository.Get(taskId);

            var tasks = taskRepository.GetAll(m => m.ProcessInstance.Id == currentTask.ProcessInstance.Id);

            throw new NotImplementedException();
        }

        public void SaveData(Guid processInstanceId, dynamic data)
        {
            throw new NotImplementedException();
        }

        public void GetProcessInstanceHistory(Guid processInstanceId)
        {
            throw new NotImplementedException();
        }
    }
}
