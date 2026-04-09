# Intelligence: Anonymous Benchmarking Analytics

A new benchmarking feature demanded an anonymised view of hundreds of millions of expenditure rows across the platform —
no tooling existed, and the implementation approach was deliberately left open.

The task was to design and deliver an idempotent ingestion pipeline from scratch, reliable enough to rerun safely and
performant enough to handle the full data volume at production quality.

I personally built a .NET 10 command-line interface (CLI) leveraging local hardware concurrency: unbuffered queries,
async enumeration for streaming, `Channel<T>` producer/consumer handoff, concurrent coroutine marshalling, O(1)
hash/binary-search indexing, artificial intelligence (AI)-assisted data enrichment and category classification, and
crosswalk construction between source and target taxonomies.

The delivery of this tooling unlocked new capabilities within the SaaS flagship product, which was reported on by Spend
Matters as value added to clients and was also used in negotiations to tender high-profile clients.

If you are interested in reading more or seeing examples of code, you can see more [here](README.md).
