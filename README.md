# gRPC Examples Solutions

This repository contains three example solutions demonstrating gRPC communication in .NET across different frameworks and scenarios.

---

## 1. [HelloWorld Solution](HelloWorld/README.md)

A basic gRPC example for .NET, focusing on a simple request/response pattern.

**Projects:**
- **HelloWorldContracts** (.NET Standard 2.0)  
  Shared gRPC contract definitions (`.proto` files) for use by both server and client.
- **HelloWorldServer** (.NET 8)  
  gRPC HelloWorld service hosted in as a Worker Service (`BackgroundService`).
- **HelloWorldClient** (.NET 8)  
  Simple client demonstrating how to call the HelloWorld gRPC service.

**Purpose:**  
Demonstrates the fundamentals of gRPC service and client implementation in .NET 8.

---

## 2. [HelloWorldMixedClients Solution](HelloWorldMixedClients/README.md)

A cross-platform gRPC example with both modern and legacy .NET clients.

**Projects:**
- **HelloWorldContracts** (.NET Standard 2.0)  
  Shared gRPC contract definitions (`.proto` files).
- **HelloWorldServer** (.NET 8)  
  gRPC server hosted in a Worker Service.
- **Net48Client** (.NET Framework 4.8)  
  Legacy client demonstrating gRPC calls from .NET Framework.
- **Net8Client** (.NET 8)  
  Modern client demonstrating gRPC calls from .NET 8.

**Purpose:**  
Shows how to consume a gRPC service from both .NET Framework 4.8 and .NET 8 clients, highlighting cross-version compatibility.

---

## 3. [Streaming Solution](Streaming/README.md)

An advanced gRPC example focusing on server and/or client streaming.

**Projects:**
- **StreamingContracts** (.NET Standard 2.0)  
  Shared gRPC contract definitions for streaming scenarios.
- **StreamingServer** (.NET 8)  
  gRPC server implementing streaming methods (server, client, or bidirectional).
- **StreamingClient** (.NET 8)  
  Client demonstrating how to interact with streaming gRPC services.

**Purpose:**  
Demonstrates the use of streaming in gRPC, including sending and receiving multiple messages in a single call.

---

## How to Build and Run

1. **Open the desired solution** in Visual Studio 2022.
2. **Build all projects** in the solution.
3. **Start the server project** (e.g., `HelloWorldServer`, `StreamingServer`).
4. **Run one or more client projects** to test gRPC communication.

## Notes

- All solutions use shared contracts to ensure compatibility between servers and clients.
- The examples cover basic request/response, cross-version clients, and streaming scenarios in gRPC.

---

Feel free to expand this README with more details about each solution or project as needed.

