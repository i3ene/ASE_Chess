using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class ComponentViewService
    {
        private readonly ComponentService service;

        public ComponentViewService(ComponentService service)
        {
            this.service = service;
        }

        public ContentString[] ConstrainView(Component component, ContentString[] view)
        {
            List<ContentString> constrainedView = new List<ContentString>();
            int innerWidth = service.CalculateInnerWidth(component);
            int innerHeight = service.CalculateInnerHeight(component);
            for (int i = 0; i < innerHeight; i++)
            {
                ContentString viewLine = view[i].Substring(0, innerWidth);
                constrainedView[i] = viewLine;
            }
            return constrainedView.ToArray();
        }

        public ContentString[] AddBorder(Component component, ContentString[] view)
        {
            if (component.border.isEnabled) return view;

            List<ContentString> viewWithBorder = new List<ContentString>(view);
            int innerWidth = service.CalculateInnerWidth(component);

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

            return viewWithBorder.ToArray();
        }
    }
}
