/*

Razor Syntax

View:

- Provides the UI of the application to the user.

- It is used to display content of an application and also to accept user inputs.

- Uses model data to create this UI.

- Contains both HTML markup and C# code that runs on the Web server.

- View has a file extension .cshtml

-> Razor engine 

The MVC Framework uses a view engine to convert the code of a view into HTML markup that a browser can understand.

- It is uses a default view engine by the MVC Framework.

- Compiles a view of your application when the view is requested for the first time. 

- Delivers the compiled view for subsequent request untilyou make changes to the view.

- Does not introduce a new set of programming language, but provides themplate markup syntax to segregate HTML markup and programming a code in a view.

-> A Razor

- First requires identifying the server-side code from the markup code to interpret the server-side code embedded inside a view file.

- Uses the @symbol, to seperate the server-side code from the markup code.

Razor is a markup syntax used in ASP.NET MVC views to create dynamic web pages. It allows developers to embed server-side code within HTML markup, making it easy to generate dynamic content. Razor syntax is concise and expressive, enabling a seamless integration of C# code with HTML.

-> Key Features of Razor Syntax

1. Code blocks: Razor allows you to write C# code directly in your views using the @ symbol. This can be used to execute code, declare variables, or call methods.

-----------------------------------------------------------------------------------------------------------------
@{
    var message = "Hello, World!";
}
<h1>@message</h1>
-----------------------------------------------------------------------------------------------------------------

2. Inline Expression:

You can use the @ symbol to output the value of a variable or expression directly into the HTML.

-----------------------------------------------------------------------------------------------------------------
<p>The current date is: @DateTime.Now</p>
-----------------------------------------------------------------------------------------------------------------

3. Control Structures: 

Razor supports standard C# control structures like if, for, foreach, and while. This allows you to conditionally render content or iterate over collections.

-----------------------------------------------------------------------------------------------------------------
@if (Model.IsAdmin)
{
    <p>Welcome, Admin!</p>
}
else
{
    <p>Welcome, User!</p>
}

<ul>
@foreach (var item in Model.Items)
{
    <li>@item.Name</li>
}
</ul>
-----------------------------------------------------------------------------------------------------------------

4. HTML Helpers: 

Razor views can utilize HTML helpers to generate HTML elements. These helpers are methods that return HTML strings and can be used to create forms, links, and other UI elements.
 
-----------------------------------------------------------------------------------------------------------------
@Html.TextBoxFor(model => model.Name)
@Html.LabelFor(model => model.Name)
-----------------------------------------------------------------------------------------------------------------

5. Layout Pages

Razor supports layout pages, which allow you to define a common structure for your views. You can use the @RenderBody() method in the layout to specify where the content of individual views should be rendered. _Layout.cshtml:

-----------------------------------------------------------------------------------------------------------------
<!DOCTYPE html>
<html>
<head>
    <title>@ViewBag.Title</title>
</head>
<body>
    <header>
        <h1>My Application</h1>
    </header>
    <div>
        @RenderBody()
    </div>
    <footer>
        <p>© 2023 My Application</p>
    </footer>
</body>
</html>
-----------------------------------------------------------------------------------------------------------------

View:

-----------------------------------------------------------------------------------------------------------------
@{
    Layout = "_Layout";
    ViewBag.Title = "Home Page";
}
<h2>Welcome to the Home Page!</h2>
-----------------------------------------------------------------------------------------------------------------

6. Partial Views: 

Razor allows you to create reusable components called partial views. These are views that can be rendered within other views, promoting code reuse. Partial View (_MyPartial.cshtml):

-----------------------------------------------------------------------------------------------------------------
<div>
    <h3>Partial View Content</h3>
</div>
-----------------------------------------------------------------------------------------------------------------

Using Partial View:

-----------------------------------------------------------------------------------------------------------------
@Html.Partial("_MyPartial")
-----------------------------------------------------------------------------------------------------------------

7. ViewData and ViewBag: 

Razor views can access data passed from the controller using ViewData and ViewBag. ViewData is a dictionary, while ViewBag is a dynamic object.

-----------------------------------------------------------------------------------------------------------------
<h2>@ViewBag.Title</h2>
<p>@ViewData["Message"]</p>
-----------------------------------------------------------------------------------------------------------------

8. Model Binding:

Razor views can strongly type their content by specifying a model type at the top of the view. This allows for IntelliSense support and compile-time checking.

-----------------------------------------------------------------------------------------------------------------
@model MyApp.Models.Product

<h2>@Model.Name</h2>
<p>Price: @Model.Price</p>
-----------------------------------------------------------------------------------------------------------------

-> Conclusion

Razor syntax in ASP.NET MVC views provides a powerful and flexible way to create dynamic web pages. By combining HTML with C# code, developers can easily generate content based on user input, model data, and application logic. The use of layout pages, partial views, and HTML helpers further enhances the maintainability and reusability of the code, making Razor a popular choice for building web applications in the ASP.NET ecosystem.



 */