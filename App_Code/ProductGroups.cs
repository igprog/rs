using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for ProductGroups
/// </summary>
[WebService(Namespace = "http://rivierasplit.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
[DataContract(IsReference = true)]
public class ProductGroups : System.Web.Services.WebService {

    public ProductGroups () {
        productGroupId = null;
        title = null;
    }

    [DataMember]
    public Guid? productGroupId { get; set; }
    [DataMember]
    public String title { get; set; }

    public class NewProductGroup
    {
        public Guid? productGroupId { get; set; }
        public String title { get; set; }
    }


    string _InfoMessage;
    public string InfoMessage
    {
        get { return _InfoMessage; }
        set { _InfoMessage = value; }
    }



    [WebMethod]
    public string GetProductGroups()
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT ProductGroupId, Title FROM ProductGroups", connection);

        SqlDataReader reader = command.ExecuteReader();
        List<ProductGroups> productGroups = new List<ProductGroups>();
        while (reader.Read())
        {
            ProductGroups xx = new ProductGroups()
            {
                productGroupId = reader.GetGuid(0),
                title = reader.GetValue(1) == DBNull.Value ? "" : reader.GetString(1)
            };
            productGroups.Add(xx);
        }
        connection.Close();

        string json = JsonConvert.SerializeObject(productGroups, Formatting.Indented);
        return json;
    }

    [WebMethod]
    public string GetProductGroupByProductGroupId(string productGroupId)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT ProductGroupId, Title FROM ProductGroups WHERE ProductGroupId = ProductGroupId ", connection);

        command.Parameters.Add(new SqlParameter("ProductId", productGroupId));
        SqlDataReader reader = command.ExecuteReader();
        List<ProductGroups> products = new List<ProductGroups>();
        while (reader.Read())
        {
            if (productGroupId == reader.GetGuid(0).ToString())
            {
                ProductGroups xx = new ProductGroups()
                {
                    productGroupId = reader.GetGuid(0),
                    title = reader.GetString(1),
                };
                products.Add(xx);
            }
        }
        connection.Close();

        string json = JsonConvert.SerializeObject(products, Formatting.Indented);
        return json;
        //CreateFolder("~/json/");
        //WriteFile("~/json/products.json", json);
    }



    [WebMethod]
    public void Save(NewProductGroup productGroup)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        try
        {
            string sql = @"INSERT INTO ProductGroups VALUES
                        (@ProductGroupId, @Title)";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductGroupId", productGroup.productGroupId));
            command.Parameters.Add(new SqlParameter("Title", productGroup.title));
            command.ExecuteNonQuery();
            connection.Close();

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
    public void Update(NewProductGroup productGroups)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        try
        {
            string sql = @"
                            UPDATE ProductGroups SET
                            [Title] = @Title
                            WHERE [ProductGroupId] = @ProductGroupId";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductGroupId", productGroups.productGroupId));
            command.Parameters.Add(new SqlParameter("Title", productGroups.title));
            command.ExecuteNonQuery();
            connection.Close();

            _InfoMessage = string.Format(@"
                        <div class=""alert alert-success alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Product Group updated Successfully!</strong>
                        </div>");

        }
        catch (Exception ex)
        {
            _InfoMessage = string.Format(@"
                     <div class=""alert alert-danger alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Error! Product Group not updated.</strong>
                        </div>");
            return;
        }
    }

    [WebMethod]
    public void DeleteProduct(NewProductGroup productGroups)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        try
        {
            string sql = @"
                            DELETE * FROM ProductGroup WHERE
                            WHERE [ProductGroupId] = @ProductGroupId";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductGroupId", productGroups.productGroupId));

            command.ExecuteNonQuery();
            connection.Close();

            _InfoMessage = string.Format(@"
                        <div class=""alert alert-success alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Product group deleted Successfully!</strong>
                        </div>");

        }
        catch (Exception ex)
        {
            _InfoMessage = string.Format(@"
                     <div class=""alert alert-danger alert-dismissable"">
                            <a href=""#"" class=""close"" data-dismiss=""alert"" aria-label=""close"">×</a>
                            <strong>Error! Product group not deleted.</strong>
                        </div>");
            return;
        }
    }







    
}
