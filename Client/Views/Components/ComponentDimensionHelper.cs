using Client.Views.Components.Styles;

namespace Client.Views.Components
{
    public class ComponentDimensionHelper
    {
        public ComponentSize CalculateInnerSize(Component component)
        {
            return new ComponentSize(new ComponentValue(CalculateInnerWidth(component)), new ComponentValue(CalculateInnerHeight(component)));
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
            return new ComponentSize(new ComponentValue(CalculateOuterWidth(component)), new ComponentValue(CalculateOuterHeight(component)));
        }

        public int CalculateOuterWidth(Component component) => component.size.width.unit switch
        {
            ComponentUnit.Fixed => CalculateOuterWidthFixed(component),
            ComponentUnit.Absolute => CalculateOuterWidthAbsolute(component),
            ComponentUnit.Relative => CalculateOuterWidthRelative(component),
            ComponentUnit.Auto => CalculateOuterWidthAuto(component),
            _ => 0
        };

        private int CalculateOuterWidthFixed(Component component)
        {
            return component.size.width.value;
        }

        private int CalculateOuterWidthAbsolute(Component component)
        {
            int parentInnerWidth = component.parent is null ? 0 : CalculateInnerWidth(component.parent);
            return (int)(parentInnerWidth * ToPercent(component.size.width.value));
        }

        private int CalculateOuterWidthRelative(Component component)
        {
            int parentInnerWidth = component.parent is null ? 0 : CalculateInnerWidth(component.parent);
            int outerWidth = parentInnerWidth;
            outerWidth = (int)(outerWidth * ToPercent(component.size.width.value));
            return outerWidth;
        }

        private int CalculateOuterWidthAuto(Component component)
        {
            int outerWidth = 0;
            int parentInnerWidth = component.parent is null ? 0 : CalculateInnerWidth(component.parent);
            if (component is IDynamicDimension)
            {
                outerWidth = ((IDynamicDimension)component).GetDynamicWidth();
                if (component.border.isEnabled)
                {
                    if (component.border.HasAnyLeft()) outerWidth += 1;
                    if (component.border.HasAnyRight()) outerWidth += 1;
                }
                outerWidth = Math.Min(outerWidth, parentInnerWidth);
                return outerWidth;
            }

            outerWidth = parentInnerWidth;
            if (component.parent != null && component.parent is ComponentContainer)
            {
                foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                {
                    if (child == component) break;
                    outerWidth -= CalculateOuterWidth(child);
                }
            }
            return outerWidth;
        }

        public int CalculateOuterHeight(Component component) => component.size.height.unit switch
        {
            ComponentUnit.Fixed => CalculateOuterHeightFixed(component),
            ComponentUnit.Absolute => CalculateOuterHeightAbsolute(component),
            ComponentUnit.Relative => CalculateOuterHeightRelative(component),
            ComponentUnit.Auto => CalculateOuterHeightAuto(component),
            _ => 0
        };

        private int CalculateOuterHeightFixed(Component component)
        {
            return component.size.height.value;
        }

        private int CalculateOuterHeightAbsolute(Component component)
        {
            int parentInnerHeight = component.parent is null ? 0 : CalculateInnerHeight(component.parent);
            return (int)(parentInnerHeight * ToPercent(component.size.height.value));
        }

        private int CalculateOuterHeightRelative(Component component)
        {
            int parentInnerHeight = component.parent is null ? 0 : CalculateInnerHeight(component.parent);
            int outerHeight = parentInnerHeight;
            outerHeight = (int)(outerHeight * ToPercent(component.size.height.value));
            return outerHeight;
        }

        private int CalculateOuterHeightAuto(Component component)
        {
            int outerHeight = 0;
            int parentInnerHeight = component.parent is null ? 0 : CalculateInnerHeight(component.parent);
            if (component.size.height.unit == ComponentUnit.Auto && component is IDynamicDimension)
            {
                outerHeight = ((IDynamicDimension)component).GetDynamicHeight();
                if (component.border.isEnabled)
                {
                    if (component.border.HasAnyTop()) outerHeight += 1;
                    if (component.border.HasAnyBottom()) outerHeight += 1;
                }
                outerHeight = Math.Min(outerHeight, parentInnerHeight);
                return outerHeight;
            }

            outerHeight = parentInnerHeight;
            if (component.parent != null && component.parent is ComponentContainer)
            {
                foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                {
                    if (child == component) break;
                    outerHeight -= CalculateOuterHeight(child);
                }
            }
            return outerHeight;
        }

        public ComponentPosition CalculatePosition(Component component)
        {
            return new ComponentPosition(new ComponentValue(CalculateXPosition(component)), new ComponentValue(CalculateYPosition(component)));
        }

        public int CalculateXPosition(Component component) => component.position.x.unit switch
        {
            ComponentUnit.Fixed => CalculateXPositionFixed(component),
            ComponentUnit.Absolute => CalculateXPositionAbsolute(component),
            ComponentUnit.Relative => CalculateXPositionRelative(component),
            ComponentUnit.Auto => CalculateXPositionAuto(component),
            _ => 0
        };

        private int CalculateXPositionFixed(Component component) => component.alignment.horizontal switch
        {
            Styles.Alignments.ComponentHorizontalAlignment.Position or
            Styles.Alignments.ComponentHorizontalAlignment.Left or
            Styles.Alignments.ComponentHorizontalAlignment.Right or
            Styles.Alignments.ComponentHorizontalAlignment.Center => component.position.x.value,
            _ => 0
        };

        private int CalculateXPositionAbsolute(Component component) => component.alignment.horizontal switch
        {
            Styles.Alignments.ComponentHorizontalAlignment.Position or
            Styles.Alignments.ComponentHorizontalAlignment.Left => CalculateXPositionAbsoluteLeft(component),
            Styles.Alignments.ComponentHorizontalAlignment.Right => CalculateXPositionAbsoluteRight(component),
            Styles.Alignments.ComponentHorizontalAlignment.Center => CalculateXPositionAbsoulteCenter(component),
            _ => 0
        };
        
        private int CalculateXPositionAbsoluteLeft(Component component)
        {
            int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
            return parentXPosition + component.position.x.value;
        } 

        private int CalculateXPositionAbsoluteRight(Component component)
        {
            int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
            int parentInnerWidth = component.parent is null ? 0 : CalculateInnerWidth(component.parent);
            int outerWidth = CalculateOuterWidth(component);
            return (parentXPosition + parentInnerWidth) - component.position.x.value - outerWidth;
        }

        private int CalculateXPositionAbsoulteCenter(Component component)
        {
            int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
            int parentInnerWidth = component.parent is null ? 0 : CalculateInnerWidth(component.parent);
            int width = CalculateOuterWidth(component);
            int center = (parentInnerWidth / 2) - (width / 2);
            center += component.position.x.value;
            return parentXPosition + center;
        }

        private int CalculateXPositionRelative(Component component) => component.alignment.horizontal switch
        {
            Styles.Alignments.ComponentHorizontalAlignment.Position or
            Styles.Alignments.ComponentHorizontalAlignment.Left => CalculateXPositionRelativeLeft(component),
            Styles.Alignments.ComponentHorizontalAlignment.Right => CalculateXPositionRelativeRight(component),
            Styles.Alignments.ComponentHorizontalAlignment.Center => CalculateXPositionRelativeCenter(component),
            _ => 0
        };

        private int CalculateXPositionRelativeLeft(Component component)
        {
            int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
            int parentOuterWidth = component.parent is null ? 0 : CalculateOuterWidth(component.parent);
            return (int)(parentXPosition + (parentOuterWidth * ToPercent(component.position.x.value)));
        }

        private int CalculateXPositionRelativeRight(Component component)
        {
            int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
            int parentOuterWidth = component.parent is null ? 0 : CalculateOuterWidth(component.parent);
            int outerWidth = CalculateOuterWidth(component);
            return (int)(parentXPosition + parentOuterWidth - (parentOuterWidth * ToPercent(component.position.x.value)) - outerWidth);
        }

        private int CalculateXPositionRelativeCenter(Component component)
        {
            int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
            int parentInnerWidth = component.parent is null ? 0 : CalculateInnerWidth(component.parent);
            int width = CalculateOuterWidth(component);
            int center = (parentInnerWidth / 2) - (width / 2);
            center = (int)(center * ToPercent(component.position.x.value));
            return parentXPosition + center;
        }

        private int CalculateXPositionAuto(Component component) => component.alignment.horizontal switch
        {
            Styles.Alignments.ComponentHorizontalAlignment.Position or
            Styles.Alignments.ComponentHorizontalAlignment.Left => CalculateXPositionAutoLeft(component),
            Styles.Alignments.ComponentHorizontalAlignment.Right => CalculateXPositionAutoRight(component),
            Styles.Alignments.ComponentHorizontalAlignment.Center => CalculateXPositionAutoCenter(component),
            _ => 0
        };

        private int CalculateXPositionAutoLeft(Component component)
        {
            int xPosition = component.position.x.value;
            if (component.parent is ComponentContainer)
            {
                Component? lastChild = null;
                foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                {
                    if (child == component) break;
                    if (child.position.x.unit != ComponentUnit.Auto ||
                        (child.alignment.horizontal != Styles.Alignments.ComponentHorizontalAlignment.Position &&
                        child.alignment.horizontal != Styles.Alignments.ComponentHorizontalAlignment.Left)) continue;
                    lastChild = child;
                }

                if (lastChild != null)
                {
                    xPosition += CalculateXPosition(lastChild);
                    xPosition += CalculateOuterWidth(lastChild);
                }
                else
                {
                    int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
                    xPosition += parentXPosition;
                }
            }
            return xPosition;
        }

        private int CalculateXPositionAutoRight(Component component)
        {
            int xPosition = -component.position.x.value;
            xPosition -= CalculateOuterWidth(component);
            if (component.parent is ComponentContainer)
            {
                Component? lastChild = null;
                foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                {
                    if (child == component) break;
                    if (child.position.x.unit != ComponentUnit.Auto ||
                        child.alignment.horizontal != Styles.Alignments.ComponentHorizontalAlignment.Right) continue;
                    lastChild = child;
                }

                if (lastChild != null)
                {
                    xPosition += CalculateXPosition(lastChild);
                }
                else
                {
                    int parentInnerWidth = component.parent is null ? 0 : CalculateInnerWidth(component.parent);
                    xPosition += parentInnerWidth;
                }
            }
            return xPosition;
        }

        private int CalculateXPositionAutoCenter(Component component)
        {
            int xPosition = 0;
            if (component.parent is ComponentContainer)
            {
                Component[] childs = ((ComponentContainer)component.parent).GetAllChilds().Where(child => child.alignment.horizontal == Styles.Alignments.ComponentHorizontalAlignment.Center).ToArray();

                Component? lastChild = null;
                foreach (Component child in childs)
                {
                    if (child == component) break;
                    lastChild = child;
                }

                if (lastChild != null)
                {
                    xPosition += CalculateXPosition(lastChild);
                }
                else
                {
                    int parentInnerWidth = component.parent == null ? 0 : CalculateInnerWidth(component.parent);
                    int totalWidth = CalculateOuterWidth(component) + component.position.x.value;
                    foreach (Component child in childs)
                    {
                        if (child == component) continue;
                        totalWidth += CalculateOuterWidth(child) + child.position.x.value;
                    }
                    int parentXPosition = component.parent is null ? 0 : CalculateXPosition(component.parent);
                    xPosition += parentXPosition;
                    int center = (parentInnerWidth / 2) - (totalWidth / 2);
                    xPosition += center;
                }
            }
            return xPosition;
        }

        public int CalculateYPosition(Component component) => component.position.y.unit switch
        {
            ComponentUnit.Fixed => CalculateYPositionFixed(component),
            ComponentUnit.Absolute => CalculateYPositionAbsolute(component),
            ComponentUnit.Relative => CalculateYPositionRelative(component),
            ComponentUnit.Auto => CalculateYPositionAuto(component),
            _ => 0
        };

        private int CalculateYPositionFixed(Component component) => component.alignment.vertical switch
        {
            Styles.Alignments.ComponentVerticalAlignment.Position or
            Styles.Alignments.ComponentVerticalAlignment.Top or
            Styles.Alignments.ComponentVerticalAlignment.Middle or
            Styles.Alignments.ComponentVerticalAlignment.Bottom => component.position.y.value,
            _ => 0
        };

        private int CalculateYPositionAbsolute(Component component) => component.alignment.vertical switch
        {
            Styles.Alignments.ComponentVerticalAlignment.Position or
            Styles.Alignments.ComponentVerticalAlignment.Top => CalculateYPositionAbsoluteTop(component),
            Styles.Alignments.ComponentVerticalAlignment.Middle => CalculateYPositionAbsoluteMiddle(component),
            Styles.Alignments.ComponentVerticalAlignment.Bottom => CalculateYPositionAbsoluteBottom(component),
            _ => 0
        };

        private int CalculateYPositionAbsoluteTop(Component component)
        {
            int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
            return parentYPosition + component.position.y.value;
        }

        private int CalculateYPositionAbsoluteMiddle(Component component)
        {
            int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
            int parentInnerHeight = component.parent is null ? 0 : CalculateInnerHeight(component.parent);
            int height = CalculateOuterHeight(component);
            int center = (parentInnerHeight / 2) - (height / 2);
            center += component.position.y.value;
            return parentYPosition + center;
        }

        private int CalculateYPositionAbsoluteBottom(Component component)
        {
            int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
            int parentInnerHeight = component.parent is null ? 0 : CalculateInnerHeight(component.parent);
            int outerHeight = CalculateOuterHeight(component);
            return (parentYPosition + parentInnerHeight) - component.position.y.value - outerHeight;
        }

        private int CalculateYPositionRelative(Component component) => component.alignment.vertical switch
        {
            Styles.Alignments.ComponentVerticalAlignment.Position or
            Styles.Alignments.ComponentVerticalAlignment.Top => CalculateYPositionRelativeTop(component),
            Styles.Alignments.ComponentVerticalAlignment.Middle => CalculateYPositionRelativeMiddle(component),
            Styles.Alignments.ComponentVerticalAlignment.Bottom => CalculateYPositionRelativeBottom(component),
            _ => 0
        };

        private int CalculateYPositionRelativeTop(Component component)
        {
            int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
            int parentOuterHeight = component.parent is null ? 0 : CalculateOuterHeight(component.parent);
            return (int)(parentYPosition + (parentOuterHeight * ToPercent(component.position.y.value)));
        }

        private int CalculateYPositionRelativeBottom(Component component)
        {
            int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
            int parentOuterHeight = component.parent is null ? 0 : CalculateOuterHeight(component.parent);
            int outerHeight = CalculateOuterHeight(component);
            return (int)(parentYPosition + parentOuterHeight - (parentOuterHeight * ToPercent(component.position.y.value)) - outerHeight);
        }

        private int CalculateYPositionRelativeMiddle(Component component)
        {
            int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
            int parentInnerHeight = component.parent is null ? 0 : CalculateInnerHeight(component.parent);
            int height = CalculateOuterHeight(component);
            int center = (parentInnerHeight / 2) - (height / 2);
            center = (int)(center * ToPercent(component.position.y.value));
            return parentYPosition + center;
        }

        private int CalculateYPositionAuto(Component component) => component.alignment.vertical switch
        {
            Styles.Alignments.ComponentVerticalAlignment.Position or
            Styles.Alignments.ComponentVerticalAlignment.Top => CalculateYPositionAutoTop(component),
            Styles.Alignments.ComponentVerticalAlignment.Middle => CalculateYPositionAutoMiddle(component),
            Styles.Alignments.ComponentVerticalAlignment.Bottom => CalculateYPositionAutoBottom(component),
            _ => 0
        };

        private int CalculateYPositionAutoTop(Component component)
        {
            int yPosition = component.position.y.value;
            if (component.parent is ComponentContainer)
            {
                Component? lastChild = null;
                foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                {
                    if (child == component) break;
                    if (child.position.y.unit != ComponentUnit.Auto ||
                        (child.alignment.vertical != Styles.Alignments.ComponentVerticalAlignment.Position &&
                        child.alignment.vertical != Styles.Alignments.ComponentVerticalAlignment.Top)) continue;
                    lastChild = child;
                }

                if (lastChild != null)
                {
                    yPosition += CalculateYPosition(lastChild);
                    yPosition += CalculateOuterHeight(lastChild);
                }
                else
                {
                    int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
                    yPosition += parentYPosition;
                }
            }
            return yPosition;
        }

        private int CalculateYPositionAutoBottom(Component component)
        {
            int yPosition = -component.position.y.value;
            yPosition -= CalculateOuterHeight(component);
            if (component.parent is ComponentContainer)
            {
                Component? lastChild = null;
                foreach (Component child in ((ComponentContainer)component.parent).GetAllChilds())
                {
                    if (child == component) break;
                    if (child.position.y.unit != ComponentUnit.Auto ||
                        child.alignment.vertical != Styles.Alignments.ComponentVerticalAlignment.Bottom) continue;
                    lastChild = child;
                }

                if (lastChild != null)
                {
                    yPosition += CalculateYPosition(lastChild);
                }
                else
                {
                    int parentInnerHeight = component.parent is null ? 0 : CalculateInnerHeight(component.parent);
                    yPosition += parentInnerHeight;
                }
            }
            return yPosition;
        }
        
        private int CalculateYPositionAutoMiddle(Component component)
        {
            int yPosition = 0;
            if (component.parent is ComponentContainer)
            {
                Component[] childs = ((ComponentContainer)component.parent).GetAllChilds().Where(child => child.alignment.vertical == Styles.Alignments.ComponentVerticalAlignment.Middle).ToArray();

                Component? lastChild = null;
                foreach (Component child in childs)
                {
                    if (child == component) break;
                    lastChild = child;
                }

                if (lastChild != null)
                {
                    yPosition += CalculateYPosition(lastChild);
                }
                else
                {
                    int parentInnerHeight = component.parent == null ? 0 : CalculateInnerHeight(component.parent);
                    int totalHeight = CalculateOuterHeight(component) + component.position.y.value;
                    foreach (Component child in childs)
                    {
                        if (child == component) continue;
                        totalHeight += CalculateOuterHeight(child) + child.position.y.value;
                    }
                    int parentYPosition = component.parent is null ? 0 : CalculateYPosition(component.parent);
                    yPosition += parentYPosition;
                    int center = (parentInnerHeight / 2) - (totalHeight / 2);
                    yPosition += center;
                }
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
