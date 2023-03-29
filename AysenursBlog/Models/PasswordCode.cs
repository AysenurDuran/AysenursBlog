using System.ComponentModel.DataAnnotations;

namespace AysenursBlog.Models
{
	public class PasswordCode
	{
		public int Id { get; set; }
		public Author Author { get; set; }
		public int AuthorId { get; set; }
		[StringLength(6)]
		public string Code { get; set; }
	}
}
