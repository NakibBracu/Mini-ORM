﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM
{
    public abstract class Base<G>
    {
        public G Id { get; set; }
    }
}
