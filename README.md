# Bus Ticketing System API Documentation

## Base URL

`http://localhost:5000/api`

## Endpoints

### 1. Get All Routes

- **URL:** `/routes`
- **Method:** GET
- **Description:** Retrieves all available bus routes.
- **Response:**
  ```json
  [
    {
      "id": 1,
      "name": "Mecer to Johannesburg CBD",
      "stops": [
        { "id": 5, "name": "Mecer" },
        { "id": 4, "name": "Midrand First" },
        { "id": 3, "name": "Midrand Second" },
        { "id": 2, "name": "Randburg" },
        { "id": 1, "name": "Johannesburg CBD" }
      ]
    }
  ]
  ```

### 2. Get All Schedules

- **URL:** `/schedules`
- **Method:** GET
- **Description:** Retrieves all bus schedules.
- **Response:**
  ```json
  [
    {
      "id": 1,
      "routeId": 1,
      "routeName": "Mecer to Johannesburg CBD",
      "busId": 1,
      "busNumber": "Bus001",
      "departureTime": "06:00:00"
    },
    {
      "id": 2,
      "routeId": 1,
      "routeName": "Mecer to Johannesburg CBD",
      "busId": 2,
      "busNumber": "Bus002",
      "departureTime": "13:00:00"
    },
    {
      "id": 3,
      "routeId": 1,
      "routeName": "Mecer to Johannesburg CBD",
      "busId": 3,
      "busNumber": "Bus003",
      "departureTime": "17:00:00"
    }
  ]
  ```

### 3. Get Available Seats

- **URL:** `/schedules/{scheduleId}/available-seats`
- **Method:** GET
- **Description:** Retrieves the number of available seats for a specific schedule.
- **Response:**
  ```json
  {
    "scheduleId": 1,
    "availableSeats": 45
  }
  ```

### 4. Purchase Ticket

- **URL:** `/tickets`
- **Method:** POST
- **Description:** Purchases a ticket for a specific schedule.
- **Request Body:**
  ```json
  {
    "userId": 1,
    "scheduleId": 1
  }
  ```
- **Response:**
  ```json
  {
    "id": 1,
    "userId": 1,
    "scheduleId": 1,
    "purchaseDate": "2024-10-17T10:30:00Z"
  }
  ```

### 5. Get User Tickets

- **URL:** `/users/{userId}/tickets`
- **Method:** GET
- **Description:** Retrieves all tickets purchased by a specific user.
- **Response:**
  ```json
  [
    {
      "id": 1,
      "scheduleId": 1,
      "routeName": "Mecer to Johannesburg CBD",
      "departureTime": "06:00:00",
      "purchaseDate": "2024-10-17T10:30:00Z"
    }
  ]
  ```
