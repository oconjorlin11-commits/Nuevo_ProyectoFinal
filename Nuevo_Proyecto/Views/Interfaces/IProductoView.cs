using System;

namespace Nuevo_Proyecto.Views.Interfaces
{
    public interface IProductoView
    {
        // Propiedades de lectura/escritura de los controles de la vista

        string Codigo { get; set; }

        string Nombre { get; set; }

        int CategoriaId { get; set; }

        int UnidadId { get; set; }

        string Descripcion { get; set; }

        decimal PrecioVenta { get; set; }

        bool Activo { get; set; }

        int stockInicial { get; set; }

        int StockMinimo { get; set; }


        // Eventos que la vista notifica al presentador

        event EventHandler GuardarClicked;

        event EventHandler CancelarClicked;


        // Métodos de control visual ordenados por el presentador

        void showMessage(string message, string titulo, bool esError);

        void ResetFields();



    }
}
