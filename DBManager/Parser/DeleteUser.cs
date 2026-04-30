using DbManager.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbManager
{
 
    public class DeleteUser : MiniSqlQuery
    {
        public string Username { get; private set; }

        public DeleteUser(string username)
        {
            //TODO DEADLINE 4: Initialize member variables
            Username = username;
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, UserDoesNotExistError, DeleteUserSuccess
            var profile = database.SecurityManager.ProfileByUser(Username);//si profile by user devuelve null sabemos que no existe el usuario
            if (!database.SecurityManager.IsUserAdmin())
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }
            if (profile == null)
            {
                return Constants.UserDoesNotExistError;
            }
            var user = database.SecurityManager.UserByName(Username);
            profile.Users.Remove(user);
            return Constants.DeleteUserSuccess;
            //return null;
        }
    }
}
