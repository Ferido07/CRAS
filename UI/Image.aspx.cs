using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // String path = Request.QueryString[0];
        
        //if (path != null) { 
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(@"C:\\Users\Ferid\Desktop\17359020_410240986006318_6766412691828154636_o.jpg");
            image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg); 
        
       // }
    }
}