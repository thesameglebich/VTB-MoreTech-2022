using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models.CommonModels
{
    public class PaginatedList<T> : List<T>
    {
        // These fields will not be present in the resulting JSON
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        /// <summary>Pagination starts from page 1</summary>
        public int PageIndex { get; set; }


        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;

            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        /// <summary>NEVER USE THIS</summary>
        public PaginatedList() {}

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = await source.CountAsync();
            int pageIndexFixed = pageIndex > 0 ? pageIndex : 1;
            var items = source.Skip((pageIndexFixed - 1) * pageSize).Take(pageSize);
            return new PaginatedList<T>(await items.ToListAsync(), count, pageIndexFixed, pageSize);
        }

        public static PaginatedList<T> Create(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count();
            int pageIndexFixed = pageIndex > 0 ? pageIndex : 1;
            var items = source.Skip((pageIndexFixed - 1) * pageSize).Take(pageSize);
            return new PaginatedList<T>(items.ToList(), count, pageIndexFixed, pageSize);
        }
    }
}
