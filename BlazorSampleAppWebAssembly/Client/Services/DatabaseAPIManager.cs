using BlazorSampleAppWebAssembly.Shared.Models;
using DatabaseAccessLib;
using Newtonsoft.Json;
using Radzen;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSampleAppWebAssembly.Client.Services;

public class DatabaseAPIManager
{
    HttpClient http;

    static string _controllerName = "databaseapi";

    public DatabaseAPIManager(HttpClient _http) 
    {
        http = _http;
    }

    private JsonSerializerOptions? GetPostJsonOptions()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        return options;
    }

    public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
    {
        try
        {
            string url = $"{_controllerName}/GetAllEmployees";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<EmployeeDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<EmployeeDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersByEmployeeID(int id)
    {
        try
        {
            var url = $"{_controllerName}/GetOrdersByEmployeeID/{id}";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<OrderDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<OrderDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }



}
