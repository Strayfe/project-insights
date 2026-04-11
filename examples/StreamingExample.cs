using System.Threading.Channels;

// This is a C# 14 File-based application demonstrating the Producer-Consumer pattern
// used in the Intelligence project for buffered handoff between stages.
// Run via: dotnet run --file StreamingExample.cs

var rawChannel = Channel.CreateBounded<RawData>(10);
var transformedChannel = Channel.CreateBounded<TransformedData>(10);

try
{
    List<Exception> caughtExceptions = [];

    // start producing without awaiting
    var producer = Producer()
        .ContinueWith(task =>
        {
            // handle failure state in producer and ensure we don't wait forever for consumer
            if (task.IsFaulted)
                caughtExceptions.Add(task.Exception);

            rawChannel.Writer.TryComplete(task.Exception);
        });

    var transformer = Transformer()
        .ContinueWith(task =>
        {
            if (task.IsFaulted)
                caughtExceptions.Add(task.Exception);

            transformedChannel.Writer.TryComplete(task.Exception);
        });

    var consumer = Consumer()
        .ContinueWith(task =>
        {
            if (task.IsFaulted)
                caughtExceptions.Add(task.Exception);
        });

    // wait for the whole transformation pipeline to complete asynchronously and concurrently
    await Task.WhenAll(producer, transformer, consumer);

    // rethrow any exceptions that occurred during pipeline execution
    if (caughtExceptions.Count > 0)
        throw new AggregateException(caughtExceptions);

    Console.WriteLine("Pipeline complete.");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

return;

// simulate generating records from IO
async Task Producer()
{
    for (var i = 0; i < 50; i++)
    {
        var record = new RawData(i, $"Raw {i}");
        await rawChannel.Writer.WriteAsync(record);
        Console.WriteLine($"[Producer] Pushed record {record}");
        await Task.Delay(Random.Shared.Next(1, 5));
    }
    rawChannel.Writer.Complete();
}

// simulate applying transformations to records before passing to consumer (like an ETL pipeline)
async Task Transformer()
{
    await foreach (var record in rawChannel.Reader.ReadAllAsync())
    {
        var transformedRecord = new TransformedData(record.Id, $"Transformed {record.Value}");
        await transformedChannel.Writer.WriteAsync(transformedRecord);
        Console.WriteLine($"[Transformer] Transformed record {transformedRecord}...");
        await Task.Delay(Random.Shared.Next(1, 5));
    }
    transformedChannel.Writer.Complete();
}

// simulate persisting records
async Task Consumer()
{
    await foreach (var record in transformedChannel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"[Consumer] Consumed record {record}...");
        await Task.Delay(Random.Shared.Next(1, 5));
    }
}

file record RawData(int Id, string Value);
file record TransformedData(int Id, string Value);
