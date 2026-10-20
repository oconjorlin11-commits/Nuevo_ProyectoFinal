using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Nuevo_Proyecto.Presenters;
using Nuevo_Proyecto.Views.Interfaces;
using Nuevo_Proyecto.Services;
using Nuevo_Proyecto.Services.Helpers;

namespace Nuevo_Proyecto.Models.Views
{
    public partial class Empleados : Form, IEmpleadoView
    {
        private readonly EmpleadoPresenter _presenter;
        private readonly EmpleadoRepository _empleadoRepository;
        private string _codigoEmpleadoOriginal = string.Empty;
        private string _nombreEmpleadoOriginal = string.Empty;

        public Empleados()
        {
            InitializeComponent();
            _presenter = new EmpleadoPresenter(this);
            _empleadoRepository = new EmpleadoRepository();
        }

        // =========================================================================
        // Implementación de IEmpleadoView: propiedades enlazadas a controles UI
        // =========================================================================

        public string Codigo { get => txtCodigoEmpl.Text; set => txtCodigoEmpl.Text = value; }
        public string Nombre { get => txtNombreEmpl.Text; set => txtNombreEmpl.Text = value; }
        public string Cedula { get => txtCedulaEmpl.Text; set => txtCedulaEmpl.Text = value; }
        public string Telefono { get => txtTelefonoEmpl.Text; set => txtTelefonoEmpl.Text = value; }
        public string Cargo { get => cmboxCargoEmpl.Text; set => cmboxCargoEmpl.Text = value; }
        public decimal Salario
        {
            get => decimal.TryParse(txtSalarioEmpl.Text, out var s) ? s : 0;
            set => txtSalarioEmpl.Text = value.ToString("0.00");
        }
        public DateTime? FechaIngreso
        {
            get => dateTimePickerEmpleado.Value;
            set => dateTimePickerEmpleado.Value = value ?? DateTime.Now;
        }
        public bool Activo { get => checkBoxEmpleado.Checked; set => checkBoxEmpleado.Checked = value; }
        public string BuscarTexto { get => txtBuscarEmpl.Text; set => txtBuscarEmpl.Text = value; }
        public bool HayMultiplesResultados { get; set; }

        // =========================================================================
        // Eventos de la vista notificados al presentador
        // =========================================================================

        public event EventHandler? EditarClicked;
        public event EventHandler? EliminarClicked;
        public event EventHandler? BuscarChanged;

        // =========================================================================
        // Métodos de control visual ordenados por el presentador
        // =========================================================================

        public void showMessage(string message, string titulo, bool esError)
        {
            MessageBoxIcon icon = esError ? MessageBoxIcon.Error : MessageBoxIcon.Information;
            MessageBox.Show(message, titulo, MessageBoxButtons.OK, icon);
        }

        public void ResetFields()
        {
            txtCodigoEmpl.Clear();
            txtNombreEmpl.Clear();
            cmboxCargoEmpl.SelectedIndex = -1;
            cmboxCargoEmpl.Text = string.Empty;
            txtCedulaEmpl.Clear();
            txtTelefonoEmpl.Clear();
            txtSalarioEmpl.Clear();
            dateTimePickerEmpleado.Value = DateTime.Now;
            checkBoxEmpleado.Checked = false;
            checkBoxEmpleado.Enabled = false;
            _codigoEmpleadoOriginal = string.Empty;
            _nombreEmpleadoOriginal = string.Empty;
        }

        public void MostrarEmpleados(DataTable dt)
        {
            dataGridEmpleados.DataSource = dt;
        }

        public void CargarCargos(DataTable cargos)
        {
            cmboxCargoEmpl.DataSource = cargos;
            cmboxCargoEmpl.DisplayMember = "Cargo";
            cmboxCargoEmpl.ValueMember = "Cargo";
            cmboxCargoEmpl.DropDownStyle = ComboBoxStyle.DropDown;
        }

        public void SetActivoEnabled(bool enabled)
        {
            checkBoxEmpleado.Enabled = enabled;
        }

        // =========================================================================
        // Configuración y eventos visuales
        // =========================================================================

        private void Empleados_Load(object sender, EventArgs e)
        {
            txtCodigoEmpl.ReadOnly = true;
            txtCodigoEmpl.BackColor = Color.LightGray;
            dateTimePickerEmpleado.Enabled = false;
            checkBoxEmpleado.Enabled = false;

            dataGridEmpleados.ReadOnly = true;
            dataGridEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridEmpleados.MultiSelect = false;

            ConfigurarDataGridView();
            dataGridEmpleados.CellFormatting += datagrewEmpleados_CellFormatting;
            dataGridEmpleados.SelectionChanged += dataGridEmpleados_SelectionChanged;
            txtBuscarEmpl.TextChanged += (s, e) => BuscarChanged?.Invoke(this, EventArgs.Empty);

            _presenter.InicializarVista();
        }

        private void ConfigurarDataGridView()
        {
            dataGridEmpleados.AutoGenerateColumns = false;
            dataGridEmpleados.Columns.Clear();

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Código",
                DataPropertyName = "Codigo"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cedula",
                HeaderText = "Cédula",
                DataPropertyName = "Cedula"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cargo",
                HeaderText = "Cargo",
                DataPropertyName = "Cargo"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Teléfono",
                DataPropertyName = "Telefono"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Salario",
                HeaderText = "Salario",
                DataPropertyName = "Salario"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaIngreso",
                HeaderText = "Fecha Ingreso",
                DataPropertyName = "FechaIngreso"
            });

            dataGridEmpleados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Activo",
                HeaderText = "Estado",
                DataPropertyName = "Activo"
            });
        }

        private void dataGridEmpleados_SelectionChanged(object? sender, EventArgs e)
        {
            // Solo cargar campos si hay múltiples resultados (resultado de búsqueda)
            if (!HayMultiplesResultados)
                return;

            if (dataGridEmpleados.SelectedRows.Count > 0)
            {
                var row = dataGridEmpleados.SelectedRows[0];
                if (row.DataBoundItem is DataRowView drv)
                {
                    var dataRow = drv.Row;
                    _presenter?.SeleccionarEmpleado(
                        dataRow["Codigo"]?.ToString() ?? string.Empty,
                        dataRow["Nombre"]?.ToString() ?? string.Empty,
                        dataRow["Cargo"]?.ToString() ?? string.Empty,
                        dataRow["Cedula"]?.ToString() ?? string.Empty,
                        dataRow["Telefono"]?.ToString(),
                        Convert.ToDecimal(dataRow["Salario"]),
                        Convert.ToDateTime(dataRow["FechaIngreso"]),
                        Convert.ToBoolean(dataRow["Activo"])
                    );
                }
            }
        }

        private void btnNuevoEmpleado_Click(object sender, EventArgs e)
        {
            // Verificar acceso: só lo admin puro puede crear empleados
            if (!SesionActual.EsAdministrador)
            {
                MessageBox.Show(
                    "Solo administradores pueden crear nuevos empleados.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using (var frm = new NuevoEmpleado())
            {
                frm.ShowDialog();
            }
            _presenter.CargarEmpleadosActivos();
        }

        private void btnEditarEmpl_Click(object sender, EventArgs e)
        {
            // Verificar acceso: solo admin puro puede editar
            if (!SesionActual.EsAdministrador)
            {
                MessageBox.Show(
                    "Solo administradores pueden editar empleados.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string codigo = Codigo.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Debe ingresar un código de empleado a editar.", "Código requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar cambios
            if (Nombre.Trim() == _nombreEmpleadoOriginal &&
                Cargo == string.Empty &&
                Cedula == string.Empty &&
                Telefono == string.Empty &&
                Salario == 0)
            {
                MessageBox.Show("No hay cambios para guardar.", "Sin cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                bool resultado = _empleadoRepository.Actualizar(
                    codigo,
                    Nombre.Trim(),
                    Cargo,
                    Cedula.Trim(),
                    Telefono,
                    Salario
                );

                if (resultado)
                {
                    MessageBox.Show("Actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BuscarTexto = string.Empty;  // Limpiar búsqueda
                    ResetFields();               // Limpiar campos
                    _presenter.CargarEmpleadosActivos();  // Recargar lista
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarEmpl_Click(object sender, EventArgs e)
        {
            // Verificar acceso: solo admin puro puede eliminar
            if (!SesionActual.EsAdministrador)
            {
                MessageBox.Show(
                    "Solo administradores pueden despedir empleados.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string codigo = Codigo.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Debe ingresar un código de empleado a despedir.", "Código requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación previa
            DialogResult conf = MessageBox.Show(
                $"¿Está seguro de que desea despedir al empleado {_nombreEmpleadoOriginal}?",
                "Confirmar despido",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (conf != DialogResult.Yes)
                return;

            try
            {
                bool resultado = _empleadoRepository.Desactivar(codigo);

                if (resultado)
                {
                    MessageBox.Show("Empleado despedido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BuscarTexto = string.Empty;  // Limpiar búsqueda
                    ResetFields();               // Limpiar campos
                    _presenter.CargarEmpleadosActivos();  // Recargar lista
                }
                else
                {
                    MessageBox.Show("No se pudo despedir al empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al despedir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Solicita un motivo mediante un cuadro de diálogo simple
        /// </summary>
        private string PromptMotivo(string titulo)
        {
            Form formMotivo = new Form
            {
                Text = titulo,
                Width = 550,
                Height = 200,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lbl = new Label
            {
                Text = titulo,
                Left = 10,
                Top = 15,
                Width = 520,
                AutoSize = true
            };
            TextBox txt = new TextBox
            {
                Left = 10,
                Top = 40,
                Width = 520,
                Height = 70,
                Multiline = true,
                AcceptsReturn = true
            };
            Button btnOk = new Button
            {
                Text = "Aceptar",
                Left = 380,
                Top = 120,
                Width = 70,
                DialogResult = DialogResult.OK
            };
            Button btnCancel = new Button
            {
                Text = "Cancelar",
                Left = 460,
                Top = 120,
                Width = 70,
                DialogResult = DialogResult.Cancel
            };

            formMotivo.Controls.Add(lbl);
            formMotivo.Controls.Add(txt);
            formMotivo.Controls.Add(btnOk);
            formMotivo.Controls.Add(btnCancel);
            formMotivo.AcceptButton = btnOk;
            formMotivo.CancelButton = btnCancel;

            if (formMotivo.ShowDialog() == DialogResult.OK)
            {
                return txt.Text.Trim();
            }

            return string.Empty;
        }

        private void datagrewEmpleados_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridEmpleados.Columns[e.ColumnIndex].Name == "Activo" && e.Value is bool activo)
            {
                e.Value = activo ? "Trabajando" : "Despedido";
                e.FormattingApplied = true;
            }
        }

        private void txtCedulaEmpl_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
