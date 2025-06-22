# Corebyte  | Back-End Application

## Summary

Corebyte is a specialized inventory management solution designed specifically for producer and distributor. Key features include:

- Efficient management of liquor stock
- User-friendly interface
- Comprehensive sales analytics and reporting
- Integration with suppliers

## Features

The application includes the following documentation:

- CRUD operations for managing liquor stock.
- User authentication and authorization.
- Role-based access control for different user types (producer and distributor)
- Detailed sales reports and analytics.
- Profile management for users.

## Bounded Contexts

This version of Corebyte is focused on the stock management aspect of the application, allowing authentication and management of profiles by their user roles. It includes the following bounded contexts:

### Batch Management Context

This context focuses on managing the stock of liquors in multiple stores, allowing users to manage each lot, add new products and manage the inventory efficiently. Includes the following characteristics:

- Fermentation Management (Add).
- Pressure management (add).
- Clarification management (add and update lot).
- Aging management (add).
- Bottling management (add).
- Lot status monitoring (see current stock levels, update stock).
- Date/temperature management.

This context also includes an anti -corruption layer to ensure that the inventory management system is safe and does not interfere with the central commercial logic of the application. Its capabilities include:

- Multiple management and stock level
- Management of product expiration dates.

### History and Record Context

This context provides detailed lot management reports, helping users to make reported decisions based on their actions. Includes the following characteristics:

- Restock planning.
- Payment history tracking.
- Loses and damages reporting.

### Order Context

This context focuses on orders, allowing users to track the entire list of orders that exist, add new products and administer the inventory efficiently. Includes the following characteristics:

- Customer management.
- Product management (add, delete products).
- Shares monitoring (see date, product, quantity).
- Tell the products and the total amount of the purchase.
- Management of the product expiration date.

This context also includes an anti -corruption layer to avoid that the orders management system is safe and does not interfere with the central commercial logic of the application. Its capabilities include:

- Multiple orders management.
- Customer tracking.
- Management of product expiration dates.

### Authentication Context

This context handles user authentication and authorization, ensuring secure access to the application. It includes the following features:

- User registration and login.
- Role-based access control.
- Token-based authentication.
- Session management.

This context includes also an anticorruption layer to ensure that the authentication process is secure and does not interfere with the core business logic of the application. Its capabilities include:

- Validating user credentials.
- Generating and validating authentication tokens.
- Ensuring secure password storage and management.
- Implementing role-based access control to restrict access to certain features based on user roles.

### Replenishment Context

This context focuses on managing the liquor stock, allowing users to track stock levels, add new products and manage the inventory efficiently. Includes the following characteristics:

- Product management (add, update, eliminate products).
- Shares monitoring (see current stock levels, update stock).
- Management of the product expiration date.

This context also includes an anti -corruption layer to ensure that the inventory management system is safe and does not interfere with the central commercial logic of the application. Its capabilities include:

- Multiple stock management.
- Management of product expiration dates and care guides.
