using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Models;
using MyProject.DAL;

namespace MyProject.BLL
{
    public class UserManager{
        UserService us = new UserService();
        public bool login(User user) {
            return us.login(user);
        }
    }
}
