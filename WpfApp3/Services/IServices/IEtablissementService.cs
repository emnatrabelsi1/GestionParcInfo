﻿using platapp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Services.IServices
{
    public interface IEtablissementService
    {
        Task<List<Etablissement>> GetAllEtablissements();
    }
}