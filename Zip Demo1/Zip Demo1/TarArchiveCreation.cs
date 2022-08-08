using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip_Demo1
{
    internal class TarArchiveCreation
    {
        private const int HighestCompressionLevel = 9; // highest compression

        public TarArchiveCreation()
        { }

        public void Archive(string dirName, string archiveName)
        {
            var outStream = File.Create(archiveName);
            var bzip2OutputStream = new BZip2OutputStream(outStream, HighestCompressionLevel);
            var archive = TarArchive.CreateOutputTarArchive(bzip2OutputStream, TarBuffer.DefaultBlockFactor);
            
            var filesInfo = GetFilesRecursively(dirName);
            int fileNo = 1;
            foreach (var fileInfo in filesInfo)
            {
                TarEntry entry = TarEntry.CreateEntryFromFile(fileInfo.FullName);
                archive.WriteEntry(entry, true);

                LogArchiveProgress(entry.Name, fileNo, filesInfo.Count);
                fileNo++;
            }
            archive.Close();
            bzip2OutputStream.Close();
        }

        private void LogArchiveProgress(string fileName, int currentFileNo, int totalFilesCount)
        {
            Console.WriteLine(fileName);
            Console.WriteLine($"{currentFileNo} / {totalFilesCount} - ({currentFileNo * 100 / totalFilesCount} %)");
        }

        private static void SetArchiveOptions(TarArchive archive)
        {
            if (archive != null)
            {                      // SET ARCHIVE OPTIONS
                //archive.SetKeepOldFiles(this.keepOldFiles);
                //archive.AsciiTranslate = this.asciiTranslate;

                //archive.SetUserInfo(this.userId, this.userName, this.groupId, this.groupName);
            }

        }

        private static List<FileInfo> GetFilesRecursively(string directoryNameToCompress)
        {
            List<FileInfo> totalFileInfos = new();
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryNameToCompress);
            foreach(var directory in directoryInfo.GetDirectories())
            {
                var fileInfos = GetFilesRecursively(directory.FullName).ToList();
                totalFileInfos.AddRange(fileInfos);
            }
            totalFileInfos.AddRange(directoryInfo.GetFiles().ToList());
            return totalFileInfos;
        }

        public void UnArchive(string archiveName, string outputDirectoryFullpath)
        {
            Stream inStream = File.OpenRead(archiveName);

            inStream = new BZip2InputStream(inStream);

            var archive = TarArchive.CreateInputTarArchive(inStream, TarBuffer.DefaultBlockFactor);

            archive.ExtractContents(outputDirectoryFullpath);
            archive.Close();
            inStream.Close();
        }
    }
}
