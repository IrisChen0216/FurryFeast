namespace FurryFeast.ViewModels
{
	public class Atom<T>
	{
		public bool IsSuccess { get; set; }
		public bool IsError { get; set; }
		public string Message { get; set; }
		public T Item { get; set; }
	}
}
