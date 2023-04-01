using AysenursBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace AysenursBlog.Controllers
{

	public class AccountController : Controller
	{
		private readonly BlogContext _context;
		private string code = null;
		public AccountController(BlogContext context)
		{
			_context = context;	
		}
		public IActionResult SignUp()
		{
			if (HttpContext.Session.GetInt32("id").HasValue)
			{
				return Redirect("Home/MainIndex");
			}
			return View();
		}
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult ForgatPassword()
		{
			if (HttpContext.Session.GetInt32("id").HasValue)
			{
				return Redirect("Home/MainIndex");
			}
			return View();
		}
		public IActionResult ResetPassword()
		{
			if (HttpContext.Session.GetInt32("id").HasValue)
			{
				return Redirect("Home/MainIndex");
			}
			return View();
		}

		public IActionResult ResetPasswordCode(string code, string NewPassword)
		{
			var passwordcode = _context.PasswordCode.FirstOrDefault(w => w.Code.Equals(code));
			if (passwordcode != null) 
			{
				var authhor = _context.Author.Find(passwordcode.AuthorId);
				authhor.Password = NewPassword;
				_context.Update(authhor);
				_context.Remove(passwordcode);
				_context.SaveChanges();
				return RedirectToAction("Index", "Home");
			}
			return RedirectToAction("Index", "Home");
		}
		public IActionResult SendCode(string Email)
		{
			var author = _context.Author.FirstOrDefault(w => w.Email.Equals(Email));
			if (author != null)
			{
				_context.Add(new PasswordCode { AuthorId = author.Id, Code = getCode() });
				_context.SaveChanges();
				string text = "<h1>Sıfırlama için kodunuz:</h1>" + getCode() + " ";
				string subject = "Parola sıfırlama";
				MailMessage msg = new MailMessage("sdevelopers.jr@gmail.com",Email,subject,text);
				msg.IsBodyHtml = true;	
				SmtpClient sc = new SmtpClient("smtp.gmail.com",587);
				sc.UseDefaultCredentials= false;
				NetworkCredential cre = new NetworkCredential("sdevelopers.jr@gmail.com", "*****");
				sc.Credentials = cre;
				sc.EnableSsl = true;
				sc.Send(msg);
				return Redirect("ResetPassword");
			}
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Register(Author model)
		{
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			
        }
		public string getCode()
		{
			if (code == null)
			{
				Random rand = new Random();
				code = "";
				for (int i = 0; i < 6; i++)
				{
					char tmp = Convert.ToChar(rand.Next(48, 58));
					code += tmp;
				}
			}
			return code;
		}

       
    }
}
