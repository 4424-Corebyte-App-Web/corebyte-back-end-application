# User Stories

This document contains the technical user stories for the ```corebyte-platform``` REST API from the perspective of a developer interacting with it thought HTTP request.

## TS007: Deleting a Distributor Client

**As a** backend developer at CoreByte, **I want** to implement the ability to delete a distributor client via an API to ensure winemakers can keep their distributor client list clean and up-to-date.

**Acceptance Criteria**

- Scenario 1: Successfully Implement Distributor Client Deletion
  - **Given** that I am authorized to use the API and the distributor client deletion endpoint,
  - **when** I send a DELETE request to delete the distributor client with a valid ID,
  - **then** server responds with a 204 No Content status code and confirms that the distributor client deletion was successful.

- Scenario 02: Implement Reseller Client Deletion by Non-Existent ID
  - **Given** that I am authorized to use the API and the Reseller Client Deletion endpoint,
  - **when** I send a DELETE request to delete a Reseller Client with a non-existent ID,
  - **then** server responds with a 404 Not Found status code and I receive an error message indicating that the Reseller Client cannot be found.

- Scenario 03: Implement Reseller Client Deletion without Authorization
  - **Given** that I am not authorized to use the API for the Reseller Client Deletion endpoint,
  - **when** I send a DELETE request to delete a Reseller Client,
  - **then** server responds with a 403 Forbidden status code and I receive an error message indicating that I am not authorized to perform this action.

## TS009: Register an Order

**As a** backend developer at CoreByte, I want to create a new order through an API to allow winemakers to register their orders efficiently.

**Acceptance Criteria**

- Scenario 1: Create Order Successfully
  - **Given** that I am authorized to use the API and the order creation endpoint,
  - **when** I send a POST request with the necessary data from the distributor client,
  - **then** server responds with a 201 Created status code and I receive the new order information in a JSON response that includes the newly generated ID.

- Scenario 2: Create Order with Invalid Data
  - **Given** that I am authorized to use the API and the order creation endpoint,
  - **when** I send a POST request with data that does not meet the required validations,
  - **then** server responds with a 400 Bad Request status code and I receive an error message detailing which fields are invalid.

- Scenario 03: Create Order Without Authorization
  - **Given** that I am not authorized to use the order creation endpoint API,
  - **when** I send a POST request to create a new order,
  - **then** server responds with a 403 Forbidden status code and I receive an error message indicating

## TS0010: View Order Details

**As a** backend developer at CoreByte, I want to implement the ability to view order details through an API so that winemakers can efficiently access detailed order information.

**Acceptance Criteria**

- Scenario 1: View Order Details Successfully
  - **Given** that I am authorized to use the API and the order details endpoint,
  - **when** I send a GET request with the order ID,
  - **then** server responds with a 200 OK status code, and I receive the order information in a JSON response.

- Scenario 2: Error Retrieving Order Details Data
  - **Given** that I am authorized to use the API and the order details endpoint,
  - **when** I send a GET request with the ID, but there is an internal error communicating with the backend,
  - **then** server responds with a 400 Bad Request status code, and I receive an error message, and the data will not be displayed on the frontend.

## TS0013: Order Status Tracking and Notifications

**As a** backend developer at CoreByte, **I want** to implement an API endpoint so distributors can track the status of their orders and receive notifications so they are informed of any changes in the order status or delivery date.

**Acceptance Criteria**

- Scenario 1: Viewing Order Status
  - **Given** that an authenticated distributor accesses the platform,
  - **When** they send a GET request to the order tracking endpoint,
  - **Then** the server responds with a 200 OK status code, providing a list of orders in JSON format.

- Scenario 2: Order Status Update and Notification
  - **Given** that an order status changes (for example, from pending to shipped),
  - **When** the system updates the order status,
  - **Then** the distributor receives an email and platform notification, and the server responds with a 200 OK status code, confirming the notification was sent.

- Scenario 3: Error When Attempting to Modify an Order Already Sent
  - **Given** that an authenticated distributor attempts to modify an order that has already been sent,
  - **When** they send a PUT request to the order modification endpoint,
  - **Then** server then responds with a 400 Bad Request status code, indicating that an order that has already been sent cannot be modified.

## TS0014: Viewing Inventory Item Details

**As a** backend developer at CoreByte, I want to implement the ability to view inventory item details through an API, allowing users to efficiently access detailed information about each item.

**Acceptance Criteria**

- Scenario 1: View Item Details Successfully
  - **Given** that I have authorization to use the API and the item details viewing endpoint,
  - **When** the user sends a GET request to retrieve item details with a valid ID,
  - **Then** the server responds with a 200 OK status code.
  - **And** the user receives the item information in a JSON response.

- Scenario 2: View Item Details by Nonexistent ID
  - **Given** that I am authorized to use the API and the item details view endpoint,
  - **When** the user sends a GET request to retrieve item details with an ID that does not exist,
  - **Then** the server responds with a 404 Not Found status code.
  - **And** the user receives an error message indicating that the item cannot be found.

- Scenario 3: View Item Details Without Authorization
  - **Given** that I am not authorized to use the API for the item details view endpoint,
  - **When** the user sends a GET request to retrieve item details,
  - **Then** the server responds with a 403 Forbidden status code.
  - **And** the user receives an error message indicating that they are not authorized to perform this action.

## TS0016: Adding a New Item to Inventory

**As a** backend developer at CoreByte, I want to implement the ability to add new items to inventory via an API, allowing administrators to efficiently register new products.

**Acceptance Criteria**

- Scenario 1: Add Item Successfully
  - **Given** that I have authorization to use the API and the item creation endpoint,
  - **When** I send a POST request with the necessary data for the new item (name, quantity, unit, supplier, cost per unit),
  - **Then** the server responds with a 201 Created status code,
  - **And** I receive the new item information in a JSON response that includes the newly generated ID.

- Scenario 2: Adding an Item with Invalid Data
  - **Given** that I am authorized to use the API and the item creation endpoint,
  - **When** I send a POST request with data that does not meet the required validations (e.g., empty fields),
  - **Then** the server responds with a 400 Bad Request status code,
  - **And** I receive an error message detailing which fields are invalid.

- Scenario 3: Adding an Item Without Authorization
  - **Given** that I am not authorized to use the item creation endpoint API,
  - **When** I send a POST request to add a new item,
  - **Then** the server responds with a 403 Forbidden status code,
  - **And** I receive an error message indicating that I am not authorized to perform this action.
