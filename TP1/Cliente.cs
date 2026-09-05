public class Cliente
{
    //principío de ocultamiento de la inf mediante propiedades
    public string Nombre{get;set;}
    public string Direccion{get;set;}
    public string Telefono{get;set;}
    public string DatosReferencia{get;set;}

    // constructor
    public Cliente(string nombre, string direccion, string telefono, string datosReferencia)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        DatosReferencia = datosReferencia;
    }

}