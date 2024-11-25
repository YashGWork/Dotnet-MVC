/*  Session 

In ASP.NET MVC, a session is a server-side state management technique that helps store and retrieve user-specific data across multiple requests. When a user connects to an application, the server creates a unique Session ID for that user, which is stored on the user's browser as a cookie (if cookies are enabled). This session ID is then used by the server to identify the session data associated with that user across different pages and requests.

- Session is a property of Controller class whose type is HttpSessionStateBase

- Session is also used to pass data within the ASP.NET MVC application and unlike TempData, Session persist for it's expiration time (default time is 20 minutes but it can be increased)

- Session is valid for all requests, not for a single redirect.

- It also requires typecasting for getting data and check for null value to avoid error.

- Session has a performance drawback because it slow down the application that's why it is not recommended to always uses Session, Session can be used according to the situation.


Here’s a breakdown of how session management works in ASP.NET MVC:

1. Session Creation

- When a user visits a page for the first time, the server creates a new session for the user and assigns a unique session ID.

- This ID is stored in a cookie in the user's browser (or in the URL if cookies are disabled), allowing the server to retrieve the session data in subsequent requests.

2. Storing Data in Session

- The Session object, which is accessible in controllers and views, is used to store and retrieve user-specific data.

- You can store different types of data in a session, such as strings, numbers, objects, etc.

-----------------------------------------------------------------------------------------------
// Storing data in session
Session["UserName"] = "JohnDoe";
-----------------------------------------------------------------------------------------------

3. Retrieving Data from Session

- Data stored in the session can be accessed from any controller or view within the application, as long as the session is active.

-----------------------------------------------------------------------------------------------
// Retrieving data from session
string userName = Session["UserName"] as string;
-----------------------------------------------------------------------------------------------

4. Session Lifetime and Timeout

By default, sessions in ASP.NET MVC expire after 20 minutes of inactivity, but this timeout can be configured in the Web.config file.

-----------------------------------------------------------------------------------------------
<system.web>
    <sessionState timeout="30" />
</system.web>
-----------------------------------------------------------------------------------------------

- The session can also be manually abandoned or cleared using:

-----------------------------------------------------------------------------------------------
Session.Abandon(); // Ends the session
Session.Clear();   // Clears all session data but keeps the session active
-----------------------------------------------------------------------------------------------

5. Session Storage Options

ASP.NET MVC supports multiple session storage options, which can be configured based on your application needs:

- InProc (default): Stores session data in memory on the web server. It's fast but has limitations when scaling across multiple servers.

- StateServer: Stores session data in a separate process. This allows session data to persist across multiple servers.

- SQLServer: Stores session data in a SQL Server database, making it scalable and suitable for load-balanced applications.

- Custom: Allows you to implement custom session providers if you need a specific session storage mechanism.

-----------------------------------------------------------------------------------------------
<system.web>
    <sessionState mode="SQLServer" sqlConnectionString="Data Source=server_name;Integrated Security=True;" />
</system.web>
-----------------------------------------------------------------------------------------------

6. Best Practices

- Minimize Session Usage: Use session sparingly for data that must persist between requests. Avoid storing large objects or sensitive data.

- Session Expiration Management: Always handle the scenario where a session might expire, and redirect users appropriately.

- Avoid Overusing: Excessive use of sessions can lead to scalability issues, particularly in high-traffic applications. Consider alternatives like caching or storing user data in cookies or a database.

-> Summary

ASP.NET MVC sessions provide a straightforward way to maintain user state across multiple requests, but they should be used carefully to avoid performance issues. Properly managing session expiration and data storage location is crucial for building a scalable and user-friendly application.

 */