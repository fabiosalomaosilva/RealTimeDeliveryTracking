Aqui está a versão revisada do README em inglês, levando em consideração que o projeto já está criado e configurado no repositório:

---

# Real-Time Delivery Tracking System

## Introduction

This repository contains the source code for a real-time delivery tracking system built using .NET 8 and Apache Kafka. The system simulates a microservices environment where different services—such as Order Service, Tracking Service, and Notification Service—communicate via Kafka to create and update delivery orders in real time.

## Project Overview

### Services

- **Order Service:** Manages the creation and management of delivery orders.
- **Tracking Service:** Processes and updates the delivery status in real time.
- **Notification Service:** Sends status updates to end-users.

### Architecture

The system is based on a microservices architecture, with each service operating independently and communicating asynchronously via Kafka. This setup allows for scalable and decoupled service interactions.

### Technologies

- **.NET 8.0**: Used to develop the microservices.
- **Apache Kafka**: Handles asynchronous communication and event processing.
- **Docker**: Facilitates deployment of services and Kafka.
- **Entity Framework Core**: Manages database interactions.
- **PostgreSQL**: Relational database used for storing order and tracking information.
- **Redis**: Utilized as a cache for optimizing read operations.
- **React with TypeScript**: Frontend for tracking delivery status.

## Getting Started

### Prerequisites

Ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

### Running the Solution

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/realtime-delivery-tracking.git
   cd realtime-delivery-tracking
   ```

2. **Set Up the Solution:**

   This solution contains multiple projects (OrderService, TrackingService, and NotificationService). To run all services together:

   - Open the solution in your preferred IDE (e.g., Visual Studio).
   - In the Solution Explorer, right-click the solution and go to **Properties**.
   - Under **Startup Project**, select **Multiple startup projects**.
   - Set the Action for each service (OrderService, TrackingService, NotificationService) to **Start**.

3. **Docker Setup:**

   The repository includes a `docker-compose.yml` file that sets up Kafka and Zookeeper. To start the Kafka services:

   ```bash
   docker-compose up -d
   ```

   This command will start Kafka and Zookeeper containers required for running the microservices.

4. **Run the Application:**

   With the services configured to start together, you can now run the entire solution:

   - Press `F5` (or click on **Start** in Visual Studio) to start all the microservices simultaneously.

5. **Accessing the APIs:**

   Each service will be running on its own port. You can interact with the APIs using tools like Postman or via a frontend application. For example:

   - Order Service: `http://localhost:5000/api/orders`
   - Tracking Service: `http://localhost:5001/api/tracking`
   - Notification Service: `http://localhost:5002/api/notifications`

## Usage

Once the services are running, you can create delivery orders via the Order Service, track their status through the Tracking Service, and receive updates from the Notification Service.

## Contributing

Contributions are welcome! Please fork the repository, create a new branch, and submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For any questions or inquiries, feel free to reach out via [LinkedIn](https://www.linkedin.com/in/yourprofile).

---

This README now reflects the fact that the project is already set up in the repository and provides instructions on how to run the solution with multiple startup projects.
