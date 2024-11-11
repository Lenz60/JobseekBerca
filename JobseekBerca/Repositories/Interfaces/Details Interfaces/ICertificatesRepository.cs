using JobseekBerca.Models;
using JobseekBerca.ViewModels;

namespace JobseekBerca.Repositories.Interfaces
{
    public interface ICertificatesRepository
    {
        public IEnumerable<Certificates> GetCertificateById(string userId);
        public int CreateCertificate(Certificates certificate);
        public int UpdateCertificate(Certificates certificates);
        public int DeleteCertificate(DetailsVM.DeleteVM certificate);
    }
}
