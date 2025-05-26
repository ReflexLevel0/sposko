# How to run the app
1. database creation
Start database in the root directory of the project: "docker-compose up".
After using this command the database should be populated with test data and accessible on localhost:5432.

2. start the API
Install .NET SDK 9 if it's not installed, and then run "dotnet run" in (./api/api directory).
The API should now be accessible on localhost:5023/api, and the documentation for it should be accessible on localhost:5023:/swagger.
Additionally, there is an ./Insomnia.json file which can be imported into Insomnia to try out API calls (just take into account you might need to change some data about requests to make it all work).

3. start the website
Navigate into ./frontend and then run "npm start".
After the application start, the website should be accesible on localhost:3000 from which you can go to "Kreiraj grupu" (localhost:3000/create-group) or "Moji treninzi" (localhost:3000/my-trainings) to try out website functionality.
