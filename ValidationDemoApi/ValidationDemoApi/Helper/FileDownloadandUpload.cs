using ValidationDemoApi.Entities;
using ValidationDemoApi.Models;
using System.IO;

namespace ValidationDemoApi.Helper
{
    public class FileDownloadandUpload
    {
        private readonly JWTokenDBContext _db;

        public FileDownloadandUpload(JWTokenDBContext db)
        {

            _db = db;
        }
        public Response UploadFile(string DocumentType, IFormFile UploadDocument)
        {
            Response response = new Response();
            try
            {
                TblUserDocumentMst tbl = new TblUserDocumentMst();
                if (UploadDocument.Length >= 0)
                {
                    var fileExt = Path.GetExtension(UploadDocument.FileName).ToLower();
                    Guid guid = Guid.NewGuid();
                    var fileExtname = Path.GetFileName(UploadDocument.FileName);
                    var fileName = fileExtname; //Create a new Name for the file due to security reasons.

                    var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder/Files");

                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    //var fileName = Path.GetFileName(file.FileName);
                    //var fileExt=Path.GetExtension(file.FileName).ToLower();
                    //if (fileExt != ".pdf")
                    //{
                    //    response.Status = false;
                    //    response.Code = 415;
                    //    response.Message = "File does not support,Plz upload in PDF format..";
                    //}
                    //else
                    //{
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder/Files", fileName);
                    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder/images", fileName);




                    using (var fileSrteam = new FileStream(path, FileMode.Create))
                    {
                        UploadDocument.CopyTo(fileSrteam);
                    }
                    //return true;
                    //var result = "Add";
                    if (UploadDocument != null)
                    {
                        response.Data = UploadDocument;
                        response.Message = "Document uploaded Succesfully";
                        response.Code = 200;
                        response.Status = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }



        public Response DownloadFile(string filename)
        {
            Response response = new Response();
            if (filename == null)
            {
                response.Message = "filename not present";
            }

            // var path = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", filename);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder\\Files\\" + filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
           // memory.Position = 0;

            //return File(memory, GetContentType(path), Path.GetFileName(path));
            byte[] data = memory.ToArray();

            response.Data = (data, Path.GetFileName(path));
            return response;
        }

    }
}
