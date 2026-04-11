// This is a C# 14 File-based application demonstrating the Strategy Resolver Pattern
// used in the BYOD project to handle extensible data imports.
// Run via: dotnet run --file StrategyResolverExample.cs

var resolver = new ImportStrategyResolver(new List<IImportStrategy> 
{
    new EsgImportStrategy(),
    new FinancialImportStrategy()
});

// Simulate metadata from an uploaded file
const string fileMetadata = "ESG_2025_Q1.csv"; 
var strategy = resolver.Resolve(fileMetadata);

strategy.Process(fileMetadata);

file interface IImportStrategy
{
    bool CanHandle(string metadata);
    void Process(string file);
}

file class EsgImportStrategy : IImportStrategy
{
    public bool CanHandle(string metadata) => metadata.Contains("ESG");
    public void Process(string file) => Console.WriteLine($"[ESG Strategy] Processing {file} into the ESG dimension...");
}

file class FinancialImportStrategy : IImportStrategy
{
    public bool CanHandle(string metadata) => metadata.Contains("FIN");
    public void Process(string file) => Console.WriteLine($"[Financial Strategy] Processing {file} into the Financial dimension...");
}

file class ImportStrategyResolver(IEnumerable<IImportStrategy> strategies)
{
    public IImportStrategy Resolve(string metadata)
    {
        return strategies.FirstOrDefault(s => s.CanHandle(metadata)) 
               ?? throw new Exception("No strategy found for this data type.");
    }
}
