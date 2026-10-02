using System.Collections.Generic;
using System.IO;
public class AccesoDatosCSV : IAccesoDatos
{
    public Cadeteria ObtenerCadeteria(string ruta)
    {
        if (File.Exists(ruta))
        {
            var lineas = File.ReadAllLines(ruta);
            if(lineas.Length > 0)
            {
                var datos = lineas[0].Split(',');
                return new Cadeteria(datos[0], datos[1], new List<Cadete>());

            }
        }

        return new Cadeteria("nuevaCadeteria", "2444", new List<Cadete>());
    }

    public List<Cadete> ObtenerCadetes(string ruta)
    {
        var cadetes = new List<Cadete>();
        if (File.Exists(ruta))
        {
            var lineas = File.ReadAllLines(ruta);
            foreach(var linea in lineas)
            {
                var datos = linea.Split(',');
                if (datos.Length > 0 && int.TryParse(datos[0], out int id))
                {
                    cadetes.Add(new Cadete(id, datos[1], datos[2]));
                    
                }
            }
        }
        return cadetes;
    }
}