public class Cadeteria
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    // cadeteria tiene un listado de cadetes
    public List<Cadete> ListadoCadetes { get; set; }
    public List<Pedido> PedidosNoAsignados { get; set; }

    public Cadeteria(string nombre, string telefono)
    {
        Nombre = nombre;
        Telefono = telefono;
        // inicializa la lista en el constructor para evitarq sea vacia
        ListadoCadetes = new List<Cadete>();
        PedidosNoAsignados = new List<Pedido>();
    }

    public bool AsignarPedidoCadete(Pedido pedido, int idCadete)
    {
        var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);
        // si encuentra al cadete q coincida con el id se le asigna un pedido
        if (cadete != null)
        {
            cadete.AgregarPedido(pedido);
            return true;
        }
        // si no lo encontró retorna false
        return false;
    }

    public bool CambiarEstado(int nroPedido, int estadoNuevo)
    {
        // busca si en la lista de cadetes hay alguno q tiene asignado un pedido :p
        foreach (var cadete in ListadoCadetes)
        {
            var pedido = cadete.ListadoPedidos.FirstOrDefault(p => p.Id == nroPedido);
            if (pedido != null)
            {
                pedido.Estado = (EstadoPedido)estadoNuevo;
                return true;
            }
        }
        return false;
    }

    public bool ReasignarPedido(int nroPedido, int idCadeteOrigen, int idCadeteDestino)
    {
        // busca a los dos cadetes para reasignar
        var cadeteOrigen = ListadoCadetes.FirstOrDefault(c => c.Id == idCadeteOrigen);
        var cadeteDestino = ListadoCadetes.FirstOrDefault(c => c.Id == idCadeteDestino);

        // siambos existen en la lista
        if (cadeteOrigen != null && cadeteDestino != null)
        {
            // busca el pedido en cadete origen
            var pedido = cadeteOrigen.ListadoPedidos.FirstOrDefault(p => p.Id == nroPedido);

            if (pedido != null)
            {
                //hace el pasaje
                cadeteOrigen.ListadoPedidos.Remove(pedido);
                cadeteDestino.ListadoPedidos.Add(pedido);
                return true; 
            }
        }
        return false; // fallo la reasignación (cadetes o pedido no encontrados)
    }

}