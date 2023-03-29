using System.ComponentModel.DataAnnotations;

namespace AysenursBlog.Models

{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field cannot be left blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field cannot be left blank")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        [RegularExpression(@"/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&.])[A-Za-z\d$@$!%*?&.]{6, 15}/",
            ErrorMessage = "En az 1 büyük, 1 küçük, 1 özel karakter ve en falza 15 karakter giriniz.")]
        public string Password { get; set; }
    }
}
