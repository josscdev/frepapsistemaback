using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Security.Model
{
    public class JwtModel
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public int KeyLength { get; set; } // Agrega esta propiedad para almacenar la longitud de la clave

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar Token",
                        result = ""
                    };
                }

                var dniClaim = identity.Claims.FirstOrDefault(x => x.Type == "dni");
                string dni = dniClaim?.Value;

                // Obtenemos el token completo del encabezado "Authorization"
                var token = identity.FindFirst("Authorization")?.Value?.Replace("Bearer ", "");

                return new
                {
                    success = true,
                    message = "exito",
                    result = "todo correcto",
                    dni,
                    token // Agregamos el token al resultado
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "catcha" + ex.Message,
                    result = ""
                };
            }

        }
    }
}
