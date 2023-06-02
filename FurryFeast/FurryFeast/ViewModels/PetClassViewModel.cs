namespace FurryFeast.ViewModels
{
	public class PetClassViewModel
	{
		public int PetClassId { get; set; }
		public string PetClassName { get; set; } = null!;
		public int PetClassPrice { get; set; }
		public string PetClassInformation { get; set; } = null!;
		public DateTime PetClassDate { get; set; }
		public int TeacherId { get; set; }
		public int PetTypesId { get; set; }
		public int PetClassTypeId { get; set; }
		public string PetClassTypeName { get; set; }
		public string TeacherName { get; set; }

        public byte[] PetClassPics { get; set; }
    }
}
