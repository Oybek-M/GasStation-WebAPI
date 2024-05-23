namespace GasStation.Application.Common.Utils;

public class PaginationMetaData
{
    public int TotalPages { get; set; } // All of Datas / PageSize
    public int CurrentPage { get; set; } // Current page => 1
    public int PageSize { get; set; } // Elements in One page
    public bool Prev { get; set; }
    public bool Next { get; set; }

    public PaginationMetaData(int count, int pageIndex, int pageSize)
    {
        TotalPages = (int)Math.Ceiling((double)count / pageSize);
        CurrentPage = pageIndex;
        PageSize = pageSize;
        Prev = 1 < pageIndex;
        Next = pageIndex < TotalPages;
    }
}
