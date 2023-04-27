using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Helper;
using ValidationDemoApi.Models;
using DocumentITextPDF = iTextSharp.text.Document;
using FontITP = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using ImageITP = iTextSharp.text.Image;
using Paragraph = iTextSharp.text.Paragraph;
using Path = System.IO.Path;
using Rectangle = iTextSharp.text.Rectangle;

namespace ValidationDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportToExcelController : ControllerBase
    {
        private readonly ExportToExcelData _ete;
        public readonly JWTokenDBContext _db;
        public ExportToExcelController(ExportToExcelData ete, JWTokenDBContext db)
        {
            _ete = ete;
            _db = db;
        }

        [HttpPost("DownloadExcel")]
        public IActionResult DownloadExcel(IFormCollection obj)
        {
            string reportname = $"Student.xlsx";
            var list = _ete.GetStudentReport();
            var exportbytes = list.Data;
            return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
        }
        [HttpPost("Downloadpdf")]
        public IActionResult Downloadpdf(IFormFileCollection obj)
        {
            string reportname = $"Student.pdf";
            var list = _ete.GetStudentpdf();
            byte[] byteArray = list.Data;
            return File(byteArray, "application/pdf", reportname);

        }

        [HttpGet("Downloadpdf2")]
        public IActionResult Downloadpdf2()
        {
            string filePath = string.Empty;
            string pdfFileName = string.Empty;

            Rectangle pageSize = new Rectangle(PageSize.A4.Rotate());
            pageSize.BackgroundColor = new BaseColor(234, 244, 251);
            Document document = new Document(pageSize);


            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();


                string Fontpath = "D:\\NewProject\\ValidationDemoApi\\ValidationDemoApi\\NewFolder\\Font\\Poppins ExtraLight 275.ttf";
                Font fontSummary = FontFactory.GetFont(Fontpath, 24, Font.NORMAL, BaseColor.BLACK);
                Font fontCHWTitle = FontFactory.GetFont(Fontpath, 20, Font.NORMAL, BaseColor.BLACK);

                var fontTableHeader = FontFactory.GetFont("https://fonts.googleapis.com/css?family=Poppins", 12, Font.NORMAL, BaseColor.BLACK);
                var fontTableRow = FontFactory.GetFont("https://fonts.googleapis.com/css?family=Poppins", 10, Font.NORMAL, BaseColor.GRAY);
                PdfContentByte content = writer.DirectContentUnder;




                #region image
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 28;

                table.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                PdfPCell cell2 = new PdfPCell((iTextSharp.text.Image.GetInstance("D:\\NewProject\\ValidationDemoApi\\ValidationDemoApi\\NewFolder\\images\\CHWLOGO.png")));
                cell2.FixedHeight = 50;

                cell2.Border = Rectangle.NO_BORDER;
                cell2.PaddingLeft = 0;
                cell2.PaddingTop = 10;
                cell2.PaddingBottom = 10;
                cell2.PaddingRight = 0;
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell2);

                PdfPCell cell = new PdfPCell(new Phrase("Click Heal Weal", fontCHWTitle));
                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingLeft = 0;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 10;
                cell.PaddingRight = 0;
                table.AddCell(cell);

                #endregion



                //main table create
                PdfPTable MainTable = new PdfPTable(3);
                MainTable.WidthPercentage = 100;


                #region summary
                PdfPCell MainTableCell_1 = new PdfPCell(new Phrase("Summary Report", fontSummary));
                MainTableCell_1.Colspan = 3;
                MainTableCell_1.PaddingBottom = 10;
                MainTableCell_1.BorderWidthBottom = 0;
                MainTableCell_1.BorderWidthLeft = 0;
                MainTableCell_1.BorderWidthTop = 0;
                MainTableCell_1.BorderWidthRight = 0;
                MainTableCell_1.PaddingBottom = 10;
                MainTableCell_1.Border = PdfPCell.NO_BORDER;
                MainTableCell_1.CellEvent = new RoundedBorder();
                MainTableCell_1.HorizontalAlignment = 1;
                MainTable.AddCell(MainTableCell_1);
                #endregion

                #region first

                PdfPTable InnerTable_1 = new PdfPTable(3);
                PdfPCell MainTableCell_2 = new PdfPCell(InnerTable_1);
                MainTableCell_2.Border = Rectangle.NO_BORDER;
                MainTableCell_2.PaddingLeft = 10;
                MainTableCell_2.PaddingTop = 20;
                MainTableCell_2.PaddingBottom = 20;
                MainTableCell_2.PaddingRight = 10;
                MainTableCell_2.CellEvent = new RoundedBorder();
                InnerTable_1.WidthPercentage = 100;
                InnerTable_1.SpacingBefore = 0;
                InnerTable_1.SpacingAfter = 0;


                PdfPCell InnerTable_1_Cell_1 = new PdfPCell(new Phrase("Patient Information", fontTableHeader));
                InnerTable_1_Cell_1.Colspan = 3;
                InnerTable_1_Cell_1.PaddingBottom = 10;
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_1.AddCell(InnerTable_1_Cell_1);


                InnerTable_1_Cell_1 = new PdfPCell(new Phrase("First Name", fontTableHeader));
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_1.AddCell(InnerTable_1_Cell_1);

                InnerTable_1_Cell_1 = new PdfPCell(new Phrase("Last Name", fontTableHeader));
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_1.AddCell(InnerTable_1_Cell_1);


                InnerTable_1_Cell_1 = new PdfPCell(new Phrase("Dignosis", fontTableHeader));
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_1.AddCell(InnerTable_1_Cell_1);


                InnerTable_1_Cell_1 = new PdfPCell(new Phrase("Jhon", fontTableRow));
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_1.AddCell(InnerTable_1_Cell_1);

                InnerTable_1_Cell_1 = new PdfPCell(new Phrase("Deo", fontTableRow));
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_1.AddCell(InnerTable_1_Cell_1);

                InnerTable_1_Cell_1 = new PdfPCell(new Phrase("Hypertension", fontTableRow));
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_1.AddCell(InnerTable_1_Cell_1);


                InnerTable_1_Cell_1.AddElement(InnerTable_1);
                InnerTable_1_Cell_1.Border = Rectangle.NO_BORDER;

                MainTable.AddCell(MainTableCell_2);

                #endregion first

                #region second
                PdfPTable InnerTable_2 = new PdfPTable(1);
                PdfPCell MainTableCell_3 = new PdfPCell(InnerTable_2);
                MainTableCell_3.Border = Rectangle.NO_BORDER;
                MainTableCell_3.PaddingLeft = 10;
                MainTableCell_3.PaddingTop = 20;
                MainTableCell_3.PaddingBottom = 20;
                MainTableCell_3.PaddingRight = 10;
                MainTableCell_3.CellEvent = new RoundedBorder();
                InnerTable_2.WidthPercentage = 100;
                InnerTable_2.SpacingBefore = 0;
                InnerTable_2.SpacingAfter = 0;


                PdfPCell InnerTable_2_Cell_1 = new PdfPCell(new Phrase("Physician Information", fontTableHeader));
                InnerTable_2_Cell_1.Colspan = 3;
                InnerTable_2_Cell_1.PaddingBottom = 10;
                InnerTable_2_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_2.AddCell(InnerTable_2_Cell_1);

                InnerTable_2_Cell_1 = new PdfPCell(new Phrase("Name", fontTableHeader));

                InnerTable_2_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_2.AddCell(InnerTable_2_Cell_1);

                InnerTable_2_Cell_1 = new PdfPCell(new Phrase("Jhon", fontTableRow));
                InnerTable_2_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_2.AddCell(InnerTable_2_Cell_1);

                InnerTable_2_Cell_1.AddElement(InnerTable_2);
                InnerTable_2_Cell_1.Border = Rectangle.NO_BORDER;

                MainTable.AddCell(MainTableCell_3);
                #endregion second

                #region third


                PdfPTable InnerTable_3 = new PdfPTable(3);
                PdfPCell MainTableCell_4 = new PdfPCell(InnerTable_3);
                MainTableCell_4.Border = Rectangle.NO_BORDER;
                MainTableCell_4.PaddingLeft = 10;
                MainTableCell_4.PaddingTop = 20;
                MainTableCell_4.PaddingBottom = 20;
                MainTableCell_4.PaddingRight = 10;
                MainTableCell_4.CellEvent = new RoundedBorder();

                InnerTable_3.WidthPercentage = 100;
                InnerTable_3.SpacingBefore = 0;
                InnerTable_3.SpacingAfter = 0;


                PdfPCell InnerTable_3_Cell_1 = new PdfPCell(new Phrase("Appointment Information", fontTableHeader));
                InnerTable_3_Cell_1.Colspan = 3;
                InnerTable_3_Cell_1.PaddingBottom = 10;
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_3.AddCell(InnerTable_3_Cell_1);


                InnerTable_3_Cell_1 = new PdfPCell(new Phrase("Date", fontTableHeader));
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_3.AddCell(InnerTable_3_Cell_1);

                InnerTable_3_Cell_1 = new PdfPCell(new Phrase("Time", fontTableHeader));
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_3.AddCell(InnerTable_3_Cell_1);


                InnerTable_3_Cell_1 = new PdfPCell(new Phrase("Loction", fontTableHeader));
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_3.AddCell(InnerTable_3_Cell_1);


                InnerTable_3_Cell_1 = new PdfPCell(new Phrase("28/07/2022", fontTableRow));
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_3.AddCell(InnerTable_3_Cell_1);

                InnerTable_3_Cell_1 = new PdfPCell(new Phrase("01:00", fontTableRow));
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_3.AddCell(InnerTable_3_Cell_1);

                InnerTable_3_Cell_1 = new PdfPCell(new Phrase("Vadodara", fontTableRow));
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;
                InnerTable_3.AddCell(InnerTable_3_Cell_1);


                InnerTable_3_Cell_1.AddElement(InnerTable_3);
                InnerTable_3_Cell_1.Border = Rectangle.NO_BORDER;

                MainTable.AddCell(MainTableCell_4);


                #endregion third

                #region forth
                var res = _db.Students.ToList();
                //res = res.Take(1).ToList();
                if (res != null)
                {
                    int TotalCount = res.Count;
                    int count = Convert.ToInt32(TotalCount / 3);
                    count = (count + 1) * 3;
                    int addcell = count - TotalCount;
                    foreach (var student in res)
                    {

                        PdfPTable InnerTable_4 = new PdfPTable(3);
                        PdfPCell MainTableCell_5 = new PdfPCell(InnerTable_4);
                        MainTableCell_5.PaddingLeft = 10;
                        MainTableCell_5.PaddingTop = 20;
                        MainTableCell_5.PaddingBottom = 20;
                        MainTableCell_5.PaddingRight = 10;
                        MainTableCell_5.BorderWidthBottom = 0;
                        MainTableCell_5.BorderWidthLeft = 0;
                        MainTableCell_5.BorderWidthTop = 0;
                        MainTableCell_5.BorderWidthRight = 0;
                        MainTableCell_5.CellEvent = new RoundedBorder();

                        InnerTable_4.WidthPercentage = 100;
                        InnerTable_4.SpacingBefore = 0;
                        InnerTable_4.SpacingAfter = 0;


                        PdfPCell InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Medication", fontTableHeader));
                        InnerTable_4_Cell_1.Colspan = 3;
                        InnerTable_4_Cell_1.PaddingBottom = 10;
                        InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
                        InnerTable_4.AddCell(InnerTable_4_Cell_1);

                        InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Name", fontTableHeader));
                        InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
                        InnerTable_4.AddCell(InnerTable_4_Cell_1);

                        InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Time", fontTableHeader));
                        InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
                        InnerTable_4.AddCell(InnerTable_4_Cell_1);

                        InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Phone", fontTableHeader));
                        InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
                        InnerTable_4.AddCell(InnerTable_4_Cell_1);

                        InnerTable_4_Cell_1 = new PdfPCell(new Phrase(student.Name, fontTableRow));
                        InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
                        InnerTable_4.AddCell(InnerTable_4_Cell_1);

                        InnerTable_4_Cell_1 = new PdfPCell(new Phrase(student.Address, fontTableRow));
                        InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
                        InnerTable_4.AddCell(InnerTable_4_Cell_1);

                        InnerTable_4_Cell_1 = new PdfPCell(new Phrase(student.Phone, fontTableRow));
                        InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
                        InnerTable_4.AddCell(InnerTable_4_Cell_1);

                        MainTable.AddCell(MainTableCell_5);

                    }
                    if (addcell > 0)
                    {
                        for (int i = 0; i < addcell; i++)
                        {

                            PdfPTable InnerTable_4 = new PdfPTable(3);
                            PdfPCell MainTableCell_5 = new PdfPCell(InnerTable_4);
                            MainTableCell_5.PaddingLeft = 10;
                            MainTableCell_5.PaddingTop = 20;
                            MainTableCell_5.PaddingBottom = 20;
                            MainTableCell_5.PaddingRight = 10;
                            InnerTable_4.WidthPercentage = 100;
                            InnerTable_4.SpacingBefore = 0;
                            InnerTable_4.SpacingAfter = 0;
                            MainTableCell_5.BorderWidthBottom = 0;
                            MainTableCell_5.BorderWidthLeft = 0;
                            MainTableCell_5.BorderWidthTop = 0;
                            MainTableCell_5.BorderWidthRight = 0;
                            MainTable.AddCell(MainTableCell_5);
                        }

                    }
                }
                #endregion forth


                document.Add(table);
                document.Add(MainTable);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                var filename = "CHW";
                pdfFileName = filename.ToString() + ".pdf";
                //var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder\\images");
                filePath = (filename + ".pdf");
                return File(bytes, "application/pdf", filePath);
            }
        }

        #region MyRegion

        //[HttpGet("Downloadpdf2")]
        //public IActionResult Downloadpdf2()
        //{
        //    string filePath = string.Empty;
        //    string pdfFileName = string.Empty;

        //    Rectangle pageSize = new Rectangle(PageSize.A4.Rotate());
        //    pageSize.BackgroundColor = new BaseColor(234, 244, 251);
        //    Document document = new Document(pageSize);


        //    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        //    {

        //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        //        document.Open();


        //        string Fontpath = "D:\\NewProject\\ValidationDemoApi\\ValidationDemoApi\\NewFolder\\Font\\Poppins ExtraLight 275.ttf";
        //        Font fontSummary = FontFactory.GetFont(Fontpath, 24, Font.NORMAL, BaseColor.BLACK);
        //        Font fontCHWTitle = FontFactory.GetFont(Fontpath, 20, Font.NORMAL, BaseColor.BLACK);

        //        var fontTableHeader = FontFactory.GetFont("https://fonts.googleapis.com/css?family=Poppins", 12, Font.NORMAL, BaseColor.BLACK);
        //        var fontTableRow = FontFactory.GetFont("https://fonts.googleapis.com/css?family=Poppins", 10, Font.NORMAL, BaseColor.GRAY);
        //        PdfContentByte content = writer.DirectContentUnder;




        //        #region image
        //        PdfPTable table = new PdfPTable(4);
        //        table.WidthPercentage = 28;

        //        table.HorizontalAlignment = Rectangle.ALIGN_LEFT;
        //        PdfPCell cell2 = new PdfPCell((iTextSharp.text.Image.GetInstance("D:\\NewProject\\ValidationDemoApi\\ValidationDemoApi\\NewFolder\\images\\CHWLOGO.png")));
        //        cell2.FixedHeight = 50;

        //        cell2.Border = Rectangle.NO_BORDER;
        //        cell2.PaddingLeft = 0;
        //        cell2.PaddingTop = 10;
        //        cell2.PaddingBottom = 10;
        //        cell2.PaddingRight = 0;
        //        cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
        //        table.AddCell(cell2);

        //        PdfPCell cell = new PdfPCell(new Phrase("Click Heal Weal", fontCHWTitle));
        //        cell.Colspan = 3;
        //        cell.Border = Rectangle.NO_BORDER;
        //        cell.PaddingLeft = 0;
        //        cell.PaddingTop = 10;
        //        cell.PaddingBottom = 10;
        //        cell.PaddingRight = 0;
        //        table.AddCell(cell);

        //        #endregion



        //        //main table create
        //        PdfPTable MainTable = new PdfPTable(4);
        //        MainTable.WidthPercentage = 100;


        //        #region summary
        //        PdfPCell MainTableCell_1 = new PdfPCell(new Phrase("Encounter Report", fontSummary));
        //        MainTableCell_1.Colspan = 4;
        //        MainTable.AddCell(MainTableCell_1);

        //        PdfPCell MainTableCell_2 = new PdfPCell(new Phrase("Patient Information", fontSummary));
        //        MainTableCell_2.Colspan = 3;
        //        MainTableCell_2.Colspan = 3;
        //        MainTableCell_2.PaddingBottom = 10;

        //        MainTable.AddCell(MainTableCell_2);


        //        MainTableCell_2 = new PdfPCell(new Phrase("Date:", fontSummary));

        //        MainTable.AddCell(MainTableCell_2);

        //        MainTable.AddCell(MainTableCell_2);
        //        #endregion Summary







        //        #region forth
        //        var res = _db.Students.ToList();
        //        //res = res.Take(1).ToList();
        //        if (res != null)
        //        {
        //            int TotalCount = res.Count;
        //            int count = Convert.ToInt32(TotalCount / 3);
        //            count = (count + 1) * 3;
        //            int addcell = count - TotalCount;
        //            foreach (var student in res)
        //            {

        //                PdfPTable InnerTable_4 = new PdfPTable(3);
        //                PdfPCell MainTableCell_5 = new PdfPCell(InnerTable_4);
        //                MainTableCell_5.PaddingLeft = 10;
        //                MainTableCell_5.PaddingTop = 20;
        //                MainTableCell_5.PaddingBottom = 20;
        //                MainTableCell_5.PaddingRight = 10;
        //                MainTableCell_5.BorderWidthBottom = 0;
        //                MainTableCell_5.BorderWidthLeft = 0;
        //                MainTableCell_5.BorderWidthTop = 0;
        //                MainTableCell_5.BorderWidthRight = 0;
        //                MainTableCell_5.CellEvent = new RoundedBorder();

        //                InnerTable_4.WidthPercentage = 100;
        //                InnerTable_4.SpacingBefore = 0;
        //                InnerTable_4.SpacingAfter = 0;


        //                PdfPCell InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Medication", fontTableHeader));
        //                InnerTable_4_Cell_1.Colspan = 3;
        //                InnerTable_4_Cell_1.PaddingBottom = 10;
        //                InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
        //                InnerTable_4.AddCell(InnerTable_4_Cell_1);

        //                InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Name", fontTableHeader));
        //                InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
        //                InnerTable_4.AddCell(InnerTable_4_Cell_1);

        //                InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Time", fontTableHeader));
        //                InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
        //                InnerTable_4.AddCell(InnerTable_4_Cell_1);

        //                InnerTable_4_Cell_1 = new PdfPCell(new Phrase("Phone", fontTableHeader));
        //                InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
        //                InnerTable_4.AddCell(InnerTable_4_Cell_1);

        //                InnerTable_4_Cell_1 = new PdfPCell(new Phrase(student.Name, fontTableRow));
        //                InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
        //                InnerTable_4.AddCell(InnerTable_4_Cell_1);

        //                InnerTable_4_Cell_1 = new PdfPCell(new Phrase(student.Address, fontTableRow));
        //                InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
        //                InnerTable_4.AddCell(InnerTable_4_Cell_1);

        //                InnerTable_4_Cell_1 = new PdfPCell(new Phrase(student.Phone, fontTableRow));
        //                InnerTable_4_Cell_1.Border = Rectangle.NO_BORDER;
        //                InnerTable_4.AddCell(InnerTable_4_Cell_1);

        //                MainTable.AddCell(MainTableCell_5);

        //            }
        //            if (addcell > 0)
        //            {
        //                for (int i = 0; i < addcell; i++)
        //                {

        //                    PdfPTable InnerTable_4 = new PdfPTable(3);
        //                    PdfPCell MainTableCell_5 = new PdfPCell(InnerTable_4);
        //                    MainTableCell_5.PaddingLeft = 10;
        //                    MainTableCell_5.PaddingTop = 20;
        //                    MainTableCell_5.PaddingBottom = 20;
        //                    MainTableCell_5.PaddingRight = 10;
        //                    InnerTable_4.WidthPercentage = 100;
        //                    InnerTable_4.SpacingBefore = 0;
        //                    InnerTable_4.SpacingAfter = 0;
        //                    MainTableCell_5.BorderWidthBottom = 0;
        //                    MainTableCell_5.BorderWidthLeft = 0;
        //                    MainTableCell_5.BorderWidthTop = 0;
        //                    MainTableCell_5.BorderWidthRight = 0;
        //                    MainTable.AddCell(MainTableCell_5);
        //                }

        //            }
        //        }
        //        #endregion forth


        //        document.Add(table);
        //        document.Add(MainTable);
        //        document.Close();
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();
        //        var filename = "CHW";
        //        pdfFileName = filename.ToString() + ".pdf";
        //        //var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "NewFolder\\images");
        //        filePath = (filename + ".pdf");
        //        return File(bytes, "application/pdf", filePath);
        //    }
        //}

        #endregion


    }



 

    


    class RoundedBorder : IPdfPCellEvent
    {
        public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvas)
        {
            BaseColor bColor = new BaseColor(0xFF, 0xD0, 0x00);
            PdfContentByte cb = canvas[PdfPTable.BACKGROUNDCANVAS];
            cb.RoundRectangle(
              rect.Left + 3.5f,
              rect.Bottom + 3.5f,
              rect.Width - 5,
              rect.Height - 5, 6

            );
            cb.SetColorStroke(BaseColor.BLACK);
            cb.Stroke();
        }
    }



}




