namespace Infrastructure.Utility
{
    using System.Collections;
    using System.Collections.Generic;

    public class PagedResult<T> : ICollection<T>
    {
        public PagedResult(int pageSize, int pageNumber, int totalPages, int totalRecords, List<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = totalPages;
            Data = data;
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public List<T> Data { get; set; }

        public int Count
        {
            get { return Data.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Data.Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(T item)
        {
            return Data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Data.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return Data.Remove(item);
        }
    }
}