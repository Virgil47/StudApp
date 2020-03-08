using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServiceLayer.Abstract;
using ServiceLayer.Models;
using StudApp.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace StudAppTest.ControllersTests
{
    public class GroupsControllerTests
    {
        [TestFixture]
        public class GroupControllerTests
        {
            private GroupController controllerTest;
            private Mock<IGroupManager> managerTest;
            [SetUp]
            public void SetUp()
            {
                managerTest = new Mock<IGroupManager>();
                controllerTest = new GroupController(managerTest.Object);
            }
            [Test]
            public void GetAllGroups_EmptyRequest_ShouldReturnModel()
            {
                // Arrange 
                var groupIdFirst = 1;
                var groupIdSecond = 2;
                managerTest.Setup(m => m.GetAllGroups(It.IsAny<GroupRequest>())).Returns(new List<GroupGetResponse> {
                new GroupGetResponse { Id = groupIdFirst },
                new GroupGetResponse { Id = groupIdSecond }});

                // Act 
                var result = controllerTest.GetGroups(new GroupRequest { });

                // Assert 
                Assert.IsNotNull(result);
                var okResult = result as OkObjectResult;
                Assert.AreEqual(200, okResult.StatusCode);
                var groups = okResult.Value as List<GroupGetResponse>;
                Assert.AreEqual(groups.Count, 2);
                Assert.IsTrue(groups.Any(x => x.Id == groupIdFirst));
                Assert.IsTrue(groups.Any(x => x.Id == groupIdSecond));
            }
            [Test]
            public void GetGroup_idRequest_ShouldReturnModel()
            {
                // Arrange 
                var group = new GroupGetResponse
                { GroupName = "FirstGroup", StudentsCount = 5 };
                managerTest.Setup(m => m.GetGroup(It.IsAny<int>())).Returns(group);

                // Act 
                var result = controllerTest.GetGroup(1);

                // Assert 
                Assert.IsNotNull(result);
                var okResult = result as OkObjectResult;
                var groupResult = okResult.Value as GroupGetResponse;
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreEqual(groupResult, group);
            }
            [Test]
            public void CreateGroup_AddNewGroup_ShouldReturnOk()
            {
                // Arrange 
                var groupName = "FirstGroup";
                managerTest.Setup(m => m.CreateGroup(It.IsAny<string>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

                // Act 
                var result = controllerTest.CreateGroup(groupName);

                // Assert 
                Assert.IsNotNull(result);
                managerTest.Verify(m => m.CreateGroup(It.Is<string>(x => x == groupName)), Times.Once);
                var okResult = result as OkObjectResult;
                var groupResult = okResult.Value as GroupGetResponse;
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreEqual(okResult.Value, "Ok");
            }
            [Test]
            public void CreateGroup_AddNewGroup_ShouldReturnError()
            {
                // Arrange 
                var groupName = "FirstGroup";
                managerTest.Setup(m => m.CreateGroup(It.IsAny<string>())).Returns(new ResponseResult { Message = "Error", IsSuccess = false });

                // Act 
                var result = controllerTest.CreateGroup(groupName);

                // Assert 
                Assert.IsNotNull(result);
                managerTest.Verify(m => m.CreateGroup(It.Is<string>(x =>x == groupName)), Times.Once);
                var badRequest = result as BadRequestObjectResult;
                Assert.AreEqual(badRequest.Value, "Error");
            }
            [Test]
            public void UpdateGroup_UpdateExistGroup_ShouldReturnOk()
            {
                // Arrange 
                var group = new GroupUpdateRequest
                { Id = 1, Name = "FirstGroup" };
                managerTest.Setup(m => m.UpdateGroup(It.IsAny<GroupUpdateRequest>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

                // Act 
                var result = controllerTest.UpdateGroup(group);

                // Assert 
                Assert.IsNotNull(result);
                managerTest.Verify(m => m.UpdateGroup(It.IsAny<GroupUpdateRequest>()), Times.Once);
                var okResult = result as OkObjectResult;
                var groupResult = okResult.Value as GroupGetResponse;
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreEqual(okResult.Value, "Ok");
            }
            [Test]
            public void DeleteGroup_DeleteExistGroup_ShouldReturnOk()
            {
                // Arrange 
                var groupId = 1;
                managerTest.Setup(m => m.DeleteGroup(It.IsAny<int>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

                // Act 
                var result = controllerTest.DeleteGroup(groupId);

                // Assert 
                Assert.IsNotNull(result);
                managerTest.Verify(m => m.DeleteGroup(It.Is<int>(x => x == groupId)), Times.Once);
                var okResult = result as OkObjectResult;
                var groupResult = okResult.Value as GroupGetResponse;
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreEqual(okResult.Value, "Ok");
            }
            [Test]
            public void AddStudentToGroup_AddLink_ShouldReturnOk()
            {
                // Arrange 
                var groupId = 1;
                var studentId = 2;
                managerTest.Setup(m => m.AddStudentToGroup(It.IsAny<int>(), It.IsAny<int>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

                // Act 
                var result = controllerTest.AddStudentToGroup(studentId, groupId);

                // Assert 
                Assert.IsNotNull(result);
                managerTest.Verify(m => m.AddStudentToGroup(It.Is<int>(x => x == studentId), It.Is<int>(x => x == groupId)), Times.Once);
                var okResult = result as OkObjectResult;
                var groupResult = okResult.Value as GroupGetResponse;
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreEqual(okResult.Value, "Ok");
            }
            [Test]
            public void DeleteStudentFromGroup_DeleteLink_ShouldReturnOk()
            {
                // Arrange 
                var groupId = 1;
                var studentId = 2;
                managerTest.Setup(m => m.DeleteStudentFromGroup(It.IsAny<int>(), It.IsAny<int>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

                // Act 
                var result = controllerTest.DeleteStudentFromGroup(studentId, groupId);

                // Assert 
                Assert.IsNotNull(result);
                managerTest.Verify(m => m.DeleteStudentFromGroup(It.Is<int>(x => x == studentId), It.Is<int>(x => x == groupId)), Times.Once);
                var okResult = result as OkObjectResult;
                var groupResult = okResult.Value as GroupGetResponse;
                Assert.AreEqual(200, okResult.StatusCode);
                Assert.AreEqual(okResult.Value, "Ok");
            }
        }
    }
}
