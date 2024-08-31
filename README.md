# OpenHRCore

**OpenHRCore** is an open-source Human Resource Management System (HRMS) developed using Microsoft technologies. This system is built with a modular monolithic architecture and adheres to clean architecture principles, ensuring a well-organized, scalable, and maintainable solution for managing various HR functions.

## Overview

OpenHRCore provides a comprehensive suite of HR management tools designed to streamline and enhance HR processes within an organization. The system is modular, allowing for easy customization and extension, and is built using the following technologies:

- **IDE**: Visual Studio 2022
- **Language**: C#
- **Framework**: ASP.NET Core WebAPI
- **UI**: ASP.NET Blazor (Web), MAUI (Mobile for ESS)
- **ORM**: Entity Framework Core

## Modules

The system includes the following key modules:

1. **CareerConnect** - Recruitment
   - Manage job postings, applications, and candidate tracking. Simplify the recruitment process and enhance hiring efficiency.

2. **WorkForce** - Employee Management
   - Oversee employee records, roles, and organizational structure. Facilitate effective employee management and organizational alignment.

3. **TimeTrack** - Attendance
   - Track employee attendance, working hours, and absences. Ensure accurate attendance records and streamline time management.

4. **FlexiLeave** - Leave Management
   - Handle leave requests, approvals, and balances. Manage employee leave effectively and ensure compliance with company policies.

5. **PayStream** - Payroll
   - Manage payroll processing, salary calculations, and compensation. Ensure timely and accurate payroll management.

6. **EasyESS** - Employee Self-Service
   - Provide employees with self-service options for HR-related tasks via web and mobile applications. Empower employees to manage their HR needs independently.

7. **ControlCenter** - Administration
   - Administer system settings, user roles, and permissions. Centralize administrative tasks and manage system configurations.

8. **SetupCentral** - Common Configuration
   - Configure system-wide settings and manage common configurations. Ensure consistent and efficient system setup.

## Architecture

OpenHRCore is designed with a **Modular Monolithic Architecture** and follows **Clean Architecture** principles. This approach ensures a structured and maintainable codebase, with clear separation of concerns and modular design to accommodate future growth and changes.

## Getting Started

To get started with OpenHRCore, follow these steps:

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/OpenHRCore.git
