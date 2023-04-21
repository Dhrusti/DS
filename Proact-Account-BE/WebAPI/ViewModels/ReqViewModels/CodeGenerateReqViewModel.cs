using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.ReqViewModels
{
    public class CodeGenerateReqViewModel
    {
        [Required]
        public int LevelId { get; set; }
        public int? ParentId { get; set; }
    }
}
