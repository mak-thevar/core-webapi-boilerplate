![Build Status](https://github.com/mak-thevar/core-webapi-boilerplate/actions/workflows/dotnet.yml/badge.svg)
[![Contributors][contributors-shield]][contributors-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

# CoreWebApi-BoilerPlate
Built on [.NET Core 6.x](https://dotnet.microsoft.com/en-us/download/dotnet/6.0), this template follows a [n-layered architecture](https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/n-tier) and includes pre-configured libraries like Swagger, JWT, and AutoMapper, among others. It's a reliable starting point for developers seeking to create strong and adaptable applications, offering a simplified development experience.

### 👩‍💻Are you in need of a frontend solution? 
**You can access the frontend for this repository at the following link: [core-webapi-boilerplate-frontend](https://github.com/mak-thevar/core-webapi-boilerplate-frontend)**

## 📋 Table of Contents 
* [Getting Started](#-getting-started)
* [File Structure](#%EF%B8%8F-file-structure)
* [Installation](#installation)
* [Features](#-features)
* [Contributing](#-contributing)
* [Screenshots](#-screenshots)
* [License](#-license)
* [Contact](#-contact)


## 🏁 Getting Started
### Prerequisites
- [Visual Studio](https://visualstudio.microsoft.com/) OR [Visual Studio Code](https://code.visualstudio.com/)


## 🗃️ File Structure

```
│   appsettings.Development.json
│   appsettings.json
│   CoreWebApiBoilerPlate.csproj
│   CoreWebApiBoilerPlate.csproj.user
│   Program.cs
│
├───BusinessLogicLayer
│   │   PredicateBuilder.cs
│   │
│   ├───DTO
│   │       ApiResponseModel.cs
│   │       CommentRequestModel.cs
│   │       CommentResponseModel.cs
│   │       Constants.cs
│   │       RoleRequestModel.cs
│   │       RoleResponseModel.cs
│   │       TodoRequestModel.cs
│   │       TodoResponseModel.cs
│   │       UserRequestModel.cs
│   │       UserResponseModel.cs
│   │
│   └───Exceptions
│           AppException.cs
│
├───Controllers
│       ApiBaseController.cs
│       AuthController.cs
│       TodosController.cs
│       UsersController.cs
│
├───DataAccessLayer
│   ├───Context
│   │       DefaultDBContext.cs
│   │       SeedingData.cs
│   │
│   ├───Entities
│   │   │   Comment.cs
│   │   │   Role.cs
│   │   │   Todo.cs
│   │   │   TodoStatus.cs
│   │   │   User.cs
│   │   │
│   │   └───Base
│   │           EntityBase.cs
│   │           IAuditedEntity.cs
│   │           IStatusEntity.cs
│   │
│   └───Repository
│       ├───Impl
│       │       RepositoryBase.cs
│       │       RepositoryWrapper.cs
│       │       TodoRepository.cs
│       │       UserRepository.cs
│       │
│       └───Interfaces
│               IRepository.cs
│               IRepositoryWrapper.cs
│               ITodoRepository.cs
│               IUserRepository.cs
│
├───Infrastructure
│   │   AutoMapperProfile.cs
│   │   Configuration.cs
│   │   IdentityClientConfiguration.cs
│   │   JWT.cs
│   │   RegisterDBDependency.cs
│   │   SwaggerGen.cs
│   │
│   └───Middlewares
│           ExceptionHandlerMiddleWare.cs
│
├───Migrations
│       20220716122039_Initial.cs
│       20220716122039_Initial.Designer.cs
│       20220804091809_AddingRole.cs
│       20220804091809_AddingRole.Designer.cs
│       DefaultDBContextModelSnapshot.cs
```


## Installation

- Clone the repository
```sh
git clone https://github.com/mak-thevar/core-webapi-boilerplate.git
```
- Open the solution file 'CoreWebApiBoilerPlate.sln' directly in Visual Studio
- The database is cofigured to use sqllite you can change it to appropriate sql like MSSQL or mysql, the settings can be found on the RegisterSqliteDatabaseContext method in *DataLayer\RegisterDBDependency.cs* file
  ```cs
      services.AddDbContext<DefaultDBContext>(options =>
      {
          options.UseSqlite(connectionString, options =>
          {
              options.MigrationsAssembly(typeof(DefaultDBContext).Assembly.FullName);
          });
          options.EnableDetailedErrors();
          options.EnableSensitiveDataLogging();
      });
  ```
- The initial seeding data can be found on *DataLayer\Context\SeedingData.cs file
  
     <details>
  <summary>
   <h4>Show Code</h4>
  </summary>
  <pre>
  public static List<TodoStatus> GetTodoStatus()
      {
          return new List<TodoStatus>
          {
              new TodoStatus{ Id =1 , Description = "Todo", IsDefault = true},
              new TodoStatus{ Id =2 , Description = "In Progress", IsDefault = true},
              new TodoStatus {Id =3, Description = "Completed" , IsDefault  = true},
          };
      }

      public static List<Role> GetRoles()
      {
          return new List<Role>
           {
               new Role{ Id =1 , Description = "Admin", IsActive = true, CreatedOn = DateTime.UtcNow}
           };
      }

      public static List<User> GetUsers()
      {
          return new List<User>
          {
              new User{ Id =1 , CreatedOn = DateTime.UtcNow, EmailId = "mak.thevar@outlook.com", IsActive = true, Name = "mak thevar", RoleId =1, Username = "mak-thevar", Password = EasyEncryption.MD5.ComputeMD5Hash("12345678")},
          };
      }
 </pre>
</details>

- Now Build the project and run, Initially for the very first time it will create the database and will execute the migration scripts automatically.



## ✅ Features
- Uses [Serilog](https://serilog.net/) for stuctured logging.
- [JWT](https://jwt.io/) has been configured for authentication and authorization.
- Custom request, response and error handling has been configured for maintaning a detailed log of errors and requests.
- [Swagger](https://swagger.io/) for API documentation has been added.
- [Entityframework Core](https://docs.microsoft.com/en-us/ef/core/) has been configured for database communication. (_Currently have added SQLLite for sample DB_)
- Follows [Repository pattern](https://deviq.com/repository-pattern/) for the database operations.
- A Sample controller to Add Todos with Register user and Login User has been added.

## 🔘 Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


## 📸 Screenshots
| ![login_screen](https://user-images.githubusercontent.com/40656217/154900109-e8129bfb-b9aa-4091-afc8-621eefe943b8.gif) | 
|:--:| 
| *Login Using Insomnia/PostMan* |

| ![swagger_doc](https://user-images.githubusercontent.com/40656217/154900119-48cdd956-efb3-4b3e-bade-c68566a87a55.gif) | 
|:--:| 
| *Swagger Documentation* |

| ![RegisterUser](https://user-images.githubusercontent.com/40656217/154904538-959a585e-f1ab-4dbb-8d0b-46f7b4e0bcbd.gif) | 
|:--:| 
| *Register User API* |

| ![swagger_login](https://user-images.githubusercontent.com/40656217/154900137-8146dd6e-862e-4b0f-ab42-77272959da84.gif) | 
|:--:| 
| *Login Using Swagger* |



<!-- LICENSE -->
## 🎫 License

Distributed under the MIT License. See [`LICENSE`](https://github.com/mak-thevar/core-webapi-boilerplate/blob/master/LICENSE) for more information.

<!-- CONTACT -->
## 📱 Contact

Name - [Muthukumar Thevar](#) - mak.thevar@outlook.com

Project Link: [https://github.com/mak-thevar/core-webapi-boilerplate](https://github.com/mak-thevar/core-webapi-boilerplate)


[contributors-shield]: https://img.shields.io/github/contributors/mak-thevar/core-webapi-boilerplate.svg?style=flat-square
[contributors-url]: https://github.com/mak-thevar/core-webapi-boilerplate/graphs/contributors

[issues-shield]: https://img.shields.io/github/issues/mak-thevar/core-webapi-boilerplate.svg?style=flat-square
[issues-url]: https://github.com/mak-thevar/core-webapi-boilerplate/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/mak11/
