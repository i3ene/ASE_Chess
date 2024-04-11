using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components.Styles
{
    public class ComponentPosition : Vector2D
    {
        public ComponentUnit xUnit;
        public ComponentUnit yUnit;

        public ComponentPosition(int x, int y) : base(x, y)
        {
            xUnit = ComponentUnit.Character;
            yUnit = ComponentUnit.Character;
        }
    }
}
