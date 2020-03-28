using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using RuTrackerDBLoader.Data;
using FirebirdSql.Data.Client;

namespace RuTrackerDBLoader.Reader
{
    public class DataReder
    {
        private const string errDir = "errBlocks";

        public delegate void Progress(int Progress);
        public static event Progress OnProgressEvent;

        public delegate void OnError(int ErrorBlocks);
        public static event OnError OnErrorEvent;

        public delegate void OnComplite(int TotalBlocks);
        public static event OnComplite OnCompliteEvent;

        private static async Task AddFiles(int dirID, FBDataSet dataSet, dirFile[] files, int torrentID)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    dataSet.Query = "insert into FILES(DIR_ID, FILE_NAME, FILE_SIZE, TORRENT_ID) " +
                        "values(@DIR_ID, @FILE_NAME, @FILE_SIZE, @TORRENT_ID)";
                    dataSet.Parameters.Clear();
                    if (dirID == -1)
                        dataSet.Parameters.AddWithValue("DIR_ID", DBNull.Value);
                    else
                        dataSet.Parameters.AddWithValue("DIR_ID", dirID);
                    dataSet.Parameters.AddWithValue("FILE_NAME", file.name);
                    dataSet.Parameters.AddWithValue("FILE_SIZE", file.size);
                    if (torrentID == -1)
                        dataSet.Parameters.AddWithValue("TORRENT_ID", DBNull.Value);
                    else
                        dataSet.Parameters.AddWithValue("TORRENT_ID", torrentID);
                    await dataSet.ExecQueryNoDataAsync();
                }
            }
        }

        private static async Task AddDirs(int torrentID, FBDataSet dataSet, dir[] dirs, int parentDirID)
        {
            if (dirs != null)
            {
                foreach (var dir in dirs)
                {
                    dataSet.Query = "insert into DIRECTORYES(TORRENT_ID, PARENT_ID, DIR_NAME) " +
                        "values(@TORRENT_ID, @PARENT_ID, @DIR_NAME) " +
                        "returning DIR_ID";
                    dataSet.Parameters.Clear();
                    dataSet.Parameters.AddWithValue("TORRENT_ID", torrentID);
                    dataSet.Parameters.AddWithValue("DIR_NAME", dir.name);
                    if (parentDirID == -1)
                        dataSet.Parameters.AddWithValue("PARENT_ID", DBNull.Value);
                    else
                        dataSet.Parameters.AddWithValue("PARENT_ID", parentDirID);
                    await dataSet.ExecuteQueryAsync();

                    if (await dataSet.Reader.ReadAsync())
                    {
                        int dirId = Convert.ToInt32(dataSet.Reader["DIR_ID"]);

                        await AddFiles(dirId, dataSet, dir.file, torrentID);

                        if (dir.subDirs != null)
                        {
                            await AddDirs(torrentID, dataSet, dir.subDirs, dirId);
                        }
                    }
                }
            }
        }

        public static async Task Load(string FileName, string Database, string Sysdba, string Password, string Codepage, bool TestMode, bool UpdateMode, bool NoContent, bool NoFileList, bool NotDeleted, CancellationTokenSource cancellationTokenSource)
        {
            using (var xmlBlockReader = new XMLBlockReader(FileName))
            {
                using (var fbData = new FBDataSet(Database, Sysdba, Password, Codepage))
                {
                    int blockNum = 0;
                    int errBlocks = 0;
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(torrents));
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            using (var block = await xmlBlockReader.NextBlock(cancellationTokenSource))
                            {
                                try
                                {
                                    var torrents = (torrents)xmlSerializer.Deserialize(block);
                                    blockNum++;
                                    OnProgressEvent?.Invoke(blockNum);

                                    foreach (var torrentItem in torrents.Items.Select(t => (torrent)t))
                                    {
                                        if (!TestMode)
                                        {
                                            try
                                            {
                                                ulong.TryParse(torrentItem.size, out ulong torrentSize);

                                                if (!fbData.IsInTransaction())
                                                    await fbData.StartTransactionAsync();

                                                bool write = true;
                                                if (NotDeleted && torrentItem.del != null)
                                                {
                                                    write = false;
                                                }

                                                if (write)
                                                {
                                                    fbData.Parameters.Clear();

                                                    bool add = true;
                                                    if (UpdateMode)
                                                    {
                                                        fbData.Query = "select * from TORRENTS where TOPIC_ID = @TOPIC_ID";
                                                        fbData.Parameters.AddWithValue("TOPIC_ID", torrentItem.id);
                                                        await fbData.ExecuteQueryAsync();

                                                        if (fbData.Reader.HasRows)
                                                        {
                                                            add = false;
                                                        }
                                                    }

                                                    if (add)
                                                    {
                                                        fbData.Query = "insert into TORRENTS(TOPIC_ID, REGISTRED_AT, TOTAL_SIZE, TOPIC_TITLE, DATA_HASH, " +
                                                                "TRACKER_ID, FORUM_ID, DEL) " +
                                                                "values(@TOPIC_ID, @REGISTRED_AT, @TOTAL_SIZE, @TOPIC_TITLE, @DATA_HASH, " +
                                                                "@TRACKER_ID, @FORUM_ID, @DEL) " +
                                                                "returning ID";
                                                        fbData.Parameters.AddWithValue("TOPIC_ID", torrentItem.id);
                                                        if (DateTime.TryParse(torrentItem.registred_at, out DateTime registred_at))
                                                        {
                                                            fbData.Parameters.AddWithValue("REGISTRED_AT", registred_at);
                                                        }
                                                        else
                                                        {
                                                            fbData.Parameters.AddWithValue("REGISTRED_AT", DBNull.Value);
                                                        }
                                                        fbData.Parameters.AddWithValue("TOTAL_SIZE", torrentSize);
                                                        fbData.Parameters.AddWithValue("TOPIC_TITLE", torrentItem.title);
                                                        fbData.Parameters.AddWithValue("DATA_HASH", torrentItem.torrentInfo.hash);
                                                        fbData.Parameters.AddWithValue("TRACKER_ID", torrentItem.torrentInfo.tracker_id);
                                                        fbData.Parameters.AddWithValue("FORUM_ID", torrentItem.forum.id);
                                                        fbData.Parameters.AddWithValue("DEL", torrentItem.del != null);
                                                        await fbData.ExecuteQueryAsync();

                                                        if (!NoContent && !NoFileList)
                                                        {
                                                            if (await fbData.Reader.ReadAsync())
                                                            {
                                                                int id = Convert.ToInt32(fbData.Reader["id"]);

                                                                if (!NoContent)
                                                                {
                                                                    fbData.Query = "insert into TOPICS_CONTENT(CONTENT_TOPIC_ID, TOPIC_CONTENT) " +
                                                                        "values(@CONTENT_TOPIC_ID, @TOPIC_CONTENT)";
                                                                    fbData.Parameters.Clear();
                                                                    fbData.Parameters.AddWithValue("CONTENT_TOPIC_ID", id);
                                                                    fbData.Parameters.AddWithValue("TOPIC_CONTENT", torrentItem.content);
                                                                    await fbData.ExecuteQueryAsync();
                                                                }

                                                                if (!NoFileList)
                                                                {
                                                                    await AddDirs(id, fbData, torrentItem.dir, -1);
                                                                    await AddFiles(-1, fbData, torrentItem.file, id);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (blockNum % 500 == 0)
                                                        fbData.Commit();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                fbData.RollBack();
                                                throw ex;
                                            }
                                        }
                                        else
                                        {
                                            var dt = DateTime.Parse(torrentItem.registred_at);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errBlocks++;
                                    OnErrorEvent?.Invoke(errBlocks);
                                    if (!Directory.Exists(errDir))
                                    {
                                        Directory.CreateDirectory(errDir);
                                    }
                                    string fileName = $"block_{ blockNum }";
                                    await WriteBinaryFile(errDir + Path.DirectorySeparatorChar + fileName + ".xml", block.GetBuffer());
                                    File.WriteAllText(errDir + Path.DirectorySeparatorChar + fileName + ".txt", ex.Message);
                                }
                            }
                        }
                        catch (OperationCanceledException ex)
                        {
                            if (fbData.IsInTransaction())
                                fbData.Commit();

                            OnCompliteEvent?.Invoke(blockNum);
                        }
                    }
                }
            }
        }

        public static async Task<int> GetCount(string FileName, CancellationTokenSource cancellationTokenSource)
        {
            int blockNum = 0;
            using (var xmlBlockReader = new XMLBlockReader(FileName))
            {
                blockNum = await xmlBlockReader.GetBlocksCount(cancellationTokenSource);
            }
            return blockNum;
        }

        private static async Task WriteBinaryFile(string fileName, byte[] data)
        {
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                await fileStream.WriteAsync(data, 0, data.Length);
                fileStream.Close();
            }
        }

        public static async Task ClearDB(string Database, string Sysdba, string Password, string Codepage)
        {
            using (var fbData = new FBDataSet(Database, Sysdba, Password, Codepage))
            {
                try
                {
                    await fbData.StartTransactionAsync();
                    fbData.Query = "delete from TORRENTS";
                    await fbData.ExecQueryNoDataAsync();
                    fbData.Commit();
                }
                catch
                {
                    fbData.RollBack();
                }
            }
        }
    }
}
