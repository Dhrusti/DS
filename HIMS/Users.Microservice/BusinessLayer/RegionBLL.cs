using DataLayer;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class RegionBLL
    {
        private readonly HIMSUserDBContext _context;

        public RegionBLL(HIMSUserDBContext context)
        {
            this._context = context;
        }

        public async Task<ResponseDTO> GetAllCountry()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<CountryResponseDTO> data = await _context.RegionsMsts.Select(x => new CountryResponseDTO { CountryId = x.CountryId, CountryName = x.CountryName }).GroupBy(p => p.CountryName).Select(g => g.First()).ToListAsync() ?? new List<CountryResponseDTO>();
                if(data.Count > 0)
                {
                    data = data.OrderBy(x => x.CountryName).ToList();
                    responseDTO.Data = data;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No Country Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
                
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetCountryByNameAsync(string CountryName)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<RegionsMst> data = await _context.RegionsMsts.Where(x => x.CountryName.Substring(0, CountryName.Length) == CountryName).GroupBy(p => p.CountryName).Select(g => g.First()).ToListAsync() ?? new List<RegionsMst>();
                if (data != null && data.Count > 0)
                {
                    List<CountryResponseDTO> countryNameResponseDTOs = new List<CountryResponseDTO>();
                    CountryResponseDTO countryNameResponseDTO;

                    foreach (var item in data)
                    {
                        countryNameResponseDTO = new CountryResponseDTO();

                        countryNameResponseDTO.CountryId = item.CountryId;
                        countryNameResponseDTO.CountryName = item.CountryName;

                        countryNameResponseDTOs.Add(countryNameResponseDTO);
                    }
                    responseDTO.Data = countryNameResponseDTOs;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No Country Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllStateByCountryAsync(int CountryId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<StateResponseDTO> data = await _context.RegionsMsts.Where(x => x.CountryId == CountryId).Select(x => new StateResponseDTO { StateId = x.StateId, StateName = x.StateName}).GroupBy(p => p.StateName).Select(g => g.First()).ToListAsync() ?? new List<StateResponseDTO>();
                if (data.Count > 0)
                {
                    data = data.OrderBy(x => x.StateName).ToList();
                    responseDTO.Data = data;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No State Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetStateByStateAsync(int StateId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                StateResponseDTO data = await _context.RegionsMsts.Where(x => x.StateId == StateId).Select(x => new StateResponseDTO { StateId = x.StateId, StateName = x.StateName }).FirstOrDefaultAsync() ?? new StateResponseDTO();
                if (data != null)
                {
                    responseDTO.Data = data;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No State Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetStateByNameAsync(string StateName, int CountryId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<RegionsMst> data = await _context.RegionsMsts.Where(x => x.CountryId == CountryId && x.StateName.Substring(0, StateName.Length) == StateName).GroupBy(p => p.StateName).Select(g => g.First()).ToListAsync() ?? new List<RegionsMst>();
                if (data != null && data.Count > 0)
                {
                    List<StateResponseDTO> stateNameResponseDTOs = new List<StateResponseDTO>();
                    StateResponseDTO stateNameResponseDTO;

                    foreach (var item in data)
                    {
                        stateNameResponseDTO = new StateResponseDTO();

                        stateNameResponseDTO.StateId = item.StateId;
                        stateNameResponseDTO.StateName = item.StateName;

                        stateNameResponseDTOs.Add(stateNameResponseDTO);
                    }
                    responseDTO.Data = stateNameResponseDTOs;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No State Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetCityByStateAsync(int StateId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<CityResponseDTO> data = await _context.RegionsMsts.Where(x => x.StateId == StateId).Select(x => new CityResponseDTO { CityId = x.CityId, CityName = x.CityName }).GroupBy(p => p.CityName).Select(g => g.First()).ToListAsync() ?? new List<CityResponseDTO>();
                if (data.Count > 0)
                {
                    responseDTO.Data = data;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No State Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetCityByNameAsync(string CityName, int StateId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<RegionsMst> data = await _context.RegionsMsts.Where(x => x.StateId == StateId && x.CityName.Substring(0, CityName.Length) == CityName).GroupBy(p => p.CityName).Select(g => g.First()).ToListAsync() ?? new List<RegionsMst>();
                if (data != null && data.Count > 0)
                {
                    List<CityResponseDTO> cityNameResponseDTOs = new List<CityResponseDTO>();
                    CityResponseDTO cityNameResponseDTO;

                    foreach (var item in data)
                    {
                        cityNameResponseDTO = new CityResponseDTO();

                        cityNameResponseDTO.CityId = item.CityId;
                        cityNameResponseDTO.CityName = item.CityName;

                        cityNameResponseDTOs.Add(cityNameResponseDTO);
                    }
                    responseDTO.Data = cityNameResponseDTOs;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No City Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetCityByCityIdAsync(int CityId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                CityResponseDTO data = await _context.RegionsMsts.Where(x => x.CityId == CityId).Select(x => new CityResponseDTO { CityId = x.CityId, CityName = x.CityName }).FirstOrDefaultAsync();
                if (data != null)
                {
                    responseDTO.Data = data;
                    responseDTO.Message = "Success";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No State Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.Message;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

    }
}