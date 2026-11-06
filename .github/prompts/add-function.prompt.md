---
mode: agent
description: Add a new validated HTTP function with tests
---
Add a new HTTP-triggered Azure Function named ${input:name}.
- Use .NET 8 isolated worker, async/await, nullable reference types.
- Validate and sanitize all input; return 400 on invalid input.
- Use parameterized SQL and read configuration from app settings.
- Add xUnit tests in tests/DemoFunctionApp.Tests.
- Build and run the tests when finished.
