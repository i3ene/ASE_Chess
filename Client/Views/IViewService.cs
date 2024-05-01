﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views
{
    public interface IViewService
    {
        public Task Display(IViewable view);
    }
}
