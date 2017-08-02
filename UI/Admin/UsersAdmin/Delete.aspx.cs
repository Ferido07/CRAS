using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebSite1;

public partial class Admin_UsersAdmin_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.GetOwinContext().Authentication.User.IsInRole("Admin"))
            Response.Redirect("~/AccessDenied");

        String userId = Request.QueryString["userId"];
        if (String.IsNullOrEmpty(userId))
            Response.Redirect("../UsersAdmin/Default.aspx");
        var usrManager = new UsersAndRolesManager();
        var user = usrManager.FindUser(userId);
        userName.Text = user.UserName;
        ViewState["userId"] = userId;

    }
    protected void DeleteUser_Click(object sender, EventArgs e)
    {
        var userId = ViewState["userId"].ToString();
        var usrManager = new UsersAndRolesManager();
        if (usrManager.DeleteUser(userId))
            Response.Redirect("~/Admin/UsersAdmin/Default.aspx");
        else result.Text = "There was a problem deleting the user!";

    }
}