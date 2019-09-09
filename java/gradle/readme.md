# gradleによるオフラインビルド

## はじめに

gradle自身のバイナリが大きいため、リポジトリから除外しています。

その為、下記のフォルダに解凍していただく必要がございます。

[GITのルートフォルダ]\java\gradle\home\wrapper\dists\gradle-5.6.2-bin

フォルダ構成

- gradle-5.6.2-bin
  - bin
    - (省略)
  - init.d
    - (省略)
  - lib
    - (省略)
  - media
    - (省略)
  - getting-started.html
  - LICENSE
  - NOTICE

## ビルドについて

同梱のbuild.cmdを実行する事でビルドが行えます。
尚、javaのプロセスが残ってしまう事があります。

## 終わりに

何かお気づきの点がございましたら

githubのissue又はブログのコメント等でお知らせいただけると幸いです。
