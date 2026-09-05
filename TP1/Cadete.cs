public class Cadete
{
    public int Id{get;set;}
    public string Nombre{get;set;}
    public string Direccion{get;set;}
    public string Telefono{get;set;}
    // cadete tiene un lista de pedidos
    public List<Pedido> ListadoPedidos{get;set;}

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        // inicalizo la lista vacia para q no sea nula
        ListadoPedidos = new List<Pedido>();               
    }
    public void AgregarPedido(Pedido pedido)
    {
        ListadoPedidos.Add(pedido);
    }

    public void RemoverPedido(Pedido pedido)
    {
        ListadoPedidos.Remove(pedido);
    }

    // con LINQ paara contar cuantos pedidos tienen el estado "entregado"
    //$500 por cada pedido entregado
    public double JornalACobrar()
    {
        int entregados = ListadoPedidos.Count(p => p.Estado == EstadoPedido.Entregado);
        return entregados*500;
    }


}