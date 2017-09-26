@echo off
setlocal EnableDelayedExpansion

set ilmerge_bin=..\..\..\packages\ILMerge.2.14.1208\tools\ILMerge.exe
set out_dir=%1
set out_file=%2
set temp_file=temp.exe
set exclusion=\%2.config\%~n2.pdb\%temp_file%\

if not exist %out_dir% (
	echo "Out directory %out_dir% not found."
	exit /B 1
)
cd %out_dir%

if not exist %2  (
	echo "Target file %out_file% not found."
	exit /B 1
)
@copy /y %out_file% %temp_file% >nul

set merge_cmd=%temp_file%
for %%i in (*.dll) do (

	IF /i "!exclusion:\%%~nxi\=!" equ "%exclusion%" (
		set merge_cmd=!merge_cmd! %%i
	)
)

set merge_cmd=%ilmerge_bin% /target:winexe /ndebug /out:%out_file% %merge_cmd%

echo %merge_cmd%
%merge_cmd%

set exclusion=!exclusion!!out_file!\
echo %exclusion%
for %%i in (*) do (

	IF /i "!exclusion:\%%~nxi\=!" equ "%exclusion%" (
		del %%i
	)
)
del %temp_file%
