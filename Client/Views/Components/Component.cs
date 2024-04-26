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
    public abstract class Component : IViewable
    {
        public event UpdateHandler? OnUpdate;

        public Component? parent;
        public readonly ComponentPosition position;
        public readonly ComponentSize size;
        public readonly ComponentBorder border;
        public readonly ComponentAlignment alignment;
        public readonly ComponentDimensionHelper dimensionHelper;
        public readonly ComponentCanvasHelper canvasHelper;

        public Component()
        {
            position = new ComponentPosition();
            size = new ComponentSize();
            border = new ComponentBorder();
            alignment = new ComponentAlignment();
            dimensionHelper = new ComponentDimensionHelper();
            canvasHelper = new ComponentCanvasHelper(dimensionHelper);
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public abstract ContentCanvas GetCanvas(ContentCanvas canvas);

        public ContentCanvas View()
        {
            ContentCanvas canvas = canvasHelper.CreateCanvas(this);
            canvas = GetCanvas(canvas);
            canvas = canvasHelper.OverflowCanvas(this, canvas);
            canvas = canvasHelper.AddBorder(this, canvas);
            return canvas;
        }        
        
    }
}
