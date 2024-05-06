namespace E_Commerce.API.Helper
{
	public class Pagination<T>
	{
		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public int Count { get; set; } = 0;
		public IReadOnlyList<T> Data { get; set; }
	}
}
