# Online Banking System

A comprehensive web-based banking application built with ASP.NET MVC 5, Entity Framework, and SQL Server. This system provides secure online banking services for both customers and administrators.

## 🏦 Project Overview

The Online Banking System is a full-featured web application that allows users to perform various banking operations including account management, money transfers, bill payments, and transaction history viewing. The system supports two types of users: **Customers** and **Administrators**, each with different access levels and functionalities.

## 🚀 Key Features

### For Customers:
- **Secure Login System** - Role-based authentication (Customer/Admin)
- **Account Dashboard** - Overview of account information and quick actions
- **Money Transfer** - Transfer funds between accounts with beneficiary search
- **Bill Payment** - Pay various types of bills (Electricity, Gas, Water, etc.)
- **Transaction History** - View detailed transaction statements with date filtering
- **Account Balance** - Check current account balance
- **Account Statement** - Detailed financial statements

### For Administrators:
- **Admin Dashboard** - Comprehensive overview of banking operations
- **Customer Management** - Add, update, and search customer accounts
- **Transaction Monitoring** - View all banking transactions
- **Account Management** - Manage customer accounts and balances
- **Reports** - Generate various banking reports

## 🛠️ Technology Stack

- **Backend Framework**: ASP.NET MVC 5
- **Database**: SQL Server with Entity Framework 6.1.3
- **Frontend**: HTML5, CSS3, Bootstrap 5.3.3, JavaScript
- **UI Libraries**: jQuery 3.7.1, SweetAlert2, Font Awesome
- **Architecture**: Model-View-Controller (MVC) Pattern
- **ORM**: Entity Framework Code First approach

## 📁 Project Structure

```
online-banking/
├── OnlineBanking/                    # Main project directory
│   ├── Controllers/                  # MVC Controllers
│   │   ├── DashboardController.cs    # Main dashboard logic
│   │   ├── RegisterLoginController.cs # Authentication & user management
│   │   ├── TransferMoneyController.cs # Money transfer operations
│   │   ├── PayBillController.cs     # Bill payment functionality
│   │   └── TransactionsController.cs # Transaction history & statements
│   ├── Models/                       # Data Models
│   │   ├── UserTable.cs             # User information model
│   │   ├── AccountsTable.cs         # Account details model
│   │   ├── TransactionsTable.cs     # Transaction records model
│   │   ├── StatementTable.cs        # Detailed statement model
│   │   ├── BillTable.cs             # Bill payment records model
│   │   └── BankDBModel.*            # Entity Framework context
│   ├── Views/                        # Razor Views
│   │   ├── Dashboard/               # Dashboard views
│   │   ├── RegisterLogin/           # Login & registration views
│   │   ├── TransferMoney/           # Money transfer views
│   │   ├── PayBill/                 # Bill payment views
│   │   ├── Transactions/            # Transaction history views
│   │   └── Shared/                  # Shared layouts
│   ├── Content/                     # CSS and styling files
│   ├── Scripts/                     # JavaScript files
│   ├── admin/                       # Admin dashboard assets
│   └── Web.config                   # Application configuration
├── packages/                        # NuGet packages
└── SQL Queries/                     # Database scripts
    └── BankingQuery.sql            # Database schema
```

## 🗄️ Database Schema

The system uses a relational database with the following main tables:

### Core Tables:
- **UserTable**: Stores user information (customers and admins)
- **AccountsTable**: Manages bank accounts with auto-generated account numbers
- **TransactionsTable**: Records all financial transactions
- **StatementTable**: Detailed transaction statements
- **BillTable**: Bill payment records

### Key Relationships:
- One User can have multiple Accounts
- One Account can have multiple Transactions
- One Account can have multiple Statements
- One Account can have multiple Bill payments


## 🔐 Security Features

- **Role-based Authentication**: Separate access levels for customers and admins
- **Session Management**: Secure session handling with automatic logout
- **Input Validation**: Client and server-side validation for all inputs
- **SQL Injection Protection**: Entity Framework parameterized queries
- **Password Security**: Password fields with proper data types

## 📱 User Interface

### Design Features:
- **Responsive Design**: Bootstrap-based responsive layout
- **Modern UI**: Clean and professional interface
- **Interactive Elements**: SweetAlert2 for user notifications
- **Dashboard Cards**: Quick access to main functions
- **Navigation**: Intuitive sidebar navigation

### Key UI Components:
- Login form with role selection
- Dashboard with service cards
- Transaction forms with validation
- Data tables for transaction history
- Modal dialogs for confirmations

## 🔧 Configuration

### Application Settings
- **Target Framework**: .NET Framework 4.5.2
- **Authentication**: Forms Authentication
- **Session Timeout**: Configurable in Web.config
- **Database Provider**: SQL Server with Entity Framework

### Dependencies
- Entity Framework 6.1.3
- Bootstrap 5.3.3
- jQuery 3.7.1
- jQuery Validation 1.20.0
- SweetAlert2 (via CDN)

## 🧪 Testing

### Test Scenarios:
1. **User Authentication**
   - Valid login credentials
   - Invalid login attempts
   - Role-based redirection


2. **Bill Payment**
   - Bill type selection
   - Amount validation
   - Payment processing

3. **Transaction History**
   - Date range filtering
   - Statement generation
   - Data accuracy verification
