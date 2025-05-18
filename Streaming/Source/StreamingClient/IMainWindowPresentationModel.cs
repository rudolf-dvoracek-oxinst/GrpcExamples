namespace StreamingClient;

using System.Collections.ObjectModel;
using System.ComponentModel;

public interface IMainWindowPresentationModel : INotifyPropertyChanged
{
    /// <summary>
    /// Event that is raised when a property changes.
    /// </summary>
    event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets individual chunk logs.
    /// </summary>
    public ReadOnlyObservableCollection<string> ChunkLogs { get; }

    /// <summary>
    /// Gets video file path.
    /// </summary>
    public string VideoFilePath { get; }

    /// <summary>
    /// Cleans up resources when the window is closed.
    /// </summary>
    void Cleanup();
}