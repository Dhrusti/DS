using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegionWebAPI.ViewModel.ResViewModel;
using ServiceLayer.Interface;

namespace RegionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly RegionDbContext _context;
        private readonly IState _istate;

        public StateController(RegionDbContext context, IState istate)
        {
            _context = context;
            _istate = istate;
        }

        [HttpPost("StatesWithPagination")]
        public CommonResponse StatesWithPagination(StatePaginationDTO statesPaginationDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _istate.ImplStateswithPagination(statesPaginationDTO);
                List<StateDTO> stateDTOs = commonResponse.Data ?? new List<StateDTO>();
                commonResponse.Data = stateDTOs.Adapt<List<StateModel>>();
            }
            catch(Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex.Data;
            }
            return commonResponse;
        }
    }
}
