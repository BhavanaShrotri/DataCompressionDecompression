using SevenZip;
using System;
using System.IO;

namespace SevenZipSharp
{
    public class Program
    {

        static void Main(string[] args)
        {
            string _7ZipDllPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName) + @"\7-Zip\7za.dll";
            SevenZipExtractor.SetLibraryPath(_7ZipDllPath);
            SevenZipCompressor.SetLibraryPath(_7ZipDllPath);

            string startPath = @"C:\ProgramData\Klingelnberg\K-SmartProcessControl-test\119";
            string zipPath = @"C:\ProgramData\Klingelnberg\Demoultra.7z";

            var tmp = new SevenZipCompressor();
            tmp.ScanOnlyWritable = true;
            tmp.CompressionMode = CompressionMode.Create;
            tmp.CompressionMethod = CompressionMethod.Lzma2;
            tmp.CompressionLevel = CompressionLevel.Ultra;
            tmp.CompressDirectory(startPath, zipPath);
        }
    }
}
