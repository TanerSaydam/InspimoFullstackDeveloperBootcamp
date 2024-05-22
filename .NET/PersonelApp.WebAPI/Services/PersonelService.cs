﻿using PersonelApp.WebAPI.DTOs;
using PersonelApp.WebAPI.Models;
using PersonelApp.WebAPI.Repositories;

namespace PersonelApp.WebAPI.Services;

public sealed class PersonelService(
    IPersonelRepository personelRepository) : IPersonelService
{
    public bool Create(CreatePersonelDto request)
    {
        bool isPersonelExists = personelRepository.IsPersonelExists(request.FirstName, request.LastName);

        if(isPersonelExists)
        {
            throw new ArgumentException("Personel already exists");
        }

        Personel personel = new Personel()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            StartingDate = request.StartingDate,
        };

        var result = personelRepository.Create(personel);
        return result;
    }

    public List<Personel> GetAll()
    {
        return personelRepository.GetAll();
    }
}
