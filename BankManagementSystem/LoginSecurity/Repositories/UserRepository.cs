﻿using LoginSecurity.Data;
using LoginSecurity.JwtToken;
using LoginSecurity.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSecurity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BankManagementDbContext bankManagementDbContext;

        ITokenManager _tokenManager = new TokenManager();

        public UserRepository(BankManagementDbContext bankManagementDbContext)
        {
            this.bankManagementDbContext = bankManagementDbContext;
        }

        public async Task<int> ValidateUserCredAsync(string userName, string password)
        {
            UserDetail user = bankManagementDbContext.UserDetails?.Where(x => 
                                            x.UserName == userName &&
                                            x.Password == password)
                                            .FirstOrDefault();

            if (user != null)
            {
                string userToken = user.Token;
                bool valid = _tokenManager.ValidateToken(userToken);

                if (valid)
                    return user.Role;

                else
                {
                    string token = _tokenManager.GenerateJsonWebToken(user.UserName);
                    user.Token = token;
                    await bankManagementDbContext.SaveChangesAsync();
                    return user.Role;
                }
            }
            else
                return 2;
        }
        public async Task<UserDetail> GetUserAsync(string userName)
        {
           return await bankManagementDbContext.UserDetails?.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<bool> SaveUserDeatilAsync(UserDetail userDetail)
        {
            try
            {
                await bankManagementDbContext.UserDetails.AddAsync(userDetail);
                await bankManagementDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> EndSessionAsync(string userName)
        {
            UserDetail userDetail = await GetUserAsync(userName);
            if(userDetail != null)
            {
                userDetail.Token = "logout";
                await bankManagementDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUserSessionAsync(string userName)
        {
            UserDetail userDetail = await GetUserAsync(userName);
            if (userDetail != null)
            {
                return _tokenManager.ValidateToken(userDetail.Token);
            }
            return false;
        }

        public  async Task<bool> UpdateUserAsync(string userName, UserDetail userDetail)
        {
            // return await bankManagementDbContext.UserDetails.FirstOrDefaultAsync(x => x.UserName == username);
           var existingUser= await bankManagementDbContext.UserDetails.FirstOrDefaultAsync(x => x.UserName == userName);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = userDetail.Name;
            existingUser.Password = userDetail.Password;
            existingUser.Address = userDetail.Address;
            existingUser.State = userDetail.State;
            existingUser.Country = userDetail.Country;
            existingUser.Email = userDetail.Email;
            existingUser.PAN = userDetail.PAN;
            existingUser.Contact = userDetail.Contact;
            existingUser.DOB = userDetail.DOB;
            existingUser.AccountType = userDetail.AccountType;

            await bankManagementDbContext.SaveChangesAsync();

            return true;
        }
    }
}
