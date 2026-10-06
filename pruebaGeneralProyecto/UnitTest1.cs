using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Nuevo_Proyecto.Models.DTOs;
using Nuevo_Proyecto.Models.Views;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Services.Helpers;
using Nuevo_Proyecto.Services.Interfaz_service;
using Nuevo_Proyecto.Views.Interfaces;

namespace pruebaGeneralProyecto
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            SesionActual.Cerrar();
        }

        // =========================================================================
        // Tests de Seguridad: PasswordHasher
        // =========================================================================

        [Test]
        public void PasswordHasher_Hash_GeneraHashValidoConPrefijoPBKDF2()
        {
            string password = "MiPasswordSeguro123!";
            string hash = PasswordHasher.Hash(password);

            Assert.That(hash, Is.Not.Null.And.Not.Empty);
            Assert.That(PasswordHasher.EsHash(hash), Is.True);
            Assert.That(hash.StartsWith("PBKDF2$120000$"), Is.True);
        }

        [Test]
        public void PasswordHasher_Verificar_ConContrasenaCorrecta_RetornaTrue()
        {
            string password = "AdminPassword2026";
            string hash = PasswordHasher.Hash(password);

            bool esValida = PasswordHasher.Verificar(password, hash);
            Assert.That(esValida, Is.True);
        }

        [Test]
        public void PasswordHasher_Verificar_ConContrasenaIncorrecta_RetornaFalse()
        {
            string password = "PasswordCorrecto";
            string passwordErroneo = "PasswordIncorrecto";
            string hash = PasswordHasher.Hash(password);

            bool esValida = PasswordHasher.Verificar(passwordErroneo, hash);
            Assert.That(esValida, Is.False);
        }

        [Test]
        public void PasswordHasher_Verificar_ConParametrosNulosOVacios_RetornaFalse()
        {
            Assert.That(PasswordHasher.Verificar("", "PBKDF2$120000$salt$hash"), Is.False);
            Assert.That(PasswordHasher.Verificar("pass", null), Is.False);
            Assert.That(PasswordHasher.Verificar("pass", "invalido"), Is.False);
        }

        // =========================================================================
        // Tests de Generador de Códigos: CodigoGenerator
        // =========================================================================

        [Test]
        public void CodigoGenerator_SiguienteAgrupado_SinExistentes_RetornaCodigoInicial()
        {
            var existentes = new List<string?>();
            string resultado = CodigoGenerator.SiguienteAgrupado("CLI", existentes, "CLI-001");

            Assert.That(resultado, Is.EqualTo("CLI-001"));
        }

        [Test]
        public void CodigoGenerator_SiguienteAgrupado_ConExistentes_IncrementaCorrectamente()
        {
            var existentes = new List<string?> { "CLI-001", "CLI-002", "CLI-005" };
            string resultado = CodigoGenerator.SiguienteAgrupado("CLI", existentes, "CLI-001");

            Assert.That(resultado, Is.EqualTo("CLI-006"));
        }

        [Test]
        public void CodigoGenerator_SiguienteSecuencial_SinExistentes_RetornaPrimero()
        {
            var existentes = new List<string?>();
            string resultado = CodigoGenerator.SiguienteSecuencial("F", existentes, 4);

            Assert.That(resultado, Is.EqualTo("F0001"));
        }

        [Test]
        public void CodigoGenerator_SiguienteSecuencial_ConVarios_RetornaMaximoMasUno()
        {
            var existentes = new List<string?> { "F0001", "F0009", "F0010" };
            string resultado = CodigoGenerator.SiguienteSecuencial("F", existentes, 4);

            Assert.That(resultado, Is.EqualTo("F0011"));
        }

        // =========================================================================
        // Tests de Estado de Sesión: SesionActual
        // =========================================================================

        [Test]
        public void SesionActual_IniciarSesion_AlmacenaDatosCorrectamente()
        {
            var usuario = new SesionUsuarioDto
            {
                EmpleadoId = 10,
                NombreUsuario = "jperez",
                NombreEmpleado = "Juan Perez",
                Cargo = "Administrador",
                Rol = "admin"
            };

            SesionActual.Iniciar(usuario);

            Assert.That(SesionActual.HaySesion, Is.True);
            Assert.That(SesionActual.EmpleadoId, Is.EqualTo(10));
            Assert.That(SesionActual.NombreEmpleado, Is.EqualTo("Juan Perez"));
            Assert.That(SesionActual.NombreUsuario, Is.EqualTo("jperez"));
            Assert.That(SesionActual.EsAdministrador, Is.True);

            SesionActual.Cerrar();
            Assert.That(SesionActual.HaySesion, Is.False);
            Assert.That(SesionActual.EmpleadoId, Is.EqualTo(0));
        }

        [Test]
        public void SesionActual_EmpleadoORespaldo_RetornaSesionSiExisteORespaldo()
        {
            Assert.That(SesionActual.EmpleadoORespaldo(99), Is.EqualTo(99));

            var usuario = new SesionUsuarioDto
            {
                EmpleadoId = 5,
                NombreUsuario = "cajero1",
                NombreEmpleado = "Carlos",
                Cargo = "Cajero",
                Rol = "cajero"
            };
            SesionActual.Iniciar(usuario);

            Assert.That(SesionActual.EmpleadoORespaldo(0), Is.EqualTo(5));
            Assert.That(SesionActual.EsAdministrador, Is.False);
        }

        // =========================================================================
        // Tests de DTOs del Dominio
        // =========================================================================

        [Test]
        public void DTOs_ClienteDto_InstanciacionYPropiedades()
        {
            var dto = new ClienteDto
            {
                Codigo = "CLI-001",
                Nombre = "Empresa ABC",
                Telefono = "8888-8888",
                Direccion = "Avenida Central",
                Nota = "Cliente recurrente",
                Activo = true
            };

            Assert.That(dto.Codigo, Is.EqualTo("CLI-001"));
            Assert.That(dto.Nombre, Is.EqualTo("Empresa ABC"));
            Assert.That(dto.Activo, Is.True);
        }

        [Test]
        public void DTOs_NuevaFacturaDto_InstanciacionYLineas()
        {
            var factura = new NuevaFacturaDto
            {
                ClienteId = 1,
                EmpleadoId = 2,
                FormaPagoId = 1,
                Observacion = "Venta contado",
                Lineas = new List<LineaFacturaDto>
                {
                    new LineaFacturaDto { ProductoId = 10, Cantidad = 2 },
                    new LineaFacturaDto { ProductoId = 11, Cantidad = 1 }
                }
            };

            Assert.That(factura.Lineas.Count, Is.EqualTo(2));
            Assert.That(factura.Lineas[0].ProductoId, Is.EqualTo(10));
            Assert.That(factura.Lineas[0].Cantidad, Is.EqualTo(2));
        }

        // =========================================================================
        // Tests de Lógica de Negocio y Presenters (MVP)
        // =========================================================================

        [Test]
        public void ClientePresenter_Guardar_CamposObligatoriosVacios_MuestraErrorYNoCrea()
        {
            var fakeRepo = new FakeClienteRepository();
            var fakeView = new FakeNuevoClienteView
            {
                Codigo = "",
                Nombre = "",
                Direccion = ""
            };

            var presenter = new ClientePresenter(fakeView, fakeRepo);
            fakeView.DispararGuardar();

            Assert.That(fakeView.UltimoFueError, Is.True);
            Assert.That(fakeView.UltimoMensaje, Does.Contain("obligatorios"));
            Assert.That(fakeRepo.Clientes.Count, Is.EqualTo(0));
        }

        [Test]
        public void ClientePresenter_Guardar_CodigoDuplicado_MuestraErrorYNoCrea()
        {
            var fakeRepo = new FakeClienteRepository();
            fakeRepo.Crear(new ClienteDto { Codigo = "CLI-001", Nombre = "Existente", Direccion = "Calle 1" });

            var fakeView = new FakeNuevoClienteView
            {
                Codigo = "CLI-001",
                Nombre = "Nuevo",
                Direccion = "Calle 2"
            };

            var presenter = new ClientePresenter(fakeView, fakeRepo);
            fakeView.DispararGuardar();

            Assert.That(fakeView.UltimoFueError, Is.True);
            Assert.That(fakeView.UltimoMensaje, Does.Contain("ya existe"));
            Assert.That(fakeRepo.Clientes.Count, Is.EqualTo(1));
        }

        [Test]
        public void ClientePresenter_Guardar_DatosValidos_CreaClienteYCierraVista()
        {
            var fakeRepo = new FakeClienteRepository();
            var fakeView = new FakeNuevoClienteView
            {
                Codigo = "CLI-002",
                Nombre = "Cliente Nuevo",
                Telefono = "2222-3333",
                Direccion = "Centro",
                Nota = "Nota prueba",
                Activo = true
            };

            var presenter = new ClientePresenter(fakeView, fakeRepo);
            fakeView.DispararGuardar();

            Assert.That(fakeRepo.Clientes.Count, Is.EqualTo(1));
            Assert.That(fakeRepo.Clientes[0].Codigo, Is.EqualTo("CLI-002"));
            Assert.That(fakeView.UltimoFueError, Is.False);
            Assert.That(fakeView.CloseViewLlamado, Is.True);
        }

        [Test]
        public void ClientePresenter_Cancelar_LlamaResetFields()
        {
            var fakeRepo = new FakeClienteRepository();
            var fakeView = new FakeNuevoClienteView();

            var presenter = new ClientePresenter(fakeView, fakeRepo);
            fakeView.DispararCancelar();

            Assert.That(fakeView.ResetFieldsLlamado, Is.True);
        }

        // =========================================================================
        // Tests de Arquitectura SOLID: Verificación de Contratos MVP e ISP
        // =========================================================================

        [Test]
        public void Arquitectura_VistasImplementanInterfacesSegregadas()
        {
            Assert.That(typeof(IClienteView).IsAssignableFrom(typeof(Clientescs)), Is.True, "Clientescs debe implementar IClienteView");
            Assert.That(typeof(INuevoClienteView).IsAssignableFrom(typeof(NuevoCliente)), Is.True, "NuevoCliente debe implementar INuevoClienteView");

            Assert.That(typeof(IEmpleadoView).IsAssignableFrom(typeof(Empleados)), Is.True, "Empleados debe implementar IEmpleadoView");
            Assert.That(typeof(INuevoEmpleadoView).IsAssignableFrom(typeof(NuevoEmpleado)), Is.True, "NuevoEmpleado debe implementar INuevoEmpleadoView");

            Assert.That(typeof(IFacturacionView).IsAssignableFrom(typeof(Facturacion)), Is.True, "Facturacion debe implementar IFacturacionView");
            Assert.That(typeof(IFacturasEmitidasView).IsAssignableFrom(typeof(FacturasEmitidas)), Is.True, "FacturasEmitidas debe implementar IFacturasEmitidasView");
            Assert.That(typeof(IComprobanteView).IsAssignableFrom(typeof(Comprobante)), Is.True, "Comprobante debe implementar IComprobanteView");

            Assert.That(typeof(IInventarioView).IsAssignableFrom(typeof(Inventario)), Is.True, "Inventario debe implementar IInventarioView");
            Assert.That(typeof(IProductoView).IsAssignableFrom(typeof(NuevoProducto)), Is.True, "NuevoProducto debe implementar IProductoView");
            Assert.That(typeof(ILoginView).IsAssignableFrom(typeof(InicioSesion)), Is.True, "InicioSesion debe implementar ILoginView");
            Assert.That(typeof(IReportesView).IsAssignableFrom(typeof(Reportes)), Is.True, "Reportes debe implementar IReportesView");
        }

        [Test]
        public void Arquitectura_FacturasEmitidasYComprobante_NoImplementanIFacturacionView()
        {
            // ISP: Asegurar que FacturasEmitidas y Comprobante están desacoplados de IFacturacionView
            Assert.That(typeof(IFacturacionView).IsAssignableFrom(typeof(FacturasEmitidas)), Is.False,
                "FacturasEmitidas no debe implementar IFacturacionView por segregación de interfaces (ISP)");

            Assert.That(typeof(IFacturacionView).IsAssignableFrom(typeof(Comprobante)), Is.False,
                "Comprobante no debe implementar IFacturacionView por segregación de interfaces (ISP)");
        }
    }

    // =========================================================================
    // Fakes y Mocks para Pruebas Unitarias de Presenters
    // =========================================================================

    public class FakeNuevoClienteView : INuevoClienteView
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Nota { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public string AutorizadoPor { get; set; } = string.Empty;

        public event EventHandler? GuardarClicked;
        public event EventHandler? CancelarClicked;

        public string? UltimoMensaje { get; private set; }
        public bool UltimoFueError { get; private set; }
        public bool ResetFieldsLlamado { get; private set; }
        public bool CloseViewLlamado { get; private set; }

        public void DispararGuardar() => GuardarClicked?.Invoke(this, EventArgs.Empty);
        public void DispararCancelar() => CancelarClicked?.Invoke(this, EventArgs.Empty);

        public void showMessage(string message, string titulo, bool esError)
        {
            UltimoMensaje = message;
            UltimoFueError = esError;
        }

        public void ResetFields()
        {
            ResetFieldsLlamado = true;
        }

        public void CloseView()
        {
            CloseViewLlamado = true;
        }
    }

    public class FakeClienteRepository : IClienteRepository
    {
        public List<ClienteDto> Clientes { get; } = new();

        public IReadOnlyList<ClienteDto> GetActivos() => Clientes.Where(c => c.Activo).ToList();
        public IReadOnlyList<ClienteDto> BuscarPorCodigo(string prefijoCodigo) =>
            Clientes.Where(c => c.Codigo.StartsWith(prefijoCodigo, StringComparison.OrdinalIgnoreCase)).ToList();
        public IReadOnlyList<string> GetNotas() => new List<string>();
        public string GetSiguienteCodigo() => "CLI-" + (Clientes.Count + 1).ToString("D3");
        public bool CodigoExiste(string codigo) => Clientes.Any(c => c.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        public void Crear(ClienteDto cliente) => Clientes.Add(cliente);
        public bool Actualizar(string codigo, string nombre, string? telefono, string? direccion, string? nota) => true;
        public bool Desactivar(string codigo) => true;
        public bool Reactivar(string codigo) => true;
        public ClienteDto ObtenerOCrearConsumidorFinal() => new ClienteDto { Codigo = "CF-001", Nombre = "Consumidor Final" };
        public string? ValidarNuevoCliente(string codigo, string nombre, string? telefono, string? direccion, string? nota) => null; // Simulación: siempre válido
    }
}
