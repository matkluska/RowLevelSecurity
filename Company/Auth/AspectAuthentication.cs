using System;
using RowLevelSecurity.Aspect;

namespace RowLevelSecurity.Company.Auth
{
    public class AspectAuthentication : Authentication
    {
        [Authorize]
        public void authenticate(string userName, CompanyContext context)
        {
            Console.WriteLine("Zalogowany jako " + userName);
        }
    }
}