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

## 🔄 Application Flow

### 1. Authentication Flow
```
Login Page → Role Selection → Credential Validation → Dashboard Redirect
```

### 2. Customer Journey
```
Login → Customer Dashboard → Select Service → Perform Action → View Results
```

### 3. Admin Journey
```
Login → Admin Dashboard → Customer Management → Account Operations → Reports
```

### 4. Money Transfer Process
```
Search Beneficiary → Verify Account → Enter Amount → Confirm Transfer → Update Balances → Record Transaction
```

### 5. Bill Payment Process
```
Select Bill Type → Enter Amount → Verify Balance → Process Payment → Update Account → Record Transaction
```

## 🚦 Getting Started

### Prerequisites
- Visual Studio 2019 or later
- SQL Server 2016 or later
- .NET Framework 4.5.2 or later

### Installation Steps

1. **Clone the Repository**
   ```bash
   git clone [repository-url]
   cd online-banking
   ```

2. **Database Setup**
   - Open SQL Server Management Studio
   - Execute the `SQL Queries/BankingQuery.sql` script
   - Update connection string in `Web.config` if needed

3. **Configure Connection String**
   ```xml
   <connectionStrings>
     <add name="BankingDBEntities" 
          connectionString="your-connection-string-here" 
          providerName="System.Data.EntityClient" />
   </connectionStrings>
   ```

4. **Build and Run**
   - Open `OnlineBanking.sln` in Visual Studio
   - Restore NuGet packages
   - Build the solution (Ctrl+Shift+B)
   - Run the application (F5)

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

2. **Money Transfer**
   - Valid beneficiary search
   - Sufficient balance verification
   - Transaction recording

3. **Bill Payment**
   - Bill type selection
   - Amount validation
   - Payment processing

4. **Transaction History**
   - Date range filtering
   - Statement generation
   - Data accuracy verification

## 🚀 Deployment

### Production Deployment:
1. **Database Migration**: Deploy database schema to production server
2. **Application Deployment**: Publish to IIS server
3. **Configuration**: Update connection strings and settings
4. **Security**: Configure SSL certificates and security settings

### Environment Requirements:
- Windows Server 2016 or later
- IIS 8.0 or later
- SQL Server 2016 or later
- .NET Framework 4.5.2 or later

## 📊 Performance Considerations

- **Database Indexing**: Optimized queries with proper indexing
- **Session Management**: Efficient session handling
- **Caching**: Static content caching
- **Connection Pooling**: Entity Framework connection pooling

## 🔮 Future Enhancements

- **Mobile App**: Native mobile application
- **API Integration**: RESTful API for third-party integrations
- **Advanced Security**: Two-factor authentication
- **Real-time Notifications**: Push notifications for transactions
- **Advanced Reporting**: Comprehensive financial reports
- **Multi-currency Support**: Support for multiple currencies

## 👥 User Roles & Permissions

### Customer Role:
- View account information
- Transfer money to other accounts
- Pay bills
- View transaction history
- Check account balance

### Admin Role:
- All customer permissions
- Manage customer accounts
- Add/update customer information
- View all transactions
- Generate reports
- System administration

## 🐛 Troubleshooting

### Common Issues:
1. **Database Connection**: Verify connection string and SQL Server access
2. **Session Issues**: Check session configuration in Web.config
3. **Validation Errors**: Ensure all required fields are properly filled
4. **Permission Errors**: Verify user roles and access levels

### Debug Mode:
- Enable debug mode in Web.config for detailed error messages
- Check browser console for JavaScript errors
- Review server logs for backend issues

## 📝 License

This project is developed for educational and demonstration purposes. Please ensure compliance with banking regulations and security standards before using in production environments.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📞 Support

For technical support or questions about this project, please contact the development team or create an issue in the repository.

---

**Note**: This is a demonstration project. For production use, additional security measures, compliance with banking regulations, and thorough testing are required.
