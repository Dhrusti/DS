using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTOs.ReqDTOs;
using DTOs.ResDTOs;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer
{
	public class ArcheCSCBLL
	{
		private readonly ArcheCountryStateCityDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;
		public ArcheCSCBLL(ArcheCountryStateCityDbContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
		{
			_dbContext = dbContext;
			_commonRepo = commonRepo;
			_commonHelper = commonHelper;
		}

		#region Country

		public CommonResponse GetAllCountries()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var countryList = _commonRepo.countryList().ToList();
				if (countryList.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = countryList.Adapt<List<GetAllCountryResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetCountryDetailsById(GetCountryDetailsByIdReqDTO getCountryDetailsByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var countryDetail = _commonRepo.countryList().Where(x => x.CountryId == getCountryDetailsByIdReqDTO.CountryId).FirstOrDefault();
				if (countryDetail != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = countryDetail.Adapt<GetCountryDetailsByIdResDTO>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddCountry(AddCountryReqDTO addCountryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddCountryResDTO addCountryResDTO = new AddCountryResDTO();

			var trimWhiteSpace = _commonHelper.TrimSpacesBetweenString(addCountryReqDTO.CountryName);
			try
			{
				var addCountry = _commonRepo.countryList().Where(x => x.CountryName.ToLower() == addCountryReqDTO.CountryName.ToLower()).FirstOrDefault();
				if (addCountry == null)
				{
					CountryMst countryMst = new CountryMst();
					countryMst.CountryName = trimWhiteSpace;
					countryMst.Iso2 = addCountryReqDTO.Iso2;
					countryMst.DialingCode = addCountryReqDTO.DialingCode;
					countryMst.IsActive = true;
					countryMst.IsDeleted = false;
					countryMst.CreatedBy = 1;
					countryMst.UpdatedBy = 1;
					countryMst.CreatedDate = DateTime.Now;
					countryMst.UpdatedDate = DateTime.Now;

					_dbContext.CountryMsts.Add(countryMst);
					_dbContext.SaveChanges();

					addCountryResDTO.CountryId = countryMst.CountryId;
					addCountryResDTO.CountryName = countryMst.CountryName;

					commonResponse.Message = "Country added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addCountryResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Country already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse UpdateCountry(UpdateCountryReqDTO updateCountryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			UpdateCountryResDTO updateCountryResDTO = new UpdateCountryResDTO();
			try
			{
				var country = _commonRepo.countryList().Where(x => x.CountryId == updateCountryReqDTO.CountryId && (x.CountryName.ToLower() == updateCountryReqDTO.CountryName.ToLower())).ToList();
				if (country != null)
				{
					var countryDetails = _commonRepo.countryList().FirstOrDefault(x => x.CountryId == updateCountryReqDTO.CountryId);
					if (countryDetails != null)
					{
						CountryMst countryMst = countryDetails;
						countryMst.CountryName = updateCountryReqDTO.CountryName;
						countryMst.Iso2 = updateCountryReqDTO.Iso2;
						countryMst.DialingCode = updateCountryReqDTO.DialingCode;
						countryMst.UpdatedBy = 1;
						countryMst.UpdatedDate = DateTime.Now;

						_dbContext.Entry(countryMst).State = EntityState.Modified;
						_dbContext.SaveChanges();

						updateCountryResDTO.CountryId = countryMst.CountryId;
						updateCountryResDTO.CountryName = countryMst.CountryName;
						updateCountryResDTO.Iso2 = countryMst.Iso2;
						updateCountryResDTO.DialingCode = countryMst.DialingCode;

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = updateCountryResDTO;
						commonResponse.Message = "Successfully Updated.";
					}
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Country already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse DeleteCountry(DeleteCountryReqDTO deleteCountryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			DeleteCountryResDTO deleteCountryResDTO = new DeleteCountryResDTO();
			try
			{
				var country = _commonRepo.countryList().FirstOrDefault(x => x.CountryId == deleteCountryReqDTO.CountryId);
				if (country != null)
				{
					CountryMst countryMst = country;
					countryMst.CountryId = deleteCountryReqDTO.CountryId;
					countryMst.IsDeleted = true;
					countryMst.UpdatedDate = DateTime.Now;

					_dbContext.Entry(countryMst).State = EntityState.Modified;
					_dbContext.SaveChanges();

					var states = _commonRepo.stateList().Where(x => x.CountryId == deleteCountryReqDTO.CountryId).ToList();
					foreach (var item in states)
					{
						StateMst stateMst = item;
						item.IsActive = false;
						item.IsDeleted = true;
						item.UpdatedDate = DateTime.Now;

						_dbContext.Entry(stateMst).State = EntityState.Modified;
						_dbContext.SaveChanges();

						var cities = _commonRepo.cityList().Where(x => x.StateId == item.StateId).ToList();
						foreach (var items in cities)
						{
							CityMst cityMst = items;
							items.IsActive = false;
							items.IsDeleted = true;
							items.UpdatedDate = DateTime.Now;

							_dbContext.Entry(cityMst).State = EntityState.Modified;
							_dbContext.SaveChanges();
						}
					}

					deleteCountryResDTO.CountryId = countryMst.CountryId;
					deleteCountryResDTO.CountryName = countryMst.CountryName;

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = deleteCountryResDTO;
					commonResponse.Message = "Country and it's states are Deleted Successfully.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Can not find the Country.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region State

		public CommonResponse GetAllStates()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var stateList = _commonRepo.stateList().ToList();
				if (stateList.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = stateList.Adapt<List<GetAllStateResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse GetStateDetailsById(GetStateDetailsByIdReqDTO getStateDetailsByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var stateDetail = _commonRepo.stateList().Where(x => x.StateId == getStateDetailsByIdReqDTO.StateId).FirstOrDefault();
				if (stateDetail != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = stateDetail.Adapt<GetStateDetailsByIdResDTO>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse GetAllStatesByCountryId(GetAllStatesByCountryIdReqDTO getAllStatesByCountryIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var stateList = _commonRepo.stateList().Where(x=>x.CountryId == getAllStatesByCountryIdReqDTO.CountryId).ToList();
				if (stateList.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = stateList.Adapt<List<GetAllStatesByCountryIdResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse AddState(AddStateReqDTO addStateReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddStateResDTO addStateResDTO = new AddStateResDTO();
			try
			{
				var addState = _commonRepo.stateList().Where(x => x.StateName.ToLower() == addStateReqDTO.StateName.ToLower()).FirstOrDefault();
				if (addState == null)
				{
					var isCountryExist = _commonRepo.countryList().Where(x => x.CountryId == addStateReqDTO.CountryId).FirstOrDefault();
					if (isCountryExist != null)
					{
						StateMst stateMst = new StateMst();
						stateMst.CountryId = addStateReqDTO.CountryId;
						stateMst.StateName = addStateReqDTO.StateName;
						stateMst.Iso2 = addStateReqDTO.Iso2;
						stateMst.IsActive = true;
						stateMst.IsDeleted = false;
						stateMst.CreatedBy = 1;
						stateMst.UpdatedBy = 1;
						stateMst.CreatedDate = DateTime.Now;
						stateMst.UpdatedDate = DateTime.Now;

						_dbContext.StateMsts.Add(stateMst);
						_dbContext.SaveChanges();

						addStateResDTO.StateId = stateMst.StateId;
						addStateResDTO.StateName = stateMst.StateName;

						commonResponse.Message = "State added successfully.";
						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = addStateResDTO;
					}
					else
					{
						commonResponse.Message = "Country is not valid";
					}

				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "State already exist.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse UpdateState(UpdateStateReqDTO updateStateReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			UpdateStateResDTO updateStateResDTO = new UpdateStateResDTO();
			try
			{
				var IsCountryExist = _commonRepo.countryList().FirstOrDefault(x => x.CountryId == updateStateReqDTO.CountryId);
				if (IsCountryExist != null)
				{
					var state = _commonRepo.stateList().Where(x => x.StateId == updateStateReqDTO.StateId).FirstOrDefault();
					if (state != null)
					{
						StateMst stateMst = state;
						stateMst.CountryId = updateStateReqDTO.CountryId;
						stateMst.StateName = updateStateReqDTO.StateName;
						stateMst.Iso2 = updateStateReqDTO.Iso2;
						stateMst.UpdatedBy = 1;
						stateMst.UpdatedDate = DateTime.Now;

						_dbContext.Entry(stateMst).State = EntityState.Modified;
						_dbContext.SaveChanges();

						updateStateResDTO.StateName = stateMst.StateName;
						updateStateResDTO.Iso2 = stateMst.Iso2;

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = updateStateResDTO;
						commonResponse.Message = "Successfully Updated.";
					}
					else
					{
						commonResponse.Status = false;
						commonResponse.StatusCode = HttpStatusCode.BadRequest;
						commonResponse.Message = "State already exist.";
					}
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Invalid Country.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse DeleteState(DeleteStateReqDTO deleteStateReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			DeleteStateResDTO deleteStateResDTO = new DeleteStateResDTO();
			try
			{
				var state = _commonRepo.stateList().FirstOrDefault(x => x.StateId == deleteStateReqDTO.StateId);
				if (state != null)
				{
					StateMst stateMst = state;
					stateMst.StateId = deleteStateReqDTO.StateId;
					stateMst.IsDeleted = true;
					stateMst.UpdatedDate = DateTime.Now;

					_dbContext.Entry(stateMst).State = EntityState.Modified;
					_dbContext.SaveChanges();

					var city = _commonRepo.cityList().
						Where(x => x.StateId == deleteStateReqDTO.StateId).ToList();
					foreach (var item in city)
					{
						CityMst cityMst = item;
						item.IsActive = false;
						item.IsDeleted = true;
						item.UpdatedDate = DateTime.Now;

						_dbContext.Entry(cityMst).State = EntityState.Modified;
						_dbContext.SaveChanges();
					}

					deleteStateResDTO.StateId = stateMst.StateId;
					deleteStateResDTO.StateName = stateMst.StateName;

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = deleteStateResDTO;
					commonResponse.Message = "State and it's Cities are Deleted Successfully.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Can not find the State.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region City

		public CommonResponse GetAllCities()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var cityList = _commonRepo.cityList().ToList();
				if (cityList.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = cityList.Adapt<List<GetAllCityResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse GetCityDetailsById(GetCityDetailsByIdReqDTO getCityDetailsByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var cityDetail = _commonRepo.cityList().Where(x => x.CityId == getCityDetailsByIdReqDTO.CityId).FirstOrDefault();
				if (cityDetail != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = cityDetail.Adapt<GetCityDetailsByIdResDTO>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse GetAllCitiesByStateId(GetAllCitiesByStateIdReqDTO getAllCitiesByStateIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var cityList = _commonRepo.cityList().Where(x => x.StateId == getAllCitiesByStateIdReqDTO.StateId).ToList();
				if (cityList.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not Found.";
				}
				commonResponse.Data = cityList.Adapt<List<GetAllCitiesByStateIdResDTO>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse AddCity(AddCityReqDTO addCityReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddCityResDTO addCityResDTO = new AddCityResDTO();
			try
			{
				var addCity = _commonRepo.cityList().Where(x => x.CityName.ToLower() == addCityReqDTO.CityName.ToLower()).FirstOrDefault();
				if (addCity == null)
				{
					CityMst cityMst = new CityMst();
					cityMst.StateId = addCityReqDTO.StateId;
					cityMst.CityName = addCityReqDTO.CityName;
					cityMst.IsActive = true;
					cityMst.IsDeleted = false;
					cityMst.CreatedBy = 1;
					cityMst.UpdatedBy = 1;
					cityMst.CreatedDate = DateTime.Now;
					cityMst.UpdatedDate = DateTime.Now;

					_dbContext.CityMsts.Add(cityMst);
					_dbContext.SaveChanges();

					addCityResDTO.CityId = cityMst.CityId;
					addCityResDTO.CityName = cityMst.CityName;

					commonResponse.Message = "City added successfully.";
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = addCityResDTO;
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Invalid City.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse UpdateCity(UpdateCityReqDTO updateCityReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			UpdateCityResDTO updateCityResDTO = new UpdateCityResDTO();
			try
			{
				var IsStateExist = _commonRepo.stateList().FirstOrDefault(x => x.StateId == updateCityReqDTO.StateId);
				if (IsStateExist != null)
				{
					var city = _commonRepo.cityList().Where(x => x.CityId == updateCityReqDTO.CityId).FirstOrDefault();
					if (city != null)
					{
						CityMst cityMst = city;
						cityMst.CityName = updateCityReqDTO.CityName;
						cityMst.StateId = updateCityReqDTO.StateId;
						cityMst.UpdatedBy = 1;
						cityMst.UpdatedDate = DateTime.Now;

						_dbContext.Entry(cityMst).State = EntityState.Modified;
						_dbContext.SaveChanges();

						updateCityResDTO.CityId = cityMst.CityId;
						updateCityResDTO.CityName = cityMst.CityName;

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = updateCityResDTO;
						commonResponse.Message = "Successfully Updated.";
					}
					else
					{
						commonResponse.Status = false;
						commonResponse.StatusCode = HttpStatusCode.BadRequest;
						commonResponse.Message = "Can not find the country";
					}
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Invalid State.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}
		public CommonResponse DeleteCity(DeleteCityReqDTO deleteCityReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			DeleteCityResDTO deleteCityResDTO = new DeleteCityResDTO();
			try
			{
				var city = _commonRepo.cityList().FirstOrDefault(x => x.CityId == deleteCityReqDTO.CityId);
				if (city != null)
				{
					CityMst cityMst = city;
					cityMst.CityId = deleteCityReqDTO.CityId;
					cityMst.IsDeleted = true;
					cityMst.UpdatedDate = DateTime.Now;

					_dbContext.Entry(cityMst).State = EntityState.Modified;
					_dbContext.SaveChanges();

					deleteCityResDTO.CityId = cityMst.CityId;
					deleteCityResDTO.CityName = cityMst.CityName;

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = deleteCityResDTO;
					commonResponse.Message = "City Deleted Successfully.";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Can not find the City.";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion
	}
}
