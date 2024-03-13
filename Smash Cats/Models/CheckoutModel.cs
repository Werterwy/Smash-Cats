using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Smash_Cats.Models
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public UserBindingModel Input { get; set; }
    }

    public class UserModel
    {
        [Display(Name = "Your name")]
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
