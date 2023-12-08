using Microsoft.AspNetCore.Mvc;
using NutrialNet.Expand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NutrialNet.Expand.Interfaces.Web;

[Route("api/[controller]")]
[ApiController]
public class ExpandController
{


    [HttpGet("/")]
    public string Index()
    {
        return "Hello world!!!";
    }

    [HttpGet("/{database}/{table}")]
    public async Task<ExpandTable> GetTable(string database, string table)
    {
        return new ExpandTable();
    }

    [HttpPost("/{database}")]
    public async Task<ExpandTable> CreateTable(string database, [FromBody] CreateTableStatement statement)
    {
        return new ExpandTable();
    }
}
