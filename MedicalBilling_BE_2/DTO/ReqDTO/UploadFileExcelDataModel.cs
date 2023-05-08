using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class UploadFileExcelDataModel
	{
        public int RowId { get; set; }
        public string PayerName { get; set; }
		public string PayerCode { get; set; }
		public string RenderingFullName { get; set; }
		public string RefferringFullName { get; set; }
		public string PatientName { get; set; }
		public string PatientCode { get; set; }
		public DateTime? PatientBirthDate { get; set; }
		public string MedicalRecordCode { get; set; }
		public string EAIBCode { get; set; }
		public string Componant { get; set; }
		public string PayerPhone { get; set; }
		public string PolicyCode { get; set; }
		public string ClaimStatus { get; set; }
		public string ClaimCode { get; set; }
		public DateTime? DateOfService { get; set; }
		public string ServiceCPT { get; set; }
		public string ServiceCode { get; set; }
		public string ClaimModifier { get; set; }
		public string DiagnosisCode1 { get; set; }
		public string DiagnosisCode2 { get; set; }
		public string DiagnosisCode3 { get; set; }
		public string DiagnosisCode4 { get; set; }
		public string COB { get; set; }
		public decimal? InsuranceAmount1 { get; set; }
		public decimal? InsuranceAmount2 { get; set; }
		public decimal? InsuranceAmount3 { get; set; }
		public decimal? InsuranceAmount4 { get; set; }
		public decimal ChargeAmount { get; set; }
		public decimal? LineItemAmount { get; set; }
		public DateTime? LastBillDate { get; set; }
	}
}
