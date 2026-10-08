$file = "C:\Users\josep\source\repos\oconjorlin11-commits\Nuevo_ProyectoFinal\Nuevo_Proyecto\Presenters\InventarioPresenter.cs"
$content = Get-Content $file -Raw

# Primera reemplazo: cuando búsqueda está vacío
$pattern = 'BuscarInventarioPorCodigoONombre\(""\);'
$replacement = 'ObtenerInventarioActivo();'
$content = $content -replace $pattern, $replacement

Set-Content $file $content -Encoding UTF8
Write-Host "InventarioPresenter corregido para usar ObtenerInventarioActivo en búsqueda vacía"
