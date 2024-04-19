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
        public readonly ComponentDimensionService dimensionService;
        public readonly ComponentCanvasService canvasService;

        public Component()
        {
            position = new ComponentPosition();
            size = new ComponentSize();
            border = new ComponentBorder();
            alignment = new ComponentAlignment();
            dimensionService = new ComponentDimensionService();
            canvasService = new ComponentCanvasService(dimensionService);
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public abstract ContentCanvas GetCanvas(ContentCanvas canvas);

        public ContentCanvas View()
        {
            ContentCanvas canvas = canvasService.CreateCanvas(this);
            canvas = GetCanvas(canvas);
            canvas = canvasService.AddBorder(this, canvas);
            return canvas;
        }        
        
    }
}
