using Ionic.Zip;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string directoryPathToCompress = @"C:\ProgramData\Klingelnberg\K-SmartProcessControl\119";
string outputDirectoryPath = @"C:\ProgramData\Klingelnberg\";

#region DotNetZip 

using (ZipFile zipFile = new ZipFile())
{
    //Get all filepath from folder
    String[] files = Directory.GetFiles(directoryPathToCompress);
    zipFile.CompressionMethod = CompressionMethod.BZip2;
    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

    foreach (string file in files)
    {
        zipFile.AddFile(file); //Adding files  
    }

    //Save the zip content in output stream
    zipFile.Save(outputDirectoryPath + "result2.bz2");
}

#endregion