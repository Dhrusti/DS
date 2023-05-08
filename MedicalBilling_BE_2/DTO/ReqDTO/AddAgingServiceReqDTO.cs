using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class AddAgingServiceReqDTO
	{
		public int OrganizationId { get; set; }
		public int CompanyId { get; set; }
		public int DepartmentId { get; set; }
		public int PayerId { get; set; }
		public int PatientId { get; set; }
		public int PolicyId { get; set; }
		public int ClaimId { get; set; }
		public DateTime? DateOfService { get; set; }
		public string? ServiceCpt { get; set; }
		public string? ServiceCode { get; set; }
		public string? Modifier { get; set; }
		public string? DiagnosisCode1 { get; set; }
		public string? DiagnosisCode2 { get; set; }
		public string? DiagnosisCode3 { get; set; }
		public string? DiagnosisCode4 { get; set; }
		public string? Cob { get; set; }
		public decimal? InsuranceAmount1 { get; set; }
		public decimal? InsuranceAmount2 { get; set; }
		public decimal? InsuranceAmount3 { get; set; }
		public decimal? InsuranceAmount4 { get; set; }
		public decimal ChargeAmount { get; set; }
		public decimal? LineItemAmount { get; set; }
	}
}
