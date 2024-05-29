﻿using Microsoft.AspNetCore.Mvc;
using PersonelApp.WebAPI.DTOs;
using PersonelApp.WebAPI.Services;

namespace PersonelApp.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
//[MyAuthorize]
public sealed class PersonelsController(
    IPersonelService personelService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(int pageNumber = 1)
    {
        //var stopwatch = new Stopwatch();
        //stopwatch.Start();
        var personels = personelService.GetAll(pageNumber);
        //stopwatch.Stop();

        //Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
        return Ok(personels);
    }

    [HttpPost]
    public IActionResult Create(CreatePersonelDto request)
    {
        var result = personelService.Create(request);
        if (!result) return BadRequest(new { Message = "Something went wrong" });
        return Ok(new { Message = "Personel create is successful" });
    }
}