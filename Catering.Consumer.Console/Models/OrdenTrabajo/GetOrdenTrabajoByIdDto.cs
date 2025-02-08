using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catering.Consumer.Console.Models.OrdenTrabajo
{
    public class GetOrdenTrabajoByIdDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("usuarioCocineroNombre")]
        public string UsuarioCocineroNombre { get; set; }
        [JsonPropertyName("recetaNombre")]
        public string RecetaNombre { get; set; }
        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }
        [JsonPropertyName("estado")]
        public string Estado { get; set; }
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }
    }
}
