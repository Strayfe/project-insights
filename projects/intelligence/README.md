# Intelligence: Anonymous Benchmarking Analytics

## Situation

A business requirement emerged to capture anonymous analytics from user systems, enabling tenants to benchmark their
expenditure against others on the platform.

The software as a service (SaaS) flagship product's features were well-defined, but the approach to capturing and
processing the anonymous data was largely left open to developer interpretation.

## Task

Design and implement an anonymous data ingestion pipeline from scratch with minimal guidance.

The solution needed to:
- be secure by design; it should not be possible to reverse engineer anonymous spend to specific tenants
- handle hundreds of millions of rows; highlighting the need for efficiency
- be extensible; changes to requirements may arrive fast
- not cost the earth; engineering effort and infrastructure concerns should be cost-effective

## Action

Built a command-line interface (CLI) application that ran locally, leveraging developer hardware performance—critical
for the high concurrency required.

Implemented in .NET 10 with a streaming architecture:

- **unbuffered queries** to avoid loading entire result sets into memory
- **asynchronous enumeration** for composable, lazy asynchronous data iteration
- **channels** for buffered handoff between concurrent producer and consumer coroutines
- **concurrent coroutine marshalling** to parallelize transformation stages
- **O(1) binary search and hash-based indexing** on materialised lookup collections
- **artificial intelligence (AI) prompt engineering** for data enrichment and category classification
- **crosswalk construction** to map between distinct taxonomies across source and target schemas

The pipeline concurrently streamed large volumes of records from the source system through multiple transformation
stages and wrote anonymised expenditure directly into a STAR schema data warehouse.

It was also capable of creating taxonomies and leveraged AI for building crosswalks between taxonomies in a similar domain.

## Result

- Delivered **idempotent tooling** capable of safely ingesting massive data volumes into a centralised, anonymised warehouse
- Unlocked the **benchmarking feature** in the SaaS flagship product, allowing customers to analyse their expenditure trends
- Enabled **cross-tenant comparison**, giving customers insight into their categorised spend relative to the wider platform
- Featured in a Spend Matters success report, demonstrating consistent value-added to clients of the system
- Used in negotiations to onboard high-profile clients
