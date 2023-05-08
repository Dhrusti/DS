using BussinessLayer;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class RoleImpl : IRole
    {
        private readonly RoleBLL _roleBLL;

        public RoleImpl(RoleBLL roleBLL)
        {
            _roleBLL = roleBLL;
        }

        public CommonResponse GetRoles()
        {
            return _roleBLL.GetRoles();
        }
    }
}
