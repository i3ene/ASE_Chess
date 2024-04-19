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
        public readonly ComponentService service;

        public Component()
        {
            position = new ComponentPosition();
            size = new ComponentSize();
            border = new ComponentBorder();
            alignment = new ComponentAlignment();
            service = new ComponentService();
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public abstract ContentString[] GetView();

        public ContentString[] View()
        {
            ContentString[] view = GetView();
            view = ConstrainView(view);
            view = AddBorder(view);
            return view;
        }

        private ContentString[] ConstrainView(ContentString[] view)
        {
            List<ContentString> constrainedView = new List<ContentString>();
            int innerWidth = service.CalculateInnerWidth(this);
            int innerHeight = service.CalculateInnerHeight(this);
            for (int i = 0; i < innerHeight; i++)
            {
                ContentString viewLine = view[i].Substring(0, innerWidth);
                constrainedView[i] = viewLine;
            }
            return constrainedView.ToArray();
        }

        private ContentString[] AddBorder(ContentString[] view)
        {
            if (border.isEnabled) return view;

            List<ContentString> viewWithBorder = new List<ContentString>(view);
            int innerWidth = service.CalculateInnerWidth(this);

            if (border.HasAnyTop())
            {
                viewWithBorder.Insert(0, new ContentString(new string(border.GetPosition(ComponentBorderPosition.Top), innerWidth)));
            }
            if (border.HasAnyBottom())
            {
                viewWithBorder.Add(new ContentString(new string(border.GetPosition(ComponentBorderPosition.Bottom), innerWidth)));
            }
            if (border.HasAnyLeft())
            {
                foreach (ContentString viewLine in viewWithBorder)
                {
                    viewLine.Insert(0, border.GetPosition(ComponentBorderPosition.Left).ToString());
                }
            }
            if (border.HasAnyRight())
            {
                foreach (ContentString viewLine in viewWithBorder)
                {
                    viewLine.Add(border.GetPosition(ComponentBorderPosition.Right).ToString());
                }
            }

            if (border.HasPosition(ComponentBorderPosition.TopLeft))
            {
                viewWithBorder[0].Inplace(0, border.GetPosition(ComponentBorderPosition.TopLeft).ToString());
            }

            if (border.HasPosition(ComponentBorderPosition.TopRight))
            {
                viewWithBorder[0].Inplace(viewWithBorder[0].Length - 1, border.GetPosition(ComponentBorderPosition.TopRight).ToString());
            }

            if (border.HasPosition(ComponentBorderPosition.BottomLeft))
            {
                viewWithBorder[viewWithBorder.Count - 1].Inplace(0, border.GetPosition(ComponentBorderPosition.BottomLeft).ToString());
            }

            if (border.HasPosition(ComponentBorderPosition.BottomRight))
            {
                viewWithBorder[viewWithBorder.Count - 1].Inplace(viewWithBorder[0].Length - 1, border.GetPosition(ComponentBorderPosition.BottomRight).ToString());
            }

            return viewWithBorder.ToArray();
        }
        
    }
}
