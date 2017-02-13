using System;

namespace RowLevelSecurity.Company.Auth
{
    public interface Authentication
    {
        void authenticate(string userName, CompanyContext context);
    }
}