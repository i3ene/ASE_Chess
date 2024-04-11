using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class Component : IViewable
    {
        public event UpdateHandler? OnUpdate;

        public Component? parent;
        public readonly ComponentPosition position;
        public readonly ComponentSize size;
        public readonly ComponentBorder border;
        public readonly ComponentAlignment alignment;

        public Component()
        {
            position = new ComponentPosition();
            size = new ComponentSize();
            border = new ComponentBorder();
            alignment = new ComponentAlignment();
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public ContentLine[] View()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
