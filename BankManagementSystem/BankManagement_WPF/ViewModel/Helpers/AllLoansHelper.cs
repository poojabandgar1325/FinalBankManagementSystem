using BankManagement_WPF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_WPF.ViewModel.Helpers
{
    class AllLoansHelper
    {
        public const string BASE_URL = "http://localhost:5000/api/";
        public const string GET_URL = "Loan/all";

        public static async Task<IEnumerable<LoanDetail>> GetLoanDetails()
        {
            List<LoanDetail> loanDetail;
            string URL = BASE_URL + string.Format(GET_URL);

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(URL);
                string json = await response.Content.ReadAsStringAsync();
                loanDetail = JsonConvert.DeserializeObject<List<LoanDetail>>(json);
            };

            return loanDetail;
        }
    }
}
