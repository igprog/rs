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
/// ProductGroups
/// </summary>
[WebService(Namespace = "http://rivierasplit.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
[DataContract(IsReference = true)]
public class ProductGroups : System.Web.Services.WebService {

    public ProductGroups () {
    }

    public class NewProductGroup {
        public Guid? productGroupId;
        public String title;
    }

    [WebMethod]
    public string GetProductGroups() {
        try {
            List<NewProductGroup> xx = new List<NewProductGroup>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ProductGroupId, Title FROM ProductGroups", connection)) {
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            NewProductGroup x = new NewProductGroup();
                            x.productGroupId = reader.GetGuid(0);
                            x.title = reader.GetValue(1) == DBNull.Value ? "" : reader.GetString(1);
                            if (!string.IsNullOrEmpty(x.title)) {
                                xx.Add(x);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return JsonConvert.SerializeObject(xx, Formatting.Indented);
        } catch (Exception e) {
            return e.Message;
        }
    }

    [WebMethod]
    public string GetProductGroupByProductGroupId(string productGroupId) {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT ProductGroupId, Title FROM ProductGroups WHERE ProductGroupId = ProductGroupId ", connection);

        command.Parameters.Add(new SqlParameter("ProductId", productGroupId));
        SqlDataReader reader = command.ExecuteReader();
        List<NewProductGroup> products = new List<NewProductGroup>();
        while (reader.Read()) {
            if (productGroupId == reader.GetGuid(0).ToString()) {
                NewProductGroup xx = new NewProductGroup() {
                    productGroupId = reader.GetGuid(0),
                    title = reader.GetString(1),
                };
                products.Add(xx);
            }
        }
        connection.Close();
        return JsonConvert.SerializeObject(products, Formatting.Indented);
    }



    [WebMethod]
    public string Save(NewProductGroup productGroup) {
        try {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            string sql = @"INSERT INTO ProductGroups VALUES
                        (@ProductGroupId, @Title)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductGroupId", productGroup.productGroupId));
            command.Parameters.Add(new SqlParameter("Title", productGroup.title));
            command.ExecuteNonQuery();
            connection.Close();
            return string.Format(@"Product saved Successfully!");
        } catch (Exception e) {
            return string.Format(@"Error! Product not saved. ({0})", e.Message);
        }
    }

    [WebMethod]
    public string Update(NewProductGroup productGroups) {
        try {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            string sql = @"
                            UPDATE ProductGroups SET
                            [Title] = @Title
                            WHERE [ProductGroupId] = @ProductGroupId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductGroupId", productGroups.productGroupId));
            command.Parameters.Add(new SqlParameter("Title", productGroups.title));
            command.ExecuteNonQuery();
            connection.Close();
            return string.Format(@"Product Group updated Successfully!");
        } catch (Exception e) {
            return string.Format(@"Error! Product Group not updated. ({0})", e.Message);
        }
    }

    [WebMethod]
    public string DeleteProductGroup(NewProductGroup productGroups) {
        try {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            string sql = @"
                            DELETE * FROM ProductGroup WHERE
                            WHERE [ProductGroupId] = @ProductGroupId";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(new SqlParameter("ProductGroupId", productGroups.productGroupId));
            command.ExecuteNonQuery();
            connection.Close();

            return string.Format(@"Product group deleted Successfully!");
        } catch (Exception e) {
            return string.Format(@"Error! Product group not deleted. ({0})", e.Message);
        }
    }


    
}
