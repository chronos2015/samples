REM VENVのパス
SET VENV_PATH=venv\Scripts\activate.bat

IF EXIST %VENV_PATH% (GOTO PIP_INSTALL) ELSE GOTO VENV_NONE

:VENV_NONE
REM VENVのパスがない場合は、VENVを作成する.
python -m venv venv
goto PIP_INSTALL

:PIP_INSTALL
REM 要求されたソフトをインストール
pip install -r requirements.txt
