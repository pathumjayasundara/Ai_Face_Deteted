@echo off
echo ============================================
echo   SmartCampusAI - Quick Setup Script
echo ============================================
echo.

echo [1/3] Restoring NuGet packages...
cd SmartCampusAI.WinForms
dotnet restore
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: dotnet restore failed. Ensure .NET 6 SDK is installed.
    pause
    exit /b 1
)

echo.
echo [2/3] Building project...
dotnet build --configuration Release
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Build failed. Check error messages above.
    pause
    exit /b 1
)

echo.
echo [3/3] Build successful!
echo.
echo NEXT STEPS:
echo  1. Run Database\SmartCampusAI.sql in SQL Server Management Studio
echo  2. Update connection string in Data\DBConnection.cs if needed
echo  3. Place haarcascade_frontalface_default.xml in Resources\ folder
echo     (Download from: https://github.com/opencv/opencv/tree/master/data/haarcascades)
echo  4. Run: dotnet run
echo.
echo Default login: admin / Admin@123
echo.
pause
