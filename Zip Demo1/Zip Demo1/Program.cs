﻿using ICSharpCode.SharpZipLib.BZip2;

class Program
{
    static void Main(string[] args)
    {
        string startPath = @"C:\ProgramData\Klingelnberg\K-SmartProcessControl\119";
        string zipPath = @"C:\Users\Shro_Bha\Desktop\Zip\Test Data\testResult.zip";
        string dataDir = @"C:\ProgramData\Klingelnberg\";


        DirectoryInfo directory1 = new DirectoryInfo(startPath);
        var filenames = directory1.GetFiles();
        FileInfo zipFileName = new FileInfo(dataDir + "archive1234.bz2");
        using (FileStream zipTargetAsStream = zipFileName.Create()) { }

        foreach (var file in filenames)
        {
            using (FileStream fileToBeZippedAsStream = file.OpenRead())
            {
                try
                {
                    var zipTargetAsStream = zipFileName.OpenWrite();
                    BZip2.Compress(fileToBeZippedAsStream, zipTargetAsStream, true, 4096);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        using (FileStream fileToDecompressAsStream = zipFileName.OpenRead())
        {
            using (var decompressedStream = File.Create(dataDir + "123.txt"))
            {
                try
                {
                    BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}