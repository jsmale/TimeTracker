@echo off
cls
Tools\nant\NAnt.exe -buildfile:Default.build %*
pause