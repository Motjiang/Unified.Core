# ERP Management System - Unified

## Overview

This ERP Management System is a lightweight yet comprehensive solution designed to streamline core organizational operations such as role management, ticketing, inventory control, leave management, and analytics. The system facilitates smooth collaboration between Admin, HR, Technicians, and Assistants with tailored dashboards and automated processes.

Built using modern technologies including ASP.NET Core RESTful APIs, Angular 17, JWT authentication, and hosted on Microsoft Azure, this project is aimed at providing a secure, scalable, and maintainable ERP solution for small to medium enterprises.

## Features

### Role-Based Access Control
- Admin creates HR, who in turn can create other roles like Technician and Assistant with specific permissions.

### Ticketing System
- Admin, HR, and Assistants can log support tickets for Technicians to resolve.

### Inventory & Bookstore Management
- Bookstore Assistant manages sales manually, updates inventory, and can request new books from Admin if stock is unavailable.

### Leave Management
- Employees can submit leave requests; HR reviews and approves or rejects these requests.

### Employee & Department Management
- HR can create departments and designations and assign them to employees.

### Audit Trails
- Tracks changes and important operations for accountability and monitoring.

### Analytics Dashboards
- Role-specific dashboards provide insights into sales, tickets, leaves, and other KPIs.

### Authentication & Security
- Secure login using JWT tokens with password reset via Mailjet email integration.

### Cloud Deployment
- Soon >> it will be hosted on Microsoft Azure using App Services, SQL Database, and Blob Storage for scalability and reliability.

## Technology Stack

| Layer           | Technology / Tools                         |
|-----------------|--------------------------------------------|
| Backend API     | ASP.NET Core 9, C#                         |
| Frontend        | Angular 17, TypeScript, Bootstrap 5       |
| Authentication  | JWT (JSON Web Tokens)                      |
| Email Service   | Mailjet SMTP                              |
| Database        | Microsoft SQL Server                       |
| Cloud Hosting   | Microsoft Azure (App Services, SQL Database, Blob Storage) |
| Version Control | Git & GitHub                              |
