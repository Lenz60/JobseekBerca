using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;

namespace JobseekBerca.Repositories
{
    public class CertificatesRepository : ICertificatesRepository
    {
        private readonly MyContext _myContext;

        public CertificatesRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public const int INTERNAL_ERROR = -1;
        public const int SUCCESS = 1;
        public const int FAIL = 0;

        public int CheckUserId(string userId)
        {
            var check = _myContext.Profiles.Find(userId);
            if(check == null)
            {
                return FAIL;
            }
            return SUCCESS;
        }

        public int CreateCertificate(Certificates certificate)
        {
            var check = CheckUserId(certificate.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                _myContext.Certificates.Add(certificate);
                _myContext.SaveChanges();
                return SUCCESS;
            }
        }
    }
}
