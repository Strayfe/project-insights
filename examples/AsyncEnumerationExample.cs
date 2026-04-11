// This is a C# 14 File-based application demonstrating IAsyncEnumerable for streaming scenarios.
// Run via: dotnet run --file AsyncEnumerationExample.cs

await foreach (var line in GenerateDataAsync())
{
    Console.WriteLine($"[Processing] {line}");
    await Task.Delay(Random.Shared.Next(100, 500));
}

Console.WriteLine("Streaming complete.");

return;

static async IAsyncEnumerable<string> GenerateDataAsync()
{
    foreach (var i in Enumerable.Range(1, 20))
    {
        await Task.Yield();
        yield return $"Line {i}: Sample data record";
    }
}
