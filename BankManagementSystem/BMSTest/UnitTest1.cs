using LoginSecurity.Data;
using LoginSecurity.Models.Domains;
using LoginSecurity.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSTest
{

    [TestFixture]
    public class Test
    {
        private LoanDetail loanDetail_one;
        private LoanDetail loanDetail_two;



        public Test()
        {
            loanDetail_one = new LoanDetail()
            {
                LoanId = 1,
                LoanAmount = 100000,
                LoanDate = new DateTime(2022, 1, 1),
                LoanDuration = 6,
                LoanType = "Car",
                RateOfInterest = 10,
                Status = "Pending",
                UserName = "test"
            };



            loanDetail_two = new LoanDetail()
            {
                LoanId = 2,
                LoanAmount = 100500,
                LoanDate = new DateTime(2022, 10, 1),
                LoanDuration = 6,
                LoanType = "Home",
                RateOfInterest = 10,
                Status = "Pending",
                UserName = "test"
            };
        }
        [Test]
        public void SaveLoanDeatilAsync_Success_CheckValueFromDb()
        {
            //arrange
            var options = new DbContextOptionsBuilder<BankManagementDbContext>()
                .UseInMemoryDatabase("tempLoan").Options;



            //act
            using (var context = new BankManagementDbContext(options))
            {
                var repo = new ApplyLoanRepositry(context);
                _ = repo.SaveLoanDeatilAsync(loanDetail_one);
            }



            //assert
            using (var context = new BankManagementDbContext(options))
            {
                var loanFromDb = context.LoanDetails.FirstOrDefault(x => x.LoanId == loanDetail_one.LoanId);
                Assert.AreEqual(loanDetail_one.LoanId, loanFromDb.LoanId);
                Assert.AreEqual(loanDetail_one.LoanAmount, loanFromDb.LoanAmount);
                Assert.AreEqual(loanDetail_one.LoanDate, loanFromDb.LoanDate);
                Assert.AreEqual(loanDetail_one.LoanDuration, loanFromDb.LoanDuration);
                Assert.AreEqual(loanDetail_one.LoanType, loanFromDb.LoanType);
                Assert.AreEqual(loanDetail_one.UserName, loanFromDb.UserName);
            }
        }

        //[Test]
        //public void DateValidator_InputExpectedDateRange_DateValidity()
        //{
        //    TextBlockValidation textBlockValidation = new(() => DateTime.Now);

        //    var result = textBlockValidation.FutureDateValidation(DateTime.Now.AddSeconds(100));

        //    Assert.AreEqual(false, result);
        //}
    }
}
