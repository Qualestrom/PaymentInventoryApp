@echo off
echo ===================================================
echo Building .NET 8 WinForms Desktop Application...
echo ===================================================
"C:\Users\Christopher\.dotnet\dotnet.exe" build "%~dp0frontend_csharp\PaymentInventoryApp\PaymentInventoryApp.csproj"
pause
