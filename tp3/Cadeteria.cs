using System.Text;

public class Cadeteria
{
    public string Nombre { get; }
    public string Telefono { get; }

    // cadeteria tiene un listado de cadetes
    private List<Cadete> ListadoCadetes;

    // cadeteria tiene un listado de pedidos
    private List<Pedido> ListadoPedidos;

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes)
    {
        Nombre = nombre;
        Telefono = telefono;
        ListadoCadetes = cadetes;
        ListadoPedidos = new List<Pedido>();
    }

    public bool AltaPedido(int num, string obs, string clienteNombre, string clienteDireccion, string clienteTelefono, string clienteReferencia)
    {
        ListadoPedidos.Add(new Pedido(num, obs, clienteNombre, clienteDireccion, clienteTelefono, clienteReferencia));

        return true;
    }

    public bool AsignarCadete(int idCadete, int idPedido)
    {
        var pedido = ListadoPedidos.FirstOrDefault(p => p.Id == idPedido);
        var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

        if (cadete == null || pedido == null)
        {
            return false;
        }
        pedido.AsignarCadete(cadete);

        return true;
    }

    public bool CambiarEstado(int nroPedido, EstadoPedido nuevoEstado)
    {
        var pedido = ListadoPedidos.FirstOrDefault(p => p.Id == nroPedido);

        if (pedido == null)
        {
            return false;
        }

        pedido.CambiarEstado(nuevoEstado);

        return true;
    }


    public bool ReasignarPedido(int numPedido, int idCadeteN)
    {
        return AsignarCadete(idCadeteN, numPedido);
    }

    public float JornalACobrar(int idCadete)
    {
        int entregados = ListadoPedidos.Count(p => p.CadeteAsignado != null && p.CadeteAsignado.Id == idCadete && p.Estado == EstadoPedido.Entregado);

        return entregados * 500f;
    }

    public string GenerarInforme()
    {
        string informe = $"\n--- INFORME DE LA JORNADA: {Nombre} ---";

        foreach (var cadete in ListadoCadetes)
        {
            float jornal = JornalACobrar(cadete.Id);
            int envios = (int)(jornal / 500);
            informe += $"Cadete: {cadete.Nombre} | Envíos: {envios} | Jornal: ${jornal}\n";
        }

        var pedidosEntregados = ListadoPedidos.Where(p => p.Estado == EstadoPedido.Entregado).ToList();

        informe += "-----------------------------\n";
        informe += $"Total envíos: {pedidosEntregados.Count}\n";
        informe += $"Monto total a pagar: ${pedidosEntregados.Count * 500}\n";

        return informe;
    }

}