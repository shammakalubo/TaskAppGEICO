using Moq;
using NUnit.Framework;
using System;
using TaskAppGEICO.Controllers;
using TaskAppGEICO.Services;
using TaskAppGEICO.Models;

namespace TaskAppTest
{
    [TestFixture]
    public class TaskAppTest
    {
        [Test]
        public void TestAddingTask()
        {
            // ARRANGE

            var addNewTask = new TaskTable
            {
                Description = "Adding a New Task",
                TdueDate = DateTime.Now,
                Tname = "New Task 05 05 22 10:15",
                TpriorityId = 1,
                TstatusId = 1

            };

            var dependency = Mock.Of<ITaskServices>();
            var sut = new TaskController(dependency);

            // ACT
            sut.AddTask(addNewTask);

            // ASSERT
            //Mock.Get(dependency).Verify(p => p.GetTasks(returnMessege), Times.Once());
            //this should fail
            //Assert.Catch<ArgumentException>(() => sut.GetTasks());

        }

        [Test]
        public void TestEditingTask()
        {
            // ARRANGE

            var addNewTask = new TaskTable
            {
                Description = "Adding a New Task",
  
                Tname = "New Task 05 05 22 10:15",
                TpriorityId = 1,
                TstatusId = 1,
                Tid=6

            };

            var dependency = Mock.Of<ITaskServices>();
            var sut = new TaskController(dependency);

            // ACT
            sut.UpdateTask(addNewTask);

            // ASSERT
            //Mock.Get(dependency).Verify(p => p.GetTasks(returnMessege), Times.Once());
            //this should fail
            //Assert.Catch<ArgumentException>(() => sut.GetTasks());

        }

        [Test]
        public void TestGetingTask()
        {
            // ARRANGE
            var returnMessege = new ReturnMessage();
            var dependency = Mock.Of<ITaskServices>();
            var sut = new TaskController(dependency);

            // ACT
            sut.GetTasks();

            // ASSERT
            //Mock.Get(dependency).Verify(p => p.GetTasks(returnMessege), Times.Once());
            //this should fail
            //Assert.Catch<ArgumentException>(() => sut.GetTasks());
            Assert.Throws<ArgumentException>(() => sut.GetTasks());

        }



    }
}
