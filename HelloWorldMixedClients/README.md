# HelloWorldMixedClients Solution

This solution demonstrates a simple gRPC-based Hello World application implemented across multiple .NET targets. It includes a gRPC server, shared contracts, and clients for both .NET Framework 4.8 and .NET 8.

## Projects Overview

### 1. HelloWorldContracts

- **Target Framework:** .NET Standard 2.0
- **Contents:**  
  Contains shared gRPC contract definitions in Protocol Buffers (`.proto` files).  
  - `Protos/hello.proto`: Defines the `HelloWorld` gRPC service with two RPC methods (`SayHello` and `JustSayHello`), as well as the request and response message types.
- **Purpose:**  
  Provides a common contract for both server and client implementations, ensuring consistent message and service definitions.

### 2. HelloWorldServer

- **Target Framework:** .NET 8
- **Contents:**  
  Implements the gRPC server using a Worker Service (`BackgroundService`).
  - `Program.cs`: Entry point for the server application.
  - `HelloWorldBackgroundService.cs`: Hosts the gRPC server as a background service.
  - `GrpcServices/HelloWorldService.cs`: Implements the `HelloWorld` gRPC service logic.
- **Purpose:**  
  Hosts the gRPC service, handling incoming requests and sending responses according to the contract.

### 3. Net48Client

- **Target Framework:** .NET Framework 4.8
- **Contents:**  
  Implements a gRPC client targeting .NET Framework 4.8.
  - `Program.cs`: Contains client logic to connect to the gRPC server and call the defined service methods.
- **Purpose:**  
  Demonstrates how to consume the gRPC service from a legacy .NET Framework application.

### 4. Net8Client

- **Target Framework:** .NET 8
- **Contents:**  
  Implements a gRPC client targeting .NET 8.
  - `Program.cs`: Contains client logic to connect to the gRPC server and call the defined service methods.
- **Purpose:**  
  Demonstrates how to consume the gRPC service from a modern .NET application.

## How to Build and Run

1. **Build the Solution:**  
   Open the solution in Visual Studio 2022 and build all projects.

2. **Run the Server:**  
   Start the `HelloWorldServer` project to launch the gRPC server.

3. **Run a Client:**  
   Start either the `Net48Client` or `Net8Client` project to connect to the server and invoke the service methods.

## Notes

- The shared contracts project (`HelloWorldContracts`) must be referenced by both the server and client projects.
- The solution demonstrates cross-version compatibility for gRPC services in .NET.

