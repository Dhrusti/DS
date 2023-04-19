namespace RegionWebAPI.ViewModel.ReqViewModel
{
    public class CityPaginationModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; } = true;
        public int StateMainId { get; set; } = 1;
    }
}
