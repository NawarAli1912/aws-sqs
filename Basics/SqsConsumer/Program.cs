using Amazon.SQS;
using Amazon.SQS.Model;

var cts = new CancellationTokenSource();

var sqsClient = new AmazonSQSClient();

var queueRrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueRrlResponse.QueueUrl,

    // the message attribute is not loaded for 
    // efficiencies reasons you have to explicitly request them
    AttributeNames = new List<string> { "All" },
    MessageAttributeNames = new List<string> { "All" }
};

while (!cts.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);

    foreach (var message in response.Messages)
    {
        Console.WriteLine($"Message Id : {message.MessageId}");
        Console.WriteLine($"Message Body: {message.Body}");

        // Consuming a message dosen't mean that it's been deleted by the consumer.
        await sqsClient.DeleteMessageAsync(queueRrlResponse.QueueUrl, message.ReceiptHandle);
    }

    await Task.Delay(3000);
}
