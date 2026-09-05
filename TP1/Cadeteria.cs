public class Cadeteria
{
    public string Nombre{get;set;}
    public string Telefono {get;set;}
    // cadeteria tiene un listado de cadetes
    public List<Cadete> ListadoCadetes{get;set;}

    public Cadeteria(string nombre, string telefono)
    {
        Nombre = nombre;
        Telefono = telefono;
        // inicializa la lista en el constructor para evitarq sea vacia
        ListadoCadetes = new List<Cadete>();
    }

    public bool AsignarPedidoCadete(Pedido pedido, int idCadete)
    {
        var cadete = ListadoCadetes.FirstOrDefault(c=> c.Id == idCadete);
        // si encuentra al cadete q coincida con el id se le asigna un pedido
        if(cadete != null)
        {
            cadete.AgregarPedido(pedido);
            return true;
        }
        // si no lo encontró retorna false
        return false;
    }
}