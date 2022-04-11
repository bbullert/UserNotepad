using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Extensions.List;

namespace UserNotepad.Extensions.Methods
{
    public static class EnumerableExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int currentPage, int totalItems, int pageSize) where T : class
        {
            return new PagedList<T>(items, currentPage, totalItems, pageSize);
        }
    }
}
