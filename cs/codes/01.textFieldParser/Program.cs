using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textFieldParser
{
    class Program
    {
        static IEnumerable<string[]> ReadCsvFile(string filePath)
        {
            using (TextFieldParser p = new TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS")))
            {
                #region 区切り文字による分割の場合
                // 区切り文字による分割の場合、以下を使用
                p.TextFieldType = FieldType.Delimited;

                // 区切り文字による分割の場合、区切り文字を指定。
                // CSVの場合はカンマ(,)、TSVの場合は、タブ(\t)など
                // ※複数ある場合は、複数指定できる
                p.SetDelimiters(",");

                // フィールドが引用符で囲まれているかを指定
                p.HasFieldsEnclosedInQuotes = true;

                // フィールドの前後のスペースをトリムするか指定
                // ※trueの場合、全角スペースと半角スペースの両方がトリムされる。
                p.TrimWhiteSpace = true;
                #endregion

                #region 固定長の場合
                // 固定長の場合、以下を使用
                //p.TextFieldType = FieldType.FixedWidth;

                // 固定幅の場合、フィールドの幅(バイト数)を指定
                // -1を指定する事で可変幅の宣言も可能
                // ※試してはいないが、最終行のみと思われる。
                //p.SetFieldWidths(2, 4, 1, 4, 9, 9, -1);
                #endregion

                // 先頭が指定の文字で始まる場合コメントと見なす場合指定する
                //p.CommentTokens = new string[] { "//" };

                while(!p.EndOfData)
                {
                    yield return p.ReadFields();
                }
            }
        }


        static void Main(string[] args)
        {
            foreach(string[] line in ReadCsvFile("c01.csv"))
            {
                Console.WriteLine(string.Join(",", line));
            }
        }
    }
}

/*
 * 本プロジェクトに添付されたCSVファイルは、以下よりダウンロードしたものです。
 * CSVファイルのサンプル以外として利用する場合、
 * 再度ダウンロードされることをお勧めします。
 * 出典：政府統計の総合窓口(e-Stat)（https://www.e-stat.go.jp/）
 */
