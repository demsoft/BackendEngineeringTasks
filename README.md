# Task Management System

Welcome to the Task Management System, a backend API for managing tasks, projects, users, and notifications.

## Table of Contents

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [API Endpoints](#api-endpoints)
  - [Running the Application](#running-the-application)
- [Configuration](#configuration)


## Getting Started

### Prerequisites

Before you begin, ensure you have met the following requirements:

- **.NET Core SDK**: Make sure you have the .NET Core SDK installed. You can download it from the official [.NET Core website](https://dotnet.microsoft.com/download).

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/demsoft/task-management-system.git

2. Navigate to the project directory:

   ```bash
   cd BackendEngineeringTasks

3. Navigate to the project directory:

   ```bash
   dotnet build
   
### Usage
### API Endpoints
The Task Management System provides the following API endpoints:

- CRUD operations for Tasks, Projects, Users, and Notifications.
- Fetch tasks based on their status or priority.
- Fetch tasks due for the current week.
- Assign a task to a project or remove it from a project.
- Mark a notification as read or unread.
- Background service to check for due tasks and send notifications.

### Running the Application

To run the application locally, use the following command:

   ```bash
   dotnet run 
  ````
The API will be accessible at http://localhost:5104/swagger/index.html by default. 
You can modify the port and other settings in the appsettings.json file.

### Configuration

- You can configure the application settings in the appsettings.json file located in the BackendEngineeringTasks project. 
- This file contains settings for database connections, logging, and other application-specific configurations.
- As for the database connections, it's already connected to my live database with a public IP. You can change it to yours and run migration on your database(MSSQL).
