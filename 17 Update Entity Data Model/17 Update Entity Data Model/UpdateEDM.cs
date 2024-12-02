/*

Update Entity Data Model

1) Make sure both the tables(original and new one) is present in the database so that we can refer it to when creating model from them using Entity Data Model.

2) First referring to student table in UpdateEDMdb database and creating a model form the student table using Entity Data Model.

3) Now to create model class for employee table instead of deleting the already existing data model created using entity data model, we will use it to add employee model.

4) Go to edmx diagram file and right click on the window to "Update Model from Database" and select the employee table, as a result employee table model will be added in the edmx diagram.

5) Then we need to right click in order to validate our model in the edmx file, once validation is completed save the edmx file so that the employee model is added in the entity data model and the db context of employee table is also added in the db context class.

6) Then rebuild the solution in order to use the entity data model.

7) Now you can create action method views corresponding to the individual data models (employees and tables)


*/