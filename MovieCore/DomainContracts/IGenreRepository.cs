﻿using MovieCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCore.DomainContracts
{
    public interface IGenreRepository
    {
        Task<Genre?> GetByIdAsync(int id);
    }
}
