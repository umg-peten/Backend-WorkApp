﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.IServices
{
    public interface ITokenHandler
    {
        public string GenerateToken(String employee);
    }
}