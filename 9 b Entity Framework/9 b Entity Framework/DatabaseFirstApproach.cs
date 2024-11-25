/*

Database First Approach

The Database First approach in Entity Framework is a workflow where you start with an existing database, and Entity Framework generates the corresponding data model (entity classes and DbContext) for your application. This approach is particularly useful when working with a pre-existing database.

In the context of an ASP.NET MVC application, you can use LINQ (Language Integrated Query) with the generated entity model to query and manipulate data. Here’s a breakdown of how to use the Database First approach with LINQ in an ASP.NET MVC project:


The Database First approach in Entity Framework is a workflow where you start with an existing database, and Entity Framework generates the corresponding data model (entity classes and DbContext) for your application. This approach is particularly useful when working with a pre-existing database.

In the context of an ASP.NET MVC application, you can use LINQ (Language Integrated Query) with the generated entity model to query and manipulate data. Here’s a breakdown of how to use the Database First approach with LINQ in an ASP.NET MVC project:

-> Steps to Use Database First with LINQ in .NET MVC

1. Set Up Your Project

- Create an ASP.NET MVC project in Visual Studio.

- Add a connection string to your database in the Web.config file.

2. Generate the Entity Framework Model

- Right-click on the project, select Add > New Item.

- Choose ADO.NET Entity Data Model.

- Select EF Designer from Database and connect to your database.

- Choose the tables, views, and stored procedures you want to include in the model.

- Entity Framework will generate .edmx files, classes for the tables (entities), and the DbContext.

3. Access Data Using LINQ

LINQ provides a concise and type-safe way to query the database using the generated DbContext and entity classes.

-> Example:

Assume your database has a table named Products.

1. Retrieve Data Use LINQ to fetch data from the Products table:

--------------------------------------------------------------------------------------------------------
using (var context = new YourDbContext())
{
    var products = context.Products.ToList(); // Retrieve all products
}
--------------------------------------------------------------------------------------------------------

2. Filter Data Apply filtering criteria:

--------------------------------------------------------------------------------------------------------
var filteredProducts = context.Products
                               .Where(p => p.Price > 100)
                               .ToList(); // Get products with price > 100
--------------------------------------------------------------------------------------------------------

3. Sort Data Sort products by name:

--------------------------------------------------------------------------------------------------------
var sortedProducts = context.Products
                             .OrderBy(p => p.Name)
                             .ToList();
--------------------------------------------------------------------------------------------------------

4. Project Data Select specific columns:

--------------------------------------------------------------------------------------------------------
var productNames = context.Products
                           .Select(p => new { p.Id, p.Name })
                           .ToList();
--------------------------------------------------------------------------------------------------------

5. Add, Update, and Delete Data

(i) Add

--------------------------------------------------------------------------------------------------------
var newProduct = new Product
{
    Name = "New Product",
    Price = 150
};
context.Products.Add(newProduct);
context.SaveChanges();
--------------------------------------------------------------------------------------------------------

(ii) Update

--------------------------------------------------------------------------------------------------------
var product = context.Products.FirstOrDefault(p => p.Id == 1);
if (product != null)
{
    product.Price = 200;
    context.SaveChanges();
}
--------------------------------------------------------------------------------------------------------

(iii) Delete

--------------------------------------------------------------------------------------------------------
var product = context.Products.FirstOrDefault(p => p.Id == 1);
if (product != null)
{
    context.Products.Remove(product);
    context.SaveChanges();
}
--------------------------------------------------------------------------------------------------------

4. Integrate with MVC Controllers and Views

Use the LINQ queries inside controllers to provide data to the views.

(i) Controller Example:

--------------------------------------------------------------------------------------------------------
public class ProductsController : Controller
{
    private YourDbContext db = new YourDbContext();

    public ActionResult Index()
    {
        var products = db.Products.ToList();
        return View(products);
    }
}
--------------------------------------------------------------------------------------------------------

(ii) View Example: Create a Razor view to display the product list:

--------------------------------------------------------------------------------------------------------
@model IEnumerable<YourNamespace.Product>

<table>
    <thead>
        <tr>
            <th>Name</th>
            <th>Price</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var product in Model)
        {
            <tr>
                <td>@product.Name</td>
                <td>@product.Price</td>
            </tr>
        }
    </tbody>
</table>
--------------------------------------------------------------------------------------------------------

-> Benefits of Using LINQ with Database First

- Strongly Typed Queries: Errors in queries are caught at compile time.

- Ease of Use: LINQ integrates seamlessly with the Entity Framework, making it easy to write readable and concise queries.

- Maintainability: Queries are easier to understand and maintain compared to raw SQL.

-> When to Use Database First

- When working with an existing database.

- When the database schema changes frequently (it’s easier to regenerate the model).

- When you prefer to manage the database separately from the application.

This combination of Database First and LINQ provides a powerful and flexible way to work with data in .NET MVC applications.


*/

/*

Scaffold Column Annotation

The ScaffoldColumn annotation in .NET MVC is used in the Entity Framework or MVC models to control whether a particular property in a model should be included (or excluded) when scaffolding views or forms. It is part of the System.ComponentModel.DataAnnotations namespace.

Syntax

-------------------------------------------------------------------------------------------------------
[ScaffoldColumn(false)]
public DataType PropertyName { get; set; }
-------------------------------------------------------------------------------------------------------

The attribute takes a boolean value:

- true (default): The property will be included during scaffolding.

- false: The property will be excluded during scaffolding.

-> Use Case

When generating views or forms (e.g., using @Html.EditorForModel() or scaffolding tools in Visual Studio), sometimes certain properties of a model should not be displayed to the user. These properties might be:

- Internal properties like IDs or audit fields (CreatedDate, ModifiedBy).

- Properties that should not be editable by the user in a form.

-> Example

Model

Consider a Product class:

-------------------------------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    [ScaffoldColumn(false)]
    public DateTime CreatedDate { get; set; } // Exclude from scaffolding

    public decimal Price { get; set; }
}
-------------------------------------------------------------------------------------------------------

Explanation

- Id, Name, and Price will be included when scaffolding a view or form.

- CreatedDate will be excluded.

-> Impact on Scaffolding

1. View with 

Html.EditorForModel: If you scaffold a Create or Edit view:

-------------------------------------------------------------------------------------------------------
@model YourNamespace.Product
@Html.EditorForModel()
-------------------------------------------------------------------------------------------------------

The generated form will include input fields for Id, Name, and Price, but not for CreatedDate.

2. Generated Razor Code:

-------------------------------------------------------------------------------------------------------
<div class="form-group">
    <label for="Name">Name</label>
    <input type="text" id="Name" name="Name" class="form-control" />
</div>
<div class="form-group">
    <label for="Price">Price</label>
    <input type="text" id="Price" name="Price" class="form-control" />
</div>
-------------------------------------------------------------------------------------------------------

No fields for CreatedDate.

-> Alternative to ScaffoldColumn

- Explicit Model Binding: You can also control what fields are included in views by creating a ViewModel or excluding properties manually, but ScaffoldColumn is a quicker and declarative way to achieve this.

- [Bind] Attribute: Use this for fine-grained control over model properties during action binding in controllers.

-> Best Practices

- Use ScaffoldColumn(false) for properties that should never be exposed in the UI.

- For properties that require limited exposure (e.g., in some views but not others), consider using a ViewModel instead.

*/