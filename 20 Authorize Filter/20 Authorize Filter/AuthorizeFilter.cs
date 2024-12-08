/*
 
-> Authorization:

Authorization is the process of verifying whether an authenticated user has the privelege to access a requested resource.

-> Authentication:

Authentication is the process of validating the identity of a user before granting access to a restricted resources in an application.

When the application receives a request from users, it tries to authenticate the user.

Typically, authentication allows identifying an individual based on the user name and password provided by the user.

- Authorization filters execute before an action method is invoked 


-> Authorization Filters in ASP.NET MVC

Authorization filters in ASP.NET MVC are used to enforce security policies by determining whether a user is authorized to execute a particular action method or access a specific controller. They run early in the request lifecycle, before action methods are invoked, ensuring only authenticated and authorized users can proceed.

Typically, authentication allows identifying individual based on the user name and password provided by the user.

 - Authorization filters executre before an action method is invoked to make security decisions on whether to allow the execution of the action method or not

 - In ASP.NET MVC Framework, the AuthorizeAttribute class of the System.Web.Mvc namespace is an example of authorization filters.

 - This class extends the FilterAttribute class and implements the IAuthorizationFilter interface.

 - The authoriztion filter allows you to implement standard authentication and authorization functionality in your application. 

 - You can use the web.config file to specify the page to be displayed for user authentication.

 - The <forms> element specifies the login URL for the application.

 - Whenever a user tries to access a restricted resource, the user is redirected to the login URL.

 - The timeout attribute of the <forms> element specifies the amount of time in minutes, after which the authentication cookies expires. Its default value in 30 minutes.

-> Purpose of Authorization Filters

1. Access Control:

- Restrict access to certain actions or controllers based on user roles, authentication status, or custom rules.

2. Pre-Action Security:

- Prevent unauthorized requests before any business logic in the action method is executed.

3. Centralized Authorization:

- Manage access control in a reusable and consistent way across the application.

-> Built-in Authorization Filter: [Authorize]

The [Authorize] attribute is the most commonly used authorization filter in ASP.NET MVC. It provides a simple way to restrict access based on:

(i) Authentication Status: Ensures the user is logged in.

(ii) User Roles: Checks if the user belongs to specific roles.

-> Basic Usage

- Restricting Unauthorized Access:

----------------------------------------------------------------------------
[Authorize]
public class SecureController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
----------------------------------------------------------------------------

In this example, only authenticated users can access the Index action.

- Restricting Access to Specific Roles:

----------------------------------------------------------------------------
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public ActionResult Dashboard()
    {
        return View();
    }
}
----------------------------------------------------------------------------

Here, only users in the Admin role can access the Dashboard action.

- Restricting Access to Specific Users:

----------------------------------------------------------------------------
[Authorize(Users = "user1, user2")]
public class SpecificUserController : Controller
{
    public ActionResult Settings()
    {
        return View();
    }
}
----------------------------------------------------------------------------

This ensures only user1 and user2 can access the Settings action.

-> How It Works

(i) When a request is made, the [Authorize] filter runs before the action method is executed.

(ii) It checks the user's authentication status (via the User.Identity property).

(iii) If the user is not authenticated or does not meet the role/user criteria, the filter:

 - Redirects to the login page (default behavior).
 - Returns an HTTP 401 (Unauthorized) status for API requests.

-> Custom Authorization Filters

For more complex scenarios, you can create custom authorization filters by inheriting from the AuthorizeAttribute class or implementing the IAuthorizationFilter interface.

- Custom Authorization Filter Example

This custom filter restricts access based on a user's department:

----------------------------------------------------------------------------
public class DepartmentAuthorizeAttribute : AuthorizeAttribute
{
    public string Department { get; set; }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        var user = httpContext.User;

        if (!user.Identity.IsAuthenticated)
            return false;

        // Simulate custom logic for department check
        string userDepartment = GetUserDepartment(user.Identity.Name);
        return userDepartment == Department;
    }

    private string GetUserDepartment(string username)
    {
        // Replace with actual department retrieval logic
        return "IT";
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.Result = new RedirectResult("~/Account/Login");
    }
}
----------------------------------------------------------------------------

Usage

Apply the custom filter to an action or controller.

----------------------------------------------------------------------------
[DepartmentAuthorize(Department = "IT")]
public class ITController : Controller
{
    public ActionResult Dashboard()
    {
        return View();
    }
}
----------------------------------------------------------------------------

-> Global Authorization Filters

You can register authorization filters globally to enforce access control across the application. This is done in the FilterConfig class:

----------------------------------------------------------------------------
public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new AuthorizeAttribute());
    }
}
----------------------------------------------------------------------------

This ensures that every controller and action in the application requires authentication by default.

-> Customizing Unauthorized Responses

By default, unauthorized users are redirected to the login page. You can customize this behavior:

- Override the HandleUnauthorizedRequest method in a custom AuthorizeAttribute.

- Modify the response to return a specific status code or message (useful for APIs).

Example for returning a JSON response:

----------------------------------------------------------------------------
protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
{
    if (filterContext.HttpContext.Request.IsAjaxRequest())
    {
        filterContext.Result = new JsonResult
        {
            Data = new { success = false, message = "Unauthorized" },
            JsonRequestBehavior = JsonRequestBehavior.AllowGet
        };
    }
    else
    {
        base.HandleUnauthorizedRequest(filterContext);
    }
}
----------------------------------------------------------------------------

-> Best Practices for Authorization Filters

1. Use Global Authorization:

- Apply [Authorize] globally for secure applications, and use [AllowAnonymous] on actions that need to be publicly accessible.

2. Fine-Grained Control:

- Use role-based or custom filters for specific security requirements.

3. Custom Error Handling:

- Provide meaningful feedback to unauthorized users with custom responses.

4. Centralize Policies:

- Avoid duplicating authorization logic in individual controllers; use filters or policies.

5. Test Authorization Logic:

- Ensure thorough testing to prevent unauthorized access.

-> Limitations

(i) Static Role/User Assignment:

- The [Authorize] attribute does not dynamically resolve roles or users. Use a custom filter for dynamic scenarios.

(ii) Not Ideal for APIs:

- For APIs, prefer using token-based authentication and authorization frameworks like ASP.NET Identity or JWT Bearer Authentication.

(iii) Access to External Resources:

- [Authorize] alone cannot handle scenarios where access depends on data from external systems (e.g., databases). Custom filters are needed.

By leveraging authorization filters effectively, you can enforce robust security policies in your ASP.NET MVC applications, ensuring that only authorized users access sensitive resources.

*/


/* 

-> Steps to [Authorize] filter

Step 1: Set Up Authentication

The [Authorize] filter requires users to be authenticated. Configure authentication in your application:

(i) Forms Authentication:

- Enable Forms Authentication in the Web.config file

-------------------------------------------------------------------------------------------
<system.web>
    <authentication mode="Forms">
        <forms loginUrl="~/Account/Login" timeout="2880" />
    </authentication>
</system.web>
-------------------------------------------------------------------------------------------

(ii) Windows Authentication:

- Configure Windows Authentication if required.

- Enable it in IIS and set authentication mode="Windows" in the Web.config file.

(iii) Custom Authentication:

- Use ASP.NET Identity or another custom authentication mechanism to manage user login and roles.

Step 2: Add [Authorize] Attribute to Controllers or Actions

Apply the [Authorize] attribute to restrict access to authenticated users.

(i) Apply to a Controller:

- Restricts access to all actions within the controller:

-------------------------------------------------------------------------------------------
[Authorize]
public class SecureController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

(ii) Apply to Specific Actions: 

- Restricts access only to specific actions:

-------------------------------------------------------------------------------------------
public class HomeController : Controller
{
    [Authorize]
    public ActionResult SecureAction()
    {
        return View();
    }

    public ActionResult PublicAction()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

Step 3: Configure Role-Based Authorization (Optional)

- Use the Roles property of the [Authorize] attribute to restrict access based on user roles.

(i) Assign roles to users during authentication.

- Example with ASP.NET Identity:

-------------------------------------------------------------------------------------------
var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
userManager.AddToRole(userId, "Admin");
-------------------------------------------------------------------------------------------

(ii) Use the [Authorize] attribute to enforce role-based access:

-------------------------------------------------------------------------------------------
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public ActionResult Dashboard()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

Step 4: Configure User-Based Authorization (Optional)

- Use the Users property of the [Authorize] attribute to restrict access to specific users.

-------------------------------------------------------------------------------------------
[Authorize(Users = "user1, user2")]
public class SpecificUserController : Controller
{
    public ActionResult Settings()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

Step 5: Handle Unauthorized Access

- By default, unauthorized users are redirected to the login page specified in the web.config file.

(i) Define Login Page:

- Ensure the loginUrl in web.config points to the appropriate login page:

-------------------------------------------------------------------------------------------
<authentication mode="Forms">
    <forms loginUrl="~/Account/Login" timeout="2880" />
</authentication>
-------------------------------------------------------------------------------------------

2. Customize Unauthorized Response:

- For APIs or AJAX requests, override the behavior by customizing the response:

-------------------------------------------------------------------------------------------
protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
{
    if (filterContext.HttpContext.Request.IsAjaxRequest())
    {
        filterContext.Result = new JsonResult
        {
            Data = new { success = false, message = "Unauthorized" },
            JsonRequestBehavior = JsonRequestBehavior.AllowGet
        };
    }
    else
    {
        base.HandleUnauthorizedRequest(filterContext);
    }
}
-------------------------------------------------------------------------------------------

Step 6: Allow Anonymous Access (Optional)

- To allow public access to specific actions in a controller that uses [Authorize], use the [AllowAnonymous] attribute:

-------------------------------------------------------------------------------------------
[Authorize]
public class SecureController : Controller
{
    public ActionResult SecureAction()
    {
        return View();
    }

    [AllowAnonymous]
    public ActionResult PublicAction()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

Step 7: Test the Authorization

(i) Attempt to access secured resources as:
 - An authenticated user.
 - An unauthenticated user.
 - A user with incorrect roles or without permissions.

(ii) Verify that unauthorized users are redirected to the login page or receive the correct error response.

-> Example Workflow

AccountController for Authentication:

-------------------------------------------------------------------------------------------
public class AccountController : Controller
{
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Simulate user authentication
            FormsAuthentication.SetAuthCookie(model.Username, false);
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }

    public ActionResult Logout()
    {
        FormsAuthentication.SignOut();
        return RedirectToAction("Login");
    }
}
-------------------------------------------------------------------------------------------

Secured Controller:

-------------------------------------------------------------------------------------------
[Authorize]
public class SecureController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

*/

/*

-> Authorize Filters application on action, controller and global level

1. Action-Level Application

The [Authorize] filter can be applied to individual action methods, restricting access to those specific actions.

-------------------------------------------------------------------------------------------
public class HomeController : Controller
{
    [Authorize]
    public ActionResult SecureAction()
    {
        return View();
    }

    public ActionResult PublicAction()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

- Only authenticated users can access SecureAction.

- PublicAction is accessible to all users.

2. Controller-Level Application

When [Authorize] is applied at the controller level, it enforces authentication on all action methods within that controller.

-------------------------------------------------------------------------------------------
[Authorize]
public class SecureController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Details()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------------

- Both Index and Details require authentication because the [Authorize] filter is applied to the controller.

3. Global-Level Application

To apply the [Authorize] filter globally (to all controllers and actions in the application), register it in the FilterConfig class, located in the App_Start folder.

-------------------------------------------------------------------------------------------
public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new AuthorizeAttribute());
    }
}
-------------------------------------------------------------------------------------------

- All controllers and actions require authentication by default.

- Use [AllowAnonymous] to exempt specific actions or controllers (discussed below).

-> Timeout Feature in Authentication (Forms Mode)

When using Forms Authentication, the [Authorize] filter relies on the configuration in the web.config file to determine authentication settings, including timeout behavior.

1) Timeout in Forms Authentication

The timeout determines how long a user remains authenticated before needing to log in again.

Configuration in web.config:

-------------------------------------------------------------------------------------------
<system.web>
    <authentication mode="Forms">
        <forms loginUrl="~/Account/Login" timeout="30" />
    </authentication>
</system.web>
-------------------------------------------------------------------------------------------

- loginUrl: The URL where unauthorized users are redirected (e.g., the login page).

- timeout: Specifies the duration (in minutes) that the authentication ticket remains valid. After this time, the user is required to log in again.

Key Points:

(i) The timeout starts from the last request. If no requests are made within the specified time, the user session expires.

(ii) To extend the session for active users, use the sliding expiration feature:

-------------------------------------------------------------------------------------------
<forms loginUrl="~/Account/Login" timeout="30" slidingExpiration="true" />
-------------------------------------------------------------------------------------------

- Sliding expiration resets the timeout with every request, keeping active users logged in.

(iii) The timeout value is stored in the authentication cookie. Ensure that your application properly handles cookie encryption and expiration.

*/


/*

-> The [AllowAnonymous] Filter

The [AllowAnonymous] filter is used to explicitly allow access to certain controllers or actions even when the [Authorize] filter is applied globally or at the controller level.

Use case

- In an application where most actions require authentication, you may want some actions (e.g., Login, Register) to be publicly accessible.

Example:

-------------------------------------------------------------------------------------------
[Authorize]
public class AccountController : Controller
{
    public ActionResult Dashboard()
    {
        return View(); // Requires authentication
    }

    [AllowAnonymous]
    public ActionResult Login()
    {
        return View(); // Accessible to all users
    }

    [AllowAnonymous]
    public ActionResult Register()
    {
        return View(); // Accessible to all users
    }
}
-------------------------------------------------------------------------------------------

- The Dashboard action requires authentication because [Authorize] is applied at the controller level.

- The Login and Register actions are exempt from the [Authorize] filter due to the [AllowAnonymous] attribute.


-> Combining Filters

You can mix and match the [Authorize] and [AllowAnonymous] filters to meet your security requirements.

Example:

-------------------------------------------------------------------------------------------
[Authorize]
public class HomeController : Controller
{
    public ActionResult SecurePage()
    {
        return View(); // Requires authentication
    }

    [AllowAnonymous]
    public ActionResult PublicPage()
    {
        return View(); // Accessible to all users
    }
}
-------------------------------------------------------------------------------------------

-> Summary

(i) Applying the [Authorize] Filter:

- Action Level: Secures specific actions.

- Controller Level: Secures all actions within the controller.

- Global Level: Secures the entire application by default.

(ii) Timeout in Authentication:

- Defined in the web.config file via the timeout property in the <forms> element.

- Sliding expiration can be enabled for better user experience.

(iii) [AllowAnonymous] Filter:

- Overrides the [Authorize] filter, allowing public access to specific actions or controllers.

- By properly configuring and combining these filters, you can achieve robust security and user access control in your ASP.NET MVC application.


*/