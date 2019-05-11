using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.IO;

/// <summary>
/// Summary description for Products
/// </summary>
[WebService(Namespace = "http://rivierasplit.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
[DataContract(IsReference = true)]
public class Products : System.Web.Services.WebService {

    public Products () {
       
    }

   // string GalleryImage = null;
    public class Gallery {
        public Guid GalleryOwner { set; get; }
        public string Image { set; get; }
    }


    string _InfoMessage = "";
    public string InfoMessage {
        get { return _InfoMessage; }
        set { _InfoMessage = value; }
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
        // public List<Gallery> gallery;
        public string[] gellery;
    }
    

    //public String GalleryImage { get; set; }

    //[WebMethod]
    //public String GetAllProducts() {
    //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
    //    connection.Open();
    //    SqlCommand command = new SqlCommand("SELECT ProductId, Title, Description, City, Price, Category, Latitude, Longitude FROM Products", connection);

    //    SqlDataReader reader = command.ExecuteReader();
    //    List<WebService> products = new List<WebService>();
    //    while (reader.Read()) {
    //        WebService xx = new WebService() {
    //            ProductId = reader.GetGuid(0),
    //            Title = reader.GetValue(1) == DBNull.Value ? "" : reader.GetString(1),
    //            Description = reader.GetValue(2) == DBNull.Value ? "" : reader.GetString(2),
    //            City = reader.GetValue(3) == DBNull.Value ? "" : reader.GetString(3),
    //            Price = reader.GetValue(4) == DBNull.Value ? "" : reader.GetString(4),
    //            Category = reader.GetValue(5) == DBNull.Value ? "" : reader.GetString(5),
    //            Latitude = reader.GetValue(6) == DBNull.Value ? 0 : reader.GetDecimal(6),
    //            Longitude = reader.GetValue(7) == DBNull.Value ? 0 : reader.GetDecimal(7)
    //        };
    //        products.Add(xx);
    //    }
    //    connection.Close();

    //    string json = JsonConvert.SerializeObject(products, Formatting.Indented);
    //    return json;

    //}


    [WebMethod]
    public string GetAllProducts() {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products", connection);
        SqlDataReader reader = command.ExecuteReader();
        List<NewProduct> xx = new List<NewProduct>();
        while (reader.Read()) {
            NewProduct x = ReadProductData(reader);
            xx.Add(x);
        }
        connection.Close();

        string json = JsonConvert.SerializeObject(xx, Formatting.Indented);
        return json;
    }


    [WebMethod]
    public string GetAllProductsByUserId(Guid UserId) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products WHERE @ProductOwner = ProductOwner", connection);
        command.Parameters.Add(new SqlParameter("ProductOwner", UserId));
        SqlDataReader reader = command.ExecuteReader();
        List<NewProduct> xx = new List<NewProduct>();
        while (reader.Read()) {
            NewProduct x = ReadProductData(reader);
            xx.Add(x);
        }
        connection.Close();

        string json = JsonConvert.SerializeObject(xx, Formatting.Indented);
        return json;
        //CreateFolder("~/json/");
        //WriteFile("~/json/products.json", json);
    }

    [WebMethod]
    public string GetProductByProductId(string productId) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        string sql = string.Format(@"SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products WHERE ProductId = '{0}'", productId);
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        //List<NewProduct> xx = new List<NewProduct>();
        NewProduct x = new NewProduct();
        while (reader.Read()) {
            //if (ProductId == reader.GetGuid(0).ToString()) {
                x = ReadProductData(reader);
               // xx.Add(x);
           // }
        }
        reader.Close();
        //x.gallery = GetGallery(connection, productId);
        connection.Close();

        string json = JsonConvert.SerializeObject(x, Formatting.Indented);
        return json;
        //CreateFolder("~/json/");
        //WriteFile("~/json/products.json", json);
    }

    [WebMethod]
    public string GetProductsByDisplayType(int displayType) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT ProductId, ProductGroup, ProductOwner, Title, ShortDescription, LongDescription, Address, PostalCode, City, Phone, Email, Web, Price, Latitude, Longitude, Image, DateModified, IsActive, DisplayType FROM Products WHERE @DisplayType = DisplayType", connection);
        command.Parameters.Add(new SqlParameter("DisplayType", displayType));
        SqlDataReader reader = command.ExecuteReader();
        List<NewProduct> xx = new List<NewProduct>();
        while (reader.Read()) {
            NewProduct x = ReadProductData(reader);
            //Products xx = new Products() {
            //    ProductId = reader.GetValue(0) == DBNull.Value ? "" : Convert.ToString(reader.GetGuid(0)),// reader.GetGuid(0),
            //    ProductGroup = reader.GetGuid(1),
            //    ProductOwner = reader.GetGuid(2),
            //    Title = reader.GetValue(3) == DBNull.Value ? "" : reader.GetString(3),
            //    ShortDescription = reader.GetValue(4) == DBNull.Value ? "" : reader.GetString(4),
            //    LongDescription = reader.GetValue(5) == DBNull.Value ? "" : reader.GetString(5),
            //    Address = reader.GetValue(6) == DBNull.Value ? "" : reader.GetString(6),
            //    PostalCode = reader.GetValue(7) == DBNull.Value ? "" : reader.GetString(7),
            //    City = reader.GetValue(8) == DBNull.Value ? "" : reader.GetString(8),
            //    Phone = reader.GetValue(9) == DBNull.Value ? "" : reader.GetString(9),
            //    Email = reader.GetValue(10) == DBNull.Value ? "" : reader.GetString(10),
            //    Web = reader.GetValue(11) == DBNull.Value ? "" : reader.GetString(11),
            //    Price = reader.GetValue(12) == DBNull.Value ? "" : reader.GetString(12),
            //    Latitude = reader.GetValue(13) == DBNull.Value ? 0 : reader.GetDecimal(13),
            //    Longitude = reader.GetValue(14) == DBNull.Value ? 0 : reader.GetDecimal(14),
            //    Image = reader.GetValue(15) == DBNull.Value ? "" : reader.GetString(15),
            //    DateModified = reader.GetValue(16) == DBNull.Value ? DateTime.Today : reader.GetDateTime(16),
            //    IsActive = reader.GetValue(17) == DBNull.Value ? 1 : reader.GetInt32(17),
            //    DisplayType = reader.GetValue(18) == DBNull.Value ? 0 : reader.GetInt32(18)
            //};
            xx.Add(x);
        }
        connection.Close();

        string json = JsonConvert.SerializeObject(xx, Formatting.Indented);
        return json;
    }

    [WebMethod]
    public string GetGalleryByProductId(String productId) {
          SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
          connection.Open();
          SqlCommand command = new SqlCommand("SELECT GalleryOwner, Image FROM Gallery Where GalleryOwner = @ProductId", connection);
          command.Parameters.Add(new SqlParameter("ProductId", productId));
          SqlDataReader reader = command.ExecuteReader();

          List<Gallery> gallery = new List<Gallery>();
          while (reader.Read()) {
              Gallery xx = new Gallery() {
                  GalleryOwner = reader.GetGuid(0),
                  Image = reader.GetString(1)
              };
              gallery.Add(xx);
          }
       
          connection.Close();
          string json = JsonConvert.SerializeObject(gallery, Formatting.Indented);
          return json;
    }

    //List<Gallery> GetGallery(SqlConnection connection, string productId) {

    //    SqlCommand command = new SqlCommand("SELECT GalleryOwner, Image FROM Gallery Where GalleryOwner = @ProductId", connection);
    //    command.Parameters.Add(new SqlParameter("ProductId", productId));
    //    SqlDataReader reader = command.ExecuteReader();

    //    List<Gallery> gallery = new List<Gallery>();
    //    while (reader.Read()) {
    //        Gallery xx = new Gallery() {
    //            GalleryOwner = reader.GetGuid(0),
    //            Image = reader.GetString(1)
    //        };
    //        gallery.Add(xx);
    //    }
    //    return gallery;

    //}


    [WebMethod]
    public string Save(NewProduct product) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        try {
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
            return string.Format(@"Product saved successfully!");
        }
        catch (Exception e) {
            return string.Format(@"Error! Product not saved. ({0})", e.Message);
        }
    }

    [WebMethod]
    public string Update(NewProduct product) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
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
            return string.Format(@"Product updated successfully!");
        } catch (Exception e) {
            return string.Format(@"Error! Product not updated. ({0})", e.Message);
        }
    }

    [WebMethod]
    public void Delete(NewProduct product, string productId) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        try {
            string sql = @"
                            DELETE * FROM Products WHERE
                            WHERE [ProductId] = @ProductId";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductId", new Guid(productId)));
            
            command.ExecuteNonQuery();
            connection.Close();

            _InfoMessage = string.Format(@"
                        <div class=""alert alert-success alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Product deleted Successfully!</strong>
                        </div>");

        }
        catch (Exception ex) {
            _InfoMessage = string.Format(@"
                     <div class=""alert alert-danger alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Error! Product not deleted.</strong>
                        </div>");
            return;
        }
    }

    [WebMethod]
    public string GetCities() {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT DISTINCT City FROM Products", connection);
        SqlDataReader reader = command.ExecuteReader();
        string json = "";
        string comma = "";
        while (reader.Read()) {
            if (json == "") { comma = ""; } else { comma = ","; }
            json = json + comma + "{'City':'" +
                (reader.GetValue(0) == DBNull.Value ? "" : reader.GetString(0)).ToString()
                + "'}";
        }
        json = ("[" + json + "]").Replace("'", "\"");
        connection.Close();

        return json; 

        //CreateFolder("~/json/");
        //WriteFile("~/json/cities.json", json);
    }

    [WebMethod]
    public void SaveGallery(string GalleryOwner, string Image) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        try {
            string sql = @"INSERT INTO Gallery VALUES
                        (@GalleryId, @GalleryOwner, @Image)";
            Guid GalleryId = Guid.NewGuid();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("GalleryId", GalleryId));
            command.Parameters.Add(new SqlParameter("GalleryOwner", GalleryOwner));
            command.Parameters.Add(new SqlParameter("Image", Image));
            command.ExecuteNonQuery();
            connection.Close();

            GetAllGallery();

            _InfoMessage = string.Format(@"
                        <div class=""alert alert-success alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Product saved Successfully!</strong>
                        </div>");

        }
        catch (Exception ex)
        {
            _InfoMessage = string.Format(@"
                     <div class=""alert alert-danger alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Error! Product not saved.</strong>
                        </div>");
            return;
        }
    }

    [WebMethod]
    public void GetAllGallery() {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT GalleryOwner, Image FROM Gallery", connection);
        SqlDataReader reader = command.ExecuteReader();
        List<Gallery> gallery = new List<Gallery>();
        while (reader.Read()) {
            Gallery yy = new Gallery() {
                GalleryOwner = reader.GetGuid(0),
                Image = reader.GetString(1)
            };
            gallery.Add(yy);
        }

        connection.Close();
        string json = JsonConvert.SerializeObject(gallery, Formatting.Indented);
        //CreateFolder("~/json/");
        //WriteFile("~/json/gallery.json", json);
    }


    public string GetMainImage(string productId) { 
        string mainImage = "";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT Image FROM Products WHERE ProductId = @ProductId", connection);
        command.Parameters.Add(new SqlParameter("ProductId", productId));
        SqlDataReader reader = command.ExecuteReader();
        List<Gallery> gallery = new List<Gallery>();
        while (reader.Read()) {
            mainImage = reader.GetString(0);
        }
        return mainImage;
    }

    [WebMethod]
    public void DeleteGallery(string productId) {
        string[] gallery = null;
        string galleryPath = Server.MapPath("~/upload/" + productId + "/gallery/");

        if (Directory.Exists(galleryPath)) {
            gallery = Directory.GetFiles(@galleryPath);

            foreach (string filePath in gallery) {
                   File.Delete(filePath);
            }
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("DELETE FROM Gallery WHERE [GalleryOwner] = @GalleryOwner", connection);
        command.Parameters.Add(new SqlParameter("GalleryOwner", productId));

        command.ExecuteNonQuery();
        connection.Close();
    }


    [WebMethod]
    public void DeleteImage(string productId) {
        string imagePath = "~/upload/" + productId + "/mainimage/" + GetMainImage(productId);
        if (File.Exists(Server.MapPath(imagePath))) {
            File.Delete(Server.MapPath(imagePath));
        }

        string image = "";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        string sql = @"
                        UPDATE Products SET
                        [Image] = @Image
                        WHERE [ProductId] = @ProductId";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.Add(new SqlParameter("Image", image));
        command.Parameters.Add(new SqlParameter("ProductId", productId));

        command.ExecuteNonQuery();
        connection.Close();
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
        x.gellery = GetGallery(x.productId);
        return x;
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




    //public void CreateFolder(string path) {
    //    if (!Directory.Exists(Server.MapPath(path))) {
    //        Directory.CreateDirectory(Server.MapPath(path));
    //    }
    //}

    //public void WriteFile(string path, string value) {
    //    File.WriteAllText(Server.MapPath(path), value);
    //}


    //[WebMethod]
    //public void Upload(byte[] contents, string filename)
    //{
    //    var appData = Server.MapPath("~/App_Data");
    //    var file = Path.Combine(appData, Path.GetFileName(filename));
    //    File.WriteAllBytes(file, contents);
    //}

    //[WebMethod]
    //public void UploadImages(String uploadPath, String file)
    //{


    //  //  HttpPostedFile uploadFile = file; // uploadFiles[i];
    //   //             uploadFile.SaveAs(Server.MapPath(uploadPath) + uploadFile.FileName);




    //    //if (fuUploadItems.HasFile)
    //    //{
    //    //    try
    //    //    {
    //    //        HttpFileCollection uploadFiles = Request.Files;
    //    //        for (int i = 0; i < uploadFiles.Count; i++)
    //    //        {
    //    //            HttpPostedFile uploadFile = uploadFiles[i];
    //    //            uploadFile.SaveAs(Server.MapPath(uploadPath) + uploadFile.FileName);
    //    //            //   lblMessage.Text = lblMessage.Text + ", " + uploadFile.FileName;
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        lblMessage.Text = "Dogodila se pogreška prilikom Upload-a!";
    //    //        return;
    //    //    }
    //    //}
    //}



    //    public void InsertGuest()
    //    {
    //        Properties properties = new Properties();
    //        SqlConnection connection = new SqlConnection(properties.ConnectionString);
    //        connection.Open();
    //        try
    //        {
    //            string sql = @"INSERT INTO Guests
    //                        VALUES  
    //                       (@InquireDate,
    //                        @FirstName,
    //                        @LastName,
    //                        @Email,
    //                        @Arrival,
    //                        @Departure,
    //                        @Days,
    //                        @Adults,
    //                        @Children,
    //                        @Message,
    //                        @Apartment,
    //                        @PricePerDay,
    //                        @TotalPrice,
    //                        @Deposit,
    //                        @RestToPay,
    //                        @Confirmation,
    //                        @ConfirmationDate,
    //                        @Annotation)";

    //            SqlCommand command = new SqlCommand(sql, connection);

    //            command.Parameters.Add(new SqlParameter("InquireDate", InquireDate));
    //            command.Parameters.Add(new SqlParameter("FirstName", FirstName));
    //            command.Parameters.Add(new SqlParameter("LastName", LastName));
    //            command.Parameters.Add(new SqlParameter("Email", Email));
    //            command.Parameters.Add(new SqlParameter("Arrival", Arrival));
    //            command.Parameters.Add(new SqlParameter("Departure", Departure));
    //            command.Parameters.Add(new SqlParameter("Days", Days));
    //            command.Parameters.Add(new SqlParameter("Adults", Adults));
    //            command.Parameters.Add(new SqlParameter("Children", Children));
    //            command.Parameters.Add(new SqlParameter("Message", Message));
    //            command.Parameters.Add(new SqlParameter("Apartment", Apartment));
    //            command.Parameters.Add(new SqlParameter("PricePerDay", PricePerDay));
    //            command.Parameters.Add(new SqlParameter("TotalPrice", TotalPrice));
    //            command.Parameters.Add(new SqlParameter("Deposit", Deposit));
    //            command.Parameters.Add(new SqlParameter("RestToPay", RestToPay));
    //            command.Parameters.Add(new SqlParameter("Confirmation", Confirmation));
    //            command.Parameters.Add(new SqlParameter("ConfirmationDate", ConfirmationDate));
    //            command.Parameters.Add(new SqlParameter("Annotation", Annotation));

    //            command.ExecuteNonQuery();

    //            connection.Close();
    //        }
    //        catch (Exception ex)
    //        {
    //            return;
    //        }

    //    }

}
