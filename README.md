# aws-sqs
The aws-sqs is a sample application that demonstrates how to use AWS Simple Queue Service (SQS) 
to handle messaging between different services or components.

---

## Installation

To install the app, follow these steps:

1. Clone this repository to your local machine.
2. Configure your AWS credentials by setting the following environment variables:
``` bash
AWS_ACCESS_KEY_ID=your_access_key_id
AWS_SECRET_ACCESS_KEY=your_secret_access_key
AWS_REGION=us-east-1
```
3. open the solution with any editor you like.
4. Run the app using `dotnet run`.

## Usage

The aws-sqs consists of three parts:

- The Basics folder: This folder contains a basic example that demonstrates how to publish and receive messages using AWS SQS. You can learn how to create, send, and receive messages between different services or components.
- The Customers.Api folder: This folder contains a web API project that uses **Dapper** as micro ORM, **Fluent Validation**, and **AWS SQS**. You can learn how to create a **RESTful API** with CRUD operations and integrate **AWS SQS** for publishing messages to the queue. In addition, the API validates user input by making an API call to a third-party service that checks the validity of GitHub usernames.
- the Customer.QueueConsumer: this is an asp web project which runs a background service to consume the queue messages that was published by the Customer.Api project, it uses *MediatR* to send the message to the proper consumer handler.


## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
