using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Eventos.Models;
using Eventos.API.DTOs;

namespace CloudComputing.Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string baseUrl = "https://localhost:7206/api/";

            using var client = new HttpClient();
            Console.WriteLine("Iniciando pruebas de la API de Eventos...");
            // 1. GET: Obtener todos los eventos
            var getResponse = await client.GetAsync(baseUrl + "Eventos");
            if (getResponse.IsSuccessStatusCode)
            {
                var eventos = await getResponse.Content.ReadFromJsonAsync<List<EventoDTO>>();
                Console.WriteLine("GET: OK");
                foreach (var e in eventos)
                    Console.WriteLine($"{e.EventoId}: {e.Nombre}");
            }
            else
            {
                Console.WriteLine("GET: ERROR");
            }

            // 2. POST: Crear un nuevo evento
            var nuevoEvento = new EventoDTO
            {
                Nombre = "Evento de Prueba",
                Fecha = DateTime.UtcNow,
                Lugar = "Auditorio Central",
                Tipo = "Conferencia"
            };
            var postResponse = await client.PostAsJsonAsync(baseUrl + "Eventos", nuevoEvento);
            EventoDTO creado = null;
            if (postResponse.IsSuccessStatusCode)
            {
                creado = await postResponse.Content.ReadFromJsonAsync<EventoDTO>();
                Console.WriteLine($"POST: OK (ID: {creado.EventoId})");
            }
            else
            {
                Console.WriteLine("POST: ERROR");
            }
            // 1. GET: Obtener todos los eventos

            var getResponse2 = await client.GetAsync(baseUrl + "Eventos");
            if (getResponse2.IsSuccessStatusCode)
            {
                var eventos = await getResponse2.Content.ReadFromJsonAsync<List<EventoDTO>>();
                Console.WriteLine("GET: OK");
                foreach (var e in eventos)
                    Console.WriteLine($"{e.EventoId}: {e.Nombre}");
            }
            else
            {
                Console.WriteLine("GET: ERROR");
            }
            // 3. PUT: Actualizar el evento creado
            if (creado != null)
            {
                creado.Nombre = "Evento de Prueba Actualizado";
                var putResponse = await client.PutAsJsonAsync(baseUrl + $"Eventos/{creado.EventoId}", creado);
                if (putResponse.IsSuccessStatusCode)
                    Console.WriteLine("PUT: OK");
                else
                    Console.WriteLine("PUT: ERROR");
            }

            // GET antes de borrar el evento creado
            if (creado != null)
            {
                var getBeforeDelete = await client.GetAsync(baseUrl + "Eventos");
                if (getBeforeDelete.IsSuccessStatusCode)
                {
                    var eventos = await getBeforeDelete.Content.ReadFromJsonAsync<List<EventoDTO>>();
                    Console.WriteLine("GET antes de DELETE: OK");
                    foreach (var e in eventos)
                        Console.WriteLine($"{e.EventoId}: {e.Nombre}");
                }
                else
                {
                    Console.WriteLine("GET antes de DELETE: ERROR");
                }
            }

            // 4. DELETE: Eliminar el evento creado
            if (creado != null)
            {
                var deleteResponse = await client.DeleteAsync(baseUrl + $"Eventos/{creado.EventoId}");
                if (deleteResponse.IsSuccessStatusCode)
                    Console.WriteLine("DELETE: OK");
                else
                    Console.WriteLine("DELETE: ERROR");
            }
            Console.WriteLine("Presiona ENTER para continuar con las pruebas de get de participantes, ponentes y sesiones...");
            Console.ReadLine();
            // 2. GET: Obtener todos los participantes
            var getParticipantes = await client.GetAsync(baseUrl + "Participantes");
            if (getParticipantes.IsSuccessStatusCode)
            {
                var participantes = await getParticipantes.Content.ReadFromJsonAsync<List<ParticipanteDTO>>();
                Console.WriteLine("GET Participantes: OK");
                foreach (var p in participantes)
                    Console.WriteLine($"{p.ParticipanteId}: {p.Nombre}");
            }
            else
            {
                Console.WriteLine("GET Participantes: ERROR");
            }

            // 3. GET: Obtener todos los ponentes
            var getPonentes = await client.GetAsync(baseUrl + "Ponentes");
            if (getPonentes.IsSuccessStatusCode)
            {
                var ponentes = await getPonentes.Content.ReadFromJsonAsync<List<PonenteDTO>>();
                Console.WriteLine("GET Ponentes: OK");
                foreach (var p in ponentes)
                    Console.WriteLine($"{p.PonenteId}: {p.Nombre}");
            }
            else
            {
                Console.WriteLine("GET Ponentes: ERROR");
            }

            // 4. GET: Obtener todas las sesiones
            var getSesiones = await client.GetAsync(baseUrl + "Sesiones");
            if (getSesiones.IsSuccessStatusCode)
            {
                var sesiones = await getSesiones.Content.ReadFromJsonAsync<List<SesionDTO>>();
                Console.WriteLine("GET Sesiones: OK");
                foreach (var s in sesiones)
                    Console.WriteLine($"{s.SesionId} : {s.Nombre}");
            }
            else
            {
                Console.WriteLine("GET Sesiones: ERROR");
            }

            Console.WriteLine("Presiona ENTER para continuar con las pruebas de ponentes...");
            Console.ReadLine();
            // --- PRUEBA DE PONENTES ---
            var nuevoPonente = new PonenteDTO
            {
                Nombre = "Ponente de Prueba",
                Correo = "ponente@correo.com",
                Telefono = "555-0000"
            };

            // POST: Crear un ponente
            var postPonente = await client.PostAsJsonAsync(baseUrl + "Ponentes", nuevoPonente);
            PonenteDTO? ponenteCreado = null;
            if (postPonente.IsSuccessStatusCode)
            {
                ponenteCreado = await postPonente.Content.ReadFromJsonAsync<PonenteDTO>();
                Console.WriteLine($"POST Ponente: OK (ID: {ponenteCreado.PonenteId})");
            }
            else
            {
                var error = await postPonente.Content.ReadAsStringAsync();
                Console.WriteLine($"POST Ponente: ERROR - {error}");
            }

            // GET: Obtener todos los ponentes
            var getPonentesPrueba = await client.GetAsync(baseUrl + "Ponentes");
            if (getPonentes.IsSuccessStatusCode)
            {
                var ponentes = await getPonentesPrueba.Content.ReadFromJsonAsync<List<PonenteDTO>>();
                Console.WriteLine("GET Ponentes: OK");
                foreach (var p in ponentes)
                    Console.WriteLine($"{p.PonenteId}: {p.Nombre} - {p.Correo} - {p.Telefono}");
            }
            else
            {
                Console.WriteLine("GET Ponentes: ERROR");
            }

            // PUT: Actualizar el ponente creado
            if (ponenteCreado != null)
            {
                ponenteCreado.Nombre = "Ponente de Prueba Actualizado";
                var putPonente = await client.PutAsJsonAsync(baseUrl + $"Ponentes/{ponenteCreado.PonenteId}", ponenteCreado);
                Console.WriteLine(putPonente.IsSuccessStatusCode
                    ? "PUT Ponente: OK"
                    : "PUT Ponente: ERROR");

                // GET: Obtener el ponente actualizado
                var getPonenteActualizado = await client.GetAsync(baseUrl + $"Ponentes/{ponenteCreado.PonenteId}");
                if (getPonenteActualizado.IsSuccessStatusCode)
                {
                    var ponenteActualizado = await getPonenteActualizado.Content.ReadFromJsonAsync<PonenteDTO>();
                    Console.WriteLine($"GET Ponente actualizado: {ponenteActualizado.PonenteId}: {ponenteActualizado.Nombre} - {ponenteActualizado.Correo} - {ponenteActualizado.Telefono}");
                }
                else
                {
                    Console.WriteLine("GET Ponente actualizado: ERROR");
                }
            }

            // DELETE: Eliminar el ponente creado
            if (ponenteCreado != null)
            {
                var deletePonente = await client.DeleteAsync(baseUrl + $"Ponentes/{ponenteCreado.PonenteId}");
                Console.WriteLine(deletePonente.IsSuccessStatusCode
                    ? "DELETE Ponente: OK"
                    : "DELETE Ponente: ERROR");

                // GET: Obtener todos los ponentes
                var getPonentesPruebaEliminacion = await client.GetAsync(baseUrl + "Ponentes");
                if (getPonentesPruebaEliminacion.IsSuccessStatusCode)
                {
                    var ponentes = await getPonentesPruebaEliminacion.Content.ReadFromJsonAsync<List<PonenteDTO>>();
                    Console.WriteLine("GET Ponentes: OK");
                    foreach (var p in ponentes)
                        Console.WriteLine($"{p.PonenteId}: {p.Nombre} - {p.Correo} - {p.Telefono}");
                }
                else
                {
                    Console.WriteLine("GET Ponentes: ERROR");
                }
            }
            Console.WriteLine("Presiona ENTER para salir...");
            Console.ReadLine();
        }
    }
}