using System;
using System.IO;
using System.Collections.Generic;

public class HelperCsv
{

    // carga de datos de cadeteria 


    public static Cadeteria CargarCadeteria(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo))
        {
            return null;
        }
        else
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);
            if (lineas.Length > 0)
            {
                string[] datos = lineas[0].Split(',');
                return new Cadeteria(datos[0], datos[1]);
            }
            return null;
        }
    }

    public static List<Cadete> CargarCadetes(string rutaArchivo)
    {
        List<Cadete> cadetes = new List<Cadete>();
        if (!File.Exists(rutaArchivo))
        {
            return cadetes;
        }
        else
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);
            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');
                int id = int.Parse(datos[0]);
                Cadete nuevoCadete = new Cadete(id, datos[1], datos[2], datos[3]);
                cadetes.Add(nuevoCadete);
            }
            return cadetes;
        }
    }
}
