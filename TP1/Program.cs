
using System;
using System.Linq;

// carga de datos de la cadeteria
Cadeteria cadeteria = HelperCsv.CargarCadeteria("cadeteria.csv");

if (cadeteria == null)
{
    Console.WriteLine("no se pudo cargar la cadeteria correctamente");
    Console.WriteLine("creando una nueva por defecto");
    cadeteria = new Cadeteria("Cadeteria YaPedidos", "3815154871");
}

// carga de datos de los cadetes
cadeteria.ListadoCadetes = HelperCsv.CargarCadetes("cadetes.csv");

if (cadeteria.PedidosNoAsignados == null)
{
    cadeteria.PedidosNoAsignados = new System.Collections.Generic.List<Pedido>();
}

int nroPedidoActual = 1;
int opcion = 0;

// menu interactivo
while (opcion != 5)
{
    Console.WriteLine($"---Sistema de Cadeteria de {cadeteria.Nombre}---");
    Console.WriteLine("Seleccione una opcion: ");
    Console.WriteLine("1) dar de alta un pedido");
    Console.WriteLine("2) asignar pedido a un cadete");
    Console.WriteLine("3) cambiar de estado un pedido");
    Console.WriteLine("4) reasignar pedido a otro cadete");
    Console.WriteLine("5) finalizar y mostrar informe");
    Console.WriteLine("6) mostrar pedidos y cadetes");

    if (int.TryParse(Console.ReadLine(), out opcion))
    {
        switch (opcion)
        {

            case 1:
                Console.WriteLine("Observaciones: ");
                string obs = Console.ReadLine();

                Console.WriteLine("nombre del cliente");
                string nombre = Console.ReadLine();

                Console.WriteLine("direccion del cliente");
                string direccion = Console.ReadLine();

                Console.WriteLine("telefono del cliente");
                string telefono = Console.ReadLine();

                Console.WriteLine("referencia de direccion");
                string referencia = Console.ReadLine();

                // crear pedido
                Pedido nuevoPedido = new Pedido(
                    nroPedidoActual++,
                    obs,
                    nombre,
                    direccion,
                    telefono,
                    referencia
                );

                cadeteria.PedidosNoAsignados.Add(nuevoPedido);

                Console.WriteLine("pedido creado !!");
                break;



            case 2:
                Console.WriteLine("pedidos sin asignar: ");

                foreach (var p in cadeteria.PedidosNoAsignados)
                {
                    Console.WriteLine($"nro: {p.Id} - Cliente: {p.Cliente.Nombre}");
                }

                Console.WriteLine("ingrese el numero de pedido para asignar: ");
                int nroAsignar = int.Parse(Console.ReadLine());

                Console.WriteLine("Cadetes disponibles: ");

                foreach (var c in cadeteria.ListadoCadetes)
                {
                    Console.WriteLine($"Id: {c.Id} - Nombre: {c.Nombre}");
                }

                Console.WriteLine("Ingrese el id del cadete");
                int idCadete = int.Parse(Console.ReadLine());

                var pedidoAsignar = cadeteria.PedidosNoAsignados
                    .FirstOrDefault(p => p.Id == nroAsignar);

                if (pedidoAsignar != null &&
                    cadeteria.AsignarPedidoCadete(pedidoAsignar, idCadete))
                {
                    cadeteria.PedidosNoAsignados.Remove(pedidoAsignar);

                    Console.WriteLine("pedido asignado con exito");
                }
                else
                {
                    Console.WriteLine("error al asignar el pedido");
                }

                break;



            case 3:
                Console.WriteLine("ingrese el id del cadete que tiene el pedido: ");
                int idCadeteInicial = int.Parse(Console.ReadLine());

                var cad = cadeteria.ListadoCadetes
                    .FirstOrDefault(c => c.Id == idCadeteInicial);

                if (cad != null)
                {
                    Console.WriteLine("ingrese el numero del pedido");
                    int nroP = int.Parse(Console.ReadLine());

                    var pedid = cad.ListadoPedidos
                        .FirstOrDefault(p => p.Id == nroP);

                    if (pedid != null)
                    {
                        Console.WriteLine("Estados:");
                        Console.WriteLine("0: Ingresado");
                        Console.WriteLine("1: En preparacion");
                        Console.WriteLine("2: En camino");
                        Console.WriteLine("3: Entregado");
                        Console.WriteLine("4: Cancelado");

                        Console.Write("Seleccione el nuevo estado: ");
                        int estado = int.Parse(Console.ReadLine());

                        if (estado >= 0 && estado <= 4)
                        {
                            pedid.Estado = (EstadoPedido)estado;

                            Console.WriteLine(
                                $"Estado actualizado a {pedid.Estado}"
                            );
                        }
                        else
                        {
                            Console.WriteLine("Estado invalido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontro el pedido en ese cadete.");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontro el cadete.");
                }

                break;



            case 4:
                Console.WriteLine("--- reasignar pedido ---");

                Console.WriteLine("Ingrese el ID del Pedido a reasignar: ");
                int nroReasignar = int.Parse(Console.ReadLine());

                Console.WriteLine(
                    "ingrese el ID del Cadete actual (el que lo tiene ahora): "
                );
                int idOrigen = int.Parse(Console.ReadLine());

                Console.WriteLine("\nCadetes disponibles para recibir el pedido:");

                foreach (var c in cadeteria.ListadoCadetes)
                {
                    Console.WriteLine($"Id: {c.Id} - Nombre: {c.Nombre}");
                }

                Console.WriteLine("Ingrese el ID del NUEVO Cadete (destino): ");
                int idDestino = int.Parse(Console.ReadLine());

                if (cadeteria.ReasignarPedido(
                    nroReasignar,
                    idOrigen,
                    idDestino))
                {
                    Console.WriteLine("pedido reasignado");
                }
                else
                {
                    Console.WriteLine("error: No se pudo reasignar");
                }

                break;


            case 5:
                Console.WriteLine("informe final");

                int totalEnvios = 0;
                double totalJornada = 0;

                foreach (var c in cadeteria.ListadoCadetes)
                {
                    int enviosCadete = c.ListadoPedidos
                        .Count(p => p.Estado == EstadoPedido.Entregado);

                    double montoCadete = c.JornalACobrar();

                    totalEnvios += enviosCadete;
                    totalJornada += montoCadete;

                    Console.WriteLine(
                        $"Cadete: {c.Nombre} | " +
                        $"envios entregados: {enviosCadete} | " +
                        $"monto total: ${montoCadete}"
                    );
                }

                double promedioEnvio = 0;

                if (cadeteria.ListadoCadetes.Count != 0)
                {
                    promedioEnvio = (double)totalEnvios /
                                    cadeteria.ListadoCadetes.Count;
                }

                Console.WriteLine("--------------------------------");
                Console.WriteLine(
                    $"Total de envíos en la jornada: {totalEnvios}"
                );
                Console.WriteLine(
                    $"Total recaudado a pagar: ${totalJornada}"
                );
                Console.WriteLine(
                    $"Promedio de envíos por cadete: {promedioEnvio}"
                );

                break;
            case 6:
                Console.WriteLine("--Pedidos y cadetes--");
                foreach (var pedido in cadeteria.PedidosNoAsignados)
                {
                    Console.WriteLine($"Pedido: {pedido.Id} | " +
                    $"Cliente: {pedido.Cliente.Nombre} | " +
                    $"Cadete: SIN ASIGNAR");
                }
                foreach (var cadete in cadeteria.ListadoCadetes)
                {
                    foreach (var pedido in cadete.ListadoPedidos)
                    {
                        Console.WriteLine(
                            $"Pedido: {pedido.Id} | " +
                            $"Cliente: {pedido.Cliente.Nombre} | " +
                            $"Cadete: {cadete.Nombre}"
                        );

                    }
                }
                break;

        }
    }
}
