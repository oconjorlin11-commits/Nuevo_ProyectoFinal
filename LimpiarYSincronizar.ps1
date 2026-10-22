# Script para limpiar BD SQLite y ejecutar sincronización completa
Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║     SINCRONIZACIÓN COMPLETA SQL SERVER → SQLite        ║" -ForegroundColor Cyan
Write-Host "║           (Modo OFFLINE Habilitado)                    ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

# PASO 1: Verificar si SQL Server está disponible
Write-Host "1️⃣  Verificando SQL Server..." -ForegroundColor Yellow
$sqlTest = Test-Connection -ComputerName localhost -TCPPort 1433 -ErrorAction SilentlyContinue
if ($sqlTest) {
	Write-Host "✓ SQL Server DISPONIBLE" -ForegroundColor Green
} else {
	Write-Host "✗ SQL Server NO disponible - enciéndelo primero" -ForegroundColor Red
	exit 1
}

Write-Host ""

# PASO 2: Eliminar BD SQLite vieja
Write-Host "2️⃣  Eliminando BD SQLite anterior..." -ForegroundColor Yellow
$dbPath = "$env:APPDATA\Comideria\comideria_offline.db"

if (Test-Path $dbPath) {
	Remove-Item $dbPath -Force
	Write-Host "✓ BD SQLite eliminada" -ForegroundColor Green
	Start-Sleep -Seconds 1
} else {
	Write-Host "ℹ️  No había BD anterior" -ForegroundColor Gray
}

Write-Host ""

# PASO 3: Eliminar carpeta de caché si existe
$cacheDir = "$env:APPDATA\Comideria"
if ((Get-Item $cacheDir -ErrorAction SilentlyContinue).PSIsContainer) {
	Write-Host "3️⃣  Limpiando caché..." -ForegroundColor Yellow
	Remove-Item "$cacheDir\*.db-wal" -Force -ErrorAction SilentlyContinue
	Remove-Item "$cacheDir\*.db-shm" -Force -ErrorAction SilentlyContinue
	Write-Host "✓ Caché limpiado" -ForegroundColor Green
}

Write-Host ""
Write-Host "════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "✅ Preparación completada" -ForegroundColor Green
Write-Host ""
Write-Host "📝 PRÓXIMOS PASOS:" -ForegroundColor Yellow
Write-Host "1. Abre Visual Studio" -ForegroundColor Gray
Write-Host "2. Ejecuta Debug → Start Without Debugging (Ctrl+F5)" -ForegroundColor Gray
Write-Host "3. Verás en consola la sincronización completa" -ForegroundColor Gray
Write-Host "4. Loguéate con un usuario" -ForegroundColor Gray
Write-Host "5. Apaga SQL Server" -ForegroundColor Gray
Write-Host "6. Cierra y reabre la app" -ForegroundColor Gray
Write-Host "7. Loguea de nuevo - debe funcionar OFFLINE" -ForegroundColor Gray
Write-Host ""
Write-Host "════════════════════════════════════════════════════════" -ForegroundColor Cyan
