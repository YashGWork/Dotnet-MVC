/*

Entity Framework

Entity Framework (EF) is an Object-Relational Mapper (ORM) in .NET that enables developers to work with databases using .NET objects. Instead of writing SQL queries, you can use LINQ to query the database, and EF will translate these LINQ queries into SQL for you. EF integrates well with ASP.NET MVC, making it easier to work with data in an MVC pattern.

Here's a detailed explanation of Entity Framework in the context of .NET MVC:

-> Key Concepts in Entity Framework

- DbContext: This is the primary class for interacting with the database. It acts as a bridge between the application and the database, providing access to the database tables through DbSet properties.

- DbSet: This property within the DbContext represents a collection of entities (a table) in the database. It allows you to perform CRUD operations on that table.

- ntities: These are .NET classes that represent the tables in the database. Each property in an entity class corresponds to a column in the table.

- LINQ to Entities: LINQ queries are used to retrieve data from the database using strongly typed classes, providing compile-time checking and IntelliSense support.

- Migrations: Migrations allow you to create and update the database schema based on your model classes. You can add, update, or delete tables and columns by defining migrations in EF.

-> Entity Framework Workflows

EF supports two main workflows: Code-First and Database-First.

- Code-First: You start by defining the entity classes (model classes) in code, and then EF generates the database tables based on these classes. This approach is useful if you prefer defining the database schema in code.

- Database-First: You start with an existing database, and EF generates the entity classes based on the tables in the database. This approach is useful if you're working with a legacy database.

-> Setting Up Entity Framework in .NET MVC

To use EF in an ASP.NET MVC application, follow these steps:

1. Install Entity Framework

Install the Entity Framework package via NuGet:

------------------------------------------------------------------------------------------------------
Install-Package EntityFramework
------------------------------------------------------------------------------------------------------

2. Define the Model Classes (Entities)

Create classes that represent the tables in your database. Each property in the class represents a column, and the class itself represents a table. For example, let’s define an Employee class:

------------------------------------------------------------------------------------------------------
public class Employee
{
    public int EmployeeID { get; set; }  // Primary Key
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
}
------------------------------------------------------------------------------------------------------

3. Create a DbContext Class

The DbContext class manages the entities and provides an interface to the database. Create a class that inherits from DbContext and define a DbSet property for each table:

------------------------------------------------------------------------------------------------------
public class MyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
}
------------------------------------------------------------------------------------------------------

In the above code, Employees represents the table in the database, allowing you to query or modify data for the Employee entity.

4. Configure the Database Connection

Add a connection string in the Web.config file to specify the database connection for EF:

------------------------------------------------------------------------------------------------------
<connectionStrings>
  <add name="MyDbContext" connectionString="Your_Connection_String" providerName="System.Data.SqlClient" />
</connectionStrings>
------------------------------------------------------------------------------------------------------

Entity Framework will automatically detect the DbContext name and link it to the specified connection string.

-> Performing CRUD Operations in Entity Framework with ASP.NET MVC

With EF and the DbContext set up, you can now perform CRUD operations. Here's how you might implement each operation in an MVC Controller:

1. Create a Controller

Generate a controller with EF support by using the following command in Visual Studio:

- Right-click on the Controllers folder > Add > Controller.

- Select MVC 5 Controller with views, using Entity Framework and follow the steps to create the controller.

Alternatively, you can manually create a controller:

------------------------------------------------------------------------------------------------------
public class EmployeeController : Controller
{
    private MyDbContext db = new MyDbContext();

    // Index (Read)
    public ActionResult Index()
    {
        var employees = db.Employees.ToList();
        return View(employees);
    }

    // Create (Add new Employee)
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    // Edit (Update Employee)
    [HttpGet]
    public ActionResult Edit(int id)
    {
        var employee = db.Employees.Find(id);
        return View(employee);
    }

    [HttpPost]
    public ActionResult Edit(Employee employee)
    {
        if (ModelState.IsValid)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    // Delete (Remove Employee)
    [HttpGet]
    public ActionResult Delete(int id)
    {
        var employee = db.Employees.Find(id);
        return View(employee);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var employee = db.Employees.Find(id);
        db.Employees.Remove(employee);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}
------------------------------------------------------------------------------------------------------

Each action in this controller performs a specific operation:

- Index: Lists all employees.

- Create: Adds a new employee.

- Edit: Updates an existing employee.

- Delete: Deletes an employee.


2. Add Views

After defining your controller actions, create corresponding views to display the forms and data. Views can be generated automatically or manually created in the Views folder.

-> Enabling Migrations in Entity Framework

EF provides migrations to update the database schema based on changes in the model classes.

1. Enable Migrations:

------------------------------------------------------------------------------------------------------
Enable-Migrations
------------------------------------------------------------------------------------------------------

2. Add a Migration:

------------------------------------------------------------------------------------------------------
Add-Migration InitialCreate
------------------------------------------------------------------------------------------------------

This creates a migration file with the changes to be applied to the database.

3. Update the Database:

------------------------------------------------------------------------------------------------------
Update-Database
------------------------------------------------------------------------------------------------------

This applies the migration to the database, creating tables or altering the schema as defined in the model classes.


-> Advantages of Using Entity Framework in ASP.NET MVC

- Productivity: EF speeds up development by eliminating most of the SQL writing, allowing developers to focus on the model classes and business logic.

- Type-Safety and IntelliSense: Strongly typed classes make it easier to catch errors at compile time.

- Database-agnostic: EF can work with various databases like SQL Server, MySQL, PostgreSQL, and more, making applications more flexible.

- Support for Migrations: Migrations make it easier to update the database schema, especially during iterative development cycles.

-> Limitations of Entity Framework

- Performance Overhead: EF can be slower than raw SQL in certain scenarios due to the ORM layer.

- Limited Control: With complex queries, EF may not generate the most optimized SQL, which can lead to performance bottlenecks.

- Learning Curve: For developers unfamiliar with ORMs, there is a learning curve to understand the DbContext, migrations, and entity configurations.

-> Summary

Entity Framework simplifies database operations in ASP.NET MVC applications by providing an ORM framework that handles data as .NET objects. With support for LINQ and CRUD operations, it integrates well with the MVC pattern, making it easier to manage and interact with data in .NET applications. EF's migration system enables schema updates over time, making it suitable for iterative and agile development practices.

*/

/*

Code-First Approach


The Code-First approach in Entity Framework allows developers to define the database schema in code using C# or VB.NET classes, without needing a pre-existing database. With Code-First, you create model classes that represent tables in the database, and Entity Framework generates the corresponding tables and columns based on these classes. This approach is particularly useful in agile development, where database design evolves with the application.

Here’s a breakdown of how the Code-First approach works, its key components, and step-by-step examples.


The Code-First approach in Entity Framework allows developers to define the database schema in code using C# or VB.NET classes, without needing a pre-existing database. With Code-First, you create model classes that represent tables in the database, and Entity Framework generates the corresponding tables and columns based on these classes. This approach is particularly useful in agile development, where database design evolves with the application.

Here’s a breakdown of how the Code-First approach works, its key components, and step-by-step examples.

-> Key Concepts of the Code-First Approach

- Model Classes (Entities): These classes define the structure of your database tables. Each class represents a table, and each property in the class represents a column.

- DbContext: This is the main class that connects to the database and manages the entities. It contains DbSet properties for each model class, allowing access to the database tables. It is provided by the System.Data.Entity namespace of the ASP.NET MVC Framework. Can be used to define the database context class after creating a model class. Coordinates with Entity Framework and allows you to the query and save the data in the database. Uses the DbSet<T> type to define one or more properties where, T represents the type of an object that needs to be stored in the database.

- Data Annotations & Fluent API: Code-First allows you to configure entity properties using data annotations or the Fluent API. Data annotations are attributes you place on properties, while the Fluent API provides a more granular way to configure model classes in the DbContext.

- Migrations: Migrations track changes in the model classes and apply them to the database. This makes it easy to update the database schema over time as your models change.

-> Setting Up Code-First in Entity Framework

Step 1: Define Model Classes
Model classes represent database tables. For example, let’s say you’re creating an application with an Employee table.

Define the Employee class in your project:

------------------------------------------------------------------------------------------------------
public class Employee
{
    public int EmployeeID { get; set; }  // Primary Key
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
}
------------------------------------------------------------------------------------------------------

Each property represents a column in the Employee table. Entity Framework automatically recognizes EmployeeID as the primary key by convention because it follows the pattern ClassNameID.

Step 2: Create a DbContext Class

Create a class that inherits from DbContext to manage the connection to the database. Add a DbSet property for each entity class.

------------------------------------------------------------------------------------------------------
public class MyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
}
------------------------------------------------------------------------------------------------------

DbSet<Employee> Employees represents the Employees table in the database. MyDbContext provides methods to query and save data.

Step 3: Configure the Database Connection

Add a connection string in the appsettings.json file (for .NET Core) or Web.config (for .NET Framework):

------------------------------------------------------------------------------------------------------
 <connectionStrings>
    <add name="StudentContext" connectionString="Data Source=ALT-LT-789\SQLSERVER2022DEV; Initial Catalog=CodeFirstEFDB; User ID=sa; Password=Edy@1234;" providerName="System.Data.SqlClient" />
  </connectionStrings>
------------------------------------------------------------------------------------------------------

The MyDbContext class will use this connection string to connect to the database. Here Databse name will be named as Initial Catalog and additionally we have to give User ID and Password in order to connect with SSMS.

-> Customizing Model Properties with Data Annotations

Entity Framework Code-First uses conventions to configure models, but you can use data annotations to customize the behavior. For example, you might want to make the Name field required and limit its length.

------------------------------------------------------------------------------------------------------
public class Employee
{
    public int EmployeeID { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public string Position { get; set; }

    [Range(30000, 200000)]
    public decimal Salary { get; set; }
}
------------------------------------------------------------------------------------------------------

In this example:

- [Required] specifies that Name cannot be null.

- [StringLength(100)] limits Name to 100 characters.

- [Range(30000, 200000)] sets a range for Salary.

-> Fluent API Configuration

If you need more control than data annotations allow, you can use the Fluent API in the OnModelCreating method of the DbContext. This method provides additional configuration options.

------------------------------------------------------------------------------------------------------
public class MyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Salary)
            .HasColumnType("decimal(18,2)");
    }
}
------------------------------------------------------------------------------------------------------

In this example:

- IsRequired enforces that the Name column cannot be null.

- HasMaxLength limits Name to 100 characters.

- HasColumnType("decimal(18,2)") specifies the Salary column precision.

-> Adding and Applying Migrations

As you build your application, your model classes may change, requiring updates to the database schema. Migrations track changes and apply them to the database.

1. Enable Migrations (if not enabled):

------------------------------------------------------------------------------------------------------
Enable-Migrations
------------------------------------------------------------------------------------------------------

2. Add a Migration:

------------------------------------------------------------------------------------------------------
Add-Migration InitialCreate
------------------------------------------------------------------------------------------------------

This generates a migration file in your project under the Migrations folder. This file contains code to create the tables and columns based on your model classes.

3. Update the Database:

------------------------------------------------------------------------------------------------------
Update-Database
------------------------------------------------------------------------------------------------------

This command applies the migration to the database, creating the tables and columns.

Each time you change a model class (e.g., adding a property), create a new migration and update the database:

------------------------------------------------------------------------------------------------------
Add-Migration AddEmployeeDepartment
Update-Database
------------------------------------------------------------------------------------------------------

-> Performing CRUD Operations with Code-First

Once your models and DbContext are set up, you can perform CRUD operations using Entity Framework.

Example CRUD Operations in a Controller

------------------------------------------------------------------------------------------------------
public class EmployeeController : Controller
{
    private MyDbContext db = new MyDbContext();

    // Read
    public ActionResult Index()
    {
        var employees = db.Employees.ToList();
        return View(employees);
    }

    // Create
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    // Update
    [HttpGet]
    public ActionResult Edit(int id)
    {
        var employee = db.Employees.Find(id);
        return View(employee);
    }

    [HttpPost]
    public ActionResult Edit(Employee employee)
    {
        if (ModelState.IsValid)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    // Delete
    [HttpGet]
    public ActionResult Delete(int id)
    {
        var employee = db.Employees.Find(id);
        return View(employee);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var employee = db.Employees.Find(id);
        db.Employees.Remove(employee);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
}
------------------------------------------------------------------------------------------------------

-> Advantages of Code-First Approach

- Control Over Database Design: Code-First allows you to define the database schema in code, giving you complete control over its structure.

- Easy Schema Changes: Migrations make it easy to modify the database schema as your models evolve.

- Reduced Complexity: You don’t need to manually write SQL code or maintain SQL scripts; Entity Framework generates the necessary SQL based on your model classes.

- Ideal for Agile Development: Code-First is particularly suited for agile development, where requirements may evolve over time.

-> Limitations of Code-First Approach

- Performance Overhead: Code-First has more overhead than writing optimized SQL queries directly, especially for complex queries.

- Not Suitable for Large Legacy Databases: For existing, complex databases, Database-First or a direct SQL approach might be more practical.

- Dependency on Conventions: Code-First relies heavily on conventions, which may not always meet specific requirements without additional configuration.

-> Summary

The Code-First approach in Entity Framework enables you to define database schema directly in your code through model classes. With the DbContext, data annotations, Fluent API, and migrations, Code-First streamlines database management and simplifies the process of creating, updating, and maintaining database schemas, especially in agile environments where requirements are continuously evolving. This approach is popular for new applications and small to medium-sized projects, offering flexibility and control over the database while minimizing direct database management efforts.


*/