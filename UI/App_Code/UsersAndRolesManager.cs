using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite1;

//using WingtipToys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
/// <summary>
/// Summary description for UsersAndRolesManager
/// </summary>
/// 
namespace WebSite1
{
    public class UsersAndRolesManager
    {

        public void AddAdmin(ApplicationUser appUser,String password){
            // Access the application context and create result variables.
            var context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;
            
            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.
            if (!roleMgr.RoleExists("Admin"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Admin" });
            }
            
            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new ApplicationUserManager();
           
            IdUserResult = userMgr.Create(appUser, password);
            

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            if (!userMgr.IsInRole(userMgr.FindByName(appUser.UserName).Id, "Admin"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByName(appUser.UserName).Id, "Admin");
            }


        }

        public Boolean AddRole(String roleName){
            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            IdentityResult result ;
            if (!manager.RoleExists(roleName))
            {
                result = manager.Create(new IdentityRole(roleName));
                return result.Succeeded;
            }
            else 
                throw new Exception("Role Already Exists!");
        }

        public IdentityRole FindRole(String roleId)
        {

            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            return manager.FindById(roleId);
        }

        public ApplicationUser FindUser(String userId)
        {
            var userManager = ApplicationUserManager.Create();
            return userManager.FindById(userId);
        }

        public Boolean EditRole(String roleId, String roleName)
        {
            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = manager.FindById(roleId);
            role.Name =roleName;
            var result = manager.Update(role);
            return result.Succeeded;
        }
        public Boolean EditUser(String userId, String userName)
        {
            //todo: finish code
            //update the user
            var userManager = ApplicationUserManager.Create();
            var user = userManager.FindById(userId);
            user.UserName = userName;
            var result=userManager.Update(user);
            return result.Succeeded;
        }
        public Boolean DeleteRole(String roleId)
        {
            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = manager.FindById(roleId);
            var result = manager.Delete(role);
            return result.Succeeded;
        }

        public Boolean DeleteUser(String userId)
        {
            var userManager = ApplicationUserManager.Create();
            var user = userManager.FindById(userId);
            var result = userManager.Delete(user);
            return result.Succeeded;
        }

        public ApplicationUser[] UsersInRole(String roleId)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var usersInRole = roleManager.FindById(roleId).Users.ToArray();
            var userManager = ApplicationUserManager.Create();//new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));
            ApplicationUser[] users = new ApplicationUser[usersInRole.Length];
            for(int i=0; i<usersInRole.Length; i++){
                users[i]=userManager.FindById(usersInRole[i].UserId);
            }
            return users;
        }

        public IdentityRole[] UserRoles(String userId)
        {
            var userManager = ApplicationUserManager.Create();
            var userRoles = userManager.FindById(userId).Roles.ToArray();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            IdentityRole[] roles = new IdentityRole[userRoles.Length];
            for (int i = 0; i < userRoles.Length; i++)
            {
                roles[i] = roleManager.FindById(userRoles[i].RoleId);
            }

            return roles;
        }

        public IdentityRole[] GetAllRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roles = roleManager.Roles.ToArray();
            return roles;
        }

        public ApplicationUser[] GetUsers()
        {
            var userManager =  ApplicationUserManager.Create();
            var users = userManager.Users.ToArray();
            return users;
        }
    }
}