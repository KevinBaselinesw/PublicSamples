using Microsoft.AspNetCore.Mvc;
using BlazorSampleAppWebAssembly.Shared.Models;
using DatabaseAccessLib;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KMScheduler.Server.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class DatabaseAPIController : ControllerBase
{
    IDataAccessAPI database {  get; set; }
    public DatabaseAPIController(IDataAccessAPI database)
    {
        this.database = database;   
    }


    [HttpGet]
    public async Task<ActionResult<APIListOfEntityResponse<EmployeeDTO>>> GetAllEmployees()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllEmployees();
            return Ok(new APIListOfEntityResponse<EmployeeDTO>()
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(500);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<APIListOfEntityResponse<OrderDTO>>> GetOrdersByEmployeeID(int Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetOrdersByEmployeeID(Id);
            if (result != null)
            {
                return Ok(new APIListOfEntityResponse<OrderDTO>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIListOfEntityResponse<OrderDTO>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "order records not found" },
                    Data = null
                });
            }
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<ActionResult<APIListOfEntityResponse<CategoryDTO>>> GetAllProductCategories()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllProductCategories();
            return Ok(new APIListOfEntityResponse<CategoryDTO>()
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(500);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<APIListOfEntityResponse<ProductDTO>>> GetProductsByCategoryID(int Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetProductsByCategoryID(Id);
            if (result != null)
            {
                return Ok(new APIListOfEntityResponse<ProductDTO>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIListOfEntityResponse<ProductDTO>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "order records not found" },
                    Data = null
                });
            }
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(500);
        }
    }

}
