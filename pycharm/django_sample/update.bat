REM VENV�̃p�X
SET VENV_PATH=venv\Scripts\activate.bat

IF EXIST %VENV_PATH% (GOTO PIP_INSTALL) ELSE GOTO VENV_NONE

:VENV_NONE
REM VENV�̃p�X���Ȃ��ꍇ�́AVENV���쐬����.
python -m venv venv
goto PIP_INSTALL

:PIP_INSTALL
REM �v�����ꂽ�\�t�g���C���X�g�[��
pip install -r requirements.txt
