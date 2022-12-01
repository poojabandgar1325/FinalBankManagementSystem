﻿using LoginSecurity.Data;
using LoginSecurity.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public class ApplyLoanRepositry : IApplyLoanRepositry
    {
        private readonly BankManagementDbContext bankManagementDbContext;

        public ApplyLoanRepositry(BankManagementDbContext bankManagementDbContext)
        {
            this.bankManagementDbContext = bankManagementDbContext;
        }

        public async Task<List<LoanDetail>> GetAllAsync()
        {
            return await bankManagementDbContext.LoanDetails.ToListAsync();
        }

        public async Task<List<LoanDetail>> GetAllLoanAsync(string userName)
        {
            return await bankManagementDbContext.LoanDetails?.Where(x => x.UserName == userName).ToListAsync();
        }

        public async Task<LoanDetail> GetLoanAsync(int loanId)
        {
            return await bankManagementDbContext.LoanDetails?.FirstOrDefaultAsync(x => x.LoanId == loanId);
        }

        public async Task<bool> SaveLoanDeatilAsync(LoanDetail loanDetail)
        {
            try
            {
                await bankManagementDbContext.LoanDetails.AddAsync(loanDetail);
                await bankManagementDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatusAsync(int loanId, string status)
        {
            var existingUser = await bankManagementDbContext.LoanDetails.FirstOrDefaultAsync(x => x.LoanId == loanId);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Status = status;
            

            await bankManagementDbContext.SaveChangesAsync();

            return true;
        }
    }
}
