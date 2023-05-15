using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTOs.ReqDTOs;
using DTOs.ResDTOs;
using Helper.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class ArcheCSCImpl : IArcheCSC
	{
		private readonly ArcheCSCBLL _archeCSCBLL;
		public ArcheCSCImpl(ArcheCSCBLL archeCSCBLL)
		{
			_archeCSCBLL = archeCSCBLL;
		}

		#region Country

		public CommonResponse GetAllCountries()
		{
			return _archeCSCBLL.GetAllCountries();
		}

		public CommonResponse GetCountryDetailsById(GetCountryDetailsByIdReqDTO getCountryDetailsByIdReqDTO)
		{
			return _archeCSCBLL.GetCountryDetailsById(getCountryDetailsByIdReqDTO);
		}
		public CommonResponse AddCountry(AddCountryReqDTO addCountryReqDTO)
		{
			return _archeCSCBLL.AddCountry(addCountryReqDTO);
		}

		public CommonResponse UpdateCountry(UpdateCountryReqDTO updateCountryReqDTO)
		{
			return _archeCSCBLL.UpdateCountry(updateCountryReqDTO);
		}

		public CommonResponse DeleteCountry(DeleteCountryReqDTO deleteCountryReqDTO)
		{
			return _archeCSCBLL.DeleteCountry(deleteCountryReqDTO);
		}

		#endregion

		#region State

		public CommonResponse GetAllStates()
		{
			return _archeCSCBLL.GetAllStates();
		}
		public CommonResponse GetStateDetailsById(GetStateDetailsByIdReqDTO getStateDetailsByIdReqDTO)
		{
			return _archeCSCBLL.GetStateDetailsById(getStateDetailsByIdReqDTO);
		}
		public CommonResponse GetAllStatesByCountryId(GetAllStatesByCountryIdReqDTO getAllStatesByCountryIdReqDTO)
		{
			return _archeCSCBLL.GetAllStatesByCountryId(getAllStatesByCountryIdReqDTO);
		}
		public CommonResponse AddState(AddStateReqDTO addStateReqDTO)
		{
			return _archeCSCBLL.AddState(addStateReqDTO);
		}
		public CommonResponse UpdateState(UpdateStateReqDTO updateStateReqDTO)
		{
			return _archeCSCBLL.UpdateState(updateStateReqDTO);
		}
		public CommonResponse DeleteState(DeleteStateReqDTO deleteStateReqDTO)
		{
			return _archeCSCBLL.DeleteState(deleteStateReqDTO);
		}

		#endregion

		#region City

		public CommonResponse GetAllCities()
		{
			return _archeCSCBLL.GetAllCities();
		}
		public CommonResponse GetCityDetailsById(GetCityDetailsByIdReqDTO getCityDetailsByIdReqDTO)
		{
			return _archeCSCBLL.GetCityDetailsById(getCityDetailsByIdReqDTO);
		}
		public CommonResponse GetAllCitiesByStateId(GetAllCitiesByStateIdReqDTO getAllCitiesByStateIdReqDTO)
		{
			return _archeCSCBLL.GetAllCitiesByStateId(getAllCitiesByStateIdReqDTO);
		}
		public CommonResponse AddCity(AddCityReqDTO addCityReqDTO)
		{
			return _archeCSCBLL.AddCity(addCityReqDTO);
		}

		public CommonResponse UpdateCity(UpdateCityReqDTO updateCityReqDTO)
		{
			return _archeCSCBLL.UpdateCity(updateCityReqDTO);
		}

		public CommonResponse DeleteCity(DeleteCityReqDTO deleteCityReqDTO)
		{
			return _archeCSCBLL.DeleteCity(deleteCityReqDTO);
		}

		#endregion

	}
}
