![Build Status](https://github.com/mak-thevar/core-webapi-boilerplate/actions/workflows/dotnet.yml/badge.svg)
[![Contributors][contributors-shield]][contributors-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

# CoreWebApi-BoilerPlate
A [.net core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1) based template with pre-cofigured useful libraries like swagger, jwt, automapper, etc.

## 📋 Table of Contents 
* [Getting Started](#-getting-started)
* [Installation](#-installation)
* [Features](#-features)
* [Contributing](#-contributing)
* [Screenshots](#-screenshots)
* [License](#-license)
* [Contact](#-contact)



## 🏁 Getting Started
### Prerequisites
- [Visual Studio](https://visualstudio.microsoft.com/) OR [Visual Studio Code](https://code.visualstudio.com/)


## Installation

- Clone the repository
```sh
git clone https://github.com/mak-thevar/core-webapi-boilerplate.git
```
- Open the solution file 'CoreWebApiBoilerPlate.sln' directly in Visual Studio
- The database is cofigured to use sqllite you can change it to appropriate sql like MSSQL or mysql, the settings can be found on the startup file in ConfigureServices method
  ```cs
  options.UseSqlite(@"DataSource=corewebapi.db",opt=>opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
  ```
- Now Build the project and run, Initially for the very first time it will create the database and will execute the migration scripts automatically.



## ✅ Features
- Uses [Serilog](https://serilog.net/) for stuctured logging.
- [JWT](https://jwt.io/) has been configured for authentication and authorization.
- Custom request, response and error handling has been configured for maintaning a detailed log of errors and requests.
- [Swagger](https://swagger.io/) for API documentation has been added.
- [Entityframework Core](https://docs.microsoft.com/en-us/ef/core/) has been configured for database communication. (_Currently have added SQLLite for sample DB_)
- Follows [Repository pattern](https://deviq.com/repository-pattern/) for the database operations.
- A Sample controller to Add Posts with Register user and Login User has been added.

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
