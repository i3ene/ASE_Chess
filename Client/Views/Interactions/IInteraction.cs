﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Interactions
{
    public interface IInteraction
    {
        public void HandleInteraction(InteractionArgument args);
    }
}