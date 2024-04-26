using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Client.Views.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Views.Components
{
    public class ListComponent : ComponentContainer, IDynamicDimension, IInteractable
    {
        private int currentSelectionIndex;

        public ListComponent() : base()
        {
            currentSelectionIndex = 0;
        }

        public int GetCurrentSelectionIndex()
        {
            return currentSelectionIndex;
        }

        public Component GetCurrentSelection()
        {
            Component[] childs = GetAllChilds();
            if (childs.Length == 0) throw new Exception("List has no children!");
            if (childs.Length < currentSelectionIndex || currentSelectionIndex < 0) throw new Exception("Selection index is out of range!");
            return childs[currentSelectionIndex];
        }

        public new void AddChild(Component component)
        {
            base.AddChild(component);
            Select(currentSelectionIndex);
        }

        public new void RemoveChild(Component child)
        {
            base.RemoveChild(child);
            Select(currentSelectionIndex);
        }

        public int SelectNext()
        {
            return Select(currentSelectionIndex + 1);
        }

        public int SelectPrevious()
        {
            return Select(currentSelectionIndex - 1);
        }

        private int Select(int index)
        {
            int length = GetAllChilds().Length;
            index = (index % length);
            if (index < 0) index = length + index;
            currentSelectionIndex = index;
            SetChildFocus(currentSelectionIndex);
            return currentSelectionIndex;
        }

        private void SetChildFocus(int index)
        {
            Component[] childs = GetAllChilds();
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].border.style = index == i ? ComponentBorderStyle.Thick : ComponentBorderStyle.Thin;
            }
            Update();
        }

        public void HandleInteraction(InteractionArgument args)
        {
            switch (args.key.Key)
            {
                case ConsoleKey.UpArrow:
                    SelectPrevious();
                    args.handled = true;
                    return;
                case ConsoleKey.DownArrow:
                    SelectNext();
                    args.handled = true;
                    return;
            }

            Component current = GetCurrentSelection();
            args.sender.InvokeInteractionEvent(current, args);
            args.handled = true;
        }

        public int GetDynamicWidth()
        {
            int width = 0;
            foreach (Component child in GetAllChilds())
            {
                if (child.size.widthUnit != ComponentUnit.Fixed) continue;
                int childWidth = dimensionHelper.CalculateOuterWidth(child);
                childWidth += child.position.x;
                width = Math.Max(width, childWidth);
            }
            int parentWidth = parent == null ? 0 : dimensionHelper.CalculateInnerWidth(parent);
            width = Math.Min(width, parentWidth);
            return width;
        }

        public int GetDynamicHeight()
        {
            int height = 0;
            foreach (Component child in GetAllChilds())
            {
                if (child.size.heightUnit != ComponentUnit.Fixed) continue;
                int childHeight = dimensionHelper.CalculateOuterHeight(child);
                childHeight += child.position.y;
                height += childHeight;
            }
            int parentHeight = parent == null ? 0 : dimensionHelper.CalculateInnerHeight(parent);
            height = Math.Min(height, parentHeight);
            if (border.HasAnyTop()) height += 1;
            if (border.HasAnyBottom()) height += 1;
            return height;
        }
    }
}
