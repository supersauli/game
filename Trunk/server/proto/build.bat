@echo off
for /f "" %%a in ('"dir /b .\*.proto|findstr ."' ) do .\protoc.exe -I=./ %%a --cpp_out=./ 
pause