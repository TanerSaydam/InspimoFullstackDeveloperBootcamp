﻿using PersonelApp.WebAPI.Models;

namespace PersonelApp.WebAPI.Repositories;

public interface IPersonelRepository
{
    List<Personel> GetAll();
    bool Create(Personel personel);
    bool IsPersonelExists(string firstName, string lastName);
}