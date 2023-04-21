using DataLayer.Entities;

namespace WebAPI.ViewModels.ResViewModels
{
    public class GetLevelTypeResViewModel
    {
        public List<FirstLevelMst> firstLevelList { get; set; }
        public List<AllLevelMst> AllLevelList { get; set; }
    }
}
