# Script de análisis local con SonarQube para Rever
param (
    [string]$Token = $env:SONAR_TOKEN
)

$projectKey = "Rever"
$serverUrl = "http://localhost:9000"
$solutionPath = "Backend\Rever\rever.slnx"

# 0. Verificar si el servidor SonarQube está encendido
$portOpen = (Test-NetConnection -ComputerName localhost -Port 9000 -WarningAction SilentlyContinue).TcpTestSucceeded
if (-not $portOpen) {
    Write-Host "`n[ERROR] El servidor SonarQube NO esta corriendo en http://localhost:9000" -ForegroundColor Red
    Write-Host "Por favor inicia el servidor ejecutando: C:\sonarqube\server\bin\windows-x86-64\StartSonar.bat" -ForegroundColor Yellow
    Write-Host "Espera a que muestre 'SonarQube is operational' y vuelve a ejecutar este script.`n" -ForegroundColor Yellow
    exit 1
}

if (-not $Token) {
    Write-Host "ADVERTENCIA: No se detecto la variable de entorno SONAR_TOKEN." -ForegroundColor Yellow
    $Token = Read-Host "Ingresa tu token de analisis de SonarQube"
}

if (-not $Token) {
    Write-Error "El token es obligatorio para ejecutar el analisis."
    exit 1
}

Write-Host "`n>>> [1/4] Iniciando SonarScanner..." -ForegroundColor Cyan
dotnet sonarscanner begin `
    /k:$projectKey `
    /d:sonar.host.url=$serverUrl `
    /d:sonar.token=$Token `
    /d:sonar.cs.opencover.reportsPaths="**/TestResults/**/coverage.opencover.xml"

if ($LASTEXITCODE -ne 0) {
    Write-Host "`n[ERROR] Fallo el inicio de SonarScanner. Revisa tu token o la conexion con el servidor." -ForegroundColor Red
    exit 1
}

Write-Host "`n>>> [2/4] Compilando la solucion sin incrementalidad..." -ForegroundColor Cyan
dotnet build $solutionPath --no-incremental

if ($LASTEXITCODE -ne 0) {
    Write-Host "`n[ERROR] La compilacion fallo. Corrige los errores de codigo antes de analizar." -ForegroundColor Red
    exit 1
}

Write-Host "`n>>> [3/4] Ejecutando pruebas y calculando cobertura..." -ForegroundColor Cyan
dotnet test $solutionPath --no-build --collect:"XPlat Code Coverage;Format=opencover"

Write-Host "`n>>> [4/4] Finalizando analisis y subiendo resultados a SonarQube..." -ForegroundColor Cyan
dotnet sonarscanner end /d:sonar.token=$Token

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n Analisis completado con exito!" -ForegroundColor Green
    Write-Host "Revisa los resultados actualizados en: $serverUrl/dashboard?id=$projectKey" -ForegroundColor Green
} else {
    Write-Host "`n[ERROR] Ocurrio un problema al finalizar el envio a SonarQube." -ForegroundColor Red
}
