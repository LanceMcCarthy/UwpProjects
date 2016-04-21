using System;

namespace UwpHelpers.Controls.Common
{
    // Note: There is a DownloadProgressEventArgs available in Windows.UI.Xaml.Media.Imaging.DownloadProgressEventArgs, 
    // but it's sealed and we need more into than just (double)Progress that is available in DownloadProgressEventArgs

    /// <summary>
    /// To be used with Progress for DownloadStreamWithProgressAsync
    /// </summary>
    public class DownloadProgressArgs : EventArgs
    {
        public DownloadProgressArgs(int bytesReceived, int totalBytes)
        {
            BytesReceived = bytesReceived;
            TotalBytes = totalBytes;
        }

        public double TotalBytes { get; }

        public double BytesReceived { get; }

        public double PercentComplete => 100 * ((double) BytesReceived / TotalBytes);
    }
}
