using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServiceLayer.Abstract;
using ServiceLayer.Models;
using StudentApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace StudAppTest.ManagerTests
{
    [TestFixture]
    public class StudentControllerTests
    {
        private StudentController controllerTest;
        private Mock<IStudentManager> managerTest;
        [SetUp]
        public void SetUp()
        {
            managerTest = new Mock<IStudentManager>();
            controllerTest = new StudentController(managerTest.Object);
        }
        [Test]
        public void GetAllStudents_EmptyRequest_ShouldReturnModel()
        {
            // Arrange 
            var studentIdFirst = 1;
            var studentIdSecond = 2;
            managerTest.Setup(m => m.GetAllStudents(It.IsAny<StudentRequest>())).Returns(new List<StudentGetResponse> { 
                new StudentGetResponse { Id = studentIdFirst },
                new StudentGetResponse { Id = studentIdSecond }});

            // Act 
            var result = controllerTest.GetStudents(new StudentRequest { });

            // Assert 
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            var students = okResult.Value as List<StudentGetResponse>;
            Assert.AreEqual(students.Count, 2);
            Assert.IsTrue(students.Any(x => x.Id == studentIdFirst));
            Assert.IsTrue(students.Any(x => x.Id == studentIdSecond));
        }
        [Test]
        public void GetStudent_idRequest_ShouldReturnModel()
        {
            // Arrange 
            var student = new StudentGetResponse
            { FirstName = "Egor", LastName = "Starchikov", GenderName = "male", Identifier = "47", Groups = "FirstGroup" };
            managerTest.Setup(m => m.GetStudent(It.IsAny<int>())).Returns(student);

            // Act 
            var result = controllerTest.GetStudent(1);

            // Assert 
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            var studentResult = okResult.Value as StudentGetResponse;
            Assert.AreEqual(studentResult, student);
        }
        [Test]
        public void CreateStudent_AddNewStudent_ShouldReturnOk()
        {
            // Arrange 
            var student = new StudentCreateRequest
            { FirstName = "Egor", LastName = "Starchikov", GenderId = 1, Identifier = "47" };
            managerTest.Setup(m => m.CreateStudent(It.IsAny<StudentCreateRequest>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true});

            // Act 
            var result = controllerTest.CreateStudent(student);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.CreateStudent(It.IsAny<StudentCreateRequest>()), Times.Once);
            var okResult = result as OkObjectResult;
            var groupResult = okResult.Value as GroupGetResponse;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }
        [Test]
        public void CreateStudent_AddNewStudent_ShouldReturnError()
        {
            // Arrange 
            var student = new StudentCreateRequest
            { FirstName = "Egor", LastName = "Starchikov", GenderId = 1, Identifier = "47" };
            managerTest.Setup(m => m.CreateStudent(It.IsAny<StudentCreateRequest>())).Returns(new ResponseResult { Message = "Error", IsSuccess = false });

            // Act 
            var result = controllerTest.CreateStudent(student);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.CreateStudent(It.IsAny<StudentCreateRequest>()), Times.Once);
            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest.Value, "Error");
        }
        [Test]
        public void UpdateStudent_UpdateExistStudent_ShouldReturnOk()
        {
            // Arrange 
            var student = new StudentUpdateRequest
            { FirstName = "Egor", LastName = "Starchikov", GenderId = 1, Identifier = "47" };
            managerTest.Setup(m => m.UpdateStudent(It.IsAny<StudentUpdateRequest>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

            // Act 
            var result = controllerTest.UpdateStudent(student);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.UpdateStudent(It.IsAny<StudentUpdateRequest>()), Times.Once);
            var okResult = result as OkObjectResult;
            var groupResult = okResult.Value as GroupGetResponse;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }
        [Test]
        public void DeleteStudent_DeleteExistStudent_ShouldReturnOk()
        {
            // Arrange 
            var studentId = 1;
            managerTest.Setup(m => m.DeleteStudent(It.IsAny<int>())).Returns(new ResponseResult { Message = "Ok", IsSuccess = true });

            // Act 
            var result = controllerTest.DeleteStudent(studentId);

            // Assert 
            Assert.IsNotNull(result);
            managerTest.Verify(m => m.DeleteStudent(It.Is<int>(x =>x == studentId)), Times.Once);
            var okResult = result as OkObjectResult;
            var groupResult = okResult.Value as GroupGetResponse;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(okResult.Value, "Ok");
        }
    }
}
