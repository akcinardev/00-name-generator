# Name Generator

## Overview

The **Name Generator** is a console application built using C# that fetches and displays random names from an API. The application uses a JSON configuration file to retrieve the API URL and makes asynchronous requests to get random names.

## Features

- **Random Name Generation**: Fetches random names from an external API.
- **Configuration**: Loads API URL from a configuration file (`appsettings.json`).
- **Asynchronous API Calls**: Retrieves data asynchronously to avoid blocking the main thread.
- **Error Handling**: Handles errors gracefully and provides informative messages.

## Technologies Used

- **C#**: Programming language used for the application logic.
- **Microsoft.Extensions.Configuration**: Library used for configuration management.
- **System.Text.Json**: Library used for JSON serialization and deserialization.
- **HttpClient**: Class used for making HTTP requests.

### Versions

- **.NET Core SDK**: Version 6.0 or later
- **Visual Studio**: 2019 or later

## Usage

1. **Configure the API URL**:
   - Create an `appsettings.json` file in the root directory of the project with the following content:
     ```json
     {
       "ApiURL": "https://api.example.com/names"
     }
     ```

2. **Run the Application**:
   - Execute the `NameGenerator` project to fetch and display random names.
   - The application will output random names to the console.

## Code Structure

- **`Program.cs`**: Entry point of the application. Initializes configuration, fetches names from the API, and displays them.
- **`ConfigController.cs`**: Handles loading configuration from `appsettings.json`.
- **`ApiController.cs`**: Manages API interactions, including sending requests and processing responses.
- **`Name.cs`**: Defines the `Name` model used to represent individual names.
- **`Result.cs`**: Defines the `Result` model used to represent the response from the API.
- **`ApiResponse.cs`**: Defines the `ApiResponse` model used to represent the overall API response.

## Feedback & Contributing

Your feedback and contributions are welcome! If you have suggestions for improvements or find any issues, please open an issue or submit a pull request.

## Contact

For any questions or inquiries, please contact:

- **Email**: akcinardev@gmail.com
- **GitHub**: [akcinardev](https://github.com/akcinardev)
