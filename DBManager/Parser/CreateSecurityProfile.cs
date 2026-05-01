using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DbManager.Parser;
using DbManager.Security;

namespace DbManager
{
 
    public class CreateSecurityProfile : MiniSqlQuery
    {
        public string ProfileName { get; set; }

        public CreateSecurityProfile(string profileName)
        {
            //TODO DEADLINE 4: Initialize member variables
            ProfileName = profileName;
        }
        public string Execute(Database database)
        {
            //TODO DEADLINE 5: Run the query and return the appropriate message
            //UsersProfileIsNotGrantedRequiredPrivilege, CreateSecurityProfileSuccess
            if (!database.SecurityManager.IsUserAdmin())
            {
                return Constants.UsersProfileIsNotGrantedRequiredPrivilege;
            }
            database.SecurityManager.AddProfile(new Profile { Name=ProfileName});
            return Constants.CreateSecurityProfileSuccess;
            
        }

    }
}
