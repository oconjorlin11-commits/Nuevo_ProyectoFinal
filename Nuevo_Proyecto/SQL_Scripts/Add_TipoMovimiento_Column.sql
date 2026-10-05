-- Script para agregar la columna TipoMovimiento a la tabla MovimientosInventario
-- Ejecutar este script en SQL Server Management Studio

USE [Dev_Comideria]
GO

ALTER TABLE MovimientosInventario
ADD TipoMovimiento VARCHAR(50) NULL;

GO

PRINT 'Columna TipoMovimiento agregada correctamente a la tabla MovimientosInventario';
