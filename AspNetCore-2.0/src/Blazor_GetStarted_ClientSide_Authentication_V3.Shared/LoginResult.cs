using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor_GetStarted_ClientSide_Authentication_V3.Shared
{

    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
