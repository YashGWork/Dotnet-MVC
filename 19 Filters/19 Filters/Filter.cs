/*

Filters

Filters in ASP.NET MVC are components that allow you to execute logic before or after an action method is called, or after the result of an action method is executed. Filters are a powerful way to handle cross-cutting concerns such as logging, error handling, authentication, and authorization in a clean and reusable manner. We can also use built-in filters and custom filters in MVC.

-> Types of Filters in ASP.NET MVC

ASP.NET MVC provides several types of filters, each serving a specific purpose:

1. Authentication Filters:

- Executed before any other filters.

- Used to authenticate the user.

Example: Custom authentication logic to verify tokens or cookies.

2. Authorization Filters:

- Used to enforce security policies.

- Determine if a user is authorized to execute an action.

Example: [Authorize] attribute.

3. Action Filters:

- Run before and/or after an action method execution.

- Useful for logging, caching, or modifying the data passed to the action method.

Example: [ActionFilter].

4. Result Filters:

- Run before and/or after the execution of the action result.

- Modify the view result or additional processing after an action method's return.

Example: Manipulating the response headers.

5. Exception Filters:

- Handle errors raised by action methods or other filters.

- Often used for error logging or displaying custom error pages.

Example: [HandleError].

-> Order of Execution

Filters execute in a specific order:

(i) Authentication filters.
(ii) Authorization filters.
(iii) Action filters (pre-action logic).
(iv) The action method itself.
(v) Action filters (post-action logic).
(vi) Result filters (pre-result logic).
(vii) The action result execution.
(viii) Result filters (post-result logic).
(ix) Exception filters (if an error occurs).

-> Implementation Levels

While developing an ASP.NET MVC application, we can use filters at the following levels:

1) Action method Level: When you use filters in an action method, the filter will execute only when the associated action method is accessed.

2) Controller Level: When you use filters in controllers, the filter will execute for all the actions methods defined in the controller.

3) Application Level: When you use filters in an application, the filter will execute for all the action methods and controllers present in the application.

-> Implementation

Filters can be implemented in three main ways:

1. Using Attributes: Decorating controllers or action methods with filter attributes.

------------------------------------------------------------------------------------------------------------------------------------
[Authorize]
public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
------------------------------------------------------------------------------------------------------------------------------------

2. Custom Filters: Creating a custom filter by implementing specific interfaces such as IActionFilter, IResultFilter, or IExceptionFilter.

------------------------------------------------------------------------------------------------------------------------------------
public class CustomLogFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        // Custom logic before action execution
        base.OnActionExecuting(filterContext);
    }
}
------------------------------------------------------------------------------------------------------------------------------------

3. Global Filters: Applying filters globally in the FilterConfig class (located in App_Start).

------------------------------------------------------------------------------------------------------------------------------------
public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new HandleErrorAttribute());
    }
}
------------------------------------------------------------------------------------------------------------------------------------

Filters make it easier to keep the code modular and maintainable while addressing repetitive tasks across the application.


*/


/*

Exception Filters

Exception is an abnormal condition, which stops program exectuion when it occurs.

Exception is an event that disrupts the normal flow of the program. It is an object which is thrown at runtime.

Exception filters in ASP.NET MVC are a specialized type of filter designed to handle errors that occur during the execution of an action method or another filter. They provide a way to centralize error-handling logic, allowing you to manage exceptions consistently across your application.

-> Purpose

The primary purposes of exception filters are:

- Centralized Error Handling: Avoid scattering error-handling code across controllers and actions.

- Custom Error Responses: Provide user-friendly error messages or redirect users to custom error pages.
 
- Error Logging: Log exceptions for debugging or auditing purposes.
 
- Fallback Mechanism: Gracefully handle unhandled exceptions.

-> How Exception Filters Work

- Exception filters are executed only when an exception is thrown during the execution of an action method, action filters, or result filters.

- They run after the error occurs but before the response is sent to the client.

- If an exception filter handles the exception, the request pipeline stops propagating the error to higher levels.

-> Order of Execution

Exception filters are the last filters to be executed in the ASP.NET MVC pipeline. They occur after action filters and result filters, and only if an exception is thrown during their execution or the execution of the action method itself.

-> Built-in Exception Filters

ASP.NET MVC provides a built-in exception filter:

1. HandleErrorAttribute:

- Used to handle exceptions and show custom error pages.

- Redirects users to an error page specified in the web.config or in the filter configuration.

-> Custom Exception Filters

You can create custom exception filters by implementing the IExceptionFilter interface or inheriting from the FilterAttribute and IExceptionFilter.

Steps to Create a Custom Exception Filter

(i) Implement the IExceptionFilter Interface:

-------------------------------------------------------------------------------------------------------------------------------
public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
{
    public void OnException(ExceptionContext filterContext)
    {
        // Check if the exception is already handled
        if (filterContext.ExceptionHandled)
            return;

        // Log the exception
        var exception = filterContext.Exception;
        // Log the exception details (e.g., using a logging library)
        Console.WriteLine($"Error: {exception.Message}");

        // Set the result
        filterContext.Result = new ViewResult
        {
            ViewName = "Error" // Custom error view
        };

        // Mark the exception as handled
        filterContext.ExceptionHandled = true;
    }
}
-------------------------------------------------------------------------------------------------------------------------------

(ii) Apply the Filter:

Globally: Add the filter in the FilterConfig class.

-------------------------------------------------------------------------------------------------------------------------------
public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new CustomExceptionFilter());
    }
}
-------------------------------------------------------------------------------------------------------------------------------

(iii) Controller-wide: Decorate a controller with the filter

-------------------------------------------------------------------------------------------------------------------------------
[CustomExceptionFilter]
public class HomeController : Controller
{
    // Actions
}
-------------------------------------------------------------------------------------------------------------------------------
(iv) Action-specific: Apply the filter to a specific action

-------------------------------------------------------------------------------------------------------------------------------
[CustomExceptionFilter]
public ActionResult Index()
{
    // Action logic
}
-------------------------------------------------------------------------------------------------------------------------------

-> Key Properties of ExceptionContext

The ExceptionContext object passed to the OnException method provides useful information:

(i) Exception: The exception that occurred.

(ii) ExceptionHandled: A boolean indicating whether the exception has already been handled.

(iii) Result: The ActionResult to execute if the exception is handled.

(iv) HttpContext: Access to the current HTTP context.

(iv) Controller: Reference to the controller in which the exception occurred.

-> Practical Scenarios

(i) Custom Error Pages: Redirect users to a custom error page when an unhandled exception occurs.

-------------------------------------------------------------------------------------------------------------------------------
public void OnException(ExceptionContext filterContext)
{
    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
    {
        { "controller", "Error" },
        { "action", "Index" }
    });

    filterContext.ExceptionHandled = true;
}
-------------------------------------------------------------------------------------------------------------------------------

(ii) Logging: Log exceptions using a logging framework (e.g., NLog, Serilog).

-------------------------------------------------------------------------------------------------------------------------------
public void OnException(ExceptionContext filterContext)
{
    var logger = LogManager.GetLogger("ErrorLogger");
    logger.Error(filterContext.Exception, "An error occurred.");

    filterContext.ExceptionHandled = true;
}
-------------------------------------------------------------------------------------------------------------------------------

(iii) API Response: Return structured error information for API requests.

-------------------------------------------------------------------------------------------------------------------------------
public void OnException(ExceptionContext filterContext)
{
    if (filterContext.HttpContext.Request.IsAjaxRequest())
    {
        filterContext.Result = new JsonResult
        {
            Data = new { error = true, message = filterContext.Exception.Message },
            JsonRequestBehavior = JsonRequestBehavior.AllowGet
        };

        filterContext.ExceptionHandled = true;
    }
}
-------------------------------------------------------------------------------------------------------------------------------

-> Advantages of Using Exception Filters

(i) Centralized error handling simplifies maintenance.

(ii) Reduces repetitive try-catch blocks across controllers and actions.

(iii) Facilitates consistent user experience in error scenarios.

-> Limitations

(i) Exception filters won't catch exceptions that occur during routing, controller instantiation, or before the MVC pipeline begins.

(ii) They do not handle HTTP errors like 404 (Not Found); these are typically managed through custom error pages in the web.config.

By leveraging exception filters effectively, you can create robust error-handling mechanisms for your ASP.NET MVC applications.

*/