using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Text;
using SbShared.Models;
using System.Text.Json;

class Program
{
    // This is supposed to be in a configuration file, this is only for demo testing
    const string connectionString = "INSERT CONNECTION STRING HERE";
    const string queueName = "personqueue";
    static IQueueClient queueClient;

    // Note that this should be a static async Task
    static async Task Main(string[] args)
    {
        // Create a new queue client which talks to the message queue in azure service bus
        queueClient = new QueueClient(connectionString, queueName);

        var messageHandlerOptions = new MessageHandlerOptions(ExeptionReceivedHandler)
        {
            // Max one call at the time
            MaxConcurrentCalls = 1,
            // This will not mark the message as complete, it will waint until the message is read
            AutoComplete = false
        };


        // RegisterMessageHandler is a method that acts like an eventhandler and is being called when there is a message
        // This methid calls the ProcessMessagesAsync method and also takes in the messageHandlerOptions from above as input
        queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

        Console.ReadLine();

        await queueClient.CloseAsync(); 
    }

    private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
        var jsonString = Encoding.UTF8.GetString(message.Body);
        PersonModel person = JsonSerializer.Deserialize<PersonModel>(jsonString);
        Console.WriteLine($"Person Received: {person.FirstName} {person.LastName}");

        await queueClient.CompleteAsync(message.SystemProperties.LockToken); 
    }

    private static Task ExeptionReceivedHandler(ExceptionReceivedEventArgs arg)
    {
        Console.WriteLine($"Message handler exeption: {arg.Exception}");
        return Task.CompletedTask; 
    }
}