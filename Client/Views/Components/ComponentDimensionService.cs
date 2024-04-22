using Client.Views.Components.Styles.Borders;
using Client.Views.Components.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Client.Views.Components
{
    public class ComponentDimensionService
    {
        public ComponentSize CalculateInnerSize(Component component)
        {
            return new ComponentSize(CalculateInnerWidth(component), CalculateInnerHeight(component));
        }

        public int CalculateInnerWidth(Component component)
        {
            int innerWidth = CalculateOuterWidth(component);
            if (component.border.isEnabled)
            {
                if (component.border.HasAnyLeft()) innerWidth -= 1;
                if (component.border.HasAnyRight()) innerWidth -= 1;
            }
            innerWidth = Math.Max(0, innerWidth);
            return innerWidth;
        }

        public int CalculateInnerHeight(Component component)
        {
            int innerHeight = CalculateOuterHeight(component);
            if (component.border.isEnabled)
            {
                if (component.border.HasAnyTop()) innerHeight -= 1;
                if (component.border.HasAnyBottom()) innerHeight -= 1;
            }
            innerHeight = Math.Max(0, innerHeight);
            return innerHeight;
        }

        public ComponentSize CalculateOuterSize(Component component)
        {
            return new ComponentSize(CalculateOuterWidth(component), CalculateOuterHeight(component));
        }

        public int CalculateOuterWidth(Component component)
        {
            int? parentInnerWidth = component.parent is null ? null : CalculateInnerWidth(component.parent);
            int outerWidth = 0;
            switch (component.size.widthUnit)
            {
                case ComponentUnit.Fixed:
                    outerWidth = component.size.width;
                    break;
                case ComponentUnit.Absolute:
                    if (parentInnerWidth is null) break;
                    outerWidth = (int)(parentInnerWidth * ToPercent(component.size.width));
                    break;
                case ComponentUnit.Relative:
                case ComponentUnit.Auto:
                    if (component is IDynamicDimension)
                    {
                        outerWidth = ((IDynamicDimension)component).GetDynamicWidth();
                        if (component.border.isEnabled)
                        {
                            if (component.border.HasAnyLeft()) outerWidth += 1;
                            if (component.border.HasAnyRight()) outerWidth += 1;
                        }
                        break;
                    }

                    if (parentInnerWidth is null || component.parent is null) break;
                    outerWidth = (int)parentInnerWidth;
                    if (component.parent.GetType() == typeof(ComponentContainer))
                    {
                        foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                        {
                            if (child == component) break;
                            outerWidth -= CalculateOuterWidth(child);
                        }
                    }
                    if (component.size.widthUnit == ComponentUnit.Relative) outerWidth = (int)(outerWidth * ToPercent(component.size.width));
                    break;
            }
            return outerWidth;
        }

        public int CalculateOuterHeight(Component component)
        {
            int? parentInnerHeight = component.parent is null ? null : CalculateInnerHeight(component.parent);
            int outerHeight = 0;
            switch (component.size.heightUnit)
            {
                case ComponentUnit.Fixed:
                    outerHeight = component.size.height;
                    break;
                case ComponentUnit.Absolute:
                    if (parentInnerHeight is null) break;
                    outerHeight = (int)(parentInnerHeight * ToPercent(component.size.height));
                    break;
                case ComponentUnit.Relative:
                case ComponentUnit.Auto:
                    if (component is IDynamicDimension)
                    {
                        outerHeight = ((IDynamicDimension)component).GetDynamicHeight();
                        if (component.border.isEnabled)
                        {
                            if (component.border.HasAnyTop()) outerHeight += 1;
                            if (component.border.HasAnyBottom()) outerHeight += 1;
                        }
                        break;
                    }

                    if (parentInnerHeight is null || component.parent is null) break;
                    outerHeight = (int)parentInnerHeight;
                    if (component.parent.GetType() == typeof(ComponentContainer))
                    {
                        foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                        {
                            if (child == component) break;
                            outerHeight -= CalculateOuterHeight(child);
                        }
                    }
                    if (component.size.heightUnit == ComponentUnit.Relative) outerHeight = (int)(outerHeight * ToPercent(component.size.height));
                    break;
            }
            return outerHeight;
        }

        public ComponentPosition CalculatePosition(Component component)
        {
            return new ComponentPosition(CalculateXPosition(component), CalculateYPosition(component));
        }

        public int CalculateXPosition(Component component)
        {
            int? parentXPosition = component.parent is null ? null : CalculateXPosition(component.parent);
            int? parentOuterWidth = component.parent is null ? null : CalculateOuterWidth(component.parent);
            int xPosition = 0;
            switch (component.position.xUnit)
            {
                case ComponentUnit.Fixed:
                    xPosition = component.position.x;
                    break;
                case ComponentUnit.Absolute:
                    if (parentXPosition is null) break;
                    xPosition = (int)parentXPosition + component.position.x;
                    break;
                case ComponentUnit.Relative:
                    if (parentXPosition is null || parentOuterWidth is null) break;
                    xPosition = (int)(parentXPosition + (parentOuterWidth * ToPercent(component.size.x)));
                    break;
                case ComponentUnit.Auto:
                    if (parentXPosition is null || component.parent is null) break;
                    xPosition = (int)parentXPosition;
                    if (component.parent.GetType() == typeof(ComponentContainer))
                    {
                        foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                        {
                            if (child == component) break;
                            xPosition += CalculateOuterWidth(child);
                        }
                    }
                    break;
            }
            return xPosition;
        }

        public int CalculateYPosition(Component component)
        {
            int? parentYPosition = component.parent is null ? null : CalculateYPosition(component.parent);
            int? parentOuterHeight = component.parent is null ? null : CalculateOuterHeight(component.parent);
            int yPosition = 0;
            switch (component.position.xUnit)
            {
                case ComponentUnit.Fixed:
                    yPosition = component.position.y;
                    break;
                case ComponentUnit.Absolute:
                    if (parentYPosition is null) break;
                    yPosition = (int)parentYPosition + component.position.y;
                    break;
                case ComponentUnit.Relative:
                    if (parentYPosition is null || parentOuterHeight is null) break;
                    yPosition = (int)(parentYPosition + (parentOuterHeight * ToPercent(component.size.y)));
                    break;
                case ComponentUnit.Auto:
                    if (parentYPosition is null || component.parent is null) break;
                    yPosition = (int)parentYPosition;
                    if (component.parent.GetType() == typeof(ComponentContainer))
                    {
                        foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                        {
                            if (child == component) break;
                            yPosition += CalculateOuterHeight(child);
                        }
                    }
                    break;
            }
            return yPosition;
        }

        public ComponentDimension CalculateDimension(Component component)
        {
            return new ComponentDimension(
                CalculatePosition(component),
                CalculateInnerSize(component),
                CalculateOuterSize(component)
            );
        }

        private float ToPercent(int value)
        {
            return value / 100.0f;
        }
    }
}
