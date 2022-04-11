using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotepad.Extensions.List
{
    public interface IPagedList
    {
        int CurrentPage { get; }
        int FirstPage { get; }
        int LastPage { get; }
        int PageSize { get; }
        int? PreviousPage { get; }
        int? NextPage { get; }
        int TotalItems { get; }
        int TotalPages { get; }
        int PagerLength { get; }
        int PageItemsStartIndex { get; }
        int PageItemsEndIndex { get; }
        int PagerRangeStartPage { get; }
        int PagerRangeEndPage { get; }
    }
}
