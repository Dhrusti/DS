using Grpc.Core;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Models;

namespace ValidationDemoApi.Helper
{
    public class ExportToExcelData
    {
        public readonly JWTokenDBContext _db;
        public ExportToExcelData(JWTokenDBContext db)
        {
            _db = db;
        }
        public Response GetStudentReport()
        {
            Response response = new Response();
            try
            {
                List<Student> res = new List<Student>();
                res = _db.Students.ToList();
                string reportname = $"Student.xlsx";
                if (res.Count > 0)
                {
                    var exportbytes = ExporttoExcel<Student>(res, reportname);
                    response.Data = exportbytes;
                }
                else
                {
                    response.Message = "No Data";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
                response.Code = 401;
            }

            return response;
        }
        private byte[] ExporttoExcel<T>(List<T> table, string filename)
        {
            ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return pack.GetAsByteArray();
        }



        public Response GetStudentpdf()
        {
            Response response = new Response();

            var res = _db.Students.ToList();

            if (res != null)
            {

                StringBuilder st = new StringBuilder();
                st.Append("<table width='100%' cellpadding='0' cellspacing='0'><tr><td width='25%' border='1' bgcolor='lightgoldenrodyellow'><span style='text-align:left;font-size:9.5px;padding-left:5px;'>ROLL_NO</span></td><td width='25%' border='1' bgcolor='lightgoldenrodyellow'><span style='text-align:left;font-size:9.5px;padding-left:5px;'>NAME</span></td><td width='25%' border='1' bgcolor='lightgoldenrodyellow'><span style='text-align:left;font-size:9.5px;padding-left:5px;'>ADDRESS</span></td><td width='25%' border='1' bgcolor='lightgoldenrodyellow'><span style='text-align:left;font-size:9.5px;padding-left:5px;'>PHONE</span></td><td width='25%' border='1' bgcolor='lightgoldenrodyellow'><span style='text-align:left;font-size:9.5px;padding-left:5px;'>AGE</span></td></tr>");

                foreach (var query in res)
                {
                    st.Append("<tr><td width='25%' border='1'><span style='text-align:left;font-size:9.5px;padding-left:5px;'> " + query.RollNo + "</span></td><td width='25%' border='1'><span style='text-align:right;font-size:9.5px;padding-right:5px;'>" + query.Name + "</span></td><td width='25%' border='1'><span style='text-align:left;font-size:9.5px;padding-left:5px;'>" + query.Address + "</span></td><td width='25%' border='1'><span style='text-align:right;font-size:9.5px;padding-right:5px;'>" + query.Phone + "</span></td><td width='25%' border='1'><span style='text-align:right;font-size:9.5px;padding-right:5px;'>" + query.Age + "</span></td></tr>");

                }

                st.Append("</table>");
                string HTMLContent = st.ToString();

                byte[] byteArray = GetPDF(HTMLContent);


                response.Data = byteArray;

            }
            else
            {
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, "Fail to generate Salary Slip");
            }


            return response;
        }



        protected byte[] GetPDF(string pHTML)
        {


            byte[] bPDF = null;



            MemoryStream ms = new MemoryStream();
            //StringReader txtReader = new StringReader(pHTML);
            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();
            doc.NewPage();
            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            bPDF = ms.ToArray();




            return bPDF;

        }



        public Response GetJhonpdf()
        {
            Response response = new Response();
            StringBuilder st = new StringBuilder();
            Document document = new Document(PageSize.A4, 0, 0, 150, 20);
            
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder\\images");
            FileStream msReport = new FileStream((pathBuilt) + "Sample1.pdf", FileMode.Create);

            
                // creation of the different writers
                PdfWriter writer = PdfWriter.GetInstance(document, msReport);

                document.Open();

                PdfPTable PdfTable = new PdfPTable(1);
                PdfTable.SpacingBefore = 30f;


                PdfPCell PdfPCell = null;

              Font fontCategoryheader = new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLD, color:BaseColor.BLACK);

                for (int i = 0; i < 20; i++)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Sales Manager: ", fontCategoryheader)));
                    PdfPCell.BorderWidth = 0;
                    PdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;

                    if (i % 2 == 0)
                        PdfPCell.BackgroundColor = BaseColor.BLACK;

                    PdfPCell.PaddingBottom = 5f;
                    PdfPCell.PaddingLeft = 2f;
                    PdfPCell.PaddingTop = 4f;
                    PdfPCell.PaddingRight = 4f;
                    PdfTable.AddCell(PdfPCell);
                }

                document.Add(PdfTable);
                document.NewPage();

            
           
            //string HTMLContent = st.ToString();

            //    byte[] byteArray = GetPDF(HTMLContent);


                response.Data = document;

            
          


            return response;
        }




    }
}








