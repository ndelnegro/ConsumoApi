using ConsumoAPI.Models;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Net.Http.Headers;
using System.Text;

namespace ConsumoAPI.Services
{
    public class Servicio_Api:IServices_Api
    {
        private static string _usuario;
        private static string _clave;
        private static string _baseurl;
        private static string _token;





        public Servicio_Api()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _usuario = builder.GetSection("ApiSettings:usuario").Value;
            _clave = builder.GetSection("ApiSettings:clave").Value;
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task Autneticar()
        {
            var cliente  = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            var credenciales = new Credencial(){ usuario=_usuario, clave = _clave};
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), System.Text.Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Autenticacion/Validar", content);
            var json_response = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_response);
            _token = resultado.token;
        }

        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> empleado = new List<Empleado>();
            await Autneticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);
            var response = await cliente.GetAsync("api/Producto/Lista");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                empleado = resultado.lista;
            }
            return empleado;
        }
        public async Task<Empleado> Obtener(int id)
        {
            Empleado objeto = new Empleado();
            await Autneticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);
            var response = await cliente.GetAsync("api/Producto/Obtener{id}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objeto = resultado.objeto;

            }
            return objeto;
          }
        public async Task<bool> Editar(Empleado objeto)
        {
            bool respuesta = false;
            await Autneticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);
            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync("api/Producto/Editar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;

        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = false;
            await Autneticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);
            
            var response = await cliente.DeleteAsync($"api/Producto/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Guardar(Empleado objeto)
        {
                bool respuesta = false;
                await Autneticar();
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_baseurl);
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);
                var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync("api/Producto/Guardar/",content);

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }
                return respuesta;

            }

 

       
    }

}
