# BYOD: Lessons Learned

Building a user-managed data import platform taught me the value of **forward-looking architecture** and the 
importance of saying "no" to narrow requirements when a generic solution provides 10x the value for 1.2x the 
effort.

## Strategic Refusal: Targeted Domain versus Generic

The original brief was purely about ESG (Environmental, Social, and Governance) sustainability data. However, as I 
looked at the system's **STAR schema**, I realised that if I built it for ESG, I'd be back here in three months 
rebuilding a similar workflow for any other data dimension.

**The Lesson:** An engineer looks beyond the immediate ask. By proposing a **generic data import 
system**, I delivered a platform that could handle *any* data dimension. This immediately eliminated the need 
for future rework when stakeholders inevitably asked for more data types. 

*Business Impact:* This reduced the **operational burden** on the ETL team, allowing them to focus on 
high-value data engineering rather than manual file uploads.

*Continued Impact:* The engineering involved with creating this solution will continue to provide value to the team
by enabling reduced modification of existing code and extending the system to support new data dimensions; A crucial 
element of SOLID principles is the Open/Closed principle.

## The Silence of Background Tasks

The initial implementation of background processing was technically sound but user-blind. Users would upload a 
file, and then... nothing. They had no idea if it succeeded, failed, or was currently on fire.

**The Fix:** I had to refactor the underutilised **notifications system** to provide real-time updates on 
background task outcomes. 

*Takeaway:* A feature doesn't exist for the user until they get feedback. When moving from synchronous web 
requests to asynchronous **durable message queues** (see [Technical Findings](technical-findings.md#durable-job-dispatch-masstransit)), surfacing status becomes a 
first-class citizen in the architecture.

## Extensibility is Not a "Nice to Have"

By using a **strategy resolver pattern**, I made the system "poka-yoke" (mistake-proof) for other developers. 

- **Benefit:** When a new requirement for data imports arises, a developer can quickly add support for it. 
  They don't need to understand the entire system or modify existing code; they just implement a new strategy and register it.
- **Poka-yoke:** Because the core engine was already battle-tested, the new strategy inherited all the stability, 
  logging, and error handling for free.

---

If you're interested in the "guts" of the system, check out the [Technical Findings](technical-findings.md).
