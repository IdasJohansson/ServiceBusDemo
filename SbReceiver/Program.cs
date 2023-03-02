using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;
using System; 

class Program
{
    // This is supposed to be in a configuration file, this is only for demo testing
    const string connectionString = "INSERT CONNECTION STRING HERE";
    const string queueName = "personqueue";
    static IQueueClient queueClient;

    // Note that this should be a static async Task
    static async Task Main(string[] args)
    {
        queueClient = new QueueClient(connectionString, queueName);

        var messageHandlerOptions = new MessageHandlerOptions(ExeptionReceivedHandler);


    }

    private static Task ExeptionReceivedHandler(ExceptionReceivedEventArgs arg)
    {
        throw new NotImplementedException();
    }
}