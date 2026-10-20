# Script de análisis local con SonarQube para Rever
param (
    [string]$Token = $env:SONAR_TOKEN
)

$projectKey = "Rever"
$serverUrl = "http://localhost:9000"
$solutionPath = "Backend\Rever\rever.slnx"

if (-not $Token) {
    Write-Host "ADVERTENCIA: No se detectó la variable de entorno SONAR_TOKEN." -ForegroundColor Yellow
    $Token = Read-Host "Ingresa tu token de análisis de SonarQube"
}

if (-not $Token) {
    Write-Error "El token es obligatorio para ejecutar el análisis."
    exit 1
}

Write-Host "`n>>> [1/4] Iniciando SonarScanner..." -ForegroundColor Cyan
dotnet sonarscanner begin `
    /k:$projectKey `
    /d:sonar.host.url=$serverUrl `
    /d:sonar.token=$Token `
    /d:sonar.cs.opencover.reportsPaths="**/TestResults/**/coverage.opencover.xml"

Write-Host "`n>>> [2/4] Compilando la solución sin incrementalidad..." -ForegroundColor Cyan
dotnet build $solutionPath --no-incremental

Write-Host "`n>>> [3/4] Ejecutando pruebas y calculando cobertura..." -ForegroundColor Cyan
dotnet test $solutionPath --no-build --collect:"XPlat Code Coverage;Format=opencover"

Write-Host "`n>>> [4/4] Finalizando análisis y subiendo resultados a SonarQube..." -ForegroundColor Cyan
dotnet sonarscanner end /d:sonar.token=$Token

Write-Host "`n Analisis completado con exito." -ForegroundColor Green
Write-Host "Revisa los resultados en tu navegador: $serverUrl/dashboard?id=$projectKey" -ForegroundColor Green
