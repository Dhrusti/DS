using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class docimg_storage
    {
        public docimg_storage()
        {
            docimg_documents = new HashSet<docimg_document>();
        }

        public int ipkstorage_id { get; set; }
        public string sStorage_name { get; set; }
        public string sStorage_type { get; set; }
        public string sStorage_location { get; set; }
        public short? iEnabled { get; set; }
        public string sStorage_server_name { get; set; }
        public string sStorage_server_username { get; set; }
        public string sStorage_server_password { get; set; }
        public string sStorage_sub_type { get; set; }
        public DateTime? dDate_entered { get; set; }
        public string sLinuxLocationPath { get; set; }

        public virtual ICollection<docimg_document> docimg_documents { get; set; }
    }
}
