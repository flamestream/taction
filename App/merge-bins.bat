@echo off
setlocal EnableDelayedExpansion

set ilmerge_bin=..\..\..\packages\ILMerge.2.14.1208\tools\ILMerge.exe
set out_dir=%1
set out_file=%2
set temp_dir=temp
set temp_file=!temp_dir!\%2
set merge_exclusion=\%2\
set delete_exclusion=\%2.config\!temp_dir!\

if not exist !out_dir! (
	echo "Out directory %out_dir% not found."
	exit /B 1
)
cd !out_dir!
if not exist !temp_dir! md !temp_dir!

if not exist %2  (
	echo "Target file !out_file! not found."
	exit /B 1
)
rem @copy /y %out_file% %temp_file% >nul

set merge_cmd=!out_file!
for %%i in (*.dll *.exe) do (

	IF /i "!merge_exclusion:\%%~nxi\=!" equ "!merge_exclusion!" (
		set merge_cmd=!merge_cmd! %%i
	)
)

set merge_cmd=%ilmerge_bin% /target:winexe /out:!temp_file! !merge_cmd!

echo !merge_cmd!
%merge_cmd%

rem set exclusion=!exclusion!!out_file!\
rem echo %exclusion%
rem for %%i in (*) do (

rem 	IF /i "!exclusion:\%%~nxi\=!" equ "%exclusion%" (
rem 		del %%i
rem 	)
rem )
rem del %temp_file%
