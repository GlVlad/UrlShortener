URL Shortener

URL Shortener is a web application designed to shorten long URLs into short, easy-to-use links. The application also allows users to view, add, and delete shortened URLs and display information about them.

Installation
* Step 1: Clone the repository

`git clone https://github.com/GlVlad/UrlShortener.git`

`cd UrlShortener`

* Step 2: Install dependencies
Use the NuGet package manager to install all necessary dependencies:

`dotnet restore`

* Step 3: Apply database migrations
Apply the database migrations to create the required tables:

`dotnet ef database update`

* Step 4: Run the application
Start the application:

`dotnet run`

Usage:

Login
Navigate to https://localhost:****. Use the following credentials to log in as an administrator:

Email: admin@example.com

Password: Admin123!

Key Features:

* Login View: Log in with your email and password.

* Short URLs Table View: View a table of all shortened URLs, add new URLs (authorized users only), and delete records (for administrators and the original authors).

* Short URL Info View: View detailed information about a shortened URL (CreatedBy, CreatedDate, etc.).

* About View: Description of the URL shortening algorithm (editable by administrators only).
