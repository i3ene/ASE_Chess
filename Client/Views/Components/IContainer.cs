﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public interface IContainer
    {
        public object[] GetAllChilds();
    }
}
