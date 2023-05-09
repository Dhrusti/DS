using Helper.CommonHelper;
using Helpers.CommonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace PortalWebAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly CommonHelpers _commonHelpers;

        public ExceptionFilter(CommonHelpers commonHelpers)
        {
            _commonHelpers = commonHelpers;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                CommonResponse commonReponse = new CommonResponse();
                context.Result = new JsonResult(commonReponse);

                var item = context.Exception;
                _commonHelpers.AddExceptionLog(item.ToString());
            }
            catch (Exception ex)
            {
                _commonHelpers.AddExceptionLog(ex.ToString());
            }
        }
    }
}
