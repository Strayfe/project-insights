# Bring Your Own Data (BYOD): Generic Data Import Platform

The SaaS flagship product had no mechanism for user-managed data imports — all ingestion was handled 
manually by a dedicated team, creating a persistent operational bottleneck, and an initial brief 
scoped the solution narrowly to ESG sustainability data only.

The task was to implement a new import experience within the flagship product, complete with a 
back-end processing architecture capable of staging uploaded files, routing jobs to the correct 
pipeline, and surfacing outcomes to the user.

Rather than build the ESG-specific solution as scoped, I proposed and implemented a **generic data 
import platform** supporting any dimension in the existing STAR schema: a Windows service / Unix 
daemon hosting the processing engine, durable message queuing via RabbitMQ and Azure Service Bus 
(MassTransit), Azure Blob Storage for file staging and outcome logs, a strategy resolver for 
pipeline routing, asynchronous concurrent channels (`Channel<T>`) for streaming load stages, and a 
refactored notifications system to surface background task results to the user.

The feature was praised by customers for its simplicity while delivering something previously 
unavailable on the platform; stakeholders immediately requested extension to additional data types 
— a request the forward-looking architecture could accommodate without a significant rework — and 
the manual import process and its associated overhead were eliminated entirely.

If you are interested in reading more or seeing examples of code, you can see more [here](README.md).
