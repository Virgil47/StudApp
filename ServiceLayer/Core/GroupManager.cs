using Microsoft.EntityFrameworkCore;
using ServiceLayer.Abstract;
using ServiceLayer.Models;
using StudApp.Models;
using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Core
{
    public class GroupManager : IGroupManager
    {
        private readonly StudentContext _ctx;
        public GroupManager()
        {
            _ctx = new StudentContext();
        }
        public List<GroupGetResponse> GetAllGroups(GroupRequest request)
        {
            var groups = _ctx.Groups.Select(x => new
            {
                x.Id,
                GroupName = x.Name,
                StudentsCount = x.StudentGroup.Select(f => f.Student.Id).Count()
            });
            if (!string.IsNullOrEmpty(request.FiltredValue))
            {
                if (request.FiltredBy == "FirstName")
                {
                    groups = groups.Where(w => w.GroupName.Contains(request.FiltredValue));
                }
            }
            if (request.Skip == 0 & request.Take == 0)
            {
                return groups.Select(s => new GroupGetResponse
                {
                    Id = s.Id,
                    GroupName = s.GroupName,
                    StudentsCount = s.StudentsCount
                })
                .ToList();
            }
            else
            {
                return groups.Select(s => new GroupGetResponse
                {
                    Id = s.Id,
                    GroupName = s.GroupName,
                    StudentsCount = s.StudentsCount
                }).Skip(request.Skip)
                .Take(request.Take)
                .ToList();
            }
        }
        public GroupGetResponse GetGroup(int id)
        {
            var group = _ctx.Groups.Select(x => new
            {
                x.Id,
                x.Name,
                StudentCount = x.StudentGroup.Select(f => f.Student.Id).Count()
            }).FirstOrDefault(f => f.Id == id);
            if (group == null)
            {
                return null;
            }
            return new GroupGetResponse
            {
                Id = group.Id,
                GroupName = group.Name,
                StudentsCount = group.StudentCount
            };
        }
        public ResponseResult CreateGroup(string name)
        {
            try
            {
                var findedGroup = _ctx.Groups.FirstOrDefault(f =>f.Name == name);
                if (findedGroup != null)
                {
                    return new ResponseResult { IsSuccess = false, Message = "Group already exist" };
                }
                _ctx.Add(new Group
                {
                    Name = name
                });
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception ex)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }

        public ResponseResult UpdateGroup(GroupUpdateRequest group)
        {
            try
            {
                var findedGroup = _ctx.Groups.Find(group.Id);
                if (findedGroup == null)
                {
                    return new ResponseResult { IsSuccess = false, Message = "Group not found" };
                }
                findedGroup.Name = group.Name;
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
        public ResponseResult DeleteGroup(int id)
        {
            try
            {
                var findedGroup = _ctx.Groups.Find(id);
                if (findedGroup == null)
                {
                    return new ResponseResult { IsSuccess = false, Message = "Group not found" };
                }
                _ctx.Groups.Remove(findedGroup);
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
        public ResponseResult AddStudentToGroup(int studentId, int groupId)
        {
            try
            {
                var findedStudent = _ctx.Students.Include(x =>x.StudentGroup).FirstOrDefault(f =>f.Id == studentId);
                if (findedStudent.StudentGroup.Any(x =>x.GroupId == groupId))
                {
                    return new ResponseResult { IsSuccess = false, Message = "Student already is in group"};
                }
                findedStudent.StudentGroup.Add(new StudentGroup { StudentId = studentId, GroupId = groupId});
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception ex)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
        public ResponseResult DeleteStudentFromGroup(int studentId, int groupId)
        {
            try
            {
                var findedStudent = _ctx.Students.Find(studentId);
                var studentGroup = findedStudent.StudentGroup.FirstOrDefault(f => f.GroupId == groupId);
                findedStudent.StudentGroup.Remove(studentGroup);
                _ctx.SaveChanges();
                return new ResponseResult { IsSuccess = true, Message = "Ok" };
            }
            catch (Exception)
            {
                return new ResponseResult { IsSuccess = false, Message = "Error on server" };
            }
        }
    }
}
