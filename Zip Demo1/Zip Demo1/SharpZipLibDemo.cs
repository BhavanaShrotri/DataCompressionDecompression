using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using Zip_Demo1;

class SharpZipLibDemo
{
    static void Main(string[] args)
    {
        string directoryPathToCompress = @"C:\ProgramData\Klingelnberg\K-SmartProcessControl";
        string outputDirectoryPath = @"C:\ProgramData\Klingelnberg\";
        
        TarArchiveCreation tarArchiveCreation = new();
        string tarFilePath = Path.Combine(outputDirectoryPath, "sharpzip.tar");
        tarArchiveCreation.Archive(directoryPathToCompress, tarFilePath);

        tarArchiveCreation.UnArchive(tarFilePath, Path.Combine(outputDirectoryPath, "sharpzip_extracted"));
    }
}