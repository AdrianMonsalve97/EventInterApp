﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Genericos
{
    public record PeticionGeneral<T>(
        T Data,
        string Usuario
        );
}
