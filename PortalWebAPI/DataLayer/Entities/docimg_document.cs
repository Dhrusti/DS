using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class docimg_document
    {
        public long ipkdoc_id { get; set; }
        public string sRef_number { get; set; }
        public string sDoc_description { get; set; }
        public string sDoc_type { get; set; }
        public string sFilename { get; set; }
        public int? ifkstorage_id { get; set; }
        public short? iThumbnail_available { get; set; }
        public short? iPdf_available { get; set; }
        public DateTime? dDate_entered { get; set; }
        public string sFile_ext { get; set; }
        public string sInternal_filename { get; set; }
        public short? iProcessed { get; set; }
        public int? ifkAccountID { get; set; }

        public virtual docimg_storage ifkstorage { get; set; }
    }
}
