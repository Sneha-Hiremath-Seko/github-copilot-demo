# Copilot & GitHub Security Demo – Azure Function App

C# (.NET 8 isolated) Azure Functions app for demoing GitHub Copilot and GitHub Advanced Security.

## Run locally
```
cd src/DemoFunctionApp
func start
```

## Endpoints
- `GET /api/Hello?name=x` – clean sample
- `GET /api/GetUser?id=1` – SQL injection (intentional)
- `GET /api/ReadFile?file=x` – path traversal (intentional)

## Demo script

### Copilot
1. **Chat/Explain**: select `HelloFunction.cs` → "Explain this".
2. **Fix**: open `VulnerableFunctions.cs` → "Fix the SQL injection and hardcoded credential".
3. **Agent mode**: "Add a POST `CreateUser` function with validation and unit tests (xUnit)".
4. **Tests/Docs**: `/tests`, `/doc` on any function.
5. **Custom instructions**: see `.github/copilot-instructions.md`.
6. **PR summary / code review** with Copilot on a pull request.

### GitHub security
1. Push to GitHub, enable **Code scanning** (workflow in `.github/workflows/codeql.yml`).
2. Review alerts under Security → Code scanning (SQL injection, path injection, hardcoded credentials, weak hashing).
3. Use **Copilot Autofix** on an alert to generate a fix PR.
4. **Dependabot** (`dependabot.yml`) and **secret scanning** / push protection.

> The vulnerabilities are intentional. Do not use this code in production.
