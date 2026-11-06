---
mode: agent
description: Fix intentional security issues in the demo
---
Review src/DemoFunctionApp/Functions/VulnerableFunctions.cs and fix all security issues:
SQL injection, path traversal, hardcoded credentials and weak hashing.
Follow .github/copilot-instructions.md, keep behavior otherwise unchanged, then build and run tests.
