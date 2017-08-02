using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebSite1;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
public partial class Admin_UsersAdmin_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.GetOwinContext().Authentication.User.IsInRole("Admin"))
            Response.Redirect("~/AccessDenied");

        var userId = Request.QueryString["userId"].ToString();
        if (String.IsNullOrEmpty(userId))
            Response.Redirect("~/Admin/UsersAdmin/Default.aspx");

        var usrManager = new UsersAndRolesManager();
        if (!IsPostBack)
            userName.Text = usrManager.FindUser(userId).UserName;
        
        var allRoles = usrManager.GetAllRoles();
        var userRoles = usrManager.UserRoles(userId);
        for (int i = 0; i < allRoles.Length; i++ )
        {
            CheckBox roleCheckBox = new CheckBox { Text = allRoles[i].Name, CssClass = "checkbox-inline", ID = allRoles[i].Name};
            rolesSpan.Controls.Add(roleCheckBox);
            for (int j = 0; j < userRoles.Length; j++)
            {
                if (userRoles[j].Id == allRoles[i].Id)
                    roleCheckBox.Checked = true;
            }
        }
        ViewState["userId"] = userId;
    }

    protected void SaveUser_Click(object sender, EventArgs e)
    {
        var usrManager = new UsersAndRolesManager();
        //var allRoles = usrManager.GetAllRoles();

        //find the user and set the new UserName
        var userId = ViewState["userId"].ToString();
        usrManager.EditUser(userId, userName.Text);

        var userManager = new ApplicationUserManager();
        
        foreach (Control control in rolesSpan.Controls)
        {
            if (String.Equals(control.GetType().FullName, "System.Web.UI.WebControls.CheckBox"))
            {
                CheckBox roleCheckbox = (System.Web.UI.WebControls.CheckBox)control;
                var role = roleCheckbox.Text;
               //the for loop is unnecassary since all checkboxs are added once the roles are retrieved in the page load
                // for (int i = 0; i < allRoles.Length; i++)
                //{
                    //if (role == allRoles[i].Name && roleCheckbox.Checked)
                    if(roleCheckbox.Checked)
                    {
                        if (!userManager.IsInRole(userId, role))
                            userManager.AddToRole(userId, role);
                    }
                //}
            }
        }

        Response.Redirect("~/Admin/UsersAdmin/Default.aspx");
    }

    
}