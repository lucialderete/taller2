public class Cliente
{
    public string Nombre{get;}
    public string Direccion{get;}
    public string Telefono{get;}
    public string DatosReferencia{get;}


    //constructor
    public Cliente(string nombre, string direccion, string telefono, string datosref)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        DatosReferencia = datosref;
    }
    
}