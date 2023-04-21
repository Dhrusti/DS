using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.ReqViewModels
{
    public class AddLevelFirstReqViewModel
    {

        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }


    }
}
