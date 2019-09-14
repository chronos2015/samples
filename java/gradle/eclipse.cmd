rem gradle用の環境設定
rem 半角スペースが入らないフォルダの方が後々楽かも?
set GRADLE_USER_HOME=%~dp0home
set PATH=%GRADLE_USER_HOME%\wrapper\dists\gradle-5.6.2-bin\bin
gradle eclipse --offline --stacktrace build 1> build.log 2>&1
