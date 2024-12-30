This project is a full-stack application that consists of an API built with ASP.NET Core and a frontend built with Vue.js. The API allows managing Villa objects and their associated Comments. The Villa and Comment models are linked via a foreign key (VillaID). The project utilizes Entity Framework Core for database management and CORS to allow frontend communication with the API.

Project Overview
API: The backend API is built using ASP.NET Core and provides endpoints to manage Villas and Comments.
VillaAPI Controller: Handles the operations related to the Villa.
CommentAPI Controller: Manages comments related to specific villas.
Frontend: The frontend is developed using Vue.js and interacts with the API to display and manage data.
Database: The project uses Entity Framework Core for data storage, where each Villa can have multiple Comments linked through a VillaID foreign key.
Features
Villa Management: Create, read, update, and delete villas.
Comment Management: Add, retrieve, and delete comments for specific villas.
CORS Support: The project is configured to allow cross-origin requests, enabling communication between the frontend and backend.
Technologies Used
Backend:
ASP.NET Core Web API
Entity Framework Core
CORS (Cross-Origin Resource Sharing)
Frontend:
Vue.js
Vue CLI
Bootstrap (Optional: for styling)
Database:
SQL Server
Entity Framework Migrations for table creation and management
Setup and Installation
1. Clone the Repository
bash
Copy code
git clone https://github.com/snehamukherjee-02/VillaAPI.git
cd VillaAPI
2. Set up the Backend (API)
Navigate to the backend folder (where the API project is located).

Install dependencies using NuGet or dotnet CLI:

bash
Copy code
dotnet restore
Update your appsettings.json with your SQL Server connection string.

Run Entity Framework migrations to create the database:

bash
Copy code
dotnet ef migrations add InitialCreate
dotnet ef database update
Start the backend API:

bash
Copy code
dotnet run
3. Set up the Frontend (Vue.js)
Navigate to the frontend directory (vue-crud).

Install frontend dependencies using npm or yarn:

bash
Copy code
npm install
Configure the Vue.js app to point to the correct API URL.

Start the Vue.js development server:

bash
Copy code
npm run serve
Your frontend should now be running on http://localhost:8080, and the backend API will be accessible at http://localhost:5000.

Database Structure
The database consists of two main tables:

Villa: Stores information about each villa.
Comment: Stores comments related to each villa. Each comment is associated with a VillaID (foreign key).
API Endpoints
1. VillaAPI (For managing Villas)
GET /api/villa – Get a list of all villas.
GET /api/villa/{id} – Get a specific villa by ID.
POST /api/villa – Create a new villa.
PUT /api/villa/{id} – Update an existing villa.
DELETE /api/villa/{id} – Delete a villa.
2. CommentAPI (For managing Comments)
GET /api/comment – Get all comments.
GET /api/comment/{id} – Get a specific comment by ID.
POST /api/comment – Create a new comment.
DELETE /api/comment/{id} – Delete a comment.
CORS Configuration
The API has been configured with CORS to allow cross-origin requests, enabling seamless communication between the frontend (Vue.js) and backend (ASP.NET Core).
License
This project is licensed under the MIT License.
