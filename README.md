
# Tax-Service-Adaptor

## Overview

The tax-service-adaptor is a highly adaptable middleware API for integrating Electronic Fiscal Devices (EFD) into financial systems, ideal for countries with EFD setups like Zambia. It features a generic `TaxService<T>` class and a unified command structure, showcasing flexibility and ease of use for various EFD interactions.

## features

- **Framework (`TaxService<T>`)**: Facilitates handling diverse EFD services, demonstrating versatility in application.
- **Unified Command Handling**: Simplifies interaction with different EFD-related operations.
- **Adaptable Interface (`ITalkToZRA`)**: Customized for ZRA, yet easily adaptable to other Revenue Authorities' systems.


### Implementation Example

```csharp
public class TalkToZRA<T> : TaxService<T>, ITalkToZRA where T : ServiceType
{
}

// Example usage
var taxService = new TalkToZRA<YourServiceType>(serviceType, httpService);
Executing a Generic Command
```

```csharp
var command = taxService.CreateCommand<RequestType, ResponseType>(commandType, requestData);

var response = await taxService.HandleCommand(command);
```

This approach illustrates the power and flexibility of the tax-service-adaptor, capable of efficiently managing various EFD tasks.

