/*

Result Filters

Result filters are a type of filter in ASP.NET MVC that are executed before and after the execution of an action result (such as rendering a view, redirecting, or returning JSON data). They allow developers to perform additional operations or modify the result after the action method has executed

 - A result filter operates on the result that an action method returns.

 - The OutputCacheAttribute class is one example of a result filter, which is used to mark an action method whose output will be cached.

 - The OutputCache filter indidcates the MVC Framework to cache the output from an action method.

 - The same content can be reused to service subsequent requests for the same URL.

 - Caching action output can offer a significant increase in performance, because most of the time-consuming activities required to process are avoided.
 
 - Following code snipped shows how to use the OutputCache attribute

-----------------------------------------------------------------------------------
public class HomeController: Controller {

   [OutputCache]
   public ActionResult Index() {
   
   // some code
   
}
-----------------------------------------------------------------------------------

 - In this code, the [OutputCache] attribute is added to the Index() action method of the Home controller.

 - You can specify the duration for which the output of the action should be cached by specifying a Duration property with the duration time in seconds.

 - Location property specifies where the output is to be cached. It takes enum value and can be stored at: 
 
    -> Any (default value)
    -> Client
    -> Downstram (proxy server)
    -> None (no caching)
    -> Server
    -> ServerAndClient
 



-> Purpose of Result Fitlers

(i) Pre-Processing Results:

Modify or prepare the ActionResult before it is sent to the client.

(ii) Post-Processing Results:

Perform cleanup or additional logging after the result has been executed.

(iii) Cross-Cutting Concerns:

Handle tasks like caching, content modification, logging, or response compression.

-> How Result Filters Work

(i) Result filters are executed after the action method has completed its execution.

(ii) They operate around the result returned by the action (such as a ViewResult, JsonResult, etc.).

(iii) Two key methods define their lifecycle:

- OnResultExecuting: Called before the action result is executed.

- OnResultExecuted: Called after the action result is executed

-> Implementing a Custom Result Filter

To create a custom result filter, you can either:

(i) Inherit from ActionFilterAttribute.
(ii) Implement the IResultFilter interface.

Example: Custom Result Fitler

-------------------------------------------------------------------------------------
public class CustomResultFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        // Code to execute before the action result
        filterContext.HttpContext.Response.Write("<p>Result is about to execute!</p>");
    }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        // Code to execute after the action result
        filterContext.HttpContext.Response.Write("<p>Result execution finished!</p>");
    }
}
-------------------------------------------------------------------------------------

-> Applying the Custom Filter

(i) At the Action Level:

-------------------------------------------------------------------------------------
[CustomResultFilter]
public ActionResult Index()
{
    return View();
}
-------------------------------------------------------------------------------------

(ii) At the Controller Level:

-------------------------------------------------------------------------------------
[CustomResultFilter]
public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
-------------------------------------------------------------------------------------

(iii) Globally: Register the filter in the FilterConfig class:

-------------------------------------------------------------------------------------
public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new CustomResultFilter());
    }
}
-------------------------------------------------------------------------------------

-> Use Cases for Result Filters

1. Modifying Response Headers:

- Add custom headers to the HTTP response.

-------------------------------------------------------------------------------------
public override void OnResultExecuting(ResultExecutingContext filterContext)
{
    filterContext.HttpContext.Response.Headers["X-Custom-Header"] = "Value";
}
-------------------------------------------------------------------------------------

2. Logging:

Log the details of the result being returned.

-------------------------------------------------------------------------------------
public override void OnResultExecuted(ResultExecutedContext filterContext)
{
    var resultType = filterContext.Result.GetType().Name;
    Debug.WriteLine($"Result of type {resultType} executed.");
}
-------------------------------------------------------------------------------------

3. Dynamic Caching:

- Implement dynamic caching based on the result content.

4. Custom Rendering:

- Modify the content of the response before sending it to the client.

-> Execution Order

- Result filters are executed after action filters.

- They wrap the execution of the result, meaning the OnResultExecuting method runs before the result is processed, and the OnResultExecuted method runs after.

-> ResultExecutingContext and ResultExecutedContext

(i) ResultExecutingContext:

Provides information before the result is executed.

 > Key properties:

   - Result: The ActionResult being executed.

   - Controller: The controller that generated the result.

   - HttpContext: The HTTP context of the request.

(ii) ResultExecutedContext:

Provides information after the result is executed.

 > Key properties:

   - Result: The executed ActionResult.

   - Exception: Any exception that occurred during result execution.

   - ExceptionHandled: A flag indicating whether the exception was handled.

-> Default Filters in ASP.NET MVC

The ASP.NET MVC framework provides built-in support for result filters through the ActionFilterAttribute class. These are widely used for logging, caching, or other post-action operations.

-> Advantages of Result Filters

1. Centralized Logic:

 - Encapsulate reusable logic related to result modification or logging.

2. Customizability:

 - Tailor the behavior of result processing dynamically.

3. Flexibility:

 - Can be applied at multiple levels (action, controller, or global).

-> Limitations of Result Filters

1. Limited to Results:

 - They cannot modify the action execution logic; for that, use action filters.

2. Complexity:

 - Overuse of custom filters can make debugging and maintenance challenging.

3. Performance Impact:

 - Result filters add additional processing to each request.

-> Summary

- What are Result Filters? Filters that wrap the result execution process, allowing pre- and post-processing of action results.

- When to Use Them? For tasks such as logging, modifying response headers, or implementing caching.

- Key Methods:

   - OnResultExecuting: Executes before the result.

   - OnResultExecuted: Executes after the result.

By using result filters effectively, you can extend and customize the behavior of your ASP.NET MVC application’s response handling in a clean and reusable way.


*/