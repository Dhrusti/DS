using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.ReqDTOs;
using Helper.Models;

namespace ServiceLayer.Interface
{
	public interface IArcheCSC
	{
		#region Country

		public CommonResponse GetAllCountries();
		public CommonResponse GetCountryDetailsById(GetCountryDetailsByIdReqDTO getCountryDetailsByIdReqDTO);
		public CommonResponse AddCountry(AddCountryReqDTO addCountryReqDTO);
		public CommonResponse UpdateCountry(UpdateCountryReqDTO updateCountryReqDTO);
		public CommonResponse DeleteCountry(DeleteCountryReqDTO deleteCountryReqDTO);


		#endregion

		#region State

		public CommonResponse GetAllStates();
		public CommonResponse GetStateDetailsById(GetStateDetailsByIdReqDTO getStateDetailsByIdReqDTO);
		public CommonResponse GetAllStatesByCountryId(GetAllStatesByCountryIdReqDTO getAllStatesByCountryIdReqDTO);
		public CommonResponse AddState(AddStateReqDTO addStateReqDTO);
		public CommonResponse UpdateState(UpdateStateReqDTO updateStateReqDTO);
		public CommonResponse DeleteState(DeleteStateReqDTO deleteStateReqDTO);

		#endregion

		#region City

		public CommonResponse GetAllCities();
		public CommonResponse GetCityDetailsById(GetCityDetailsByIdReqDTO getCityDetailsByIdReqDTO);
		public CommonResponse GetAllCitiesByStateId(GetAllCitiesByStateIdReqDTO getAllCitiesByStateIdReqDTO);
		public CommonResponse AddCity(AddCityReqDTO addCityReqDTO);
		public CommonResponse UpdateCity(UpdateCityReqDTO updateCityReqDTO);
		public CommonResponse DeleteCity(DeleteCityReqDTO deleteCityReqDTO);

		#endregion

	}
}
