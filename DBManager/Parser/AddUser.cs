using DbManager.Parser;
using DbManager.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbManager
{
 
    public class AddUser : MiniSqlQuery
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string ProfileName { get; private set; }


        public AddUser(string username, string password, string profileName)
        {
          //TODO DEADLINE 4: Initialize member variables
          Username = username;
          Password = password;
          ProfileName = profileName;
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, SecurityProfileDoesNotExistError, AddUserSuccess
            var profile = database.SecurityManager.ProfileByName(ProfileName);
            if (!database.SecurityManager.IsUserAdmin())
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }
            if (profile == null)
            {
                return Constants.SecurityProfileDoesNotExistError;
            }
            profile.Users.Add(new User(Username, Encryption.Encrypt(Password)));
            return Constants.AddUserSuccess;
            
        }

    }
}
