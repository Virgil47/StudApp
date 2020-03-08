using ServiceLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.Abstract
{
    public interface IGroupManager
    {
        List<GroupGetResponse> GetAllGroups(GroupRequest response);
        GroupGetResponse GetGroup(int id);
        ResponseResult CreateGroup(string name);
        ResponseResult UpdateGroup(GroupUpdateRequest group);
        ResponseResult DeleteGroup(int id);
        ResponseResult AddStudentToGroup(int studentId, int groupId);
        ResponseResult DeleteStudentFromGroup(int studentId, int groupId);
    }
}
