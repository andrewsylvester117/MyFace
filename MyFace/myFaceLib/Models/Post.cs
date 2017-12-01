namespace MyFaceLib.Models
{
	public class Post
	{
		public int PublisherId { get; set; }
		public int Id { get; set; }
		public int Parentid { get; set; }
		public string Postheader {get; set;}
		public string Textcontent { get; set; }
		public string Imagefname { get; set; }
		public int Likecount { get; set; }
		public int Dislikecount { get; set; }

	}
}