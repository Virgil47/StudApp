using ServiceLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.Abstract
{
    public interface IStudentManager
    {
        List<StudentGetResponse> GetAllStudents(StudentRequest request);
        ResponseResult CreateStudent(StudentCreateRequest student);
        ResponseResult UpdateStudent(StudentUpdateRequest student);
        ResponseResult DeleteStudent(int id);
        StudentGetResponse GetStudent(int id);
    }
}
