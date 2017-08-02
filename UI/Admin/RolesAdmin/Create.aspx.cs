using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebSite1;
public partial class Admin_RolesAdmin_Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.GetOwinContext().Authentication.User.IsInRole("Admin"))
            Response.Redirect("~/AccessDenied");
    }
    protected void SaveRole_Click(object sender, EventArgs e)
    {
        var appManager = new UsersAndRolesManager();
        try
        {
            if(appManager.AddRole(roleName.Text))
                Response.Redirect("~/Admin/RolesAdmin/Default.aspx");
        }
        catch (Exception exc)
        {
            req.Text = exc.Message;
            summary.ShowSummary = true;
        }
    }
}