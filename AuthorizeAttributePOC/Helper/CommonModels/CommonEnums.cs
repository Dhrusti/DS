using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.CommonModels
{
    public class CommonEnums
    {
        public enum UserType
        {
            SuperAdmin = 1,
            Admin,
            Tutor,
            Student
        }
        public enum RequestTypeStatus
        {
            Pending = 1,
            Viewed,
            Approved,
            Rejected,
            Responded,
            Completed

        }
        public enum UserStatus
        {
            Active = 1,
            InActive,
            Suspend
        }
    }
}
