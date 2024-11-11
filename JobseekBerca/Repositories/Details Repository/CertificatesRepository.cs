using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var check = _myContext.Users.Find(userId);
            if (check == null)
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

        public int UpdateCertificate(Certificates certificate)
        {
            var check = CheckUserId(certificate.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {

                var checkCertficate = _myContext.Certificates.Find(certificate.certificateId);
                if (checkCertficate == null)
                {
                    return FAIL;
                }
                var newCertificate = new Certificates
                {
                    certificateId = certificate.certificateId,
                    certificateName = certificate.certificateName,
                    credentialId = certificate.credentialId,
                    credentialLink = certificate.credentialLink,
                    description = certificate.description,
                    startDate = certificate.startDate,
                    endDate = certificate.endDate,
                    userId = certificate.userId,

                };
                _myContext.Entry(checkCertficate).State = EntityState.Detached;
                _myContext.Entry(newCertificate).State = EntityState.Modified;
                return _myContext.SaveChanges();

            }
        }

        public int DeleteCertificate(DetailsVM.DeleteVM certificate)
        {
            var check = CheckUserId(certificate.userId);
            if (check == FAIL)
            {
                return FAIL;
            }
            else
            {
                var checkCertificate = _myContext.Certificates.Find(certificate.id);
                if (checkCertificate == null)
                {
                    return FAIL;
                }
                _myContext.Certificates.Remove(checkCertificate);
                return _myContext.SaveChanges();
            }
        }

        public IEnumerable<Certificates> GetCertificateById(string userId)
        {
            var check = CheckUserId(userId);
            if (check == FAIL)
            {
                return null;
            }
            else
            {
                var certificates = _myContext.Certificates.Select(Certificates => new Certificates
                {
                    certificateId = Certificates.certificateId,
                    certificateName = Certificates.certificateName,
                    credentialId = Certificates.credentialId,
                    credentialLink = Certificates.credentialLink,
                    description = Certificates.description,
                    startDate = Certificates.startDate,
                    endDate = Certificates.endDate,
                    userId = Certificates.userId
                }).Where(x => x.userId == userId).ToList();
                if (certificates == null)
                {
                    return null;
                }
                return certificates;
            }
        }
    }
}
