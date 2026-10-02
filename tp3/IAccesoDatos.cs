using System.Collections.Generic;

public interface IAccesoDatos
{
    Cadeteria ObtenerCadeteria(string ruta);
    List<Cadete> ObtenerCadetes(string ruta);
}