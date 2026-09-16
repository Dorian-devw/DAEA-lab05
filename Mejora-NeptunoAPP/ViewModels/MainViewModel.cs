using Mejora_NeptunoAPP.Helpers;

namespace Mejora_NeptunoAPP.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public RelayCommand NavProductosCommand { get; }
        public RelayCommand NavCategoriasCommand { get; }
        public RelayCommand NavProveedoresCommand { get; }
        public RelayCommand NavPedidosCommand { get; }
        
        public MainViewModel()
        {
            NavProductosCommand = new RelayCommand(o => CurrentViewModel = new ProductoViewModel());
            NavCategoriasCommand = new RelayCommand(o => CurrentViewModel = new CategoriaViewModel());
            NavProveedoresCommand = new RelayCommand(o => CurrentViewModel = new ProveedorViewModel());
            NavPedidosCommand = new RelayCommand(o => CurrentViewModel = new PedidoViewModel());
            
            // Default View
            CurrentViewModel = new ProductoViewModel();
        }
    }
}
