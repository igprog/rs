using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Xml;
using IGPROG;

namespace IGPROG {
    public class UploadFiles : System.Web.UI.Page {

        public UploadFiles() {
        }

        public void Upload(String uploadPath, FileUpload uploadFile) {
            if (!Directory.Exists(Server.MapPath(uploadPath))) {
                Directory.CreateDirectory(Server.MapPath(uploadPath));
            }
            try {
                uploadFile.SaveAs(Server.MapPath(uploadPath) + uploadFile.FileName);
            }
            catch (Exception ex) {
                return;
            }
        }
    }

}
