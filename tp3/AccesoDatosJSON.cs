using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class AccesoDatosJSON : IAccesoDatos
{
    public Cadeteria ObtenerCadeteria(string ruta)
    {
        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);

            var datos = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            
            if(datos != null && datos.ContainsKey("Nombre") && datos.ContainsKey("Telefono"))
            {
                return new Cadeteria(datos["Nombre"], datos["Telefono"], new List<Cadete>());
            }
        }
        return new Cadeteria("nuevaCadeteria", "1234", new List<Cadete>());
    }

    public List<Cadete> ObtenerCadetes(string ruta)
    {
        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
        }
        return new List<Cadete>();
    }
}