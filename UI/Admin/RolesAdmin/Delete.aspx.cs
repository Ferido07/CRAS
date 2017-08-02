using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebSite1;
public partial class Admin_RolesAdmin_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.GetOwinContext().Authentication.User.IsInRole("Admin"))
            Response.Redirect("~/AccessDenied");

        String roleId = Request.QueryString["roleId"];
        if (String.IsNullOrEmpty(roleId))
            Response.Redirect("../RolesAdmin/Default.aspx");
        var usrManager = new UsersAndRolesManager();
        var role = usrManager.FindRole(roleId);
        roleName.Text = role.Name;
        ViewState["roleId"] = roleId;

    }
    protected void DeleteRole_Click(object sender, EventArgs e)
    {
        var roleId = ViewState["roleId"].ToString();
        var usrManager = new UsersAndRolesManager();
        if (usrManager.DeleteRole(roleId))
            Response.Redirect("~/Admin/RolesAdmin/Default.aspx");
        else result.Text = "There was a problem deleting the role!";

    }
}