using Mejora_NeptunoAPP.Data;
using Mejora_NeptunoAPP.Helpers;
using Mejora_NeptunoAPP.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mejora_NeptunoAPP.ViewModels
{
    public class ProveedorViewModel : BaseViewModel
    {
        private readonly ProveedorRepository _repository;
        private ObservableCollection<Proveedor> _proveedores;
        private Proveedor _proveedorSeleccionado;

        // Búsqueda
        private string _filtroNombreContacto;
        private string _filtroCiudad;

        // Formulario
        private string _companiaNombre;
        private string _nombreContacto;
        private string _ciudad;
        private string _telefono;

        public ObservableCollection<Proveedor> Proveedores
        {
            get => _proveedores;
            set => SetProperty(ref _proveedores, value);
        }

        public Proveedor ProveedorSeleccionado
        {
            get => _proveedorSeleccionado;
            set
            {
                if (SetProperty(ref _proveedorSeleccionado, value) && value != null)
                {
                    CompaniaNombre = value.CompaniaNombre;
                    NombreContacto = value.NombreContacto;
                    Ciudad = value.Ciudad;
                    Telefono = value.Telefono;
                }
            }
        }

        public string FiltroNombreContacto
        {
            get => _filtroNombreContacto;
            set => SetProperty(ref _filtroNombreContacto, value);
        }

        public string FiltroCiudad
        {
            get => _filtroCiudad;
            set => SetProperty(ref _filtroCiudad, value);
        }

        public string CompaniaNombre { get => _companiaNombre; set => SetProperty(ref _companiaNombre, value); }
        public string NombreContacto { get => _nombreContacto; set => SetProperty(ref _nombreContacto, value); }
        public string Ciudad { get => _ciudad; set => SetProperty(ref _ciudad, value); }
        public string Telefono { get => _telefono; set => SetProperty(ref _telefono, value); }

        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand LimpiarCommand { get; }
        public ICommand BuscarCommand { get; }

        public ProveedorViewModel()
        {
            _repository = new ProveedorRepository();
            GuardarCommand = new RelayCommand(Guardar);
            EliminarCommand = new RelayCommand(Eliminar, CanEliminar);
            LimpiarCommand = new RelayCommand(Limpiar);
            BuscarCommand = new RelayCommand(Buscar);
            CargarProveedores();
        }

        private void CargarProveedores()
        {
            Proveedores = new ObservableCollection<Proveedor>(_repository.ListarActivos());
        }

        private void Buscar(object parameter)
        {
            Proveedores = new ObservableCollection<Proveedor>(_repository.Buscar(FiltroNombreContacto, FiltroCiudad));
        }

        private void Guardar(object parameter)
        {
            if (string.IsNullOrWhiteSpace(CompaniaNombre)) return;

            if (ProveedorSeleccionado == null)
            {
                var nuevo = new Proveedor { CompaniaNombre = CompaniaNombre, NombreContacto = NombreContacto, Ciudad = Ciudad, Telefono = Telefono };
                _repository.Insertar(nuevo);
            }
            else
            {
                ProveedorSeleccionado.CompaniaNombre = CompaniaNombre;
                ProveedorSeleccionado.NombreContacto = NombreContacto;
                ProveedorSeleccionado.Ciudad = Ciudad;
                ProveedorSeleccionado.Telefono = Telefono;
                _repository.Actualizar(ProveedorSeleccionado);
            }
            Buscar(null); // Refresca aplicando filtros actuales
            Limpiar(null);
        }

        private bool CanEliminar(object parameter) => ProveedorSeleccionado != null;

        private void Eliminar(object parameter)
        {
            if (ProveedorSeleccionado != null)
            {
                _repository.EliminarLogico(ProveedorSeleccionado.ProveedorID);
                Buscar(null);
                Limpiar(null);
            }
        }

        private void Limpiar(object parameter)
        {
            ProveedorSeleccionado = null;
            CompaniaNombre = string.Empty;
            NombreContacto = string.Empty;
            Ciudad = string.Empty;
            Telefono = string.Empty;
        }
    }
}
