rem gradle�p�̊��ݒ�
rem ���p�X�y�[�X������Ȃ��t�H���_�̕�����X�y����?
set GRADLE_USER_HOME=%~dp0home
set PATH=%GRADLE_USER_HOME%\wrapper\dists\gradle-5.6.2-bin\bin
gradle run --offline --stacktrace build 1> build.log 2>&1
