using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;

namespace Client.Views.Components
{
    public class ComponentCanvasHelper
    {
        private readonly ComponentDimensionHelper dimensionHelper;

        public ComponentCanvasHelper(ComponentDimensionHelper dimensionService)
        {
            dimensionHelper = dimensionService;
        }

        public ContentCanvas CreateCanvas(Component component)
        {
            return new ContentCanvas(dimensionHelper.CalculateInnerWidth(component), dimensionHelper.CalculateInnerHeight(component));
        }

        public ContentCanvas ToCanvas(ContentString[] rows)
        {
            int width = rows.Length == 0 ? 0 : rows.Select(row => row.Length).Max();
            int height = rows.Length;
            ContentCanvas canvas = new ContentCanvas(width, height);
            foreach ((ContentString row, int index) in rows.Select((row, index) => (row, index)))
            {
                canvas.SetRow(index, row);
            }
            return canvas;
        }

        public ContentCanvas OverflowCanvas(Component component, ContentCanvas canvas)
        {
            int innerWidth = dimensionHelper.CalculateInnerWidth(component);
            foreach((ContentString row, int index) in canvas.GetRows().Select((row, index) => (row, index)))
            {
                canvas.SetRow(index, row.Substring(0, innerWidth - 1));
            }
            return canvas;
        }

        public ContentCanvas AddBorder(Component component, ContentCanvas canvas)
        {
            if (!component.border.isEnabled) return canvas;

            List<ContentString> viewWithBorder = new List<ContentString>(canvas.GetRows());
            int innerWidth = dimensionHelper.CalculateInnerWidth(component);

            if (component.border.HasAnyTop())
            {
                viewWithBorder.Insert(0, new ContentString(new string(component.border.GetPosition(BorderPosition.Top), innerWidth)));
            }
            if (component.border.HasAnyBottom())
            {
                viewWithBorder.Add(new ContentString(new string(component.border.GetPosition(BorderPosition.Bottom), innerWidth)));
            }
            if (component.border.HasAnyLeft())
            {
                foreach (ContentString viewLine in viewWithBorder)
                {
                    viewLine.Insert(0, component.border.GetPosition(BorderPosition.Left).ToString());
                }
            }
            if (component.border.HasAnyRight())
            {
                foreach (ContentString viewLine in viewWithBorder)
                {
                    viewLine.Add(component.border.GetPosition(BorderPosition.Right).ToString());
                }
            }

            if (component.border.HasPosition(BorderPosition.TopLeft))
            {
                viewWithBorder[0].Inplace(0, component.border.GetPosition(BorderPosition.TopLeft).ToString());
            }

            if (component.border.HasPosition(BorderPosition.TopRight))
            {
                viewWithBorder[0].Inplace(viewWithBorder[0].Length - 1, component.border.GetPosition(BorderPosition.TopRight).ToString());
            }

            if (component.border.HasPosition(BorderPosition.BottomLeft))
            {
                viewWithBorder[viewWithBorder.Count - 1].Inplace(0, component.border.GetPosition(BorderPosition.BottomLeft).ToString());
            }

            if (component.border.HasPosition(BorderPosition.BottomRight))
            {
                viewWithBorder[viewWithBorder.Count - 1].Inplace(viewWithBorder[0].Length - 1, component.border.GetPosition(BorderPosition.BottomRight).ToString());
            }

            return ToCanvas(viewWithBorder.ToArray());
        }
    }
}
