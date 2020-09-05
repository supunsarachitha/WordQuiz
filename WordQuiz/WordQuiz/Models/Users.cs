using System;
using System.Collections.Generic;
using System.Text;

namespace WordQuiz.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; } = 0;

    }
}
