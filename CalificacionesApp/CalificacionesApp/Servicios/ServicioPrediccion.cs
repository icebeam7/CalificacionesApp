using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CalificacionesApp.Helpers;
using CalificacionesApp.Modelos;
using Newtonsoft.Json;

namespace CalificacionesApp.Servicios
{
    public static class ServicioPrediccion
    {
        public async static Task<string> PredecirCalificacion(string json)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new 
                        AuthenticationHeaderValue("Bearer", Constantes.PrediccionKey);
                    cliente.BaseAddress = new Uri(Constantes.PrediccionURL);

                    var content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await cliente.PostAsync("", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var resultado = await response.Content.ReadAsStringAsync();
                        var prediccion = JsonConvert.DeserializeObject<Prediccion>(resultado);

                        return prediccion.Results.output1.value.Values[0][20];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
