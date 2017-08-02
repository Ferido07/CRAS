using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebSite1;
public partial class Admin_UsersAdmin_Roles : System.Web.UI.Page
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

        var roles = usrManager.UserRoles(userId);
        if (roles.Length == 0)
            MessageLabel.Text = " No Roles for this User! ";
        else DisplayRoles(roles);
    }

    private void DisplayRoles(Microsoft.AspNet.Identity.EntityFramework.IdentityRole[] roles)
    {

        var headerRow = new TableHeaderRow();
        headerRow.Cells.Add(new TableHeaderCell() { Text = "Role Name" });
        headerRow.Cells.Add(new TableHeaderCell());
        Table1.Rows.Add(headerRow);

        foreach (var role in roles)
        {
            TableRow row = new TableRow();
            TableCell roleCell = new TableCell();
           // TableCell actionCell = new TableCell();

            Table1.Rows.Add(row);
            row.Cells.Add(roleCell);
            //row.Cells.Add(actionCell);

            roleCell.Controls.Add(new LiteralControl(role.Name));

            /*
            actionCell.Controls.Add(new HyperLink { Text = " Edit ", NavigateUrl = "~/Admin/RolesAdmin/Edit.aspx" + "?roleId=" + role.Id });
            actionCell.Controls.Add(new LiteralControl("  | "));
            actionCell.Controls.Add(new HyperLink { Text = " Delete ", NavigateUrl = "~/Admin/RolesAdmin/Delete.aspx" + "?roleId=" + role.Id });
            actionCell.Controls.Add(new LiteralControl("  | "));
            actionCell.Controls.Add(new HyperLink { Text = " Users ", NavigateUrl = "~/Admin/RolesAdmin/Users.aspx" + "?roleId=" + role.Id });
             * */
        }
    }
}