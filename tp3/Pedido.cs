public enum EstadoPedido
{
    Pendiente,
    Entregado,
    Cancelado
}

public class Pedido
{
    public int Id{get;}
    public string Observaciones{get;}
    public Cliente Cliente{get;}
    private EstadoPedido estado;
    public EstadoPedido Estado{get{return estado;}}


    // referencia al objeto cadete
    private Cadete cadeteAsignado;
    public Cadete CadeteAsignado{get{return cadeteAsignado;}}

    
    // constructor
    public Pedido(int id, string observaciones, string nombreCliente, string direccionCliente, string telefonoCliente, string referenciaCliente)
    {
        Id = id;
        Observaciones = observaciones;
        Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, referenciaCliente);
        estado = EstadoPedido.Pendiente;
        cadeteAsignado = null;
    }

    // cambiar estado 
    public void CambiarEstado(EstadoPedido nuevoEstado)
    {
        estado = nuevoEstado;
    }

    public void AsignarCadete(Cadete cadete)
    {
        cadeteAsignado = cadete;
    }
}