using System.Runtime.CompilerServices;

namespace Eventos.API.Consumer
{
    public static class Crud<T>
    {
        public static string EndPoint { get; set; }

        public static async Task<T> Get(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(EndPoint + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                    return data;
                }
                else
                {
                    throw new Exception("Error fetching data");
                }
            }
        }

        public static async Task<List<T>> GetAll()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(EndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
                    return data;
                }
                else
                {
                    throw new Exception("Error fetching data");
                }
            }
        }

        public static async Task<T> Create(T item)
        {
            using (var client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(EndPoint, content);

                json = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                    return data;
                }
                else
                {
                    throw new Exception("Error posting data");
                }
            }
        }

        public static async Task<bool> Update(int id, T item)
        {
            using (var client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync(EndPoint + "/" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error updating data: " + response.StatusCode);
                }
            }
        }

        public static async Task<bool> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(EndPoint + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error deleting data: " + response.StatusCode);
                }
            }
        }
    }
}