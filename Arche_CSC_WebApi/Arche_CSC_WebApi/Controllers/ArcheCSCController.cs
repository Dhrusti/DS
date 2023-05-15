using System.Net;
using Arche_CSC_WebApi.ViewModels.ReqViewModel;
using Arche_CSC_WebApi.ViewModels.ResViewModel;
using DTOs.ReqDTOs;
using DTOs.ResDTOs;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace Arche_CSC_WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArcheCSCController : ControllerBase
	{
		private readonly IArcheCSC _iArcheCSC;
		public ArcheCSCController(IArcheCSC iArcheCSC)
		{
			_iArcheCSC = iArcheCSC;
		}

		#region Country

		[HttpPost("GetAllCountries")]
		public CommonResponse GetAllCountries()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetAllCountries();
				List<GetAllCountryResDTO> getAllCountryResDTO = commonResponse.Data;
				commonResponse.Data = getAllCountryResDTO.Adapt<List<GetAllCountryResViewModel>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetCountryDetailsById")]
		public CommonResponse GetCountryDetailsById(GetCountryDetailsByIdReqViewModel getCountryDetailsByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetCountryDetailsById(getCountryDetailsByIdReqViewModel.Adapt<GetCountryDetailsByIdReqDTO>());
				GetCountryDetailsByIdResDTO getCountryDetailsByIdResDTO = commonResponse.Data;
				commonResponse.Data = getCountryDetailsByIdResDTO.Adapt<GetCountryDetailsByIdResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddCountry")]
		public CommonResponse AddCountry(AddCountryReqViewModel addCountryReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.AddCountry(addCountryReqViewModel.Adapt<AddCountryReqDTO>());
				AddCountryResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<AddCountryResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("UpdateCountry")]
		public CommonResponse UpdateCountry(UpdateCountryReqViewModel updateCountryReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.UpdateCountry(updateCountryReqViewModel.Adapt<UpdateCountryReqDTO>());
				UpdateCountryResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<UpdateCountryResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("DeleteCountry")]
		public CommonResponse DeleteCountry(DeleteCountryReqViewModel deleteCountryReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.DeleteCountry(deleteCountryReqViewModel.Adapt<DeleteCountryReqDTO>());
				DeleteCountryResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<DeleteCountryResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region State

		[HttpPost("GetAllStates")]
		public CommonResponse GetAllStates()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetAllStates();
				List<GetAllStateResDTO> getAllStateResDTO = commonResponse.Data;
				commonResponse.Data = getAllStateResDTO.Adapt<List<GetAllStateResViewModel>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetStateDetailsById")]
		public CommonResponse GetStateDetailsById(GetStateDetailsByIdReqViewModel getStateDetailsByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetStateDetailsById(getStateDetailsByIdReqViewModel.Adapt<GetStateDetailsByIdReqDTO>());
				GetStateDetailsByIdResDTO getStateDetailsByIdResDTO = commonResponse.Data;
				commonResponse.Data = getStateDetailsByIdResDTO.Adapt<GetStateDetailsByIdResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetAllStatesByCountryId")]
		public CommonResponse GetAllStatesByCountryId(GetAllStatesByCountryIdReqViewModel getAllStatesByCountryIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetAllStatesByCountryId(getAllStatesByCountryIdReqViewModel.Adapt<GetAllStatesByCountryIdReqDTO>());
				List<GetAllStatesByCountryIdResDTO> getAllStatesByCountryIdResDTO = commonResponse.Data;
				commonResponse.Data = getAllStatesByCountryIdResDTO.Adapt<List<GetAllStatesByCountryIdResViewModel>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddState")]
		public CommonResponse AddState(AddStateReqViewModel addStateReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.AddState(addStateReqViewModel.Adapt<AddStateReqDTO>());
				AddStateResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<AddStateResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("UpdateState")]
		public CommonResponse UpdateState(UpdateStateReqViewModel updateStateReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.UpdateState(updateStateReqViewModel.Adapt<UpdateStateReqDTO>());
				UpdateStateResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<UpdateStateResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("DeleteState")]
		public CommonResponse DeleteState(DeleteStateReqViewModel deleteStateReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.DeleteState(deleteStateReqViewModel.Adapt<DeleteStateReqDTO>());
				DeleteStateResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<DeleteStateResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		#endregion

		#region City

		[HttpPost("GetAllCities")]
		public CommonResponse GetAllCities()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetAllCities();
				List<GetAllCityResDTO> getAllCityResDTO = commonResponse.Data;
				commonResponse.Data = getAllCityResDTO.Adapt<List<GetAllCityResViewModel>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetCityDetailsById")]
		public CommonResponse GetCityDetailsById(GetCityDetailsByIdReqViewModel getCityDetailsByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetCityDetailsById(getCityDetailsByIdReqViewModel.Adapt<GetCityDetailsByIdReqDTO>());
				GetCityDetailsByIdResDTO getCityDetailsByIdResDTO = commonResponse.Data;
				commonResponse.Data = getCityDetailsByIdResDTO.Adapt<GetCityDetailsByIdResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("GetAllCitiesByStateId")]
		public CommonResponse GetAllCitiesByStateId(GetAllCitiesByStateIdReqViewModel getAllCitiesByStateIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.GetAllCitiesByStateId(getAllCitiesByStateIdReqViewModel.Adapt<GetAllCitiesByStateIdReqDTO>());
				List<GetAllCitiesByStateIdResDTO> getAllCitiesByStateIdResDTO = commonResponse.Data;
				commonResponse.Data = getAllCitiesByStateIdResDTO.Adapt<List<GetAllCitiesByStateIdResViewModel>>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("AddCity")]
		public CommonResponse AddCity(AddCityReqViewModel addCityReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.AddCity(addCityReqViewModel.Adapt<AddCityReqDTO>());
				AddCityResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<AddCityResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("UpdateCity")]
		public CommonResponse UpdateCity(UpdateCityReqViewModel updateCityReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.UpdateCity(updateCityReqViewModel.Adapt<UpdateCityReqDTO>());
				UpdateCityResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<UpdateCityResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		[HttpPost("DeleteCity")]
		public CommonResponse DeleteCity(DeleteCityReqViewModel deleteCityReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iArcheCSC.DeleteCity(deleteCityReqViewModel.Adapt<DeleteCityReqDTO>());
				DeleteCityResDTO Model = commonResponse.Data;
				commonResponse.Data = Model.Adapt<DeleteCityResViewModel>();
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
