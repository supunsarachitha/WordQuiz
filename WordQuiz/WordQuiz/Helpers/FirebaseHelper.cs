using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordQuiz.Models;

namespace WordQuiz.Helpers
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient(Common.FirebaseURL);
        public async Task<List<Users>> GetAllPersons()
        {

            return (await firebase
              .Child("Users")
              .OnceAsync<Users>()).Select(item => new Users
              {
                  UserId = item.Object.UserId,
                  UserName = item.Object.UserName,
                  CountryName  = item.Object.CountryName,
                  CountryCode = item.Object.CountryCode,
                  Score = item.Object.Score,
                  
              }).ToList();
        }

        public async Task<bool> AddNewUser(int id, string name,string countryname,string countryCode,int score)
        {

            await firebase
              .Child("Users")
              .PostAsync(new Users() { UserId = id, UserName = name, CountryName= countryname, CountryCode=countryCode, Score=score });

            return true;
        }

        public async Task UpdateScore(int score, string USerName)
        {
            if (!string.IsNullOrEmpty(USerName))
            {
                var toUpdatePerson = (await firebase
              .Child("Users")
              .OnceAsync<Users>()).Where(a => a.Object.UserName == USerName).FirstOrDefault();

                if (toUpdatePerson != null)
                {
                    await firebase
                  .Child("Users")
                  .Child(toUpdatePerson.Key)
                  .PutAsync(new Users()
                  {
                      Score = score,
                      CountryCode = toUpdatePerson.Object.CountryCode,
                      CountryName = toUpdatePerson.Object.CountryName,
                      UserId = toUpdatePerson.Object.UserId,
                      UserName = toUpdatePerson.Object.UserName,
                      Rank = 0
                  }); ;
                }
            }
            
            
        }



    }
}
