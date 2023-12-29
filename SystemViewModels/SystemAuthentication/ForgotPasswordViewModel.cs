using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDatabase;

namespace SystemViewModels.SystemAuthentication
{
    public class ForgotPasswordViewModel
    {
        public string Username { get; set; }
        public int Idenroll { get; set; }

        public int Idhremployeee { get; set; }

        public Login loginModel { get; set; }
    }
}
