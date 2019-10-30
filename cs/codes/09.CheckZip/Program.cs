using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CheckZip
{
    class Program
    {
        /// <summary>
        /// ZIPファイルに同じパスのファイルが複数含まれているか確認します.
        /// 戻り値:
        ///      0:チェックを実施し、同じパスのファイルが複数含まれていない事を確認しました.
        ///      1:チェックを実施し、同じパスのファイルが複数含まれていました.
        ///      2:チェックするファイルが指定されませんでした.
        ///      3:複数のファイルを指定したか、ファイルパスに半角スペースが含まれ、複数のファイルと判断され実行を中断しました.
        ///      4:指定されたするファイルが存在しませんでした.
        ///      5:指定されたファイルの読み込みでエラーが発生しました.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("チェックするファイルを指定してください");
                Environment.Exit(2);
                return;
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("チェックできるファイルは一つまでです.");
                Console.WriteLine("ファイルパスに半角スペースが含まれる場合、ダブルクォーテーション(\")で囲んでください");
                Environment.Exit(3);
                return;
            }

            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine("ファイルが見つかりませんでした.");
                Environment.Exit(4);
                return;
            }

            bool result;
            try
            {
                Dictionary<string, int> fileMap = new Dictionary<string, int>();

                using (ZipArchive zipArchive = ZipFile.OpenRead(filePath))
                {
                    result = zipArchive.Entries.GroupBy(i => i.FullName).Select(i => new { FilePath = i.Key, Count = i.Count() }).Where(i => i.Count > 1).Any();
                }
            }
            catch
            {
                Console.WriteLine("指定されたファイルの読み込みでエラーが発生しました.");
                Environment.Exit(4);
                return;
            }

            if (result)
            {
                Console.WriteLine("同じパスのファイルが複数含まれています.");
                Environment.Exit(1);
            }
            else
            {
                Environment.Exit(0);
            }

        }
    }
}
