using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbManager.Security
{
    public class Manager
    {
        public List<Profile> Profiles { get; private set; } = new List<Profile>();

        private string m_username;
        public Manager(string username)
        {
            m_username = username;
        }

        public bool IsUserAdmin()
        {
            //TODO DEADLINE 5: Return true if the user logged-in (m_username) is the admin, false otherwise
            
            var profile = ProfileByUser(m_username);
            if (profile == null) 
            {
                return false;
            }
            return profile.Name==Profile.AdminProfileName;
            
        }

        public bool IsPasswordCorrect(string username, string password)
        {
            //TODO DEADLINE 5: Return true if the user's password is correct. The given password should be encrypted before comparing with the saved one
            
            foreach (var profile in Profiles) 
            {
                foreach (var user in profile.Users) 
                {
                    if (user.Username == username && user.EncryptedPassword == Encryption.Encrypt(password))
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }

        public void GrantPrivilege(string profileName, string table, Privilege privilege)
        {
            //TODO DEADLINE 5: Add this privilege on this table to the profile with this name
            //If the profile or the table don't exist, do nothing
            if (!IsUserAdmin())
            {
                return;
            }
            foreach (var profile in Profiles)
            {
                if (profile.Name == profileName && table!=null) 
                {
                    profile.GrantPrivilege(table, privilege);
                }
            }
        }

        public void RevokePrivilege(string profileName, string table, Privilege privilege)
        {
            //TODO DEADLINE 5: Remove this privilege on this table to the profile with this name
            //If the profile or the table don't exist, do nothing
            
            if (string.IsNullOrEmpty(profileName) || string.IsNullOrEmpty(table))
            {
                return;
            }
			foreach (var profile in Profiles)
			{
				if (profile.Name == profileName)
				{
					profile.RevokePrivilege(table, privilege);
                    return;
				}
			}
            
		}

        public bool IsGrantedPrivilege(string username, string table, Privilege privilege)
        {
            //TODO DEADLINE 5: Return true if the username has this privilege on this table. False otherwise (also in case of error)
            
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(table))
			{
				return false;
			}
            Profile profile = ProfileByUser(username);
            if (profile == null)
            {
                return false;
            }
            return profile.IsGrantedPrivilege(table, privilege);
            
		}

        public void AddProfile(Profile profile)
        {
            //TODO DEADLINE 5: Add this profile
            if (profile == null)
            {
                return;
            }
            if (ProfileByName(profile.Name) != null)
            {
                return;
            }
            Profiles.Add(profile);
        }

        public User UserByName(string username)
        {
            //TODO DEADLINE 5: Return the user by name. If it doesn't exist, return null
            foreach (var profile in Profiles)
            {
                foreach (var user in profile.Users)
                {
                    if (user.Username == username)
                    {
                        return user;
                    }
                }
            }
            return null;
		}

        public Profile ProfileByName(string profileName)
        {
            //TODO DEADLINE 5: Return the profile by name. If it doesn't exist, return null
            foreach (var profile in Profiles)
            {
                if (profile.Name == profileName)
                {
                    return profile;
                }
            }
            return null;
		}

        public Profile ProfileByUser(string username)
        {
            //TODO DEADLINE 5: Return the profile by user. If the user doesn't exist, return null
            foreach (var profile in Profiles)
            {
                foreach (var user in profile.Users)
                {
                    if (user.Username == username)
                    {
                        return profile;
                    }
                }
            }
                return null;
		}

        public bool RemoveProfile(string profileName)
        {
            //TODO DEADLINE 5: Remove this profile
            var profile = ProfileByName(profileName);
            if (profile == null)
            {
                return false;
            }
            Profiles.Remove(profile);
            return true;
        }

        public static Manager Load(string databaseName, string username)
        {
            var securityFile = databaseName + ".security";
            if (!File.Exists(securityFile)) return null;

            var manager = new Manager(username);
            Profile currentProfile = null;

            using (var reader = File.OpenText(securityFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("PROFILE:"))
                    {
                        currentProfile = new Profile { Name = line.Substring("PROFILE:".Length) };
                        manager.Profiles.Add(currentProfile);
                    }
                    else if (line.StartsWith("USER:") && currentProfile != null)
                    {
                        var parts = line.Substring("USER:".Length).Split(':', 2);
                        currentProfile.Users.Add(new User { Username = parts[0], EncryptedPassword = parts[1] });
                    }
                    else if (line.StartsWith("PRIVILEGE:") && currentProfile != null)
                    {
                        var rest = line.Substring("PRIVILEGE:".Length);
                        var colonIdx = rest.IndexOf(':');
                        var tableName = rest.Substring(0, colonIdx);
                        var privilegeNames = rest.Substring(colonIdx + 1).Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (var privName in privilegeNames)
                        {
                            if (Enum.TryParse<Privilege>(privName, out var priv))
                                currentProfile.GrantPrivilege(tableName, priv);
                        }
                    }
                }
            }
            return manager;
        }

        public void Save(string databaseName)
        {
            using (var writer = File.CreateText(databaseName + ".security"))
            {
                foreach (var profile in Profiles)
                {
                    writer.WriteLine("PROFILE:" + profile.Name);
                    foreach (var user in profile.Users)
                    {
                        writer.WriteLine("USER:" + user.Username + ":" + user.EncryptedPassword);
                    }
                    foreach (var entry in profile.PrivilegesOn)
                    {
                        string privileges = "";

                        foreach (var privilege in entry.Value)
                        {
                            if (privileges != "")
                            {
                                privileges += ",";
                            }

                            privileges += privilege.ToString();
                        }
                        writer.WriteLine("PRIVILEGE:" + entry.Key + ":" + privileges);
                    }
                }
            }
        }
    }
}
