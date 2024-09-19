namespace GameShop.ViewModel
{
    public class PageViewModel(int count,int pageNumber ,int pageSize)
    {
        //public int PageNumber 
        //{
        //    get
        //    {
        //        return pageNumber;
        //    }
        //}

        public int PageNumber { get => pageNumber; }

        public int TotalPages { get => (int)Math.Ceiling(count / (double)pageSize); }

        public bool HasPreviousPage { get => PageNumber > 1; }

        public bool HasNextPage => PageNumber < TotalPages;
    }
}
