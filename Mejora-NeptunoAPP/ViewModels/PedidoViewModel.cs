using Mejora_NeptunoAPP.Data;
using Mejora_NeptunoAPP.Helpers;
using Mejora_NeptunoAPP.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mejora_NeptunoAPP.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        private readonly PedidoRepository _repository;
        private ObservableCollection<Pedido> _pedidos;
        private Pedido _pedidoSeleccionado;

        private ObservableCollection<DetallePedido> _reporteDetalles;
        private DateTime _filtroFechaInicio = DateTime.Today.AddMonths(-1);
        private DateTime _filtroFechaFin = DateTime.Today;

        private DateTime _fechaPedido = DateTime.Today;
        private string _destinatario;
        private string _ciudadDestino;

        public ObservableCollection<Pedido> Pedidos
        {
            get => _pedidos;
            set => SetProperty(ref _pedidos, value);
        }

        public Pedido PedidoSeleccionado
        {
            get => _pedidoSeleccionado;
            set
            {
                if (SetProperty(ref _pedidoSeleccionado, value) && value != null)
                {
                    FechaPedido = value.FechaPedido;
                    Destinatario = value.Destinatario;
                    CiudadDestino = value.CiudadDestino;
                }
            }
        }

        // Reporte
        public ObservableCollection<DetallePedido> ReporteDetalles
        {
            get => _reporteDetalles;
            set => SetProperty(ref _reporteDetalles, value);
        }

        public DateTime FiltroFechaInicio
        {
            get => _filtroFechaInicio;
            set => SetProperty(ref _filtroFechaInicio, value);
        }

        public DateTime FiltroFechaFin
        {
            get => _filtroFechaFin;
            set => SetProperty(ref _filtroFechaFin, value);
        }

        public DateTime FechaPedido { get => _fechaPedido; set => SetProperty(ref _fechaPedido, value); }
        public string Destinatario { get => _destinatario; set => SetProperty(ref _destinatario, value); }
        public string CiudadDestino { get => _ciudadDestino; set => SetProperty(ref _ciudadDestino, value); }

        public ICommand GuardarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand LimpiarCommand { get; }
        public ICommand GenerarReporteCommand { get; }

        public PedidoViewModel()
        {
            _repository = new PedidoRepository();
            GuardarCommand = new RelayCommand(Guardar);
            EliminarCommand = new RelayCommand(Eliminar, CanEliminar);
            LimpiarCommand = new RelayCommand(Limpiar);
            GenerarReporteCommand = new RelayCommand(GenerarReporte);
            
            CargarPedidos();
            // GenerarReporte(null); // Optional: load report on start
        }

        private void CargarPedidos()
        {
            Pedidos = new ObservableCollection<Pedido>(_repository.ListarActivos());
        }

        private void GenerarReporte(object parameter)
        {
            ReporteDetalles = new ObservableCollection<DetallePedido>(_repository.ReporteDetallesPorFecha(FiltroFechaInicio, FiltroFechaFin));
        }

        private void Guardar(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Destinatario)) return;

            if (PedidoSeleccionado == null)
            {
                var nuevo = new Pedido { FechaPedido = FechaPedido, Destinatario = Destinatario, CiudadDestino = CiudadDestino };
                _repository.Insertar(nuevo);
            }
            else
            {
                PedidoSeleccionado.FechaPedido = FechaPedido;
                PedidoSeleccionado.Destinatario = Destinatario;
                PedidoSeleccionado.CiudadDestino = CiudadDestino;
                _repository.Actualizar(PedidoSeleccionado);
            }
            CargarPedidos();
            Limpiar(null);
        }

        private bool CanEliminar(object parameter) => PedidoSeleccionado != null;

        private void Eliminar(object parameter)
        {
            if (PedidoSeleccionado != null)
            {
                _repository.EliminarLogico(PedidoSeleccionado.PedidoID);
                CargarPedidos();
                Limpiar(null);
            }
        }

        private void Limpiar(object parameter)
        {
            PedidoSeleccionado = null;
            FechaPedido = DateTime.Today;
            Destinatario = string.Empty;
            CiudadDestino = string.Empty;
        }
    }
}
