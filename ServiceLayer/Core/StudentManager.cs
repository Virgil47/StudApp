using ServiceLayer.Abstract;
using ServiceLayer.Models;
using StudApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Core
{
    public class StudentManager : IStudentManager
    {
        private readonly StudentContext _ctx;
        public StudentManager()
        {
            _ctx = new StudentContext();
        }

        public List<StudentGetResponse> GetAllStudents(StudentRequest request)
        {
            var students = _ctx.Students.Select(x => new
            {
                x.Id,
                GenderName = x.Gender.Name,
                x.FirstName,
                x.LastName,
                x.Patronymic,
                x.Identifier,
                GroupsName = x.StudentGroup.Select(f => f.Group.Name)
            });
            if (!string.IsNullOrEmpty(request.FiltredValue))
            {
                if (request.FiltredBy == "FirstName")
                {
                    students = students.Where(w => w.FirstName.Contains(request.FiltredValue));
                }
                if (request.FiltredBy == "LastName")
                {
                    students = students.Where(w => w.LastName.Contains(request.FiltredValue));
                }
                if (request.FiltredBy == "Patronymic")
                {
                    students = students.Where(w => w.Patronymic.Contains(request.FiltredValue));
                }
                if (request.FiltredBy == "Identifier")
                {
                    students = students.Where(w => w.Identifier.Contains(request.FiltredValue));
                }
                if (request.FiltredBy == "Gender")
                {
                    students = students.Where(w => w.GenderName.Contains(request.FiltredValue));
                }
                if (request.FiltredBy == "Group")
                {
                    students = students.Where(a => a.GroupsName.Any(w => w.Contains(request.FiltredValue)));
                }
            }
            if (request.Skip == 0 & request.Take == 0)
            {
                return students.Select(s => new StudentGetResponse
                {
                    Id = s.Id,
                    GenderName = s.GenderName,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Patronymic = s.Patronymic,
                    Identifier = s.Identifier,
                    Groups = string.Join(",", s.GroupsName)
                }).ToList();
            }
            else
            {
                return students.Select(s => new StudentGetResponse
                {
                    Id = s.Id,
                    GenderName = s.GenderName,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Patronymic = s.Patronymic,
                    Identifier = s.Identifier,
                    Groups = string.Join(",", s.GroupsName)
                }).Skip(request.Skip)
            .Take(request.Take)
            .ToList();
            }
        }
        public StudentGetResponse GetStudent(int id)
        {
            var student = _ctx.Students.Select(x => new
            {
                x.Id,
                x.Gender.Name,
                x.FirstName,
                x.LastName,
                x.Patronymic,
                x.Identifier,
                GroupsName = x.StudentGroup.Select(f => f.Group.Name)
            }).FirstOrDefault(f => f.Id == id);
            if (student == null)
            {
                return null;
            }
            return new StudentGetResponse
            {
                Id = student.Id,
                GenderName = student.Name,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Patronymic = student.Patronymic,
                Identifier = student.Identifier,
                Groups = string.Join(", ", student.GroupsName)
            };
        }
        public ResponseResult CreateStudent(StudentCreateRequest student)
        {
            try
            {
                _ctx.Add(new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Patronymic = student.Patronymic,
                    GenderId = student.GenderId,
                    Identifier = student.Identifier,
                });
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception ex)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
        public ResponseResult UpdateStudent(StudentUpdateRequest student)
        {
            try
            {
                var findedStudent = _ctx.Students.Find(student.Id);
                if (findedStudent == null)
                {
                    return new ResponseResult { IsSuccess = false, Message = "Student not found" };
                }
                findedStudent.GenderId = student.GenderId;
                findedStudent.FirstName = student.FirstName;
                findedStudent.Patronymic = student.Patronymic;
                findedStudent.LastName = student.LastName;
                findedStudent.Identifier = student.Identifier;
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception ex)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
        public ResponseResult DeleteStudent(int id)
        {
            try
            {
                var findedStudent = _ctx.Students.Find(id);
                if (findedStudent == null)
                {
                    return new ResponseResult { IsSuccess = false, Message = "Student not found" };
                }
                _ctx.Students.Remove(findedStudent);
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception ex)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
    }
}
