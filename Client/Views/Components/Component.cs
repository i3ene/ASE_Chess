using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;

namespace Client.Views.Components
{
    public abstract class Component : IViewable
    {
        public event UpdateHandler? OnUpdate;

        public Component? parent;
        public readonly StylePosition position;
        public readonly StyleSize size;
        public readonly Border border;
        public readonly Alignment alignment;
        public readonly ComponentDimensionHelper dimensionHelper;
        public readonly ComponentCanvasHelper canvasHelper;

        public Component()
        {
            position = new StylePosition();
            size = new StyleSize();
            border = new Border();
            alignment = new Alignment();
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
