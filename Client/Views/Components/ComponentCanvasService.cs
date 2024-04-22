using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class ComponentCanvasService
    {
        private readonly ComponentDimensionService dimensionService;

        public ComponentCanvasService(ComponentDimensionService dimensionService)
        {
            this.dimensionService = dimensionService;
        }

        public ContentCanvas CreateCanvas(Component component)
        {
            return new ContentCanvas(dimensionService.CalculateInnerWidth(component), dimensionService.CalculateInnerHeight(component));
        }

        public ContentCanvas ToCanvas(ContentString[] rows)
        {
            int width = rows.Select(row => row.Length).Max();
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
            int innerWidth = dimensionService.CalculateInnerWidth(component);
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
            int innerWidth = dimensionService.CalculateInnerWidth(component);

            if (component.border.HasAnyTop())
            {
                viewWithBorder.Insert(0, new ContentString(new string(component.border.GetPosition(ComponentBorderPosition.Top), innerWidth)));
            }
            if (component.border.HasAnyBottom())
            {
                viewWithBorder.Add(new ContentString(new string(component.border.GetPosition(ComponentBorderPosition.Bottom), innerWidth)));
            }
            if (component.border.HasAnyLeft())
            {
                foreach (ContentString viewLine in viewWithBorder)
                {
                    viewLine.Insert(0, component.border.GetPosition(ComponentBorderPosition.Left).ToString());
                }
            }
            if (component.border.HasAnyRight())
            {
                foreach (ContentString viewLine in viewWithBorder)
                {
                    viewLine.Add(component.border.GetPosition(ComponentBorderPosition.Right).ToString());
                }
            }

            if (component.border.HasPosition(ComponentBorderPosition.TopLeft))
            {
                viewWithBorder[0].Inplace(0, component.border.GetPosition(ComponentBorderPosition.TopLeft).ToString());
            }

            if (component.border.HasPosition(ComponentBorderPosition.TopRight))
            {
                viewWithBorder[0].Inplace(viewWithBorder[0].Length - 1, component.border.GetPosition(ComponentBorderPosition.TopRight).ToString());
            }

            if (component.border.HasPosition(ComponentBorderPosition.BottomLeft))
            {
                viewWithBorder[viewWithBorder.Count - 1].Inplace(0, component.border.GetPosition(ComponentBorderPosition.BottomLeft).ToString());
            }

            if (component.border.HasPosition(ComponentBorderPosition.BottomRight))
            {
                viewWithBorder[viewWithBorder.Count - 1].Inplace(viewWithBorder[0].Length - 1, component.border.GetPosition(ComponentBorderPosition.BottomRight).ToString());
            }

            return ToCanvas(viewWithBorder.ToArray());
        }
    }
}
