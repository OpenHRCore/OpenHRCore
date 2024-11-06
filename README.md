# OpenHRCore - Human Resource Management System (HRMS) - Main README

## Overview

**OpenHRCore** is an open-source Human Resource Management System (HRMS) developed using Microsoft technologies. This system is built with clean architecture principles, ensuring a well-organized, scalable, and maintainable solution for managing various HR functions.

OpenHRCore provides a comprehensive suite of HR management tools designed to streamline and enhance HR processes within an organization. The system is modular, allowing for easy customization and extension, and is built using the following technologies:

- **IDE**: Visual Studio 2022
- **Language**: C#
- **Libraries/Frameworks**: Automapper, FluentValidation, Entity Framework Core, ASP.NET Core 8, Serilog
- **Multi-language Support**
- **Backend**: ASP.NET Core 8 REST API
- **Frontend**: React

## Modules Overview

The HRMS consists of the following core modules:

1. [**CareerConnect (Recruitment Module)**](./src/OpenHRCore.Domain/CareerConnect) - Manage job postings, applications, and candidate tracking. Simplify the recruitment process and enhance hiring efficiency.
2. [**WorkForce (Employee Module)**](./src/OpenHRCore.Domain/Workforce) - Oversee employee records, roles, and organizational structure. Facilitate effective employee management and organizational alignment.
3. [**TimeTrack (Attendance Module)**](./src/OpenHRCore.Domain/TimeTrack) - Track employee attendance, working hours, and absences. Ensure accurate attendance records and streamline time management.
4. [**FlexiLeave (Leave Module)**](./src/OpenHRCore.Domain/FlexiLeave) - Handle leave requests, approvals, and balances. Manage employee leave effectively and ensure compliance with company policies.
5. [**PayStream (Payroll Module)**](./src/OpenHRCore.Domain/PayStream) - Manage payroll processing, salary calculations, tax, SSC (Social Security Contribution), allowances, deductions, and multi-currency payroll. Ensure timely and accurate payroll management.
6. [**EasyESS (Employee Self-Service)**] - Provide employees with self-service options for HR-related tasks via web and mobile applications. Empower employees to manage their HR needs independently.
7. [**ControlCenter (Administration Module)**](./src/OpenHRCore.Domain/ControlCenter) - Administer system settings, user roles, and permissions. Centralize administrative tasks and manage system configurations.
8. [**SetupCentral (Common Configuration)**] - Configure system-wide settings and manage common configurations. Ensure consistent and efficient system setup.

Each module is designed to be independent, with its own data modeling, flow, and business logic. Below is a summary of each module, along with links to their respective README files, which provide detailed information on data models, example scenarios, flow diagrams, and usage.

## System Architecture

OpenHRCore is designed with **Clean Architecture** principles. This approach ensures a structured and maintainable codebase, with clear separation of concerns and modular design to accommodate future growth and changes.

The project is structured into different layers:

1. **Core Layer**: Contains entities and business logic common across the system.
2. **Domain Layer**: Contains the business logic specific to each module.
3. **Infrastructure Layer**: Contains data access code, such as repositories, and integrates with databases and third-party services.
4. **Presentation Layer**: Handles the user interface and interacts with the user to perform HRMS operations.

## Getting Started

To get started with OpenHRCore, follow these steps:

1. **Clone the Repository**
   ```sh
   git clone https://github.com/OpenHRCore/OpenHRCore.git
   ```

2. **Navigate to the project directory**:
   ```sh
   cd hrms-open-source
   ```

3. **Database Migration**:
   - Install dotnet ef cli tool
   ```sh
   dotnet tool install --global dotnet-ef
   ```
   - Run ef migration script
   ```sh
   dotnet ef migrations add InitialMigration --context OpenHRCoreDbContext --project .\OpenHRCore.Infrastructure\OpenHRCore.Infrastructure.csproj --startup-project .\OpenHRCore.API\OpenHRCore.API.csproj
   ```
   - Run ef database update
   ```sh
   dotnet ef database update --context OpenHRCoreDbContext --project .\OpenHRCore.Infrastructure\OpenHRCore.Infrastructure.csproj --startup-project .\OpenHRCore.API\OpenHRCore.API.csproj
   ```

## Contributing

We welcome contributions from the open-source community. If you would like to contribute, please fork the repository and submit a pull request. Make sure to follow the contribution guidelines.

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature-name`).
3. Commit your changes (`git commit -am 'Add your feature'`).
4. Push to the branch (`git push origin feature/your-feature-name`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more information.

## Contact

If you have any questions or feedback, feel free to reach out to the project maintainers via email: [info@openhrcore.com](mailto:info@openhrcore.com).

## Conclusion

The HRMS project, **OpenHRCore**, is designed to provide a flexible, scalable, and modular HR management solution. It aims to simplify and automate various HR activities, ensuring effective HR administration for organizations of all sizes. Explore each module to understand its functionality and how it integrates with the overall system.

