/*

Form Control

-> Validation Message and Validation Summary

ValidationMessage and ValidationSummary are helper methods used to display validation errors in forms, making it easier for users to understand what they need to correct. Here’s an overview of each method and how it works:

1. ValidationMessage Method

The ValidationMessage method displays an error message for a specific field when validation fails. It shows the message next to the input field that has an error, which helps users quickly identify what went wrong. It is a loosely typed method.

Syntax:

--------------------------------------------------------------------------------------------
@Html.ValidationMessage("FieldName", "Custom Error Message", new { htmlAttributes })
--------------------------------------------------------------------------------------------

Parameters:

- FieldName: The name of the property (from your model) that needs validation.

- Custom Error Message (optional): A custom message to override the default one set in the validation attribute.

- htmlAttributes (optional): An object to set HTML attributes like CSS classes for styling.

Example:

In a model, suppose we have a Required attribute for a Name property:

--------------------------------------------------------------------------------------------
public class MyModel
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}
--------------------------------------------------------------------------------------------

In the view:

--------------------------------------------------------------------------------------------
@Html.TextBoxFor(m => m.Name)
@Html.ValidationMessage("Name")
--------------------------------------------------------------------------------------------

If the user leaves the Name field empty, they will see "Name is required" next to the textbox.

2. ValidationSummary Method

The ValidationSummary method displays a list of all validation errors in one place. It’s typically placed at the top of the form and provides a summary of all form validation errors, which is useful when you want to notify users of multiple issues at once. It displays the validation messages that are in the ModelStateDictionary object.

The ValidationSummary can be used to display all the error messages for all the fields. It can also be used to display custom error messages.

Syntax:

--------------------------------------------------------------------------------------------
@Html.ValidationSummary(includePropertyErrors, "Custom Message", new { htmlAttributes })
--------------------------------------------------------------------------------------------

Parameters:

- includePropertyErrors (boolean, optional): Specifies whether or not to include field-specific errors. If set to false, only model-level errors are shown.

- Custom Message (optional): A message to display at the top of the validation summary.

- htmlAttributes (optional): An object to set HTML attributes for styling.

Example:

--------------------------------------------------------------------------------------------
@Html.ValidationSummary(true, "Please fix the following errors:")

--------------------------------------------------------------------------------------------
If there are validation errors in the form, they will be displayed in a list format above the form, along with the custom message.

Usage in a Form:

--------------------------------------------------------------------------------------------
@using (Html.BeginForm())
{
    @Html.ValidationSummary(true, "Please correct the errors below.")
    
    <div>
        @Html.LabelFor(m => m.Name)
        @Html.TextBoxFor(m => m.Name)
        @Html.ValidationMessageFor(m => m.Name)
    </div>

    <button type="submit">Submit</button>
}
--------------------------------------------------------------------------------------------

If the form is submitted with errors, the summary will display all errors, and specific fields will also display their respective error messages.

-> Key Points:

- ValidationMessage is for individual field error messages.

- ValidationSummary is for displaying all form errors together.

- Both methods work well with data annotations on the model and make client and server-side validation errors clear to users.

3. ModelState

ModelState is an object that holds the state of the model binding and validation for a request. It plays a key role in tracking validation errors and allows you to check if a form submission contains valid data before processing it.

-> Key Concepts of ModelState

(i) ModelState Dictionary:

ModelState is a dictionary that contains the state of each form field submitted with a request. Each entry in ModelState has:

- The key, which is usually the name of the form field.

- A value, which contains information about the field, including any validation errors.

(ii) ModelState.IsValid:

- The ModelState.IsValid property is a Boolean that indicates whether all fields are valid.
It returns true if there are no validation errors in any of the fields; otherwise, it returns false.

- This property is often used in controller actions to check if the model data is valid before performing further operations.

(iii) ModelState Errors:

- Validation errors are stored in ModelState. When data annotations (such as [Required], [StringLength], etc.) are used on the model properties, ASP.NET MVC automatically validates the model properties against these attributes and populates ModelState with any errors.

- You can access specific errors for each field through ModelState["FieldName"].Errors.

-> Example Usage of ModelState

Consider a simple Person model with validation attributes:

--------------------------------------------------------------------------------------------
public class Person
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Range(0, 150, ErrorMessage = "Age must be between 0 and 150")]
    public int Age { get; set; }
}
--------------------------------------------------------------------------------------------

In the controller, you might have an action method that checks if the model is valid:

--------------------------------------------------------------------------------------------
public ActionResult SubmitPerson(Person person)
{
    if (ModelState.IsValid)
    {
        // Model is valid, proceed with saving data or other logic
        return RedirectToAction("Success");
    }
    else
    {
        // Model is invalid, return view with validation messages
        return View(person);
    }
}
--------------------------------------------------------------------------------------------

If Name or Age is missing or invalid, ModelState.IsValid will be false, and the view will return with error messages for each invalid field.

-> Adding Custom Errors to ModelState

You can manually add errors to ModelState if additional custom validation logic is required:

--------------------------------------------------------------------------------------------
if (string.IsNullOrWhiteSpace(person.Name))
{
    ModelState.AddModelError("Name", "Please provide a valid name.");
}

if (!ModelState.IsValid)
{
    return View(person);  // Displays the view with custom error messages
}
--------------------------------------------------------------------------------------------

-> Accessing and Displaying ModelState Errors in Views

In the view, use @Html.ValidationSummary() to display all validation errors or @Html.ValidationMessageFor(m => m.PropertyName) to display errors for specific fields.

-> Summary of ModelState Features

- Stores field data and validation errors: Useful for understanding the validation state of each field.

- IsValid: Checks if all fields pass validation.

- AddModelError: Allows adding custom validation errors.

- Validation Integration: Works seamlessly with data annotations and Html.ValidationSummary/Html.ValidationMessage methods in views to provide feedback to users.

ModelState is central to ensuring that the data entering your application is valid, providing both automatic and custom validation handling in ASP.NET MVC.

*/

/* 

Data Annotations


Data Annotations in ASP.NET MVC are a set of attributes that provide a declarative way to perform validation on model properties. They are part of the System.ComponentModel.DataAnnotations namespace and allow you to add validation rules, formatting options, and display configurations directly to your model classes.

-> Key Uses of Data Annotations in ASP.NET MVC

- Validation: Validate input fields on the server side. When applied to a model, data annotations are checked by the MVC framework during model binding, and any invalid data is added to ModelState, which is then used to display error messages to the user.

- Formatting and Display: Control how fields appear in the view, such as display names, format strings, and custom error messages.

-> Common Data Annotation Attributes

(i) Required

Marks a property as mandatory. If a field marked with [Required] is left empty, validation fails.

Example:

--------------------------------------------------------------------------------------------
[Required(ErrorMessage = "Name is required")]
public string Name { get; set; }
--------------------------------------------------------------------------------------------

(ii) StringLength

Restricts the length of a string property, with optional minimum and maximum length limits.

Syntax: [StringLength(maximumLength: int, MinimumLength = int, ErrorMessage = "string")]


Example

--------------------------------------------------------------------------------------------
[StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
public string Name { get; set; }
--------------------------------------------------------------------------------------------

(iii) Range

Validates that a numeric or date field falls within a specified range.

Syntax: [Range(minimum: double, maximum: double, ErrorMessage = "string")]


--------------------------------------------------------------------------------------------
[Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
public int Age { get; set; }
--------------------------------------------------------------------------------------------

(iv)  RegularExpression

Ensures a property matches a specified regular expression pattern. Useful for fields like email addresses, phone numbers, etc.

Syntax: [RegularExpression("pattern", ErrorMessage = "string")]


Example:

--------------------------------------------------------------------------------------------
[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
public string Email { get; set; }
--------------------------------------------------------------------------------------------

(v) EmailAddress

Simplified validation for email fields.

Syntax: [EmailAddress(ErrorMessage = "string")]

--------------------------------------------------------------------------------------------
[EmailAddress(ErrorMessage = "Invalid email address")]
public string Email { get; set; }
--------------------------------------------------------------------------------------------

(vi) Compare

Ensures two fields match, often used for password confirmation.

Syntax: [Compare("OtherProperty", ErrorMessage = "string")]

--------------------------------------------------------------------------------------------
[Compare("Password", ErrorMessage = "Passwords do not match")]
public string ConfirmPassword { get; set; }
--------------------------------------------------------------------------------------------

(vii)  Display

Customizes the display name for a property in the view.

Syntax: [Display(Name = "string", Description = "string", Prompt = "string")]

--------------------------------------------------------------------------------------------
[Display(Name = "Full Name")]
public string Name { get; set; }
--------------------------------------------------------------------------------------------

(viii)  DisplayFormat

Controls how data is displayed. This attribute is useful for formatting dates and numbers.

Syntax: [DisplayFormat(DataFormatString = "string", ApplyFormatInEditMode = bool)]

--------------------------------------------------------------------------------------------
[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
public DateTime BirthDate { get; set; }
--------------------------------------------------------------------------------------------

(ix)  ScaffoldColumn

Specifies if a field should be displayed or hidden in scaffolding.

Syntax: [ScaffoldColumn(bool)]s

--------------------------------------------------------------------------------------------
[ScaffoldColumn(false)]
public DateTime CreatedDate { get; set; }
--------------------------------------------------------------------------------------------

(x) Phone

Validates that a property is in the format of a valid phone number.

Syntax: [Phone(ErrorMessage = "string")]

--------------------------------------------------------------------------------------------
[Phone(ErrorMessage = "Invalid phone number")]
public string PhoneNumber { get; set; }
--------------------------------------------------------------------------------------------

(xi)  CreditCard

Validates that a property contains a valid credit card number format.

Syntax: [CreditCard(ErrorMessage = "string")]

--------------------------------------------------------------------------------------------
[CreditCard(ErrorMessage = "Invalid credit card number")]
public string CreditCardNumber { get; set; }
--------------------------------------------------------------------------------------------

(xii) ReadOnly

It is used to indicate that a property is read-only, meaning it cannot be modified. This attribute is part of the System.ComponentModel namespace, rather than System.ComponentModel.DataAnnotations. We will be able to change data but it will not be able to bind to the model property.

Syntax: [ReadOnly(<boolean_value>)]

--------------------------------------------------------------------------------------------
[ReadOnly(true)]
--------------------------------------------------------------------------------------------

(xiii) DataType Data Annotation

It is used to provide information about the specific purpose of a property at run time.

Syntax: [DataType(Datatype.<value>)]

For eg) Password, MultilineText, Date, Time

--------------------------------------------------------------------------------------------
[DataType(DataType.Date)]
--------------------------------------------------------------------------------------------


-> Custom Data Annotations

ASP.NET MVC also allows you to create custom validation attributes by inheriting from ValidationAttribute. This is useful when your validation rules are complex or specific to your application’s logic.

Example:

--------------------------------------------------------------------------------------------
public class CustomValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Custom validation logic
        if ( condition )
        {
    return ValidationResult.Success;
}
return new ValidationResult("Custom error message");
    }
}
--------------------------------------------------------------------------------------------

-> Applying Data Annotations to Models

Here’s an example of how data annotations are applied to a model class:

--------------------------------------------------------------------------------------------
public class User
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }

    [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
    public int Age { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }
}

--------------------------------------------------------------------------------------------

-> Displaying Validation Messages in Views

In the view, you can use HTML helpers to display validation messages:

- @Html.ValidationSummary() to show a summary of all errors.

- @Html.ValidationMessageFor(m => m.PropertyName) to show an error message next to a specific field.

-> Benefits of Using Data Annotations

- Declarative Syntax: Validation rules are added directly to the model, making it easy to understand and maintain.

- Reusable: The same model validation can be applied across multiple views, ensuring consistency.

- Error Handling: Automatically adds validation errors to ModelState, which makes error handling simpler in controllers.

- Client-Side Validation: ASP.NET MVC can automatically generate JavaScript for client-side validation, reducing round-trips to the server.

Data Annotations are a powerful, built-in way to enforce rules and improve the user experience by ensuring data integrity at both the client and server levels in ASP.NET MVC applications.

*/