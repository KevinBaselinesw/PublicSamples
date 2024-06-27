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

    [HttpPost]
    public async Task<ActionResult<APIEntityResponse<OrderDTO>>> CreateNewOrder(OrderDTO newOrder)
    {
        try
        {
            await Task.Delay(0);

            var result = database.CreateNewOrder(newOrder);
            if (result != null)
            {
                return Ok(new APIEntityResponse<OrderDTO>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIEntityResponse<OrderDTO>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "unable to create order record" },
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


    [HttpGet]
    public async Task<ActionResult<APIListOfEntityResponse<ProductDTO>>> GetAllProducts()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllProducts();
            return Ok(new APIListOfEntityResponse<ProductDTO>()
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
    public async Task<ActionResult<APIListOfEntityResponse<CategoryDTO>>> GetProductCategoriesByID(int Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetProductCategoriesByID(Id);
            if (result != null)
            {
                return Ok(new APIListOfEntityResponse<CategoryDTO>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIListOfEntityResponse<CategoryDTO>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "product records not found" },
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


    [HttpGet("{Id}")]
    public async Task<ActionResult<APIListOfEntityResponse<SupplierDTO>>> GetSuppliersByID(int Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetSuppliersByID(Id);
            if (result != null)
            {
                return Ok(new APIListOfEntityResponse<SupplierDTO>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIListOfEntityResponse<SupplierDTO>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "supplier records not found" },
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
    public async Task<ActionResult<APIListOfEntityResponse<CustomerDTO>>> GetAllCustomers()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllCustomers();
            return Ok(new APIListOfEntityResponse<CustomerDTO>()
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
    public async Task<ActionResult<APIListOfEntityResponse<OrderWithSubtotalDTO>>> GetAllOrdersWithSubtotalsByCustomerID(string Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllOrdersWithSubtotalsByCustomerID(Id);
            if (result != null)
            {
                return Ok(new APIListOfEntityResponse<OrderWithSubtotalDTO>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIListOfEntityResponse<OrderWithSubtotalDTO>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "supplier records not found" },
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
    public async Task<ActionResult<APIListOfEntityResponse<OrderDTO>>> GetAllOrders()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllOrders();
            return Ok(new APIListOfEntityResponse<OrderDTO>()
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

    [HttpGet]
    public async Task<ActionResult<int>> GetOrdersCount()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetOrdersCount();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // log exception here
            return StatusCode(500);
        }
    }

    [HttpGet("{skip}/{take}")]
    public async Task<ActionResult<APIListOfEntityResponse<OrderDTO>>> GetOrders(int skip, int take)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllOrders().Skip(skip).Take(take);
            return Ok(new APIListOfEntityResponse<OrderDTO>()
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
    public async Task<ActionResult<APIListOfEntityResponse<Order_Details_ExtendedDTO>>> GetOrderDetailsByOrderID(int Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetOrderDetailsByOrderID(Id);
            if (result != null)
            {
                return Ok(new APIListOfEntityResponse<Order_Details_ExtendedDTO>()
                {
                    Success = true,
                    Data = result
                });
            }
            else
            {
                return Ok(new APIListOfEntityResponse<Order_Details_ExtendedDTO>()
                {
                    Success = false,
                    ErrorMessages = new List<string>() { "order detail records not found" },
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
    public async Task<ActionResult<APIListOfEntityResponse<SupplierDTO>>> GetAllSuppliers()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllSuppliers();
            return Ok(new APIListOfEntityResponse<SupplierDTO>()
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
    public async Task<ActionResult<APIListOfEntityResponse<ProductDTO>>> GetProductsBySupplier(int Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetProductsBySupplier(Id);
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
                    ErrorMessages = new List<string>() { "product records not found" },
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
    public async Task<ActionResult<APIListOfEntityResponse<ShipperDTO>>> GetAllShippers()
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetAllShippers();
            return Ok(new APIListOfEntityResponse<ShipperDTO>()
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
    public async Task<ActionResult<APIListOfEntityResponse<OrderDTO>>> GetOrdersByShipVia(int Id)
    {
        try
        {
            await Task.Delay(0);

            var result = database.GetOrdersByShipVia(Id);
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



}
