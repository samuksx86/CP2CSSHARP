@echo off
echo ========================================
echo RECRIANDO MIGRATIONS COM NOMES CORRETOS
echo ========================================
echo.
echo IMPORTANTE: A API deve estar PARADA!
echo Pressione Ctrl+C se a API estiver rodando
echo.
pause

cd RestauranteData

echo.
echo [1/2] Criando nova migration...
dotnet ef migrations add InitialCreate --startup-project ../RestauranteApi

if errorlevel 1 (
    echo.
    echo ERRO ao criar migration!
    pause
    exit /b 1
)

echo.
echo [2/2] Aplicando migration no banco...
dotnet ef database update --startup-project ../RestauranteApi

if errorlevel 1 (
    echo.
    echo ERRO ao aplicar migration!
    echo Execute o script drop_tables.sql primeiro no SQL Developer
    pause
    exit /b 1
)

echo.
echo ========================================
echo SUCESSO! Migration aplicada.
echo ========================================
echo.
echo Agora execute setup_database.sql no SQL Developer
echo para popular o banco com dados iniciais
echo.
pause
