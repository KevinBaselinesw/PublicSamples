﻿using Microsoft.AspNetCore.Mvc;
using BlazorSampleAppWebAssembly.Shared.Models;
using BlazorSampleAppWebAssembly.Client;
using DatabaseAccessLib;
using BlazorSampleAppWebAssembly;
using Radzen.Blazor;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KMScheduler.Server.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class DatabaseAPIController : ControllerBase
{
    IDataAccessAPI? database;

    public DatabaseAPIController()
    {
    }

    private IDataAccessAPI GetDatabase()
    {
        return new DataAccessAPIWrapper();
    }

    [HttpGet]
    public async Task<ActionResult<APIListOfEntityResponse<EmployeeDTO>>> GetAllEmployees()
    {
        try
        {
            await Task.Delay(0);

            database = GetDatabase();

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

            database = GetDatabase();

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

}