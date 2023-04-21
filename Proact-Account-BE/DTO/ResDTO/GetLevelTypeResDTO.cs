using DataLayer.Entities;

namespace DTO.ResDTO
{
    public class GetLevelTypeResDTO
    {
        public List<FirstLevelMst> firstLevelList { get; set; }
        public List<AllLevelMst> AllLevelList { get; set; }
    }
}
