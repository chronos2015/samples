using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibGit2Sharp;

namespace git
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.リポジトリ作成
            Repository.Init("repos");

            // 2.リポジトリのクローン
            //Repository.Clone("元のURL", "先のURL");

            // 3.リポジトリを開く
            var repository =  new Repository("repos");

            // ファイル作成
            using (StreamWriter sw = File.CreateText("repos\\test.txt"))
            {
                sw.WriteLine("Sample");
            }

            // 4.ファイルを追加
            repository.Index.Add("test.txt");

            // 5.コミット
            var signature = new Signature("test", "mail@address", DateTimeOffset.Now);
            repository.Commit("commit done", signature, signature);

            // 6.プッシュ
            //repository.Network.Push(repository.Head);

            // 7.リセット
            //repository.Reset(ResetMode.Hard, repository.Head.Commits.First());

            // 8.ブランチ作成
            repository.CreateBranch("dev");

            // 9.ブランチ削除
            repository.Branches.Remove("dev");

            // 10.タグ作成
            repository.ApplyTag("tag1");

            // 11.タグ削除
            repository.Tags.Remove("tag1");

        }
    }
}
