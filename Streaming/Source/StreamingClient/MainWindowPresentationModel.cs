namespace StreamingClient;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Grpc.Core;
using Grpc.Net.Client;
using StreamingContracts;

public class MainWindowPresentationModel : IMainWindowPresentationModel
{
    private const string grpcAddress = "http://localhost:5069";
    private readonly ObservableCollection<string> chunkLogs = new();

    public MainWindowPresentationModel()
    {
        this.ChunkLogs = new ReadOnlyObservableCollection<string>(this.chunkLogs);
        this.VideoFilePath = Path.Combine(Path.GetTempPath(), "streamed_video.mp4");
        _ = this.StartStreamingAsync();
    }

    /// <inheritdoc />
    public ReadOnlyObservableCollection<string> ChunkLogs { get; }

    /// <inheritdoc />
    public void Cleanup()
    {
        try
        {
            if (File.Exists(this.VideoFilePath))
            {
                File.Delete(this.VideoFilePath);
            }
        }
        catch
        {
            // Optionally log or ignore errors
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <inheritdoc />
    public string VideoFilePath { get; }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async Task StartStreamingAsync()
    {
        using var channel = GrpcChannel.ForAddress(grpcAddress);
        var client = new VideoStreaming.VideoStreamingClient(channel);
        using AsyncServerStreamingCall<VideoChunk> call = client.StreamVideo(new VideoRequest());

        using FileStream fs = File.Create(this.VideoFilePath);
        int chunkIndex = 0;
        while (await call.ResponseStream.MoveNext())
        {
            VideoChunk chunk = call.ResponseStream.Current;
            await fs.WriteAsync(chunk.Data.Memory);
            await fs.FlushAsync();

            await Application.Current.Dispatcher.InvokeAsync(() => 
            {
                this.chunkLogs.Add($"Chunk {++chunkIndex}: {chunk.Data.Length} bytes");
            });
            
        }

        this.OnPropertyChanged(nameof(this.VideoFilePath));
    }
}