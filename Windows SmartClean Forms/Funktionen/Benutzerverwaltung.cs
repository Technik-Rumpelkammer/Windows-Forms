using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Windows_SmartClean.Funktionen
{
    class Benutzerverwaltung
    {
        public bool Erstelle_Benutzer(string username, string password, string _Gruppe, string displayName, string description, bool canChangePwd, bool pwdExpires)
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Machine);
                UserPrincipal user = new UserPrincipal(context);
                user.SetPassword(password);
                user.DisplayName = displayName;
                user.Name = username;
                user.Description = description;
                user.UserCannotChangePassword = canChangePwd;
                user.PasswordNeverExpires = pwdExpires;
                user.Save();

                //now add user to "Users" group so it displays in Control Panel
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, _Gruppe);
                group.Members.Add(user);
                group.Save();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error msg" + ex.Message);
                return false;
            }

        }

        public string Hole_Angemeldeten_Benutzer()
        {
            SelectQuery query = new SelectQuery(@"Select * from Win32_Process");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (System.Management.ManagementObject Process in searcher.Get())
                {
                    if (Process["ExecutablePath"] != null &&
                        string.Equals(Path.GetFileName(Process["ExecutablePath"].ToString()), "explorer.exe", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] OwnerInfo = new string[2];
                        Process.InvokeMethod("GetOwner", (object[])OwnerInfo);

                        return OwnerInfo[0];
                    }
                }
            }
            return "Kein Benutzer ermittelt";
        }

        public string Hole_Ausfuehrenden_Benutzer()
        {
            return System.Environment.UserName;
        }
        
        public void Benutzer_Umbenennen(string _alterName, string _neuerName)
        {
            using (var entry = new DirectoryEntry($"WinNT://{Environment.MachineName}/{_alterName}")) entry.Rename(_neuerName);
        }
        public List<string> Hole_Alle_Benutzer(int _option)
        {
            List<string> Benutzerdaten = new List<string>();
            ManagementObjectSearcher usersSearcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_UserAccount");
            ManagementObjectCollection users = usersSearcher.Get();

            var localUsers = users.Cast<ManagementObject>().Where(
                u => (bool)u["LocalAccount"] == true &&
                     (bool)u["Disabled"] == false &&
                     (bool)u["Lockout"] == false &&
                     int.Parse(u["SIDType"].ToString()) == 1 &&
                     u["Name"].ToString() != "HomeGroupUser$");

            foreach (ManagementObject user in localUsers)
            {
                if (_option == 1)
                    Benutzerdaten.Add(user["Name"].ToString());
                if (_option == 2)
                {
                    Benutzerdaten.Add(user["Name"].ToString());
                    Benutzerdaten.Add(user["Lockout"].ToString());
                    Benutzerdaten.Add(user["Status"].ToString());
                    Benutzerdaten.Add(user["Caption"].ToString());
                    Benutzerdaten.Add(user["AccountType"].ToString());
                }

                /*Console.WriteLine("Account Type: " + user["AccountType"].ToString());
                Console.WriteLine("Caption: " + user["Caption"].ToString());
                Console.WriteLine("Description: " + user["Description"].ToString());
                Console.WriteLine("Disabled: " + user["Disabled"].ToString());
                Console.WriteLine("Domain: " + user["Domain"].ToString());
                Console.WriteLine("Full Name: " + user["FullName"].ToString());
                Console.WriteLine("Local Account: " + user["LocalAccount"].ToString());
                Console.WriteLine("Lockout: " + user["Lockout"].ToString());
                Console.WriteLine("Name: " + user["Name"].ToString());
                Console.WriteLine("Password Changeable: " + user["PasswordChangeable"].ToString());
                Console.WriteLine("Password Expires: " + user["PasswordExpires"].ToString());
                Console.WriteLine("Password Required: " + user["PasswordRequired"].ToString());
                Console.WriteLine("SID: " + user["SID"].ToString());
                Console.WriteLine("SID Type: " + user["SIDType"].ToString());
                Console.WriteLine("Status: " + user["Status"].ToString());*/
            }   //  Ende foreach
            return Benutzerdaten;
        }   //  Ende Funktion Hole_Alle_Benutzer

        public List<string> Hole_Alle_Gruppen()
        {
            List<string> myItems = new List<string>();

            DirectoryEntry machine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
            foreach (DirectoryEntry child in machine.Children)
            {
                if (child.SchemaClassName == "Group")
                {
                    myItems.Add(child.Name);
                }
            }
            return myItems;
        }

        public List<string> Hole_Gruppen_Mitglieder(string sGroupName)
        {
            List<string> myItems = new List<string>();
            GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);

            PrincipalSearchResult<Principal> oPrincipalSearchResult = oGroupPrincipal.GetMembers();

            foreach (Principal oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }
            return myItems;
        }

        public static GroupPrincipal GetGroup(string sGroupName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            return oGroupPrincipal;
        }

        public static PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Machine);
            return oPrincipalContext;
        }

        public void Benutzer_Entfernen(string _Benutzername)
        {
            DirectoryEntry localDirectory = new DirectoryEntry("WinNT://" + Environment.MachineName.ToString());
            DirectoryEntries users = localDirectory.Children;
            DirectoryEntry user = users.Find(_Benutzername);
            users.Remove(user);
        }   //  Ende Methode Benutzer_Entfernen
    }
}
