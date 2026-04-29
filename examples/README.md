# Runnable Examples

This directory contains minimal, high-impact examples of the architectural patterns used in various projects.

## Examples

### 1. Strategy Resolver Pattern (`StrategyResolverExample.cs`)
Demonstrates how the system dynamically resolves the correct processing logic for different data types 
(ESG vs. Financials) without modifying the core engine.

**Run it:**
```bash
dotnet run --file StrategyResolverExample.cs
```

### 2. Producer-Consumer Streaming (`StreamingExample.cs`)
Demonstrates the use of `System.Threading.Channels` for buffered handoff between a fast producer (database reader)
and a slower consumer (AI transformer).

**Run it:**
```bash
dotnet run --file StreamingExample.cs
```

## .NET 10 C# 14+ File-Based Applications
These examples leverage the .NET 10 C# 14+ **File-based application** feature. 
This allows for zero-ceremony experimentation without the cognitive load of project files or complex directory 
structures.