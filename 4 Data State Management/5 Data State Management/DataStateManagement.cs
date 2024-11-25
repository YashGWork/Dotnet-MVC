/* Data State Management

In ASP.NET MVC, ViewData, ViewBag, and TempData are commonly used for passing data between controllers and views, but each has distinct features and purposes. Here’s a breakdown of each:


In ASP.NET MVC, ViewData, ViewBag, and TempData are commonly used for passing data between controllers and views, but each has distinct features and purposes. Here’s a breakdown of each:

1. ViewData

- Definition: ViewData is a dictionary (specifically a ViewDataDictionary) that allows passing data from a controller to a view.

- Type: It’s a Dictionary<string, object>. 

- TypeCasting: ViewData requires typecasting when you use complex data type to avoid error.

- Scope: Limited to the current request; data stored in ViewData is only accessible during a single request (controller to view).

- Persistence: The Persistence of a ViewData object exists only during the current request. The value of the ViewData becomes null if the request is redirected.

- Syntax:

ViewData["KeyName"] = value; 

where Key is a string value to identify the object present in ViewData, Value is the object present in the ViewData.

- Compile Time Error Checking - ViewData does not provide compile time error checking, it only supports Run Time Error checking.
 
- Usage:

-----------------------------------------------------------------------------------------------------------------
public ActionResult Index()
{
    ViewData["Message"] = "Hello from ViewData!";
    return View();
}
-----------------------------------------------------------------------------------------------------------------

- Access in View:

-----------------------------------------------------------------------------------------------------------------
@ViewData["Message"]
-----------------------------------------------------------------------------------------------------------------

- Data Retrieval: Because it’s a dictionary, typecasting is needed to retrieve values if they are not strings.

- Persistence: Loses data when a new request is made (e.g., after a redirect).

2. ViewBag

- Definition: ViewBag is a dynamic wrapper around ViewData. It uses the dynamic keyword introduced in C#, allowing for a more readable syntax without typecasting.

- Type: Dynamic; uses a dynamic type which resolves properties at runtime. Type casting while using ViewBag is not required.

- Typecasting - ViewBag does not require typecasting when you use complex data type.

- Scope: Same as ViewData; data is available only for the current request (controller to view).

- Compile Time Error Checking - ViewBag does not provide compile time error checking, it only supports Run Time Error checking.

- Syntax: 

ViewBag.PropertyName = value;

Value: Is the value of the property of the ViewBag, Value may be String, object, list, array or a different type, such as int, char, float, double DateTime etc.

-Persistence: ViewBag exists only for the current request and becomes null if the request is redirected.


- Usage:

-----------------------------------------------------------------------------------------------------------------
public ActionResult Index()
{
    ViewBag.Message = "Hello from ViewBag!";
    return View();
}
-----------------------------------------------------------------------------------------------------------------

- Access in View:

-----------------------------------------------------------------------------------------------------------------
@ViewBag.Message
-----------------------------------------------------------------------------------------------------------------

- Advantages:

(i) No need to cast types explicitly since it uses dynamic.

(ii) Easier to read and less prone to errors with names.

(iii) Persistence: Like ViewData, ViewBag does not persist data after the request ends (i.e., after a redirect).

3. TempData

- Definition: TempData is a dictionary (based on TempDataDictionary) that’s used to store data temporarily. It helps pass data between two consecutive requests.

- Type: Dictionary<string, object>, similar to ViewData but with an additional capability to persist data across requests.Type casting is required while using TempData.

- TypeCasting: TempData value must be type casted before use, it's also a good practice to check for null values to avoid runtime error.

- Scope: Data stored in TempData persists across the current and next request only, making it useful for scenarios involving redirects.

- Syntax: 

TempData["KeyName"] = value;

where, 

KeyName: Is a String value to identify the object present in TempData.

Value: Is the object present in TempData.

- Compile Time Error Checking - TempData does not provide compile time error checking, it only supports Run Time Error checking.

- Persistence: tempData allows passing data from the current request to the subsequent requies during request redirection. We can further extend it using TempData.Keep("<KeyValue>") method for the next request.

- Usage:

-----------------------------------------------------------------------------------------------------------------
public ActionResult Index()
{
    TempData["Message"] = "This is TempData!";
    return RedirectToAction("Next");
}

public ActionResult Next()
{
    var message = TempData["Message"]; // Available here due to TempData persistence
    return View();
}
-----------------------------------------------------------------------------------------------------------------

- Data Retention:

(i) Once TempData is read, it’s removed from memory (i.e., TempData is a one-time-use storage).

(ii) Use TempData.Keep() to retain data for another request if needed.

(iii) Use TempData.Peek() to read data without marking it for deletion.

- Persistence: Especially useful for passing data during redirects, unlike ViewData and ViewBag.

-> Common Usage Scenarios

1.  ViewData and ViewBag:

- Useful for passing data like page titles, small pieces of data, or configuration values from the controller to the view.

- Because they are limited to the current request, they are best for single-request data sharing (e.g., showing a success message in the view after a form submission).

- They can be used interchangeably to access data associated with one another.

2. TempData:

- Ideal for scenarios requiring data to be available across multiple requests, such as redirects.

- Commonly used for success messages, error messages, or small pieces of data that need to persist after an action method redirects.

-> Key Points

- ViewData and ViewBag are largely interchangeable for passing data to views, with ViewBag being simpler due to dynamic typing.

- TempData stands apart for multi-request scenarios, retaining data across a redirect.

- In modern ASP.NET Core MVC, using TempData for flash messages is common, while ViewData and ViewBag can be replaced with View Models for more structured data handling.

-> Similarities and Differences between ViewData and ViewBag in MVC

1. Similarities

- Both ViewData and ViewBag are used to pass data from a controller to a view.

- Both Helps to maintain data when you move from controller to view.

- Short Persistence means value becomes null when redirection occurs.

- ViewData and ViewBag are DataDictionary objects.

- Both ViewData and ViewBag does not provided compile time error checking. For example - If you mis-spell keys you wouldn't get any compile time errors. You get to know about the error only at runtime.

2. Differences

- ViewData is a dictionary of objects that is derived from ViewDataDictionary class and is accessible using strings as keys whereas ViewBag uses Dynamic feature that was introduced in C# 4.0, it allows an object to have properties dynamically added to it.

- ViewData requires typecasting when used with complex data type to avoid error whereas ViewBag does not require typecasting when used with complex data type.

- Syntactically both are different

ViewData["<Key>"] = <Value>;
ViewBag.<PropertyName> = <Value>;

*/



