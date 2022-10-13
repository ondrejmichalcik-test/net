# net-scaffolder

## Generated Swagger URL

```
https://localhost:5001/swagger/index.html
```

## Deployment configuration

All files located in `./codenow/config` directory will be deployed alongside the application and available in `/codenow/config` directory

## Curl

```
curl -X GET "https://localhost:5001/HelloWorld" -H  "accept: text/plain"
```

## Application configuration

- can be done in `./codenow/config/appsettings.json`
- and can be overridden in `./codenow/config/appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json` for development or debugging

## Logging

- can be configured in `./codenow/config/appsettings.json` (linked to the solution) or in `./codenow/config/appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json`

| Level name      | Description                                                                                                                                                                                          |
| --------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **None**        | Not used for writing log messages. Specifies that a logging category should not write any messages.                                                                                                  |
| **Critical**    | Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.                                                                        |
| **Error**       | Logs that highlight when the current flow of execution is stopped due to a failure. These should indicate a failure in the current activity, not an application-wide failure.                        |
| **Warning**     | Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.                                                           |
| **Information** | Logs that track the general flow of the application. These logs should have long-term value.                                                                                                         |
| **Trace**       | Logs that contain the most detailed messages. These messages may contain sensitive application data. These messages are disabled by default and should never be enabled in a production environment. |

**Example**

```
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Net.Scaffolder.Controllers.HelloWorldController": "Trace",
    }
  }
```

## Environment variables usage in configuration file

- `${CODENOW_EXAMPLE_ENVIRONMENT_VARIABLE}` will be replaced with `CODENOW_EXAMPLE_ENVIRONMENT_VARIABLE` env variable value

**Example**

```
  "CodeNowHelloWorldExampleUsageOfEnvVar": "${CODENOW_EXAMPLE_ENVIRONMENT_VARIABLE}"
```
