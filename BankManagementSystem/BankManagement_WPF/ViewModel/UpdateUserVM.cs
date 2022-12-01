using BankManagement_WPF.Model;
using BankManagement_WPF.ViewModel.Commands;
using BankManagement_WPF.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankManagement_WPF.ViewModel
{
         class UpdateUserVM : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            private string name;

            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }

            private string userName;

            public string UserName
            {
                get { return userName; }
                set
                {
                    userName = value;
                    OnPropertyChanged("userName");
                }
            }

            private string password;

            public string PassWord
            {
                get { return password; }
                set
                {
                    password = value;
                    OnPropertyChanged("PassWord");
                }
            }

            private string address;

            public string Address
            {
                get { return address; }
                set
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }

            private string state;

            public string State
            {
                get { return state; }
                set
                {
                    state = value;
                    OnPropertyChanged("State");
                }
            }

            private string country;

            public string Country
            {
                get { return country; }
                set
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }

            private string emailId;

            public string EmailId
            {
                get { return emailId; }
                set
                {
                    emailId = value;
                    OnPropertyChanged("EmailId");
                }
            }

            private string pan;

            public string PAN
            {
                get { return pan; }
                set
                {
                    pan = value;
                    OnPropertyChanged("PAN");
                }
            }

            private string contactNo;

            public string ContactNo
            {
                get { return contactNo; }
                set
                {
                    contactNo = value;
                    OnPropertyChanged("ContactNo");
                }
            }


            public string EndDate { get; set; }

            private string dob;

            public string DOB
            {
                get { return dob; }
                set
                {
                    dob = value;
                    OnPropertyChanged("DOB");
                }
            }

            private string accountType;

            public string AccountType
            {
                get { return accountType; }
                set
                {
                    accountType = value;
                    OnPropertyChanged("AccountType");
                }
            }

            private string warning;

            public string Warning
            {
                get { return warning; }
                set { warning = value; OnPropertyChanged("Warning"); }
            }

        public UpdateAccountCommand updateAccountCommand { get; set; } 

            // TextBlockValidation textBlockValidation;

            public UpdateUserVM()
            {
             EndDate = DateTime.Now.ToString("dd/MM/yyyy");
             updateAccountCommand = new UpdateAccountCommand(this);
            //textBlockValidation = new TextBlockValidation();
        }

        public async void UpdateAccount()
        {
            UpdateUserHelper updateUserHelper = new UpdateUserHelper();

            string[] dates = DOB.Split(" ")[0].Split("/");
            string myDate = dates[1] + "/" + dates[0] + "/" + dates[2];

            UserDetail user = new UserDetail()
            {
                Name = Name,
                //UserName = UserName,
                Password = PassWord,
                Address = Address,
                State = State,
                Country = Country,
                Email = EmailId,
                PAN = long.Parse(PAN),
                Contact = long.Parse(contactNo),
                DOB = DateTime.Parse(myDate),
                AccountType = AccountType.Split(":")[1].Trim()
            };

            string createAccountStatus = await updateUserHelper.UpdateUser(GlobalVariables.USERNAME, user);
            

            if (createAccountStatus == "Account created Succesfully")
            {
                Warning = "Your Account Created Successfully";
            }
            else if (createAccountStatus == "User Already Exists")
            {
                Warning = "User Name is already taken";
            }
            else
            {
                Warning = "Something went wrong !!!";
            }

        }
        }
    }



