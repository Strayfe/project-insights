# Bring Your Own Data (BYOD): Generic Data Import Platform

## Situation

The existing software as a service (SaaS) flagship product did not support user-managed data imports. \
A dedicated team handled all data imports manually, creating a scaling bottleneck and an ongoing operational cost. \
An initial proposal was raised to build a specific import feature for allowing users to upload their own Environmental,
Social, and Governance (ESG) sustainability data.

## Task

Implement a new feature in the system that would allow users to import ESG sustainability data via the user 
interface, following some loose guidelines around nomenclature, graphics, and location. \
Adhering to templates and existing functionality for consistency, implement a new user experience for the flagship
product that allows users to import their ESG data. \
Plan and implement the back-end architecture that will allow the system to store the data and inform the user of 
the outcome.

## Action

Proposed and implemented a **[generic data import platform](technical-findings.md)** rather than an ESG-specific 
one. \
With a focus on extensibility, users can upload any data into their system without the aid of the ETL team. \
With comparable effort to the original specification, the system was designed to handle import and management of 
data against any dimension in the existing **STAR schema**.

The implementation comprised:

- A **Windows service / Unix daemon** hosting the import processing engine
- **[Message queues](technical-findings.md#durable-job-dispatch-masstransit)** (RabbitMQ, Azure Service Bus via 
  MassTransit) for durable, decoupled job dispatch
- **[Azure Blob Storage](technical-findings.md#blob-storage-staging)** for staging uploaded files prior to 
  processing and log files of outcomes
- **[strategy resolver pattern](technical-findings.md#strategy-resolver-pattern)** to detect and route each job to 
  the appropriate processing pipeline
- **[Asynchronous concurrent channels](technical-findings.md#streaming-load-stages-channelt)** (`Channel<T>`) to 
  stream and transform data through the load stages
- A clean user-facing interface exposing the capability without exposing the underlying complexity
- A refactor to the underutilised **notifications system** to display the outcome of the background task to the 
  user

## Deep Dives

- **[Technical Findings](technical-findings.md)** — Architectural "guts," strategy resolver patterns, and 
  durable message queuing.
- **[Lessons Learned](lessons-learned.md)** — Strategic decisions, "ESG vs. Generic" thinking, and the silence of 
  background tasks.
- **[Examples](examples/README.md)** — Runnable demonstrations of the strategy resolver pattern.

## Result

- Delivered a new **end-to-end data import feature** that customers praised for its simplicity while 
  enabling something previously unavailable on the platform
- The generic design received immediate recognition: stakeholders requested extension to additional 
  data types beyond ESG—a request the forward-looking architecture could accommodate without 
  significant rework
- Reduced operational burden by removing the manual import process and its associated team overhead
