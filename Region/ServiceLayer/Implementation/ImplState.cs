using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ImplState : IState
    {
        private readonly StateBLL _stateBLL;

        public ImplState(StateBLL stateBLL)
        {
            _stateBLL = stateBLL;
        }

        public CommonResponse ImplStateswithPagination(StatePaginationDTO statesPaginationDTO)
        {
            return _stateBLL.StateswithPaginationBLL(statesPaginationDTO);
        }

    }
}
