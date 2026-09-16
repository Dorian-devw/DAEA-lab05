using Mejora_NeptunoAPP.Data;
using Mejora_NeptunoAPP.Helpers;
using Mejora_NeptunoAPP.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mejora_NeptunoAPP.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        private readonly CategoriaRepository _repository;
        private ObservableCollection<Categoria> _categorias;
        private Categoria _categoriaSeleccionada;

        private string _nombreCategoria;
        private string _descripcion;

        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set => SetProperty(ref _categorias, value);
        }

        public Categoria CategoriaSeleccionada
        {
            get => _categoriaSeleccionada;
            set
            {
                if (SetProperty(ref _categoriaSeleccionada, value) && value != null)
                {
                    NombreCategoria = value.NombreCategoria;
                    Descripcion = value.Descripcion;
                }
            }
        }

        public string NombreCategoria
        {
            get => _nombreCategoria;
            set => SetProperty(ref _nombreCategoria, value);
        }

        public string Descripcion
        {
            get => _descripcion;
            set => SetProperty(ref _descripcion, value);
        }

        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand LimpiarCommand { get; }

        public CategoriaViewModel()
        {
            _repository = new CategoriaRepository();
            GuardarCommand = new RelayCommand(Guardar);
            EliminarCommand = new RelayCommand(Eliminar, CanEliminar);
            LimpiarCommand = new RelayCommand(Limpiar);
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            Categorias = new ObservableCollection<Categoria>(_repository.ListarActivos());
        }

        private void Guardar(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NombreCategoria)) return;

            if (CategoriaSeleccionada == null)
            {
                var nueva = new Categoria { NombreCategoria = NombreCategoria, Descripcion = Descripcion };
                _repository.Insertar(nueva);
            }
            else
            {
                CategoriaSeleccionada.NombreCategoria = NombreCategoria;
                CategoriaSeleccionada.Descripcion = Descripcion;
                _repository.Actualizar(CategoriaSeleccionada);
            }
            CargarCategorias();
            Limpiar(null);
        }

        private bool CanEliminar(object parameter) => CategoriaSeleccionada != null;

        private void Eliminar(object parameter)
        {
            if (CategoriaSeleccionada != null)
            {
                _repository.EliminarLogico(CategoriaSeleccionada.CategoriaID);
                CargarCategorias();
                Limpiar(null);
            }
        }

        private void Limpiar(object parameter)
        {
            CategoriaSeleccionada = null;
            NombreCategoria = string.Empty;
            Descripcion = string.Empty;
        }
    }
}
