/*
Cookies 

In ASP.NET MVC, cookies are used to store small pieces of information on the client side. They are part of the HTTP protocol and are often used for managing user sessions, storing user preferences, and tracking user activities. Here's an overview of cookies in the context of ASP.NET MVC:

Cookies are one of the State Management techniques, so that we can store information for later use. Cookies are small files that are created in the web browser's memeory (if they're temporary) or on the client's hard drive (if they're permanent).

Cookies are used to store small pieces of information related to a user's computer, such as it's IP address, browser type, operating system, and Web pages last visited.

The purpose of sorting this information is to offer a personalized experience to the user.

Cookies are sent to a client computer along with the page output.

These cookies are used to store small pieces of information related to a user's computer, such as it's IP address, browser type operating system, and Web pages last visited.

The purpose of storing this information is to offer a personalized experience to the user.

Cookies are sent to a client computer along with the page layout.

These cookies are stored on the client's computer.

When a browser requests the same page the next time, it sends th ecookies along with the request information.

The Web server reads the cookie and extracts its value.

It then process the Web page according to the information contained in the cookie and renders it on the Web browser.

1. What are Cookies?

Definition: A cookie is a small text file stored in the user's browser.

Scope: Cookies are tied to a specific domain and can only be accessed by the domain that set them.

Lifetime: Cookies can be persistent (stored on the user's device for a specified period) or session (deleted when the browser is closed).

2. Working with Cookies in ASP.NET MVC

Setting a Cookie

You can create and add cookies to the response object using the HttpCookie class.

--------------------------------------------------------------------------------------------------
public ActionResult SetCookie()
{
    HttpCookie cookie = new HttpCookie("UserName");
    cookie.Value = "JohnDoe";
    cookie.Expires = DateTime.Now.AddDays(7); // Persistent cookie, valid for 7 days
    Response.Cookies.Add(cookie);
    return Content("Cookie is set.");
}
--------------------------------------------------------------------------------------------------

Reading a Cookie

You can retrieve a cookie from the Request.Cookies collection.

--------------------------------------------------------------------------------------------------
public ActionResult GetCookie()
{
    HttpCookie cookie = Request.Cookies["UserName"];
    if (cookie != null)
    {
        string userName = cookie.Value;
        return Content($"Cookie Value: {userName}");
    }
    return Content("Cookie not found.");
}
--------------------------------------------------------------------------------------------------

Deleting a Cookie

You can delete a cookie by setting its expiration date to a past date.

--------------------------------------------------------------------------------------------------
public ActionResult DeleteCookie()
{
    if (Request.Cookies["UserName"] != null)
    {
        HttpCookie cookie = new HttpCookie("UserName");
        cookie.Expires = DateTime.Now.AddDays(-1); // Set expiration to the past
        Response.Cookies.Add(cookie);
        return Content("Cookie deleted.");
    }
    return Content("No cookie found to delete.");
}
--------------------------------------------------------------------------------------------------

3. Types of Cookies

Session Cookies: These are temporary and exist only while the user's browser is open. They are deleted when the browser is closed.

Persistent Cookies: These remain on the user's device for a specified duration, even after the browser is closed.

4. Key Considerations

Size Limit: Cookies have a size limit of 4KB.

Security:

- Use Secure Cookies for HTTPS connections by setting cookie.Secure = true;.

- Use HttpOnly Cookies to prevent JavaScript from accessing them by setting cookie.HttpOnly = true;.

Performance: Avoid overusing cookies since they are sent with every HTTP request and can increase latency.

Privacy: Be mindful of storing sensitive data in cookies. Prefer encrypting data before storing it.

5. Practical Use Cases

Authentication: Storing session tokens or user identifiers.
User Preferences: Remembering themes, language settings, etc.

Tracking: Analytics and tracking user activities across requests.

By carefully managing cookies, you can enhance user experience while maintaining security and privacy in your ASP.NET MVC application.

6. Types of Cookies

Cookies are categorized based on their behavior, lifespan, and intended usage. Here are the different types of cookies commonly used:

1. Based on Lifespan

a. Session Cookies

Definition: Temporary cookies that are stored in the browser's memory and deleted when the browser is closed (temporary cookies which are saved for current session only).

Use Case: Ideal for session management, such as tracking a user's login state during a single session.

Example:

-----------------------------------------------------------------------------------------
HttpCookie cookie = new HttpCookie("SessionID", "12345");
Response.Cookies.Add(cookie); // No expiration date set
-----------------------------------------------------------------------------------------

b. Persistent Cookies

Definition: Cookies that are stored on the user's device for a specified duration, even after the browser is closed. (This type of cookie is useful when you need to store information for a longtime)

Use Case: Useful for remembering user preferences, login information, or other settings across multiple sessions.

Example:

-----------------------------------------------------------------------------------------
HttpCookie cookie = new HttpCookie("UserPreferences", "DarkTheme");
cookie.Expires = DateTime.Now.AddDays(30); // Cookie valid for 30 days
Response.Cookies.Add(cookie);
-----------------------------------------------------------------------------------------

2. Based on Security

a. Secure Cookies

Definition: Cookies that are transmitted only over HTTPS connections.

Use Case: Protects sensitive information like authentication tokens from being transmitted over insecure channels.

Example:

-----------------------------------------------------------------------------------------
HttpCookie cookie = new HttpCookie("AuthToken", "abcd1234");
cookie.Secure = true; // Only transmitted over HTTPS
Response.Cookies.Add(cookie);
-----------------------------------------------------------------------------------------

b. HttpOnly Cookies

Definition: Cookies that are inaccessible via client-side scripts (JavaScript).

Use Case: Prevents cross-site scripting (XSS) attacks by ensuring cookies cannot be manipulated by malicious scripts

Example:

-----------------------------------------------------------------------------------------
HttpCookie cookie = new HttpCookie("AuthToken", "abcd1234");
cookie.HttpOnly = true; // Prevent JavaScript access
Response.Cookies.Add(cookie);
-----------------------------------------------------------------------------------------

c. SameSite Cookies

Definition: Cookies that restrict cross-site requests to mitigate CSRF (Cross-Site Request Forgery) attacks.

Use Case: Limits when cookies can be sent with requests initiated from third-party sites.

Example:

-----------------------------------------------------------------------------------------
HttpCookie cookie = new HttpCookie("AuthToken", "abcd1234");
cookie.SameSite = SameSiteMode.Strict; // Cookies only sent for first-party requests
Response.Cookies.Add(cookie);
-----------------------------------------------------------------------------------------

3. Based on Usage

a. First-Party Cookies

Definition: Cookies created by the website the user is currently visiting.

Use Case: Tracks user preferences or sessions for the site's domain.

b. Third-Party Cookies

Definition: Cookies created by a domain different from the one the user is visiting (e.g., ad networks).

Use Case: Used for advertising, tracking, and analytics across websites.

4. Special Types

a. Flash Cookies

Definition: Cookies stored by Adobe Flash. They are less common due to the decline of Flash.

Use Case: Historically used for storing larger amounts of data than HTTP cookies.

b. Zombie Cookies

Definition: Persistent cookies that are recreated after being deleted. Often used maliciously.

Use Case: Tracking users across sessions or domains.

c. Supercookies

Definition: Cookies stored at a higher level (e.g., by ISPs), often used for tracking across multiple domains.

Use Case: Rarely used but have significant privacy concerns.

*/


/*

Cookies vs Session

1. Storage Location 

- Cookies: Data is stored on the client's browser.

- Sessions: Data is stored on the server, with a unique ID sent to the client.

2. Data Accessibility

- Cookies: Can be viewed and modified by the user (if not encrypted).

- Sessions: Not accessible to the user; only the session ID is shared with the client.

3. Lifespan

- Cookies:  Can be persistent (remain for a set duration) or session-based (deleted when the browser is closed).

- Sessions: Last for the duration of the user’s session or until a timeout (e.g., after 20 minutes of inactivity).

4. Data Size

- Cookies: Limited to approximately 4KB.

- Sessions: Can store much larger amounts of data, limited by server memory or configuration.

5. Security

- Cookies: Less secure as they are stored client-side and can be intercepted or manipulated.

- Sessions: More secure as the data resides on the server.

6. Automatic Transmission

- Cookies: Sent automatically with every HTTP request to the server.

- Sessions: Require server-side logic to retrieve data during a user session.

7. Dependency

- Cookies: Require browser support to function correctly.

- Sessions: Require server resources and proper configuration.

8. Use Cases

- Cookies:

Storing non-sensitive data like user preferences (e.g., themes or language settings).

Persistent login functionality ("Remember Me").

- Sessions:

Storing sensitive data like authentication credentials.

Temporary data storage during user interactions (e.g., shopping cart).

9. Security Features

- Cookies:

Can be secured using flags like HttpOnly, Secure, and SameSite.

- Sessions:

Managed on the server, so the security depends on the server's implementation and encryption methods.

10. Lifetime Control

- Cookies: The developer controls the expiration explicitly through the Expires property.

- Sessions: Expire automatically based on server settings (e.g., session timeout).


*/