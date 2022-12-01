using LoginSecurity.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public interface IApplyLoanRepositry
    {
        Task<LoanDetail> GetLoanAsync(int loanId);
        Task<List<LoanDetail>> GetAllLoanAsync(string userName);
        Task<List<LoanDetail>> GetAllAsync();

        Task<bool> UpdateStatusAsync(int loanId, string status);
        Task<bool> SaveLoanDeatilAsync(LoanDetail loanDetail);
    }
}
