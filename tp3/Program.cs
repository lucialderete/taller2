class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("---SISTEMA DE CADETERIA---");


        // menu

        //seleccionar el tipo de dato de los archivos
        Console.WriteLine("seleccione el tipo de datos: ");
        Console.WriteLine("1) CSV");
        Console.WriteLine("2) JSON");

        Console.Write("opcion: ");

        string opcionDatos = Console.ReadLine();

        IAccesoDatos accesoDatos;

        if (opcionDatos == "1")
        {
            accesoDatos = new AccesoDatosCSV();
        }
        else if (opcionDatos == "2")
        {
            accesoDatos = new AccesoDatosJSON();
        }
        else
        {
            Console.WriteLine("seleccione una opcion valida xfa");
            return;

        }

        // carga de datos
        string rutaCadeteria = "cadeteria.csv";
        string rutaCadetes = "cadetes.csv";

        Cadeteria cadeteria = accesoDatos.ObtenerCadeteria(rutaCadeteria);
        List<Cadete> cadetes = accesoDatos.ObtenerCadetes(rutaCadetes);

        cadeteria = new Cadeteria(cadeteria.Nombre, cadeteria.Telefono, cadetes);

        int opcion;

        do
        {
            Console.WriteLine("---MENU---");
            Console.WriteLine("1) dar de alta un pedido");
            Console.WriteLine("2) asignar pedido a cadete");
            Console.WriteLine("3) cambiar estado de pedido");
            Console.WriteLine("4) reasignar pedido");
            Console.WriteLine("5) generar informe");
            Console.WriteLine("6) salir del programa :( )");

            int.TryParse(Console.ReadLine(), out opcion);

            switch (opcion)
            {
                case 1:
                    AltaPedido(cadeteria);
                    break;
                case 2:
                    AsignarPedido(cadeteria);
                    break;
                case 3:
                    CambiarEstado(cadeteria);
                    break;
                case 4:
                    ReasignarPedido(cadeteria);
                    break;
                case 5:
                    Console.WriteLine(cadeteria.GenerarInforme());
                    break;
                case 6:
                    Console.WriteLine("abandonando del programa :( )");
                    break;
                default:
                    Console.WriteLine("seleccione una opcion valida");
                    break;
            }

        } while (opcion != 6);
    }

    static void AltaPedido(Cadeteria cadeteria)
    {
        Console.WriteLine("numero de pedido: ");
        int.TryParse(Console.ReadLine(), out int num);

        Console.WriteLine("observacion: ");
        string observacion = Console.ReadLine();

        Console.WriteLine("nombre cliente: ");
        string clienteNombre = Console.ReadLine();

        Console.WriteLine("direccion cliente: ");
        string clienteDireccion = Console.ReadLine();

        Console.WriteLine("telefono cliente: ");
        string clienteTelefono = Console.ReadLine();

        Console.WriteLine("referencia cliente: ");
        string clienteReferencia = Console.ReadLine();

        bool resultado = cadeteria.AltaPedido(num, observacion, clienteNombre, clienteDireccion, clienteTelefono, clienteReferencia);

        if (resultado)
        {
            Console.WriteLine($"pedido numero {num} dado de alta correctamente");
        }
        else
        {
            Console.WriteLine($"pedido numero {num} no se pudo dar de alta");
        }
    }

    static void AsignarPedido(Cadeteria cadeteria)
    {
        Console.Write("ID del pedido: ");
        int.TryParse(Console.ReadLine(), out int idPedido);

        Console.Write("ID del cadete: ");
        int.TryParse(Console.ReadLine(), out int idCadete);

        bool resultado = cadeteria.AsignarCadete(
            idCadete,
            idPedido
        );

        if (resultado)
        {
            Console.WriteLine("Pedido asignado correctamente.");
        }
        else
        {
            Console.WriteLine("No se pudo asignar el pedido.");
        }

    }
    static void CambiarEstado(Cadeteria cadeteria)
    {
        Console.Write("ID del pedido: ");
        int.TryParse(Console.ReadLine(), out int idPedido);

        Console.WriteLine();
        Console.WriteLine("Estados:");
        Console.WriteLine("0. Pendiente");
        Console.WriteLine("1. Entregado");
        Console.WriteLine("2. Cancelado");

        Console.Write("Seleccione el nuevo estado: ");
        int.TryParse(Console.ReadLine(), out int estado);

        if (estado < 0 || estado > 2)
        {
            Console.WriteLine("Estado inválido.");
            return;
        }

        EstadoPedido nuevoEstado = (EstadoPedido)estado;

        bool resultado = cadeteria.CambiarEstado(
            idPedido,
            nuevoEstado
        );

        if (resultado)
        {
            Console.WriteLine("Estado actualizado correctamente.");
        }
        else
        {
            Console.WriteLine("No se encontró el pedido.");
        }
    }


    static void ReasignarPedido(Cadeteria cadeteria)
    {
        Console.Write("ID del pedido: ");
        int.TryParse(Console.ReadLine(), out int idPedido);

        Console.Write("ID del nuevo cadete: ");
        int.TryParse(Console.ReadLine(), out int idCadete);

        bool resultado = cadeteria.ReasignarPedido(
            idPedido,
            idCadete
        );

        if (resultado)
        {
            Console.WriteLine("Pedido reasignado correctamente.");
        }
        else
        {
            Console.WriteLine("No se pudo reasignar el pedido.");
        }
    }
}