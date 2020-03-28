using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace RuTrackerDBLoader
{
    public class XMLBlockReader : IDisposable
    {
        private const string BeginLineMarker = "<torrent id=\"";
        private const string EndLineMarker = "</torrent>";
        private const string Header = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
        private const string Header2 = "<torrents>";
        private const string Futer = "</torrents>";

        private StreamReader textReader;

        public XMLBlockReader(string xmlFile)
        {
            textReader = new StreamReader(xmlFile);
        }

        private async Task<string> readLine(CancellationTokenSource cancellationToken)
        {
            var result = await textReader.ReadLineAsync();
            if (textReader.EndOfStream)
            {
                cancellationToken.Cancel();
                throw new OperationCanceledException();
            }
            return result;
        }

        public async Task<MemoryStream> NextBlock(CancellationTokenSource cancellationToken)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.AutoFlush = false;

            var line = await readLine(cancellationToken);
            while (line != null && !line.StartsWith(BeginLineMarker, StringComparison.InvariantCultureIgnoreCase))
            {
                line = await readLine(cancellationToken);
            }

            await writer.WriteLineAsync(Header);
            await writer.WriteLineAsync(Header2);

            while (line != null && !line.Equals(EndLineMarker, StringComparison.InvariantCultureIgnoreCase))
            {
                await writer.WriteLineAsync(line);
                line = await readLine(cancellationToken);
            }
            await writer.WriteLineAsync(line);
            await writer.WriteLineAsync(Futer);

            await writer.FlushAsync();
            stream.Position = 0;

            return stream;
        }

        public async Task<int> GetBlocksCount(CancellationTokenSource cancellationToken)
        {
            int blocks = 0;
            while (!textReader.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                var line = await textReader.ReadLineAsync();
                if (line.StartsWith(BeginLineMarker, StringComparison.InvariantCultureIgnoreCase))
                {
                    while (!textReader.EndOfStream && line != null)
                    {
                        line = await textReader.ReadLineAsync();

                        if (line.Equals(EndLineMarker, StringComparison.InvariantCultureIgnoreCase))
                        {
                            blocks++;
                            break;
                        }
                    }
                }
            }
            return blocks;
        }

        public void Dispose()
        {
            if (textReader != null)
                textReader.Dispose();
        }
    }
}
