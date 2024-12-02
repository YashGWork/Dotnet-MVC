/*

Images CRUD App

1) First, install the EntityFramework NuGet Package to enable the use of the Entity Data Model (Data First Approach).

2) Ensure that the table you want to refer to (e.g., the employee table) is present in the database in SQL Server Management Studio (SSMS).

3) Add data models using the Entity Data Model (Data First Approach) by creating a model class that corresponds to the employee table. This class should include properties that match the columns in the table, including an image file property for handling image uploads.

4) Add a HomeController to your project. Inside the HomeController, create a context class instance (e.g., `db`) to interact with the database. This instance will be used to perform CRUD operations on the employee records.

5) Implement the following action methods in the HomeController to handle CRUD operations:

   - **Index**: 
     - Retrieve all employee records from the database and convert them into a list.
     - Pass the list of employees to the view for rendering.

   - **Create**: 
     - Render a view for creating a new employee record.
     - Handle the form submission:
       - Validate the model state.
       - If a new image is uploaded, validate the file type and size, then save the image to the server and update the employee record.
       - If the operation is successful, redirect to the Index action and display a success message; otherwise, show an error message.

   - **Edit**: 
     - Retrieve an existing employee record for editing based on the provided ID.
     - Store the current image path in the session for potential use during the edit process.
     - Render the edit view with the employee data.
     - Handle the form submission:
       - If a new image is uploaded, validate the file type and size, save the new image, and delete the old image from the server.
       - If no new image is uploaded, retain the original image path.
       - Update the employee record in the database and redirect to the Index action with a success message.

   - **Delete**: 
     - Check if the provided ID is valid and retrieve the corresponding employee record.
     - If the record exists, mark it for deletion and save changes to the database.
     - Delete the associated image file from the server if it exists.
     - Redirect to the Index action with a success or error message based on the outcome.

   - **Details**: 
     - Retrieve the employee record based on the provided ID.
     - Store the image path in the session for potential use in the view.
     - If the record exists, render the details view; otherwise, redirect to the Index action with an error message.

6) Create the corresponding views for each action method using View Templates. Ensure that the views include forms for creating and editing employee records, as well as displaying employee details and lists.

7) Test the application to ensure that all CRUD operations work as expected, including image uploads and deletions.

8) Optionally, implement client-side validation and improve user experience by adding notifications for successful or failed operations.

*/
