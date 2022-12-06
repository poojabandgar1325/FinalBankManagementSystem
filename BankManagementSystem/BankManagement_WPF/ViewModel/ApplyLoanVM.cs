﻿using BankManagement_WPF.Model;
using BankManagement_WPF.Validations;
using BankManagement_WPF.View;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel
{
    class ApplyLoanVM :  INotifyPropertyChanged
    {

        private string loanType;

        public string LoanType
        {
            get { return loanType; }
            set 
            { 
                loanType = value;
                OnPropertyChanged("LoanType");
            }
        }

        private string loanAmount;

        public string LoanAmount
        {
            get { return loanAmount; }
            set { loanAmount = value; OnPropertyChanged("LoanAmount"); }
        }

        private string loanDate;

        public string LoanDate
        {
            get { return loanDate; }
            set { loanDate = value; OnPropertyChanged("LoanDate"); }
        }

        private string roi;

        public string ROI
        {
            get { return roi; }
            set { roi = value; OnPropertyChanged("ROI"); }
        }

        private string loanDuration;

        public string LoanDuration
        {
            get { return loanDuration; }
            set
            { 
                loanDuration = value;
                 
                OnPropertyChanged("LoanDuration");
            }
        }

        private string warning;

        public string Warning
        {
            get { return warning; }
            set { warning = value; OnPropertyChanged("Warning"); }
        }

        TextBlockValidation textBlockValidation;
        
        public ApplyLoanCommand ApplyLoanCommand { get; set; }
        public PreviousAppliedLoansCommand PreviousAppliedLoansCommand { get; set; }

        public ApplyLoanVM()
        {
            //Session Check
            LoanDate = DateTime.Today.ToString("MM/dd/yyyy").Replace('-','/');
            ROI = "0";
            PreviousAppliedLoansCommand = new PreviousAppliedLoansCommand(this);
            ApplyLoanCommand = new ApplyLoanCommand(this);
            textBlockValidation = new TextBlockValidation();
        }

        public void PreviousAppliedLoanWindowOpen()
        {
            PreviousAppliedLoansWindow loansWindow = new PreviousAppliedLoansWindow();
            loansWindow.ShowDialog();
        }

        public async void CreateNewLoan()
        {
            //validation
            if(string.IsNullOrEmpty(LoanType) || string.IsNullOrEmpty(LoanAmount) || string.IsNullOrEmpty(LoanDate) || string.IsNullOrEmpty(LoanDuration))
            {
                Warning = "All fields are mandatory";
                return;
            }

            if(LoanAmount.Any(char.IsLetter) || LoanAmount.Any(char.IsSymbol) || LoanAmount.Any(char.IsPunctuation))
            {
                Warning = "Loan Amount should be numbers only";
                return;
            }

            if (textBlockValidation.FutureDateValidation(LoanDate))
            {
                Warning = "Date should not be future date";
                return;
            }

            if (LoanDuration.Any(char.IsLetter) || LoanDuration.Any(char.IsSymbol) || LoanDuration.Any(char.IsPunctuation))
            {
                Warning = "Loan Duration should be numbers only";
                return;
            }

            float duration = float.Parse(LoanDuration);
            ROI = (duration/6).ToString();

            string[] dates = LoanDate.Split(" ")[0].Split("/");
            string myDate = dates[1] + "/" + dates[0] + "/" + dates[2];

            LoanDetail loan = new LoanDetail()
            {
                UserName = GlobalVariables.USERNAME,
                LoanType = LoanType.Split(":")[1].Trim(),
                LoanDate = DateTime.Parse(myDate),
                LoanAmount = double.Parse(LoanAmount),
                LoanDuration = int.Parse(LoanDuration),
                RateOfInterest = float.Parse(ROI),                
            };

            string status = await ApplyLoanHelper.CreateLoan(loan);

            Warning = status;
        }       

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
