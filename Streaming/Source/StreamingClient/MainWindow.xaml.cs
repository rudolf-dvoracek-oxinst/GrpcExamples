namespace StreamingClient
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMainWindowPresentationModel model;

        public MainWindow(IMainWindowPresentationModel model)
        {
            InitializeComponent();
            this.DataContext = this.model = model;
            // Subscribe to the CollectionChanged event of the underlying ObservableCollection
            if (this.model.ChunkLogs is INotifyCollectionChanged notifyCollection)
            {
                notifyCollection.CollectionChanged += ChunkLogs_CollectionChanged;
            }
            this.Closed += MainWindow_Closed;

            // Subscribe to property changes
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.model.VideoFilePath))
            {
                // Stop and restart the MediaElement
                this.mediaElement.Source = new Uri(this.model.VideoFilePath, UriKind.RelativeOrAbsolute);
                this.mediaElement.Play();
            }
        }

        private void ChunkLogs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && sender is ReadOnlyObservableCollection<string> observableCollection)
            {
                this.ChunkListBox.SelectedIndex = observableCollection.Count - 1;
            }

            // Also scroll the ScrollViewer to the bottom
            this.ChunkScrollViewer?.ScrollToEnd();
        }

        private void MainWindow_Closed(object? sender, System.EventArgs e)
        {
            this.model.Cleanup();
        }
    }
}