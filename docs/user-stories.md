# User Stories

This document contains the technical user stories for the ```corebyte-platform``` REST API from the perspective of a developer interacting with it thought HTTP request.

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

