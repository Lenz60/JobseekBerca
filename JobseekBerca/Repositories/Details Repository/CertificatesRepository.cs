using JobseekBerca.Context;
using JobseekBerca.Models;
using JobseekBerca.Repositories.Interfaces;
using JobseekBerca.ViewModels;
using Microsoft.EntityFrameworkCore;
using JobseekBerca.Helper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Server.IIS.Core;

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
                throw new HttpResponseExceptionHelper(404, "User not found");
            }
            return SUCCESS;
        }

        public int CreateCertificate(Certificates certificate)
        {
            try
            {
                CheckUserId(certificate.userId);
                _myContext.Certificates.Add(certificate);
                _myContext.SaveChanges();
                return SUCCESS;

            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);

            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public int UpdateCertificate(Certificates certificate)
        {
            try
            {
                CheckUserId(certificate.userId);
                var checkCertficate = _myContext.Certificates.Find(certificate.certificateId);
                if (checkCertficate == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Certificate not found");
                }
                var newCertificate = new Certificates
                {
                    certificateId = certificate.certificateId,
                    title = certificate.title,
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
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }

        public int DeleteCertificate(DetailsVM.DeleteVM certificate)
        {
            try
            {
                CheckUserId(certificate.userId);
                var checkCertificate = _myContext.Certificates.Find(certificate.id);
                if (checkCertificate == null)
                {
                    throw new HttpResponseExceptionHelper(404, "Certificate not found");
                }
                _myContext.Certificates.Remove(checkCertificate);
                return _myContext.SaveChanges();

            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);

            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);

            }
            //if (check == FAIL)
            //{
            //}
            //else
            //{
            //}
        }

        public IEnumerable<Certificates> GetCertificateById(string userId)
        {
            try
            {
                CheckUserId(userId);
                var certificates = _myContext.Certificates.Select(Certificates => new Certificates
                {
                    certificateId = Certificates.certificateId,
                    title = Certificates.title,
                    credentialId = Certificates.credentialId,
                    credentialLink = Certificates.credentialLink,
                    description = Certificates.description,
                    startDate = Certificates.startDate,
                    endDate = Certificates.endDate,
                    userId = Certificates.userId
                }).Where(x => x.userId == userId).ToList();
                if (certificates == null)
                {
                    throw new HttpResponseExceptionHelper(404, "No certificate found");
                }
                return certificates;
            }
            catch (HttpResponseExceptionHelper e)
            {
                throw new HttpResponseExceptionHelper(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseExceptionHelper(500, e.Message);
            }
        }
    }
}
