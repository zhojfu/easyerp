using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    using System.Collections;

    using Infrastructure.Domain.Model;

    public class PagedResult<T> : ICollection<T> where T : IAggregateRoot
    {
        public PagedResult(int pageSize, int pageNumber, int totalPages, int totalRecords, List<T> data)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.Data = data;
        }

        public int Count 
        {
            get
            {
                return this.Data.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public List<T> Data { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            this.Data.Add(item); 
        }

        public void Clear()
        {
            this.Data.Clear(); 
        }

        public bool Contains(T item)
        {
            return this.Data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Data.CopyTo(array, arrayIndex); 
        }

        public bool Remove(T item)
        {
            return this.Data.Remove(item);
        }
    }
}
