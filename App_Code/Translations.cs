using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

/// <summary>
/// Language translations
/// </summary>
[WebService(Namespace = "http://rivierasplit.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Translations : System.Web.Services.WebService {

    

    [WebMethod]
    public void GetAllTranslations() {
        string translations = readLanguages(2);
        CreateFolder("~/json/translations/hr/");
        WriteFile("~/json/translations/hr/main.json", translations);
        translations = readLanguages(3);
        CreateFolder("~/json/translations/en/");
        WriteFile("~/json/translations/en/main.json", translations);
        translations = readLanguages(4);
        CreateFolder("~/json/translations/de/");
        WriteFile("~/json/translations/de/main.json", translations);
    }

    public void CreateFolder(string path) {
        if (!Directory.Exists(Server.MapPath(path))) {
            Directory.CreateDirectory(Server.MapPath(path));
        }
    }

    public void WriteFile(string path, string value) {
        File.WriteAllText(Server.MapPath(path), value);
    }

    public string readLanguages(int col){
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("SELECT TranslationId, Title, Language1, Language2, Language3, Language4, Language5 FROM Translations", connection);
        SqlDataReader reader = command.ExecuteReader();
        string json = "";
        string comma = "";
        while (reader.Read()) {
            if (json == "") { comma = ""; } else { comma = ","; }
            json = json + comma + "'" +
                (reader.GetValue(1) == DBNull.Value ? "" : reader.GetString(1)).ToString()
                + "':'" +
                (reader.GetValue(col) == DBNull.Value ? "" : reader.GetString(col)).ToString()
                + "'";
        }
        json = ("{" + json + "}").Replace("'", "\"");
        connection.Close();

        return json;
    }

}
