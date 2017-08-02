using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebSite1;

public partial class Admin_RolesAdmin_Users : System.Web.UI.Page
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

        var users = usrManager.UsersInRole(roleId);
        if (users.Length==0)
            MessageLabel.Text = " No Users in this Role! ";
        else DisplayUsers(users);

    }

    private void DisplayUsers(Microsoft.AspNet.Identity.EntityFramework.IdentityUser[] users)
    {

        var headerRow = new TableHeaderRow();
        headerRow.Cells.Add(new TableHeaderCell() { Text = "User Name" });
        //headerRow.Cells.Add(new TableHeaderCell());
        Table1.Rows.Add(headerRow);

        foreach (var user in users)
        {
            TableRow row = new TableRow();
            TableCell userCell = new TableCell();
            //TableCell actionCell = new TableCell();

            Table1.Rows.Add(row);
            row.Cells.Add(userCell);
            //row.Cells.Add(actionCell);

            userCell.Controls.Add(new LiteralControl(user.UserName));

            /*
            actionCell.Controls.Add(new HyperLink { Text = " Manage ", NavigateUrl = "#to be finished " + "?userId=" + user.Id });
            actionCell.Controls.Add(new LiteralControl("  | "));
            actionCell.Controls.Add(new HyperLink { Text = " Delete ", NavigateUrl = "~/Admin/UsersAdmin/Delete.aspx" + "?userId=" + user.Id });
            actionCell.Controls.Add(new LiteralControl("  | "));
            actionCell.Controls.Add(new HyperLink { Text = " Roles ", NavigateUrl = "~/Admin/UsersAdmin/Roles.aspx" + "?userId=" + user.Id });*/
        }
    }
}