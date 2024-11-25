/*

HTML Helpers

HTML Helpers in ASP.NET MVC are methods used to generate HTML markup for rendering UI elements in a view. They help developers create HTML controls like text boxes, labels, drop-down lists, and more, programmatically from the server side, making it easier to build dynamic, data-driven forms.

- Are extension methods to the Html Helper class, can be called only from views.

- An HTML helper is a method that is used to render html content in a view.

- Simplifies the process of creating a view.

- Allows generating HTML markup that you can resuse across the Web application.

There are mainly three type of HTML helpers in ASP.NET MVC:

1. Standard HTML Helpers

These are basic helper methods provided by the ASP.NET MVC framework. They simplify the process of creating HTML form elements by generating the necessary HTML markup automatically. Examples include:

- Html.TextBox("name"): Generates a <input type="text"> element.

- Html.Label("name"): Generates a <label> element.

- Html.DropDownList("name", items): Generates a <select> element with options.

2. Strongly Typed HTML Helpers

Strongly typed helpers bind directly to model properties, offering better compile-time checking and intellisense in Visual Studio. These helpers help reduce the risk of errors due to mismatched property names. Examples include:

- Html.TextBoxFor(m => m.Name): Creates a <input type="text"> element bound to the Name property of the model.

- Html.LabelFor(m => m.Name): Creates a <label> element for the Name property.

- Html.DropDownListFor(m => m.Category, items): Creates a <select> element bound to the Category property.

3. Custom HTML Helpers

Developers can create custom HTML helpers to encapsulate reusable HTML and logic that isn’t available with the built-in helpers. This is useful for complex controls or UI components. A custom helper is usually created by defining an extension method on HtmlHelper.

*/

/*

-> Standard HTML Helpers

Standard HTML Helpers in ASP.NET MVC are methods provided by the framework to simplify the creation of basic HTML form elements. These helpers generate the HTML markup for common form controls like text boxes, labels, and buttons, allowing developers to write less HTML manually and focus on server-side logic.

Standard HTML Helpers do not directly bind to model properties, so they rely on string-based identifiers for elements (unlike strongly typed HTML helpers). They are easy to use, especially when you don’t need to bind a control directly to a model property.

-> Examples of Standard HTML Helpers

Here are some commonly used standard HTML Helpers:

1. TextBox

Html.TextBox("name", "value") generates a text input field.

-----------------------------------------------------------------------------------------------
@Html.TextBox("FirstName", "John")
-----------------------------------------------------------------------------------------------

Output:

-----------------------------------------------------------------------------------------------
<input type="text" name="FirstName" id="FirstName" value="John" />
-----------------------------------------------------------------------------------------------

2. Password

Html.Password("name") generates a password input field, which hides the characters as they are typed.

-----------------------------------------------------------------------------------------------
@Html.Password("Password")
-----------------------------------------------------------------------------------------------

Output:

-----------------------------------------------------------------------------------------------
<input type="password" name="Password" id="Password" />
-----------------------------------------------------------------------------------------------

3. Hidden

Html.Hidden("name", "value") generates a hidden input field, often used to store values that need to be passed back with a form but not displayed to the user.

-----------------------------------------------------------------------------------------------
@Html.Hidden("UserId", "123")
-----------------------------------------------------------------------------------------------

Output:

-----------------------------------------------------------------------------------------------
<input type="hidden" name="UserId" id="UserId" value="123" />
-----------------------------------------------------------------------------------------------

4. Label

Html.Label("name", "label text") generates a label for an input field.

-----------------------------------------------------------------------------------------------
@Html.Label("FirstName", "First Name:")
-----------------------------------------------------------------------------------------------

Output:

-----------------------------------------------------------------------------------------------
<label for="FirstName">First Name:</label>
-----------------------------------------------------------------------------------------------

5. DropDownList

Html.DropDownList("name", IEnumerable<SelectListItem> items) generates a dropdown list.

-----------------------------------------------------------------------------------------------
@Html.DropDownList("Country", new SelectList(new[] { "USA", "Canada", "UK" }))
-----------------------------------------------------------------------------------------------

Output:

-----------------------------------------------------------------------------------------------
<select name="Country" id="Country">
    <option>USA</option>
    <option>Canada</option>
    <option>UK</option>
</select>
-----------------------------------------------------------------------------------------------

6. RadioButton

Html.RadioButton("name", "value") generates a radio button.

-----------------------------------------------------------------------------------------------
@Html.RadioButton("Gender", "Male") Male
@Html.RadioButton("Gender", "Female") Female
-----------------------------------------------------------------------------------------------

Output:

-----------------------------------------------------------------------------------------------
<input type="radio" name="Gender" id="Gender" value="Male" /> Male
<input type="radio" name="Gender" id="Gender" value="Female" /> Female
-----------------------------------------------------------------------------------------------

7. CheckBox

Html.CheckBox("name", bool isChecked) generates a checkbox.

----------------------------------------------------------------------------------------------
@Html.CheckBox("AgreeToTerms", true)
----------------------------------------------------------------------------------------------

Output:

----------------------------------------------------------------------------------------------
<input type="checkbox" name="AgreeToTerms" id="AgreeToTerms" checked="checked" />
----------------------------------------------------------------------------------------------

8. TextArea


Html.TextArea("name", "text") generates a multi-line text input.

----------------------------------------------------------------------------------------------
@Html.TextArea("Comments", "Your comments here")
----------------------------------------------------------------------------------------------

Output:

----------------------------------------------------------------------------------------------
<textarea name="Comments" id="Comments">Your comments here</textarea>
----------------------------------------------------------------------------------------------

9. Submit Button

Html.SubmitButton("button text") generates a submit button for form submission.

----------------------------------------------------------------------------------------------
@Html.SubmitButton("Submit")
----------------------------------------------------------------------------------------------

Output:

----------------------------------------------------------------------------------------------
<input type="submit" value="Submit" />
----------------------------------------------------------------------------------------------

10. Html.ActionLink

Html.ActionLink is an HTML helper used to generate an <a> (anchor) tag that links to a specified controller action. It is commonly used in views to create navigation links that are bound to MVC controller actions.

Syntax:

----------------------------------------------------------------------------------------------
@Html.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes)
----------------------------------------------------------------------------------------------

Example: 

----------------------------------------------------------------------------------------------
@Html.ActionLink("Go to Home", "Index", "Home", new { id = 1 }, new { @class = "btn btn-primary" })
----------------------------------------------------------------------------------------------

Output:

----------------------------------------------------------------------------------------------
@Html.ActionLink("Go to Home", "Index", "Home", new { id = 1 }, new { @class = "btn btn-primary" })
----------------------------------------------------------------------------------------------

11. Url.Action

Url.Action is used to generate a URL string that points to a specific controller action. It doesn’t generate an HTML element directly; instead, it simply returns the URL as a string, which can be useful for various scenarios such as building links in JavaScript, redirecting URLs, or generating form action URLs.

Syntax:

----------------------------------------------------------------------------------------------
@Url.Action(actionName, controllerName, routeValues)
----------------------------------------------------------------------------------------------

Example:

----------------------------------------------------------------------------------------------
@{
    string homeUrl = Url.Action("Index", "Home", new { id = 1 });
}
<a href="@homeUrl" class="btn btn-primary">Go to Home</a>
----------------------------------------------------------------------------------------------

Output:

----------------------------------------------------------------------------------------------
<a href="/Home/Index/1" class="btn btn-primary">Go to Home</a>
----------------------------------------------------------------------------------------------

Html.ActionLink generates a complete <a> tag for navigation, while Url.Action returns only the URL string for flexible use in links, forms, or scripts.

-> Advantages of Using Standard HTML Helpers

- Simplification of Markup: Reduces the amount of HTML markup you need to write manually.

- Automatic ID and Name Attributes: ASP.NET MVC automatically assigns id and name attributes based on the helper method parameters, ensuring they align with server-side naming conventions.

- Reusable Code: Standard helpers make it easier to reuse code snippets across views.

-> Limitations of Standard HTML Helpers

- No Strong Typing: Standard helpers are string-based and do not benefit from compile-time checking against model properties.

- Error Prone with Renaming: Since they rely on string identifiers, renaming a property requires manual updates.

-> Summary

Standard HTML Helpers are a straightforward way to create basic HTML elements programmatically in ASP.NET MVC. They are best suited for simpler forms or scenarios where direct binding to a model is not necessary. For scenarios that need model binding, Strongly Typed HTML Helpers are often a better choice.

 */

/*

Strongly Typed Html Helpers

In ASP.NET MVC, strongly typed HTML helpers are helper methods that are specifically designed to work with strongly typed views. They enable you to generate HTML elements that are bound to model properties, with IntelliSense and compile-time type checking, making the code more robust and maintainable.

-> Key Benefits of Strongly Typed HTML Helpers

(i) Type Safety: Strongly typed helpers are bound to specific model properties, providing compile-time checking and reducing runtime errors.

(ii) IntelliSense Support: When you use strongly typed helpers, you get IntelliSense for model properties in your editor, making it easier to write and navigate the code.

(iii) Cleaner Code: These helpers eliminate the need for string-based property names, reducing the risk of errors and making the code more readable and maintainable.

-> Common Strongly Typed HTML Helpers

Here are some of the commonly used strongly typed HTML helpers in ASP.NET MVC:

(i) Html.TextBoxFor: Generates a text box for a specified model property.

(ii) Html.TextAreaFor: Generates a text area for a specified model property.

(iii) Html.CheckBoxFor: Generates a checkbox for a specified model property.

(iv) Html.RadioButtonFor: Generates a radio button for a specified model property.

(v) Html.LabelFor: Generates a label for a specified model property.

(vi) Html.DropDownListFor: Generates a dropdown list for a specified model property.

(vii) Html.DisplayFor: Displays a read-only version of a specified model property.

(viii) Html.EditorFor: Generates a complete input UI for a specified model property, adapting based on the property type.

(ix) Html.HiddenFor: Generates a hidden input for a specified model property.

-> Example: Using Strongly Typed HTML Helpers

Suppose you have a model representing a User in your application:

--------------------------------------------------------------------------------------------
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsSubscribed { get; set; }
}
--------------------------------------------------------------------------------------------

-> Creating a Strongly Typed View

(i) To use strongly typed HTML helpers, you need a view that is strongly typed to this User model.

(ii) Create a view for User (e.g., CreateUser.cshtml) and specify the model type at the top of the view using @model.

Use strongly typed helpers to bind HTML elements directly to model properties.

--------------------------------------------------------------------------------------------
@model YourNamespace.Models.User

<h2>Create User</h2>

@using (Html.BeginForm("CreateUser", "User", FormMethod.Post))
{
    <div>
        @Html.LabelFor(model => model.FirstName)
        @Html.TextBoxFor(model => model.FirstName, new { @class = "form-control" })
    </div>

    <div>
        @Html.LabelFor(model => model.LastName)
        @Html.TextBoxFor(model => model.LastName, new { @class = "form-control" })
    </div>

    <div>
        @Html.LabelFor(model => model.Email)
        @Html.TextBoxFor(model => model.Email, new { @class = "form-control" })
    </div>

    <div>
        @Html.LabelFor(model => model.IsSubscribed)
        @Html.CheckBoxFor(model => model.IsSubscribed)
    </div>

    <button type="submit">Submit</button>
}
--------------------------------------------------------------------------------------------

-> Explanation of the Strongly Typed Helpers Used

- Html.LabelFor(model => model.FirstName): Generates a label for the FirstName property. The label text is automatically set to the property name (FirstName) by default.

- Html.TextBoxFor(model => model.FirstName): Creates a text box bound to the FirstName property of the User model. Any value set for FirstName will be displayed in this text box, and any user input will update the model property when posted.

- Html.TextBoxFor(model => model.Email): Similar to the above, this creates a text box for the Email property, allowing the user to enter an email address.

- Html.CheckBoxFor(model => model.IsSubscribed): Generates a checkbox for the IsSubscribed property. If IsSubscribed is true, the checkbox will be checked; if false, it will be unchecked.

- Html.DisplayNameFor(): Is used to display the values of the model properties.

- Html.DisplayFor(): Is used to display the values of the model properties.

-> Other Strongly Typed Helpers
Here are some additional examples of strongly typed HTML helpers:

(i) Html.TextAreaFor: Used to create a multi-line text area for model properties, typically for larger text content.

--------------------------------------------------------------------------------------------
@Html.TextAreaFor(model => model.Description, new { @class = "form-control", rows = "4" })
--------------------------------------------------------------------------------------------

(ii) Html.DropDownListFor: Generates a dropdown list. You can pass a SelectList to populate the dropdown with values.

-------------------------------------------------------------------------------------------
@Html.DropDownListFor(model => model.Country, new SelectList(ViewBag.CountryList, "Value", "Text"), "Select Country")
-------------------------------------------------------------------------------------------

(iii) Html.HiddenFor: Creates a hidden input field for a model property, often used for IDs or data that shouldn't be visible to the user but needs to be submitted with the form.

-------------------------------------------------------------------------------------------
@Html.HiddenFor(model => model.Id)
-------------------------------------------------------------------------------------------

(iv) Html.DisplayFor and Html.EditorFor: Used to render the display or editor template for a model property. DisplayFor is used to show read-only values, while EditorFor automatically selects the appropriate input based on the property type.


-------------------------------------------------------------------------------------------
@Html.DisplayFor(model => model.Email)
@Html.EditorFor(model => model.BirthDate)
-------------------------------------------------------------------------------------------

-> Summary

Strongly typed HTML helpers in ASP.NET MVC provide a type-safe way to generate HTML elements for model properties within a view. They improve:

- Code readability by providing IntelliSense and avoiding hard-coded strings for property names.

- Type safety by linking directly to model properties and allowing for compile-time checks.

- Efficiency by reducing the risk of errors and improving development productivity.

These helpers make it easier to build robust, maintainable, and error-free MVC applications, with views that are tightly integrated with their model data.

*/