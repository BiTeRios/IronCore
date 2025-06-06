﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.BusinessLogic.BL;
using IronCore.Domain.Entities.User;

namespace IronCore.BusinessLogic.Interfaces
{
    public interface IUser
    {
        List<UserDTO> GetAllUsers();
        UserDTO GetById(int id);
        bool Update(UserDTO User);
        bool Delete(int id);
        bool ChangePassword(int userId, string oldPassword, string newPassword, out string message);

    }
}
