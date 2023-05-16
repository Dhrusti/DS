using CsvHelper;
using DTO.ReqDTO;
using GeoCoordinatePortable;
using Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
	public class DistanceBLL
	{
		private readonly CommonHelper _commonHelper;
		public DistanceBLL(CommonHelper commonHelper)
		{
			_commonHelper = commonHelper;
		}

		public CommonResponse GetDistanceByZipCodes(GetDistanceByZipCodesReqDTO getDistanceByZipCodesReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (!string.IsNullOrWhiteSpace(getDistanceByZipCodesReqDTO.FromZipCode) && !string.IsNullOrWhiteSpace(getDistanceByZipCodesReqDTO.FromZipCode))
				{
					List<CSVDataReqDTO> CSVDataList;
					var CSVDataListRes = GetCSVDataList();
					double FromLat = 0;
					double FromLng = 0;
					double ToLat = 0;
					double ToLng = 0;
					CSVDataList = CSVDataListRes.Status ? CSVDataListRes.Data : new List<CSVDataReqDTO>();
					if (CSVDataList.Count > 0)
					{
						var FromZipCodeDetails = CSVDataList.FirstOrDefault(x => x.ZIP == getDistanceByZipCodesReqDTO.FromZipCode);
						var ToZipCodeDetails = CSVDataList.FirstOrDefault(x => x.ZIP == getDistanceByZipCodesReqDTO.ToZipCode);
						if (FromZipCodeDetails != null && ToZipCodeDetails != null)
						{
							FromLat = FromZipCodeDetails.LAT;
							FromLng = FromZipCodeDetails.LNG;
							ToLat = ToZipCodeDetails.LAT;
							ToLng = ToZipCodeDetails.LNG;

							double res = CalculateDistance(FromLat, ToLat, FromLng, ToLng);
							commonResponse.Data = res;
							commonResponse.Message = "Success!";
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Status = true;
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.NotFound;
							commonResponse.Message = "ZipCode Not Matched With Csv File Data!";
						}
					}
					else
					{
						commonResponse = CSVDataListRes;
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
				}
			}
			catch { throw; }
			return commonResponse;
		}

		private double CalculateDistance(double FromLat, double ToLat, double FromLng, double ToLng)
		{
			// The math module contains
			// a function named toRadians
			// which converts from degrees
			// to radians.
			FromLng = ToRadians(FromLng);
			ToLng = ToRadians(ToLng);
			FromLat = ToRadians(FromLat);
			ToLat = ToRadians(ToLat);

			// Haversine formula
			double dlon = ToLng - FromLng;
			double dlat = ToLat - FromLat;
			double a = Math.Pow(Math.Sin(dlat / 2), 2) +
					   Math.Cos(FromLat) * Math.Cos(ToLat) *
					   Math.Pow(Math.Sin(dlon / 2), 2);

			double c = 2 * Math.Asin(Math.Sqrt(a));

			// Radius of earth in kilometers. Use 3956 for miles
			//double r = 6371;
			double r = 3956;

			// calculate the result
			return Math.Round((c * r), 4);
		}

		private double ToRadians(double angleIn10thofaDegree)
		{
			// Angle in 10th of a degree
			return angleIn10thofaDegree * Math.PI / 180;
		}

		private CommonResponse GetCSVDataList()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				List<CSVDataReqDTO> CSVDataList = new List<CSVDataReqDTO>();
				string rootPath = _commonHelper.GetPhysicalRootPath();
				var filePath = Path.Combine(rootPath, @"ZipCodes\ZipCodes.csv");
				using (var reader = new StreamReader(filePath))
				{
					using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
					{
						CSVDataList = csv.GetRecords<CSVDataReqDTO>().ToList();
						if (CSVDataList.Count > 0)
						{
							commonResponse.Data = CSVDataList;
							commonResponse.Message = "Success!";
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Status = true;
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.NotFound;
						}
					}
				}
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
