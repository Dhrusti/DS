namespace RegionWebAPI.ViewModel.ReqViewModel
{
    public class StatePaginationModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool Orderby { get; set; }
        public int CountryMainId { get; set; } = 1;
    }
}
