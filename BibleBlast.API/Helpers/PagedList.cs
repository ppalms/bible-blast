using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BibleBlast.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            base.AddRange(items);
        }

        /// <summary>
        /// Used to pare down an IQueryable prior to retreving
        /// the data from the repository.
        /// </summary>
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source,
            int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }

    public class PagedListParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 6;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public int UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
