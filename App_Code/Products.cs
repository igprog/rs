using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.IO;

/// <summary>
/// Products
/// </summary>
[WebService(Namespace = "http://rivierasplit.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Products : System.Web.Services.WebService {
    string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

    public Products () {
       
    }

    public class NewProduct {
        public Guid? productId;
        public Guid? productGroup;
        public Guid? productOwner;
        public String title;
        public String shortDescription;
        public String longDescription;
        public String address;
        public String postalCode;
        public String city;
        public String phone;
        public String email;
        public String web;
        public String price;
        public Decimal latitude;
        public Decimal longitude;
        public String image;
        public DateTime dateModified;
        public int isActive;
        public int displayType;
        public string[] gallery;
    }

    public class City {
        public string city;
    }

     [WebMethod]
    public string Init(Guid? userId) {
        try {
            if (CheckProductsLimit(userId)) {
                NewProduct x = new NewProduct();
                x.productId = null;
                x.productGroup = null;
                x.productOwner = null;
                x.title = "";
                x.shortDescription = "";
                x.longDescription = "";
                x.address = "";
                x.postalCode = "";
                x.city = "";
                x.phone = "";
                x.email = "";
                x.web = "";
                x.price = "";
                x.latitude = 0;
                x.longitude = 0;
                x.image = "";
                x.dateModified = new DateTime();
                x.isActive = 1;
                x.displayType = 0;
                return JsonConvert.SerializeObject(x, Formatting.Indented);
            } else {
                return JsonConvert.SerializeObject("product limit exceeded", Formatting.Indented);
            }
                
        } catch (Exception e) {
            return e.Message;
        }
    }

    [WebMethod]
    public string GetAllProducts() {
        try {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products", connection);
            SqlDataReader reader = command.ExecuteReader();
            List<NewProduct> xx = new List<NewProduct>();
            while (reader.Read()) {
                NewProduct x = ReadProductData(reader);
                xx.Add(x);
            }
            connection.Close();
            return JsonConvert.SerializeObject(xx, Formatting.Indented);
        } catch (Exception e) {
            return JsonConvert.SerializeObject(e.Message, Formatting.Indented);
        }
    }


    [WebMethod]
    public string GetAllProductsByUserId(Guid userId) {
        try {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products WHERE @ProductOwner = ProductOwner", connection);
            command.Parameters.Add(new SqlParameter("ProductOwner", userId));
            SqlDataReader reader = command.ExecuteReader();
            List<NewProduct> xx = new List<NewProduct>();
            while (reader.Read()) {
                NewProduct x = ReadProductData(reader);
                xx.Add(x);
            }
            connection.Close();
            return JsonConvert.SerializeObject(xx, Formatting.Indented);
        } catch (Exception e) {
            return JsonConvert.SerializeObject(e.Message, Formatting.Indented);
        }
    }

    [WebMethod]
    public string GetProductByProductId(string productId) {
        try {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = string.Format(@"SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products WHERE ProductId = '{0}'", productId);
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NewProduct x = new NewProduct();
            while (reader.Read()) {
                x = ReadProductData(reader);
            }
            reader.Close();
            connection.Close();
            return JsonConvert.SerializeObject(x, Formatting.Indented);
        } catch(Exception e) {
            return JsonConvert.SerializeObject(e.Message, Formatting.Indented);
        }
    }

    [WebMethod]
    public string GetProductsByDisplayType(int displayType) {
        try {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products WHERE @DisplayType = DisplayType", connection);
            command.Parameters.Add(new SqlParameter("DisplayType", displayType));
            SqlDataReader reader = command.ExecuteReader();
            List<NewProduct> xx = new List<NewProduct>();
            while (reader.Read()) {
                NewProduct x = ReadProductData(reader);
                xx.Add(x);
            }
            connection.Close();
            return JsonConvert.SerializeObject(xx, Formatting.Indented);
        } catch (Exception e) {
            return JsonConvert.SerializeObject(e.Message, Formatting.Indented);
        }
    }

    [WebMethod]
    public string SaveProduct(NewProduct product, Users.NewUser user) {
        if (product.productId == null) {
            return Save(product, user);
        } else {
            return Update(product);
        }
    }

    [WebMethod]
    public string Save(NewProduct product, Users.NewUser user) {
        try {
            product.productId = Guid.NewGuid();
            product.productOwner = user.userId;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = @"INSERT INTO Products VALUES
                        (@ProductId, @ProductGroup, @ProductOwner, @Title, @ShortDescription, @LongDescription, @Address, @PostalCode, @City, @Phone, @Email, @Web, @Price, @Latitude, @Longitude, @Image, @DateModified, @IsActive, @DisplayType)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductId", product.productId));
            command.Parameters.Add(new SqlParameter("ProductGroup", product.productGroup));
            command.Parameters.Add(new SqlParameter("ProductOwner", product.productOwner));
            command.Parameters.Add(new SqlParameter("Title", product.title));
            command.Parameters.Add(new SqlParameter("ShortDescription", product.shortDescription));
            command.Parameters.Add(new SqlParameter("LongDescription", product.longDescription));
            command.Parameters.Add(new SqlParameter("Address", product.address));
            command.Parameters.Add(new SqlParameter("PostalCode", product.postalCode));
            command.Parameters.Add(new SqlParameter("City", product.city));
            command.Parameters.Add(new SqlParameter("Phone", product.phone));
            command.Parameters.Add(new SqlParameter("Email", product.email));
            command.Parameters.Add(new SqlParameter("Web", product.web));
            command.Parameters.Add(new SqlParameter("Price", product.price));
            command.Parameters.Add(new SqlParameter("Latitude", product.latitude));
            command.Parameters.Add(new SqlParameter("Longitude", product.longitude));
            command.Parameters.Add(new SqlParameter("Image", product.image));
            command.Parameters.Add(new SqlParameter("DateModified", DateTime.Now));
            command.Parameters.Add(new SqlParameter("IsActive", product.isActive));
            command.Parameters.Add(new SqlParameter("DisplayType", product.displayType));
            command.ExecuteNonQuery();
            connection.Close();
            return JsonConvert.SerializeObject(product, Formatting.Indented);
        } catch (Exception e) {
            return string.Format(@"Error! Product not saved. ({0})", e.Message);
        }
    }

    [WebMethod]
    public string Update(NewProduct product) {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        try { 
             string sql = @"
                            UPDATE Products SET
                            [ProductGroup] = @ProductGroup,
                            [Title] = @Title,
                            [ShortDescription] = @ShortDescription,
                            [LongDescription] = @LongDescription,
                            [Address] = @Address,
                            [PostalCode] = @PostalCode,
                            [City] = @City,
                            [Phone] = @Phone,
                            [Email] = @Email,
                            [Web] = @Web,
                            [Price] = @Price,
                            [Latitude] = @Latitude,
                            [Longitude] = @Longitude,
                            [Image] = @Image,
                            [DateModified] = @Datemodified,
                            [IsActive] = @IsActive,
                            [DisplayType] = @DisplayType
                            WHERE [ProductId] = @ProductId";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductId", product.productId));
            command.Parameters.Add(new SqlParameter("ProductGroup", product.productGroup));
            //command.Parameters.Add(new SqlParameter("ProductOwner", product.productOwner));
            command.Parameters.Add(new SqlParameter("Title", product.title));
            command.Parameters.Add(new SqlParameter("ShortDescription", product.shortDescription));
            command.Parameters.Add(new SqlParameter("LongDescription", product.longDescription));
            command.Parameters.Add(new SqlParameter("Address", product.address));
            command.Parameters.Add(new SqlParameter("PostalCode", product.postalCode));
            command.Parameters.Add(new SqlParameter("City", product.city));
            command.Parameters.Add(new SqlParameter("Phone", product.phone));
            command.Parameters.Add(new SqlParameter("Email", product.email));
            command.Parameters.Add(new SqlParameter("Web", product.web));
            command.Parameters.Add(new SqlParameter("Price", product.price));
            command.Parameters.Add(new SqlParameter("Latitude", product.latitude));
            command.Parameters.Add(new SqlParameter("Longitude", product.longitude));
            command.Parameters.Add(new SqlParameter("Image", product.image));
            command.Parameters.Add(new SqlParameter("DateModified", DateTime.Now));
            command.Parameters.Add(new SqlParameter("IsActive", product.isActive));
            command.Parameters.Add(new SqlParameter("DisplayType", product.displayType));
            command.ExecuteNonQuery();
            connection.Close();
            return JsonConvert.SerializeObject(product, Formatting.Indented);
        } catch (Exception e) {
            return string.Format(@"Error! Product not updated. ({0})", e.Message);
        }
    }

    [WebMethod]
    public string Delete(Guid? productId) {
        try {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = string.Format(@"DELETE FROM Products WHERE [ProductId] = '{0}'", productId.ToString());
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
            DeleteProductFolder(productId);
            return JsonConvert.SerializeObject("Product deleted Successfully!", Formatting.Indented);
        } catch (Exception e) {
            return JsonConvert.SerializeObject(string.Format(@"Error! Product not deleted. ({0})", e.Message), Formatting.Indented);
        }
    }

    [WebMethod]
    public string GetCities() {
        try {
            List<City> xx = new List<City>();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT DISTINCT City FROM Products", connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            City x = new City();
                            x.city = reader.GetValue(0) == DBNull.Value ? "" : reader.GetString(0);
                            if (!string.IsNullOrEmpty(x.city)) {
                                xx.Add(x);
                            }
                        }
                    }
                }
                connection.Close();
                return JsonConvert.SerializeObject(xx, Formatting.Indented);
            }
        } catch (Exception e) {
            return JsonConvert.SerializeObject(e.Message, Formatting.Indented);
        }
    }

    [WebMethod]
    public string DeleteImg(Guid? productId, string img) {
        try {
            string path = Server.MapPath(string.Format("~/upload/{0}/gallery", productId));
            if (Directory.Exists(path)) {
                string[] gallery = Directory.GetFiles(path);
                foreach (string file in gallery) {
                    if (Path.GetFileName(file) == img) {
                        File.Delete(file);
                    }
                }
            }
            return JsonConvert.SerializeObject(GetGallery(productId), Formatting.Indented);
        } catch (Exception e) {
            return null;
        }
    }

    [WebMethod]
    public string LoadProductGallery(Guid? productId) {
        try {
            string[] x = GetGallery(productId);
            return JsonConvert.SerializeObject(x, Formatting.Indented);
        } catch(Exception e) {
            return null;
        }
    }

    [WebMethod]
    public string SetMainImg(Guid? productId, string img) {
        try {
            string sql = string.Format("UPDATE Products SET [Image] = '{0}' WHERE [ProductId] = '{1}'", img, productId);
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection)) {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return JsonConvert.SerializeObject("OK", Formatting.Indented);
        } catch (Exception e) {
            return JsonConvert.SerializeObject(string.Format(@"Error! Product not updated. ({0})", e.Message), Formatting.Indented);
        }
    }

    NewProduct ReadProductData(SqlDataReader reader) {
        NewProduct x = new NewProduct();
        x.productId = reader.GetGuid(0);
        x.productGroup = reader.GetGuid(1);
        x.productOwner = reader.GetGuid(2);
        x.title = reader.GetValue(3) == DBNull.Value ? "" : reader.GetString(3);
        x.shortDescription = reader.GetValue(4) == DBNull.Value ? "" : reader.GetString(4);
        x.longDescription = reader.GetValue(5) == DBNull.Value ? "" : reader.GetString(5);
        x.address = reader.GetValue(6) == DBNull.Value ? "" : reader.GetString(6);
        x.postalCode = reader.GetValue(7) == DBNull.Value ? "" : reader.GetString(7);
        x.city = reader.GetValue(8) == DBNull.Value ? "" : reader.GetString(8);
        x.phone = reader.GetValue(9) == DBNull.Value ? "" : reader.GetString(9);
        x.email = reader.GetValue(10) == DBNull.Value ? "" : reader.GetString(10);
        x.web = reader.GetValue(11) == DBNull.Value ? "" : reader.GetString(11);
        x.price = reader.GetValue(12) == DBNull.Value ? "" : reader.GetString(12);
        x.latitude = reader.GetValue(13) == DBNull.Value ? 0 : reader.GetDecimal(13);
        x.longitude = reader.GetValue(14) == DBNull.Value ? 0 : reader.GetDecimal(14);
        x.image = reader.GetValue(15) == DBNull.Value ? "" : reader.GetString(15);
        //x.dateModified = reader.GetValue(16) == DBNull.Value ? DateTime.Today : reader.GetDateTime(16);
        //x.isActive = reader.GetValue(17) == DBNull.Value ? 1 : reader.GetInt32(17);
        x.displayType = reader.GetValue(18) == DBNull.Value ? 0 : reader.GetInt32(18);
        x.gallery = GetGallery(x.productId);
        return x;
    }

    public void DeleteProductFolder(Guid? productId) {
        string path = Server.MapPath(string.Format("~/upload/{0}/", productId.ToString()));
        if (Directory.Exists(path)) {
            Directory.Delete(path, true);
        }
    }

    string[] GetGallery(Guid? productId) {
        string[] xx = null;
        string path = Server.MapPath(string.Format("~/upload/{0}/gallery", productId));
        if (Directory.Exists(path)) {
            string[] ss = Directory.GetFiles(path);
            xx = ss.Select(a => Path.GetFileName(a)).ToArray();
        }
        return xx;
    }

    private bool CheckProductsLimit(Guid? userId) {
        int count = 0;
        int productsLimit = Convert.ToInt32(ConfigurationManager.AppSettings["productsLimit"]);
        string sql = string.Format(@"SELECT COUNT(ProductId) FROM Products WHERE ProductOwner = '{0}'", userId);
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sql, connection)) {
                SqlDataReader reader = command.ExecuteReader();
                NewProduct x = new NewProduct();
                while (reader.Read()) {
                    count = reader.GetValue(0) == DBNull.Value ? 0 : reader.GetInt32(0);
                }
                reader.Close();
            }
            connection.Close();
        }
        return count < productsLimit ? true :false;
    }

}
