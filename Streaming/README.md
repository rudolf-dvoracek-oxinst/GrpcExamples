# Streaming Solution

This solution consists of several .NET 8 projects that together implement a streaming application using gRPC and a client-server architecture. Below is an overview of each project and its main components.

## Projects Overview

### 1. StreamingClient

A WPF client application that connects to the streaming service and provides a user interface for interacting with the video stream.

**Key files:**
- `App.xaml` / `App.xaml.cs`: Application entry point and configuration.
- `MainWindow.xaml` / `MainWindow.xaml.cs`: Main window UI and logic.
- `IMainWindowPresentationModel.cs` / `MainWindowPresentationModel.cs`: Presentation model for the main window, following MVVM principles.

### 2. StreamingService

A .NET Worker Service that hosts the gRPC server for video streaming.

**Key files:**
- `GrpcServices/VideoStreamingService.cs`: Implementation of the gRPC video streaming service.
- `StreamingBackgroundService.cs`: Background service that manages server lifecycle.

### 3. StreamingContracts

A shared project containing protocol definitions and generated code for gRPC communication.

**Key files:**
- `protos/streaming.proto`: Protocol Buffers definition for streaming messages and services.

## How It Works

- The **StreamingService** project runs a gRPC server that streams video data.
- The **StreamingClient** project connects to the server and displays the streamed content in a WPF interface.
- The **StreamingContracts** project defines the communication protocol and is referenced by both the client and the service.

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or later

## Getting Started

1. Build the solution in Visual Studio.
2. Start the `StreamingService` project to launch the gRPC server.
3. Run the `StreamingClient` project to connect and interact with the streaming service.

---

This solution demonstrates a modular approach to building a streaming application using modern .NET technologies and gRPC.
