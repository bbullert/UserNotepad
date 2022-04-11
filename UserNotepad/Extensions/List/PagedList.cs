using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotepad.Extensions.List
{
    public class PagedList<T> : List<T>, IPagedList where T : class
    {
        public PagedList(IEnumerable<T> items, int currentPage, int totalItems, int pageSize)
        {
            CurrentPage = currentPage;
            TotalItems = totalItems;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
            PagerLength = Math.Min(10, TotalPages);

            AddRange(items);
        }

        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItems { get; private set; }

        public int TotalPages { get; private set; }

        public int PagerLength { get; private set; }

        public int FirstPage => 1;

        public int LastPage => TotalPages;

        public int? PreviousPage => CurrentPage - 1 < FirstPage ? null : CurrentPage - 1;

        public int? NextPage => CurrentPage + 1 > LastPage ? null : CurrentPage + 1;

        public int PageItemsStartIndex => (CurrentPage - 1) * PageSize;

        public int PageItemsEndIndex => Math.Min(PageItemsStartIndex + PageSize - 1, TotalItems - 1);

        public int PagerRangeStartPage => Math.Clamp(CurrentPage - (PagerLength - 1) / 2, FirstPage, LastPage - PagerLength + 1);

        public int PagerRangeEndPage => PagerRangeStartPage + PagerLength;
    }
}
