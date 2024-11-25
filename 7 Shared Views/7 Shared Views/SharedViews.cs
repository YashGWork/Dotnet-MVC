/* 

Shared Views

==============================================================================================================================
1. Layout view

In ASP.NET MVC, the layout view (commonly named _Layout.cshtml) acts as a master template for other views in the application. It defines a consistent structure and design for pages, such as headers, footers, navigation menus, and other reusable elements, helping to avoid duplicating HTML across views.

-> Key Points about the Layout View

i) Location and Naming

By convention, _Layout.cshtml is stored in the Views/Shared folder. It starts with an underscore (_) to indicate that it's a partial view intended for reuse across different views.

(ii) Defining Content Sections

The layout view defines placeholders, or "sections," for content that will vary across different pages. The most common placeholder is @RenderBody(), which is where each individual view’s content will be inserted. You can also define custom sections using @RenderSection("SectionName", required: false) for areas like sidebars or page-specific scripts.

(iii) 

Here's a basic structure for a layout view:

-----------------------------------------------------------------------------------------------
<!DOCTYPE html>
<html>
<head>
    <title>@ViewBag.Title - My Application</title>
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <header>
        <!-- Common header content, like navigation -->
        <nav>
            <a href="@Url.Action("Index", "Home")">Home</a>
            <a href="@Url.Action("About", "Home")">About</a>
        </nav>
    </header>

    <main>
        @RenderBody() <!-- Placeholder for the main content of the view -->
    </main>

    <footer>
        <!-- Common footer content -->
        <p>&copy; @DateTime.Now.Year - My Application</p>
    </footer>

    <!-- Optional Section for scripts -->
    @RenderSection("Scripts", required: false)
</body>
</html>
-----------------------------------------------------------------------------------------------


(iv) Using the Layout in Views

To specify a layout view for a particular view, you can set the Layout property at the top of the view file:

-----------------------------------------------------------------------------------------------
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
-----------------------------------------------------------------------------------------------

Alternatively, you can set a default layout in the _ViewStart.cshtml file, located in the Views folder:

-----------------------------------------------------------------------------------------------
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}
-----------------------------------------------------------------------------------------------

(v) Custom Sections

You can create custom sections in the layout (like @RenderSection("Scripts")), allowing views to inject specific content, such as JavaScript, where needed. Sections can be optional or required.

Example of defining a Scripts section in a layout:

-----------------------------------------------------------------------------------------------
@RenderSection("Scripts", required: false)
-----------------------------------------------------------------------------------------------

And using the section in a view:

-----------------------------------------------------------------------------------------------
@section Scripts {
    <script src="~/js/custom-script.js"></script>
}
-----------------------------------------------------------------------------------------------

-> Benefits of Using a Layout View

- Consistency: Ensures a uniform look and feel across all pages.

- DRY Principle: Reduces code repetition by centralizing the design structure.

- Easier Maintenance: Updating shared elements like headers, footers, or styles in the layout automatically updates them across all views.

-> Summary

The layout view in ASP.NET MVC acts as a master page template, allowing you to define a common structure for your web application and inject individual views’ content dynamically using @RenderBody and @RenderSection. This simplifies the creation of consistent and maintainable web pages.

==============================================================================================================================

2. Strongly Typed Views

In ASP.NET MVC, strongly typed views are views that are bound to a specific model type. This allows for compile-time checking and IntelliSense support, making it easier to work with model properties directly in the view. Strongly typed views are particularly useful for passing data between the controller and the view in a way that's type-safe and clear.

- Strongly typed view or strongly typed object is used to pass data from controller to a view.

- The view which binds with any model is called as stronly typed view.

- You can bindy any class as a mode to view. (Only one model can be bound to a view, can be bound to a single object or list of objects of the model)

- You can access model properties on that view.

- You can use data associated with model to render controls.

- The view that is designed by targeting specific model class object then that view is called "Strongly typed View". In stronly typed view, view is bind with corresponding model class object or list of objects.

-> How to Create a Strongly Typed View

(i) Define a Model Class

Create a model class with properties that represent the data you want to display or collect in the view.

-----------------------------------------------------------------------------------------------
// Models/User.cs
namespace YourNamespace.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
-----------------------------------------------------------------------------------------------

(ii) Specify the Model in the View

In the view file, declare the model type at the top with @model. This makes the view strongly typed to that specific model, so you can access its properties directly.

-----------------------------------------------------------------------------------------------
@model YourNamespace.Models.User

<h2>User Details</h2>

<div>
    <p>First Name: @Model.FirstName</p>
    <p>Last Name: @Model.LastName</p>
    <p>Email: @Model.Email</p>
</div>
-----------------------------------------------------------------------------------------------

Here, @model binds the User model to the view, making Model an instance of User so that its properties (FirstName, LastName, Email, etc.) are accessible with IntelliSense support.

(iii) Passing the Model from the Controller

In the controller, create an instance of the model and pass it to the view.

-----------------------------------------------------------------------------------------------
// Controllers/UserController.cs
using YourNamespace.Models;

public class UserController : Controller
{
    public ActionResult Details()
    {
        var user = new User
        {
            UserId = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        return View(user);
    }
}
-----------------------------------------------------------------------------------------------

When View(user) is called, ASP.NET MVC automatically passes the user model object to the view, allowing the view to render the User properties directly.

-> Benefits of Strongly Typed Views

(i) IntelliSense Support

Because the view is aware of the model’s type, you get IntelliSense support in Visual Studio, which makes it easier to access model properties and reduces the risk of typos.

(ii) Compile-Time Checking

Since the model type is defined, you’ll get compile-time checking for property names. If you try to access a property that doesn’t exist on the model, you’ll get an error at compile-time, not runtime.

(iii) Improved Readability and Maintainability

Strongly typed views make the code more readable and self-explanatory. You can see directly which model the view is working with, making it easier for others (or yourself, in the future) to understand and maintain.

(iv) Better Validation Support

Strongly typed views work well with model validation. You can define data annotations on the model properties (such as [Required], [StringLength], [Range], etc.) and the validation will automatically apply to input fields generated with HTML helpers.

-> Using HTML Helpers in Strongly Typed Views

In strongly typed views, you can use strongly typed HTML helpers to create form elements bound to the model properties:

-----------------------------------------------------------------------------------------------
@model YourNamespace.Models.User

@using (Html.BeginForm())
{
    <div>
        @Html.LabelFor(m => m.FirstName)
        @Html.TextBoxFor(m => m.FirstName)
    </div>
    
    <div>
        @Html.LabelFor(m => m.LastName)
        @Html.TextBoxFor(m => m.LastName)
    </div>
    
    <div>
        @Html.LabelFor(m => m.Email)
        @Html.TextBoxFor(m => m.Email)
    </div>
    
    <button type="submit">Submit</button>
}
-----------------------------------------------------------------------------------------------

Example Output

Assuming the User model has the following values:

FirstName = "John"
LastName = "Doe"
Email = "john.doe@example.com"

The output HTML will look something like this:

-----------------------------------------------------------------------------------------------
<form method="post" action="/User/Submit">
    <div>
        <label for="FirstName">First Name</label>
        <input type="text" id="FirstName" name="FirstName" value="John" />
    </div>
    <div>
        <label for="LastName">Last Name</label>
        <input type="text" id="LastName" name="LastName" value="Doe" />
    </div>
    <div>
        <label for="Email">Email</label>
        <input type="text" id="Email" name="Email" value="john.doe@example.com" />
    </div>
    <button type="submit">Submit</button>
</form>
-----------------------------------------------------------------------------------------------

-> Summary

Strongly typed views are bound to a specific model type, allowing you to use the model’s properties directly within the view.

They provide IntelliSense and compile-time checking for improved code accuracy.

Strongly typed views work well with HTML helpers that are specific to model properties, making it easier to build forms and handle validation.

They enhance readability, maintainability, and productivity in ASP.NET MVC applications.

==============================================================================================================================

3. Partial Views

In ASP.NET MVC, a partial view is a reusable view component that represents a part of a web page. It’s similar to a regular view but is designed to be rendered within other views, often to avoid repeating the same code across multiple pages. Partial views are commonly used for modular elements such as navigation menus, footers, and sections of forms.

-> Key Characteristics of Partial Views

- Reusable: Partial views allow for code reuse. You can create a single partial view and render it across multiple pages or views.

- Encapsulated: They encapsulate sections of HTML and logic that may be needed in various parts of an application.

- Lightweight: Partial views do not require a full-page reload, which helps to reduce server load and improve performance.

-> Creating a Partial View

(i) Add a Partial View

To create a partial view, add a new .cshtml file and name it appropriately (e.g., _UserDetails.cshtml), typically placing it in the Views/Shared folder so it’s accessible from any view in the application. 
By convention, partial views often start with an underscore (_) to distinguish them from regular views.

(ii) Design the Partial View
The partial view can contain HTML and Razor code, and it may also accept a model if necessary.

Example: _UserDetails.cshtml

-----------------------------------------------------------------------------------------------
@model YourNamespace.Models.User

<div class="user-details">
    <h3>@Model.FirstName @Model.LastName</h3>
    <p>Email: @Model.Email</p>
</div>
-----------------------------------------------------------------------------------------------

-> Rendering a Partial View

There are several ways to render a partial view in ASP.NET MVC:

(i) Using Html.Partial

Html.Partial renders the partial view as part of the current page without invoking the controller again. This is useful when you don’t need to pass any additional data or update the view model.

-----------------------------------------------------------------------------------------------
@Html.Partial("_UserDetails", Model)
-----------------------------------------------------------------------------------------------

_UserDetails is the name of the partial view file.

Model is the data being passed to the partial view (in this case, the User model).

(ii)  Using Html.RenderPartial

Html.RenderPartial works similarly to Html.Partial but writes the output directly to the response stream. It’s generally more efficient for rendering larger sections, but it doesn’t return a value and must be used within a code block (@{ }).

-----------------------------------------------------------------------------------------------
@{ Html.RenderPartial("_UserDetails", Model); }
-----------------------------------------------------------------------------------------------

(iii) Using Html.Action

Html.Action renders a partial view by invoking a specified action method. This is useful when the partial view requires its own data or business logic from the controller.

-----------------------------------------------------------------------------------------------
@Html.Action("UserDetails", "UserController", new { id = 1 })
-----------------------------------------------------------------------------------------------

In this example:

- "UserDetails" is an action in the UserController that fetches the user’s data.

- new { id = 1 } is a route parameter that the action method can use to fetch the specific user.

(iv) Using Html.RenderAction

Html.RenderAction is similar to Html.Action, but it writes output directly to the response stream and must be used in a code block.

-----------------------------------------------------------------------------------------------
@{ Html.RenderAction("UserDetails", "UserController", new { id = 1 }); }
-----------------------------------------------------------------------------------------------

-> When to Use Each Method

- Html.Partial / Html.RenderPartial: Use these if you already have the data needed in the current view model.

- Html.Action / Html.RenderAction: Use these if the partial view requires its own data or logic and should call an action method to fetch that data.

-> Example Usage

Let’s consider an example where you want to display user details as part of a page that shows a list of users.

Parent View: UserList.cshtml

-----------------------------------------------------------------------------------------------
@model List<YourNamespace.Models.User>

<h2>All Users</h2>

@foreach (var user in Model)
{
    <!-- Rendering the partial view for each user -->
    @Html.Partial("_UserDetails", user)
}
-----------------------------------------------------------------------------------------------

Partial View: _UserDetails.cshtml

-----------------------------------------------------------------------------------------------
@model YourNamespace.Models.User

<div class="user-details">
    <h3>@Model.FirstName @Model.LastName</h3>
    <p>Email: @Model.Email</p>
</div>
-----------------------------------------------------------------------------------------------

-> Passing a Model to a Partial View

Partial views can accept their own model, which can be passed when rendering them. In the example above, each user object from the list is passed to _UserDetails, allowing it to render each user’s details separately.

-> Using JavaScript and AJAX with Partial Views

Partial views can also be rendered dynamically with AJAX, enabling sections of the page to update without a full-page reload. You can use AJAX to load partial views and improve the user experience by reducing server requests and increasing responsiveness.


-> Benefits of Using Partial Views

(i) Reusability: Partial views make it easy to reuse sections of HTML and logic throughout the application, which reduces duplication.

(ii) Simplifies Views: By breaking down complex views into smaller partial views, you make the code more organized and easier to maintain.

(iii) Improves Performance: Since partial views don’t require a full-page reload, they help reduce the load on the server and improve page performance.

(iv) Modularity: Partial views enable a modular approach to UI development, making it easier to make isolated changes to specific sections of the UI.

-> Types of partial views

(i) Static 

Views whose layout not changed i.e header, footer, navigation bar etc.

For static partial views we use two methods of html helper class which are Html.Partial and Html.RenderPartial

(ii) Dynamic

Views whose contents can change accordingly, just like shopping cart where number of product can be changed.

For dynamic partial view we use two methods of html helper class which are Html.Action and Html.RenderAction

-> Summary

- Partial views are reusable view components that represent part of a page.

- They are typically used for shared UI components like headers, footers, forms, and navigation.

- Partial views can be rendered using Html.Partial, Html.RenderPartial, Html.Action, or Html.RenderAction, depending on whether additional data or action logic is required.

- They simplify complex views, improve code reusability, and enable AJAX-based dynamic updates for better performance.

 */


/* Difference between Html.Partial and Html.RenderPartial

In ASP.NET MVC, both Html.Partial and Html.RenderPartial are used to render partial views within a view, but there are some important distinctions between them. Let’s explore their similarities and differences.

-> Similarities

1. Purpose: Both Html.Partial and Html.RenderPartial are intended to render partial views (reusable sections of a web page) within a view.

2. Static Rendering: Neither method requires an additional controller action to be invoked; they render the partial view directly, using data already available in the current view model.

3. Reusability: Both are often used to include commonly shared UI components, such as headers, footers, or specific parts of forms.

4. Input Model Support: Both can accept a model object to pass data into the partial view if the partial view expects it.

-> Differences

1. Return type

Html.Partial returns an MvcHtmlString (HTML string)

Html.RenderPartial returns void: writed output directly to the response stream.

2. Usage

Html.Partial can be used inline within HTML or as part of an expression.

Html.RenderPartial must be used within a Razor code block (@{ })

3. Performace

Html.Partial is slightly slower due to returning an HTML string before rendering.

Html.RenderPartial is slightly faster as it writes directly to response stream, avoiding buffering.

4. Best For

Html.Partial is best for smaller, simpler partials or when HTML needs to be modified before rendering.

Html.RenderPartial is best for larger partials or cases where rendering performance is critical.

5. Syntax

@Html.Partial("_PartialViewName",model)

@Html.RenderPartial must be used in razor code block

@{
  Html.RenderPartial("_PartialViewName", model);
}

-> When to Use Each

Html.Partial: Ideal for simpler, smaller partial views, or if you need to manipulate the HTML string before displaying it (e.g., saving it to a variable). Because it returns an MvcHtmlString, it can be used in Razor expressions and other HTML tags.

Html.RenderPartial: Better suited for larger partial views or views where performance is a concern, as it writes directly to the response stream. Since it does not return an HTML string, it requires a Razor code block.

-> Summary of Key Points

- Return Type: Html.Partial returns an HTML string (MvcHtmlString), while Html.RenderPartial does not.

- Performance: Html.RenderPartial is slightly faster because it writes directly to the response stream.

- Syntax: Html.Partial can be used inline; Html.RenderPartial must be used within a Razor code block.

In general, for smaller partials and flexibility, use Html.Partial. For performance with larger partials, use Html.RenderPartial

*/

/*

Strongly Typed Partial View

In ASP.NET MVC, a strongly typed partial view is a partial view that is bound to a specific model type, allowing you to pass a model directly to it and access the model’s properties with IntelliSense and compile-time type checking. This approach ensures type safety and makes the code easier to read and maintain.

-> Why Use Strongly Typed Partial Views?

- Type Safety: The partial view has a defined model type, so you get compile-time checking and IntelliSense support. This reduces the chances of runtime errors.

- Data Passing: You can pass specific data (a model or a portion of it) to the partial view, making it easier to render customized, model-bound HTML.

- Reusability: Strongly typed partial views can be reused across multiple views, as long as the model type matches.

-> Creating a Strongly Typed Partial View

(i) Define a Partial View and Specify the Model Type

To create a strongly typed partial view, first, add a new partial view file (usually with an underscore prefix, like _UserDetails.cshtml). Then, specify the model type at the top of the partial view with @model.

-----------------------------------------------------------------------------------------------
// _UserDetails.cshtml
@model YourNamespace.Models.User

<div class="user-details">
    <h3>@Model.FirstName @Model.LastName</h3>
    <p>Email: @Model.Email</p>
</div>
-----------------------------------------------------------------------------------------------

In this example:

- @model YourNamespace.Models.User makes the partial view strongly typed to the User model.

- @Model.FirstName, @Model.LastName, and @Model.Email access properties of the User model directly.

(ii) Rendering a Strongly Typed Partial View

There are multiple ways to render a strongly typed partial view and pass the model data to it:

- Using Html.Partial: This renders the partial view as part of the current page without a full-page reload. Pass the model as a parameter.

-----------------------------------------------------------------------------------------------
@Html.Partial("_UserDetails", userModel)
-----------------------------------------------------------------------------------------------

- Using Html.RenderPartial: Similar to Html.Partial, but writes the output directly to the response stream, making it more efficient for larger content.

-----------------------------------------------------------------------------------------------
@{ Html.RenderPartial("_UserDetails", userModel); }
-----------------------------------------------------------------------------------------------

- Using Html.Action: If the partial view requires additional data preparation by a controller action, you can call Html.Action to invoke the action and render the partial view. This approach is more common with dynamically loaded partials.

-----------------------------------------------------------------------------------------------
@Html.Action("UserDetails", "User", new { id = userId })
-----------------------------------------------------------------------------------------------

(iii) Passing a Model to the Partial View

When rendering a strongly typed partial view, you need to provide the data that it will use. This is typically done by passing a model object as shown above. If you have a list of models, you can render the partial view for each item in a loop.

-----------------------------------------------------------------------------------------------
@model List<YourNamespace.Models.User>

<h2>All Users</h2>

@foreach (var user in Model)
{
    @Html.Partial("_UserDetails", user)
}
-----------------------------------------------------------------------------------------------

In this example, _UserDetails will be rendered for each user in the list, and each instance of _UserDetails will have its own User model data.

-> Example of Using a Strongly Typed Partial View with a Parent View

Parent View: UserList.cshtml

-----------------------------------------------------------------------------------------------
@model List<YourNamespace.Models.User>

<h2>All Users</h2>

@foreach (var user in Model)
{
    <!-- Rendering the strongly typed partial view for each user -->
    @Html.Partial("_UserDetails", user)
}
-----------------------------------------------------------------------------------------------

Strongly Typed Partial View: _UserDetails.cshtml

-----------------------------------------------------------------------------------------------
@model YourNamespace.Models.User

<div class="user-details">
    <h3>@Model.FirstName @Model.LastName</h3>
    <p>Email: @Model.Email</p>
</div>
-----------------------------------------------------------------------------------------------

Here, each user object from the list is passed to _UserDetails, allowing the partial view to render user details independently.

-> Benefits of Strongly Typed Partial Views

- Improved IntelliSense and Type Safety: You get IntelliSense for model properties and compile-time checking, which reduces errors and speeds up development.

- Cleaner Code: Since the model type is specified, there’s no need for casting or manually retrieving properties.

- Reusability and Consistency: The strongly typed partial view can be reused throughout the application, ensuring consistency in how the model data is displayed.

- Separation of Concerns: Each partial view focuses on rendering specific data, making the codebase more modular and maintainable.

-> Summary

A strongly typed partial view in ASP.NET MVC is a partial view that’s bound to a specific model type, allowing for type safety, IntelliSense support, and reusable UI components. It can be rendered using methods like Html.Partial, Html.RenderPartial, or Html.Action depending on the requirements. By using strongly typed partial views, developers can create modular, reusable, and maintainable code for presenting specific data throughout the application.


*/