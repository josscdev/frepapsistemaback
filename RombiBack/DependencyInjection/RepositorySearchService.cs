using System.Reflection;

namespace RombiBack.DependencyInjection
{
    public class RepositorySearchService
    {

        //public static List<InterfaceImplementacion> ListarInterfacesEImplementaciones()
        //{
        //    List<InterfaceImplementacion> resultados = new List<InterfaceImplementacion>();

        //    // Obtiene la carpeta actual donde se encuentra el ensamblado principal
        //    string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

        //    // Busca todos los ensamblados en la carpeta y subcarpetas
        //    string[] archivosEnsamblado = Directory.GetFiles(directorioBase, "*.dll", SearchOption.AllDirectories);

        //    foreach (var archivoEnsamblado in archivosEnsamblado)
        //    {
        //        try
        //        {
        //            Assembly ensamblado = Assembly.LoadFrom(archivoEnsamblado);

        //            // Filtra los tipos que pertenecen a la capa de servicios
        //            var servicios = ensamblado.GetTypes()
        //                .Where(type => type.Namespace != null && type.Namespace.StartsWith("EccoBack.Services"))
        //                .ToList();

        //            // Itera sobre los servicios y lista las interfaces y sus implementaciones
        //            foreach (var servicio in servicios)
        //            {
        //                if (servicio.IsClass)
        //                {
        //                    // El tipo es una implementación, identificamos la interfaz correspondiente
        //                    var interfacesImplementadas = servicio.GetInterfaces()
        //                        .Where(i => i.Namespace != null && i.Namespace.StartsWith("EccoBack.Services"))
        //                        .ToList();

        //                    foreach (var interfaz in interfacesImplementadas)
        //                    {
        //                        resultados.Add(new InterfaceImplementacion
        //                        {
        //                            Interfaz = interfaz.Name,
        //                            Implementacion = servicio.Name
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error al cargar el ensamblado {archivoEnsamblado}: {ex.Message}");
        //        }
        //    }

        //    return resultados;
        //}

        //public static List<RepositoryImplementacion> ListarRepositoryEImplementaciones()
        //{
        //    List<RepositoryImplementacion> resultados = new List<RepositoryImplementacion>();

        //    // Obtiene la carpeta actual donde se encuentra el ensamblado principal
        //    string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

        //    // Busca todos los ensamblados en la carpeta y subcarpetas
        //    string[] archivosEnsamblado = Directory.GetFiles(directorioBase, "*.dll", SearchOption.AllDirectories);

        //    foreach (var archivoEnsamblado in archivosEnsamblado)
        //    {
        //        try
        //        {
        //            Assembly ensamblado = Assembly.LoadFrom(archivoEnsamblado);

        //            // Filtra los tipos que pertenecen a la capa de repository
        //            var repositorios = ensamblado.GetTypes()
        //                .Where(type => type.Namespace != null && type.Namespace.StartsWith("EccoBack.Repository"))
        //                .ToList();

        //            // Itera sobre los repositorios y lista las interfaces y sus implementaciones
        //            foreach (var repositorio in repositorios)
        //            {
        //                if (repositorio.IsClass)
        //                {
        //                    // El tipo es una implementación, identificamos la interfaz correspondiente
        //                    var interfacesImplementadas = repositorio.GetInterfaces()
        //                        .Where(i => i.Namespace != null && i.Namespace.StartsWith("EccoBack.Repository"))
        //                        .ToList();

        //                    foreach (var interfaz in interfacesImplementadas)
        //                    {
        //                        resultados.Add(new RepositoryImplementacion
        //                        {
        //                            Interfaz = interfaz.Name,
        //                            Implementacion = repositorio.Name
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error al cargar el ensamblado {archivoEnsamblado}: {ex.Message}");
        //        }
        //    }

        //    return resultados;
        //}

        //public class RepositoryImplementacion
        //{
        //    public string Interfaz { get; set; }
        //    public string Implementacion { get; set; }
        //}

        //public class InterfaceImplementacion
        //{
        //    public string Interfaz { get; set; }
        //    public string Implementacion { get; set; }
        //}

        public static void RegistrarRepository(IServiceCollection services)
        {
            // Obtiene la carpeta actual donde se encuentra el ensamblado principal
            string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

            // Busca todos los ensamblados en la carpeta y subcarpetas
            string[] archivosEnsamblado = Directory.GetFiles(directorioBase, "*.dll", SearchOption.AllDirectories);

            foreach (var archivoEnsamblado in archivosEnsamblado)
            {
                try
                {
                    Assembly ensamblado = Assembly.LoadFrom(archivoEnsamblado);

                    // Filtra los tipos que pertenecen a la capa de repository
                    var repositorios = ensamblado.GetTypes()
                        .Where(type => type.Namespace != null && type.Namespace.StartsWith("RombiBack.Repository"))
                        .ToList();

                    // Itera sobre los repositorios y empareja con las interfaces
                    foreach (var repositorio in repositorios)
                    {
                        if (repositorio.IsClass)
                        {
                            // El tipo es una implementación, identificamos la interfaz correspondiente
                            var interfacesImplementadas = repositorio.GetInterfaces()
                                .Where(i => i.Namespace != null && i.Namespace.StartsWith("RombiBack.Repository"))
                                .ToList();

                            foreach (var interfaz in interfacesImplementadas)
                            {
                                // Registra la interfaz del repositorio con su implementación del repositorio
                                services.AddScoped(interfaz, repositorio);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar el ensamblado {archivoEnsamblado}: {ex.Message}");
                }
            }
        }


        public static void RegistrarServices(IServiceCollection services)
        {
            // Obtiene la carpeta actual donde se encuentra el ensamblado principal
            string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

            // Busca todos los ensamblados en la carpeta y subcarpetas
            string[] archivosEnsamblado = Directory.GetFiles(directorioBase, "*.dll", SearchOption.AllDirectories);

            foreach (var archivoEnsamblado in archivosEnsamblado)
            {
                try
                {
                    Assembly ensamblado = Assembly.LoadFrom(archivoEnsamblado);

                    // Filtra los tipos que pertenecen a la capa de servicios
                    var servicios = ensamblado.GetTypes()
                        .Where(type => type.Namespace != null && type.Namespace.StartsWith("RombiBack.Services"))
                        .ToList();

                    // Itera sobre los servicios y empareja con las interfaces
                    foreach (var servicio in servicios)
                    {
                        if (servicio.IsClass)
                        {
                            // El tipo es una implementación, identificamos la interfaz correspondiente
                            var interfacesImplementadas = servicio.GetInterfaces()
                                .Where(i => i.Namespace != null && i.Namespace.StartsWith("RombiBack.Services"))
                                .ToList();

                            foreach (var interfaz in interfacesImplementadas)
                            {
                                // Registra la interfaz del servicio con su implementación del servicio
                                services.AddScoped(interfaz, servicio);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar el ensamblado {archivoEnsamblado}: {ex.Message}");
                }
            }
        }
    }
}
