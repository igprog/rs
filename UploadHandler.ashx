<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;

public class UploadHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string userId = context.Request.Form["userid"];
        if (context.Request.Files.Count > 0) {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++) {
                HttpPostedFile file = files[i];
                string fname = context.Server.MapPath(string.Format("~/upload/{0}/gallery/{1}", userId, file.FileName));
                if (!string.IsNullOrEmpty(file.FileName)) {
                    string folderPath = context.Server.MapPath(string.Format("~/upload/{0}/gallery", userId));
                    if (!Directory.Exists(folderPath)) {
                        Directory.CreateDirectory(folderPath);
                    }
                    file.SaveAs(fname);
                    context.Response.Write("OK");
                } else {
                    context.Response.Write("please choose a file to upload");
                }
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}