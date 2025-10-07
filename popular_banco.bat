@echo off
echo ========================================
echo Populando banco de dados Oracle
echo ========================================
echo.

sqlplus system/oracle@localhost:1521/XEPDB1 @setup_database.sql

echo.
echo ========================================
echo Concluido!
echo ========================================
pause
