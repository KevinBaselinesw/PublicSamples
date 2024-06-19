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

    public async Task<IEnumerable<CategoryDTO>> GetAllProductCategories()
    {
        try
        {
            string url = $"{_controllerName}/GetAllProductCategories";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<CategoryDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<CategoryDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryID(int id)
    {
        try
        {
            var url = $"{_controllerName}/GetProductsByCategoryID/{id}";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<ProductDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<ProductDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public async Task<IEnumerable<ProductDTO>> GetAllProducts()
    {
        try
        {
            string url = $"{_controllerName}/GetAllProducts";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<ProductDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<ProductDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<CategoryDTO>> GetProductCategoriesByID(int id)
    {
        try
        {
            var url = $"{_controllerName}/GetProductCategoriesByID/{id}";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<CategoryDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<CategoryDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<SupplierDTO>> GetSuppliersByID(int id)
    {
        try
        {
            var url = $"{_controllerName}/GetSuppliersByID/{id}";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<SupplierDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<SupplierDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
    {
        try
        {
            string url = $"{_controllerName}/GetAllCustomers";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<CustomerDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<CustomerDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<OrderWithSubtotalDTO>> GetAllOrdersWithSubtotalsByCustomerID(string id)
    {
        try
        {
            var url = $"{_controllerName}/GetAllOrdersWithSubtotalsByCustomerID/{id}";
            var result = await http.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<APIListOfEntityResponse<OrderWithSubtotalDTO>>(responseBody);
            if (response.Success)
                return response.Data;
            else
                return new List<OrderWithSubtotalDTO>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}
