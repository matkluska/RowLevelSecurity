using System;

namespace RowLevelSecurity.Company.Auth
{
    public class NormalAuthentication : Authentication
    {
        public void authenticate(string userName, CompanyContext context)
        {
            context.Authorize(userName);
            Console.WriteLine("Zalogowany jako " + userName);
        }
    }
}