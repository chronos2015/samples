using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace zip
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.フォルダをそのままZIPファイルに圧縮する場合
            ZipFile.CreateFromDirectory("target", "target1.zip", CompressionLevel.Optimal, true, Encoding.UTF8);

            // 2.ファイルを指定して、又はメモリ上のストリームなどを圧縮する場合
            using (ZipArchive zip = ZipFile.Open("target2.zip", ZipArchiveMode.Create))
            {
                ZipArchiveEntry entry = zip.CreateEntry("English.txt", CompressionLevel.Optimal);
                using (var stream = entry.Open())
                {
                    byte[] data = readFile("target\\English.txt");
                    stream.Write(data, 0, data.Length);
                }

                entry = zip.CreateEntry("日本語.txt", CompressionLevel.Optimal);
                using (var stream = entry.Open())
                {
                    byte[] data = readFile("target\\日本語.txt");
                    stream.Write(data, 0, data.Length);
                }
            }

            // 3.ファイルをそのままファイルシステムに展開する場合
            ZipFile.ExtractToDirectory("target1.zip", "target1");

            // 4.特定のファイルのみ展開する場合
            using (ZipArchive archive = ZipFile.OpenRead("target2.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // 4.1.ファイルに展開
                    if(entry.FullName == "日本語.txt")
                    {
                        entry.ExtractToFile("日本語.txt");
                    }

                    // 4.2.メモリ上に展開
                    if (entry.FullName == "English.txt")
                    {
                        using (Stream stream = entry.Open())
                        {
                            byte[] data = new byte[1000];
                            stream.Read(data, 0, 1000);
                            Console.WriteLine(Encoding.UTF8.GetString(data));
                        }
                    }
                }
            }
        }

        static byte[] readFile(string filePath)
        {
            using(var stream = File.Open(filePath, FileMode.Open))
            {
                byte[] ret = new byte[stream.Length];
                stream.Read(ret, 0, ret.Length);
                return ret;
            }
        }
    }
}
