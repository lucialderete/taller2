public enum EstadoPedido
{
    Ingresado,
    EnPreparacion,
    EnCamino,
    Entregado,
    Cancelado
}

public class Pedido
{
    public int Id { get; set; }
    public string Obs { get; set; }
    public EstadoPedido Estado { get; set; }
    //define q el pedido tiene un cliente
    public Cliente Cliente { get; set; }

    // constructor
    public Pedido(int id, string obs, string nombreCliente, string direccionCliente, string telefonoCliente, string datosReferenciaCliente)
    {
        Id = id;
        Obs = obs;
        // cliente se crea junto con el pedido
        Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferenciaCliente);
    }

}