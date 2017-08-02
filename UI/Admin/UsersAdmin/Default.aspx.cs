using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebSite1;

public partial class Admin_UsersAdmin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.GetOwinContext().Authentication.User.IsInRole("Admin"))
            Response.Redirect("~/AccessDenied");

        var usrManager = new UsersAndRolesManager();
        var users = usrManager.GetUsers();
        DisplayUsers(users);
    }

    private void DisplayUsers(ApplicationUser[] users)
    {
        
        var headerRow = new TableHeaderRow();
        headerRow.Cells.Add(new TableHeaderCell() { Text = "User Name" });
        headerRow.Cells.Add(new TableHeaderCell());
        Table1.Rows.Add(headerRow);

        foreach (var user in users)
        {
            TableRow row = new TableRow();
            TableCell roleCell = new TableCell();
            TableCell actionCell = new TableCell();

            Table1.Rows.Add(row);
            row.Cells.Add(roleCell);
            row.Cells.Add(actionCell);

            roleCell.Controls.Add(new LiteralControl(user.UserName));


            actionCell.Controls.Add(new HyperLink { Text = " Edit ", NavigateUrl = "~/Admin/UsersAdmin/Edit.aspx" + "?userId=" + user.Id });
            actionCell.Controls.Add(new LiteralControl("  | "));
            actionCell.Controls.Add(new HyperLink { Text = " Delete ", NavigateUrl = "~/Admin/UsersAdmin/Delete.aspx" + "?userId=" + user.Id });
            //actionCell.Controls.Add(new LiteralControl("  | "));
            //actionCell.Controls.Add(new HyperLink { Text = " Roles ", NavigateUrl = "~/Admin/UsersAdmin/Roles.aspx" + "?userId=" + user.Id });
        }

    }
}