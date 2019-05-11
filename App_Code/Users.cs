using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using Newtonsoft.Json;

[WebService(Namespace = "http://rivierasplit.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Users : System.Web.Services.WebService {

    public Users () {
    }

    public class NewUser {
        public Guid? userId;
        public Int32 userType;
        public String firstName;
        public String lastName;
        public String companyName;
        public String address;
        public String postalCode;
        public String city;
        public String country;
        public String pin;
        public String email;
        public String userName;
        public String password;
        public Int32 adminType;
        public Guid? userGroupId;
        public DateTime? activationDate;
        public DateTime? expirationDate;
        public Int32 isActive;
        public String ipAddress;
    }

    [WebMethod]
    public string Init() {
        NewUser user = new NewUser();
        user.userId = null;
        user.userType = 1;
        user.firstName = "";
        user.lastName = "";
        user.companyName = "";
        user.address = "";
        user.postalCode = "";
        user.city = "";
        user.country = "";
        user.pin = "";
        user.email = "";
        user.userName = "";
        user.password = "";
        user.adminType = 1;
        user.userGroupId = null;
        user.activationDate = null;
        user.expirationDate = null; ;
        user.isActive = 0;
        user.ipAddress = "";
        return JsonConvert.SerializeObject(user, Formatting.Indented);
    }

    [WebMethod]
    public string Login(string userName, string password) {
        try {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(
                  "SELECT UserId, UserType, FirstName, LastName, CompanyName, Address, PostalCode, City, Country, Pin, Email, UserName, Password, AdminType, UserGroupId, ActivationDate, ExpirationDate, IsActive, IPAddress FROM Users WHERE UserName = @UserName AND Password = @Password ", connection);
            command.Parameters.Add(new SqlParameter("UserName", userName));
            command.Parameters.Add(new SqlParameter("Password", Encrypt(password)));
            NewUser user = new NewUser();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                user.userId = reader.GetGuid(0);
                user.userType = reader.GetInt32(1);
                user.firstName = reader.GetString(2);
                user.lastName = reader.GetString(3);
                user.companyName = reader.GetString(4);
                user.address = reader.GetString(5);
                user.postalCode = reader.GetString(6);
                user.city = reader.GetString(7);
                user.country = reader.GetString(8);
                user.pin = reader.GetString(9);
                user.email = reader.GetString(10);
                user.userName = reader.GetString(11);
                user.password = "";
                user.adminType = reader.GetInt32(13);
                user.userGroupId = reader.GetGuid(14);
                user.activationDate = reader.GetDateTime(15);
                user.expirationDate = reader.GetDateTime(16);
                user.isActive = reader.GetInt32(17);
                user.ipAddress = reader.GetString(18);
            }
            connection.Close();
            return JsonConvert.SerializeObject(user, Formatting.Indented);
        } catch (Exception e) {
            return e.Message;
        }
        
    }

    public NewUser CheckUser(string userName, string password) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(
              "SELECT UserId, UserType, FirstName, LastName, CompanyName, Address, PostalCode, City, Country, Pin, Email, UserName, Password, AdminType, UserGroupId, ActivationDate, ExpirationDate, IsActive, IPAddress FROM Users WHERE UserName = @UserName AND Password = @Password ", connection);
        command.Parameters.Add(new SqlParameter("UserName", userName));
        command.Parameters.Add(new SqlParameter("Password", Encrypt(password)));
        NewUser user = new NewUser();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            user.userId = reader.GetGuid(0);
            user.userType = reader.GetInt32(1);
            user.firstName = reader.GetString(2);
            user.lastName = reader.GetString(3);
            user.companyName = reader.GetString(4);
            user.address = reader.GetString(5);
            user.postalCode = reader.GetString(6);
            user.city = reader.GetString(7);
            user.country = reader.GetString(8);
            user.pin = reader.GetString(9);
            user.email = reader.GetString(10);
            user.userName = reader.GetString(11);
            user.password = "";
            user.adminType = reader.GetInt32(13);
            user.userGroupId = reader.GetGuid(14);
            user.activationDate = reader.GetDateTime(15);
            user.expirationDate = reader.GetDateTime(16);
            user.isActive = reader.GetInt32(17);
            user.ipAddress = reader.GetString(18);
        }
        connection.Close();

        return user;
    }

    protected string Encrypt(string clearText) {
        string EncryptionKey = "MDOLD54FLSK5123";  // "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create()) {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream()) {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    protected string Decrypt(string cipherText) {
        string EncryptionKey = "MDOLD54FLSK5123";  // "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create()) {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream()) {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    [WebMethod]
    public string Signup(NewUser user) {
        try {
            if (checkUser(user.email) == false) {
                return ("The user is already registered.");
            } else {
                user.userId = Guid.NewGuid();
                user.userGroupId = user.userId;
                user.password = Encrypt(user.password);
                user.activationDate = DateTime.Now;
                user.expirationDate = user.activationDate;

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
                connection.Open();

                string sql = @"INSERT INTO Users VALUES  
                       (@UserId, @UserType, @FirstName, @LastName, @CompanyName, @Address, @PostalCode, @City, @Country, @Pin, @Email, @UserName, @Password, @AdminType, @UserGroupId, @ActivationDate, @ExpirationDate, @IsActive, @IPAddress)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Add(new SqlParameter("UserId", user.userId));
                command.Parameters.Add(new SqlParameter("UserType", user.userType));
                command.Parameters.Add(new SqlParameter("FirstName", user.firstName));
                command.Parameters.Add(new SqlParameter("LastName", user.lastName));
                command.Parameters.Add(new SqlParameter("CompanyName", user.companyName));
                command.Parameters.Add(new SqlParameter("Address", user.address));
                command.Parameters.Add(new SqlParameter("PostalCode", user.postalCode));
                command.Parameters.Add(new SqlParameter("City", user.city));
                command.Parameters.Add(new SqlParameter("Country", user.country));
                command.Parameters.Add(new SqlParameter("Pin", user.pin));
                command.Parameters.Add(new SqlParameter("Email", user.email));
                command.Parameters.Add(new SqlParameter("UserName", user.userName));
                command.Parameters.Add(new SqlParameter("Password", user.password));
                command.Parameters.Add(new SqlParameter("adminType", user.adminType));
                command.Parameters.Add(new SqlParameter("UserGroupId", user.userGroupId));
                command.Parameters.Add(new SqlParameter("ActivationDate", user.activationDate));
                command.Parameters.Add(new SqlParameter("ExpirationDate", user.expirationDate));
                command.Parameters.Add(new SqlParameter("IsActive", user.isActive));
                command.Parameters.Add(new SqlParameter("IPAddress", user.ipAddress));
                command.ExecuteNonQuery();
                connection.Close();
                return ("Registration completed successfully.");
            }
        } catch (Exception e) {
            return ("Registration failed! (Error: )" + e.Message);
        }
    }

    protected bool checkUser(string email) {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(
                "SELECT Email FROM Users WHERE Email = @Email ", connection);

            command.Parameters.Add(new SqlParameter("Email", email));

            string userEmail = "";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                userEmail = reader.GetString(0);
            }
            connection.Close();

            if (userEmail == email) {
                return false;
            }
            return true;
        }



    
}
