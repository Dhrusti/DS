using Microsoft.Extensions.Configuration;

namespace Helper
{
    public class CommonConstant
    {
        #region SP_Modes

        public const int Read = 0;
        public const int Create = 1;
        public const int Update = 2;
        public const int Delete = 3;
        public const int ReadWithFilter = 4;

        #endregion

        #region LogType
        public const string Activity_log = "ActivityLog";
        public const string Exception_log = "ExceptionLog";
        #endregion

        #region File Extensions
        public const string Json = "json";
        public const string Jpeg = "jpeg";
        public const string xlsx = ".xlsx";
        #endregion

        #region UserRoles
        public const int Super_Admin = 1;
        public const int Admin = 2;
        public const int Data_Operator = 3;
        #endregion

        #region Permissions
        public const int Scheduling_Admin_Notification = 1;
        public const int Scheduling_DO_Notification = 2;
        public const int Scheduling_Appointment = 3;
        public const int Scheduling_Accept_Reject = 4;
        public const int Dashboard = 5;
        public const int Default_Permission_View = 6;

        public const int Aging_Organization_Add = 7;
        public const int Aging_Organization_Update = 8;
        public const int Aging_Organization_Delete = 9;
        public const int Aging_Organization_View = 10;

        public const int Aging_Company_Add = 11;
        public const int Aging_Company_Update = 12;
        public const int Aging_Company_Delete = 13;
        public const int Aging_Company_View = 14;

        public const int Aging_Department_Add = 15;
        public const int Aging_Department_Update = 16;
        public const int Aging_Department_Delete = 17;
        public const int Aging_Department_View = 18;

		public const int Aging_User_Add = 19;
		public const int Aging_User_Update = 20;
		public const int Aging_User_Delete = 21;
		public const int Aging_User_View = 22;

        public const int Default_Permission_Update = 23;
		#endregion
	}

}
