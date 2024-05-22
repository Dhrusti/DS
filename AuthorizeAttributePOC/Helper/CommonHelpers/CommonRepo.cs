using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Helper.CommonHelpers
{
    public class CommonRepo
    {
        private readonly DBContext _dbContext;
        public CommonRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<UserTokenMst> UserTokenMstList()
        {
            return _dbContext.UserTokenMsts.AsQueryable();
        }
        public IQueryable<UserMst> UserMstList(bool? isActive = true, bool? isDelete = false)
        {
            return _dbContext.UserMsts.Where(x => (isActive != null ? x.IsActive == isActive : true) && x.IsDelete == isDelete).AsQueryable();
        }
        public IQueryable<UserStatusMst> UserStatusMstList(bool isActive = true, bool isDelete = false)
        {
            return _dbContext.UserStatusMsts.Where(x => x.IsActive == isActive && x.IsDelete == isDelete).AsQueryable();
        }
    }
}
