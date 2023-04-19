namespace RegionWebAPI.ViewModel.ReqViewModel
{
    public class CountryPaginationFeaturesModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }
    }
}
