using Mejora_NeptunoAPP.Data;
using Mejora_NeptunoAPP.Helpers;
using Mejora_NeptunoAPP.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mejora_NeptunoAPP.ViewModels
{
    public class ProductoViewModel : BaseViewModel
    {
        private readonly ProductoRepository _repository;
        private ObservableCollection<Producto> _productos;
        private Producto _productoSeleccionado;

        // Propiedades para binding de Nuevo/Editar
        private string _nombre;
        private decimal _precio;
        private string _cantidadPorUnidad;

        public ObservableCollection<Producto> Productos
        {
            get => _productos;
            set => SetProperty(ref _productos, value);
        }

        public Producto ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                if (SetProperty(ref _productoSeleccionado, value) && value != null)
                {
                    Nombre = value.NombreProducto;
                    Precio = value.PrecioUnidad;
                    CantidadPorUnidad = value.CantidadPorUnidad;
                }
            }
        }

        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        public decimal Precio
        {
            get => _precio;
            set => SetProperty(ref _precio, value);
        }

        public string CantidadPorUnidad
        {
            get => _cantidadPorUnidad;
            set => SetProperty(ref _cantidadPorUnidad, value);
        }

        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand LimpiarCommand { get; }

        public ProductoViewModel()
        {
            _repository = new ProductoRepository();
            GuardarCommand = new RelayCommand(Guardar);
            EliminarCommand = new RelayCommand(Eliminar, CanEliminar);
            LimpiarCommand = new RelayCommand(Limpiar);
            CargarProductos();
        }

        private void CargarProductos()
        {
            var lista = _repository.ListarActivos();
            Productos = new ObservableCollection<Producto>(lista);
        }

        private void Guardar(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Nombre)) return;

            if (ProductoSeleccionado == null)
            {
                // Insertar
                var nuevo = new Producto
                {
                    NombreProducto = Nombre,
                    PrecioUnidad = Precio,
                    CantidadPorUnidad = CantidadPorUnidad
                };
                _repository.Insertar(nuevo);
            }
            else
            {
                // Actualizar
                ProductoSeleccionado.NombreProducto = Nombre;
                ProductoSeleccionado.PrecioUnidad = Precio;
                ProductoSeleccionado.CantidadPorUnidad = CantidadPorUnidad;
                _repository.Actualizar(ProductoSeleccionado);
            }

            CargarProductos();
            Limpiar(null);
        }

        private bool CanEliminar(object parameter)
        {
            return ProductoSeleccionado != null;
        }

        private void Eliminar(object parameter)
        {
            if (ProductoSeleccionado != null)
            {
                _repository.EliminarLogico(ProductoSeleccionado.ProductoID);
                CargarProductos();
                Limpiar(null);
            }
        }

        private void Limpiar(object parameter)
        {
            ProductoSeleccionado = null;
            Nombre = string.Empty;
            Precio = 0;
            CantidadPorUnidad = string.Empty;
        }
    }
}
