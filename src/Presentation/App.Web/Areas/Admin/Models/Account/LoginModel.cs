using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Areas.Admin.Models.Account
{

    public partial class LoginModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("RememberMe")]
        public bool RememberMe { get; set; }
    }
}
