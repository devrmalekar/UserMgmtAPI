# UserMgmtAPI
A simple demonstration of User Management API  built using  **C# WebAPI project** with guidance from **Ms-Copilot**.
This project showcases real-world concepts such as **Restful API design**,  **simple custom authentication middleware**, **serilog request/response logging** and a **global erroR-handling mechanism**. 

Additionally, Swagger is included to help visualize and test the API during development.

## 🤖 How Copilot Assisted in This Project

Microsoft Copilot acted as a coding assistant throughout the development. It did not generate project automatically, instead provided guidance, explanations and suggestions that improved productivity and understanding. 

### Ways Copilot Assisted:
- **Explaining the real-world RESTFUL API usage**  
  Helped calrify how REST principles apply in practical scenarios.

- **Debugging issues**  
 Assisted in identifying form validation issues, understanding global error handling, and improving logging for auditing.

- **Improving code structure**  
  Copilot suggested best practices for organizing controllers, models, mock data, and middleware.

- **Generating boilerplate code**  
  It assisted with writing repetitive or standard code such as middleware classes.

- **Providing documentation-style explanations**  
  Copilot helped create clear descriptions for README.md, comments, and architectural explanations.

### What Copilot Did *Not* Do:
- It did not design the project.
- It did not make architectural decisions.
- It did not write the entire codebase.
- It did not replace human understanding or problem‑solving.

Copilot acted as a supportive tool—similar to an intelligent pair‑programming partner—while all final decisions, implementations, and debugging were performed manually.

---

## 🚀 Features

### 🗂 User Management
= View all list of users - currently from mock data
- Add new valid user 
- Get User detail by ID.
- Update or Delete existing user 
- Response meaningful error message.

### 🧩  Component-Driven Architecture
- Controllers folder for all controller
- Model Folder for models
- Middleware Folder for middleware classes
- MockData folder in-memory data storage

---


## 🏗 Project Structure
```
UserMgmtAPI/
│   .gitignore
│   appsettings.Development.json
│   appsettings.json
│   Program.cs
│   README.md
│   UserManagementAPI.csproj
│   UserManagementAPI.http
├───controllers
│       ErrorController.cs
│       UserMgmtController.cs
│       
├───Logs
│       log-20260527.txt
│       
├───middleware
│       RequestResponseLoggingMiddleware.cs
│       TokenAuthenticationMiddleware.cs
│       
├───MockData
│       UserRepository.cs
│       
├───models
│       User.cs
└───Properties
        launchSettings.json

```

---


## ▶️ Running the Project

### 1. Install .NET 9 SDK  
https://dotnet.microsoft.com/download

### 2. Clone the repository

```bash
git clone https://github.com/devrmalekar/UserMgmtAPI
cd UserMgmtAPI
```

### 3. Run the application
```bash
dotnet run
```

### 4. Open in browser
- Can change the application port from **Properties/launchSettings.json**
--- 
https://localhost:5156/api/usermgmt

https://localhost:5156/swagger
---

## 📌 Future Enhancements 
- User Authentication + Role Based Access Control
- JWT Authentication
- Persistent Storage (SQLite or EF Core)

---