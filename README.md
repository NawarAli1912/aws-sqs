# AWS SQS Demo App

This is a demo application that demonstrates how to work with AWS Simple Queue Service (SQS).

## Installation

To install the app, follow these steps:

1. Clone this repository to your local machine.
2. Configure your AWS credentials by setting the following environment variables:
``` bash
AWS_ACCESS_KEY_ID=your_access_key_id
AWS_SECRET_ACCESS_KEY=your_secret_access_key
AWS_REGION=us-east-1
```
3. Set your queue name in the app code (e.g., in Program.cs).
4. Run the app using `dotnet run`.

## Usage

To use the app, follow these steps:

1. Start the app using `dotnet run`.
2. Use the app to create a queue, send messages to the queue, and receive messages from the queue.
3. When you're finished, use the app to delete the queue.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
