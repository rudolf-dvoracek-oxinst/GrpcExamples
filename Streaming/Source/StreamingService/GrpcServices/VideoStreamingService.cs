namespace StreamingService.GrpcServices;

using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using StreamingContracts;

public sealed class VideoStreamingService : VideoStreaming.VideoStreamingBase
{
    public override async Task StreamVideo(VideoRequest request, IServerStreamWriter<VideoChunk> responseStream, ServerCallContext context)
    {
        string videoPath = Path.Combine(AppContext.BaseDirectory, "Data", "Unity.mp4");
        try
        {
            using FileStream fileStream = File.OpenRead(videoPath);

            byte[] buffer = new byte[ushort.MaxValue*20];
            int bytesRead;
            while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                // Check for cancellation before writing
                if (context.CancellationToken.IsCancellationRequested)
                    break;

                try
                {
                    await responseStream.WriteAsync(new VideoChunk { Data = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead) }, context.CancellationToken);
                }
                catch (OperationCanceledException)
                {
                    // The client disconnected or canceled the request
                    Console.WriteLine("StreamVideo canceled by client (OperationCanceledException).");
                    return; // Exit the method immediately
                }
                catch (InvalidOperationException)
                {
                    // The client disconnected or the request is complete, stop streaming
                    return;
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Client canceled the request, log as info if needed
            Console.WriteLine("StreamVideo canceled by client (outer OperationCanceledException).");
        }
    }


}