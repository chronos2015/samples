rem gradle用の環境設定
rem 半角スペースが入らないフォルダの方が後々楽かも?
set GRADLE_USER_HOME=%~dp0home
set JAVA_HOME=C:\Program Files\Java\jdk1.8.0_211
set PATH=%GRADLE_USER_HOME%\wrapper\dists\gradle-5.6.2-bin\bin
gradle build --offline --stacktrace build 1> build.log 2>&1
