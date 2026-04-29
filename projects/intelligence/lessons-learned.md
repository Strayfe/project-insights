# Intelligence: Lessons Learned

Building a large-scale anonymisation pipeline taught me that technical excellence is only half the battle. The other 
half is managing expectations and ensuring the system doesn't become a "black box" that nobody trusts.

## From Fuzzy Searching to Vector Embeddings

Initially, I leaned on traditional fuzzy searching (Levenshtein distance) for dimension lookups. It’s a classic 
engineering fallback, but it quickly became a scaling bottleneck. At 100 million rows, the O(n*m) complexity of 
string distance algorithms is where performance goes to die.

**The Pivot:** I replaced fuzzy matching with **Vector Embeddings** and a **Vector Database**. Instead of brute-forcing 
string comparisons, I generated high-dimensional vectors for my taxonomies. This turned a "fuzzy" problem into a 
high-performance nearest-neighbour search.

*Lesson for readers:* Algorithms that worked on your laptop with 1,000 rows will fail you in production 
at scale. Embeddings aren't just for "chatbots"; they are a powerful tool for high-performance semantic retrieval.

## The Evaluation Agent: AI Sanity Checks

One of my biggest realisations was that **Prompt Engineering** and **SemanticKernel** only get you 80% of the way 
there. LLMs are non-deterministic by nature, and in a data pipeline, "mostly correct" is just another way of saying 
"corrupted data".

**The Fix:** I implemented an **Evaluation Agent** pattern. Instead of trusting the first AI response, I piped the 
result into a secondary, more constrained agent whose sole job was to sanity-check the decision against business 
rules and domain validation.

- **Heuristic Validation:** If the primary agent categorised a "Drill Bit" as "Office Supplies," the Evaluation 
  Agent would flag the anomaly based on the vector distance to known hardware categories.
- **Operational Burden:** While this added a slight latency penalty, it drastically reduced the manual QA 
  requirement, making the system truly autonomous.

## Local Hardware vs. "The Cloud"

In modern engineering, the default is to "throw it in a function app." But for this project, the **high concurrency** and 
massive I/O meant that local developer workstations (with NVMe drives and many CPU cores) were actually more 
**cost-effective** and faster for the initial development and ingestion runs.

- **Scaling Bottleneck:** The cloud-native version was throttled by network latency between the ingestion service and 
  the data warehouse.
- **Action:** I optimised the CLI to leverage local coroutine marshalling, which allowed me to process hundreds of 
  millions of rows in a fraction of the time it would have taken in a standard-tier cloud environment.

This reflects an ethos I hold close: pick the right tool for the job. 
Microservices only make sense when you genuinely need independent deployability, independent scalability, or clear 
service boundaries. Without those constraints, a monolith is usually cheaper, simpler, and easier to reason about.

The same principle applied here. Do not over-engineer the solution by default. If a focused CLI or TUI solves the problem,
build that — then spend the saved complexity budget on reliability, observability, and making the system safe to rerun.

## The Power of Idempotency

When you're running a job that takes hours, something *will* fail. A network blip, a database lock, or a cat walking 
across a keyboard.

I built the tool to be **idempotent by design**. If the process crashed at record 50,000,000, I could restart it, and 
it would either skip existing records or safely overwrite them without corrupting the warehouse.

*Pro-tip:* If your ingestion tool isn't idempotent, you're just one "oops" away from a long weekend of data cleanup.

## Future Proofing: Strategic Thinking

I knew requirements would change. Today it's anonymous benchmarking; tomorrow it's trend analysis. By using a 
**[streaming architecture](technical-findings.md#streaming-architecture)** and a decoupled 
**[strategy resolver pattern](technical-findings.md#strategy-resolver-pattern)** (see 
[Technical Findings](technical-findings.md)), I ensured that I could swap out the transformation logic or add 
new data sources without a complete rewrite.

---

If you're interested in the low-level implementation, dive into the [Technical Findings](technical-findings.md).
