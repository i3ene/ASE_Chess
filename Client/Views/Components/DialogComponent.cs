using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Alignments;
using Client.Views.Contents;

namespace Client.Views.Components
{
    public class DialogComponent : ComponentContainer, IDynamicDimension
    {
        public delegate void ActionHandler(DialogComponent sender, string action);
        public event ActionHandler? OnAction;

        private readonly TextComponent text;
        private readonly ListComponent actionContainer;

        public DialogComponent()
        {
            size.heightUnit = ComponentUnit.Auto;

            text = new TextComponent();
            text.size.widthUnit = ComponentUnit.Auto;
            text.size.heightUnit = ComponentUnit.Auto;
            text.position.yUnit = ComponentUnit.Auto;
            AddChild(text);

            actionContainer = new ListComponent(true);
            actionContainer.size.widthUnit = ComponentUnit.Auto;
            actionContainer.size.heightUnit = ComponentUnit.Fixed;
            actionContainer.size.height = 3;
            actionContainer.position.yUnit = ComponentUnit.Auto;
            actionContainer.alignment.horizontal = ComponentHorizontalAlignment.Center;
            AddChild(actionContainer);
        }

        public void SetText(ContentString text)
        {
            this.text.SetText(text);
        }

        public void SetText(string text)
        {
            this.text.SetText(text);
        }

        public void SetActions(string[] actions)
        {
            actionContainer.RemoveAllChilds();

            foreach (string action in actions)
            {
                ButtonComponent button = new ButtonComponent(action);
                button.size.widthUnit = ComponentUnit.Fixed;
                button.size.width = action.Length + 2;
                button.size.heightUnit = ComponentUnit.Auto;
                button.position.xUnit = ComponentUnit.Auto;

                button.OnSelection += (button) => OnAction?.Invoke(this, action);

                actionContainer.AddChild(button);
            }
        }

        public int GetDynamicWidth()
        {
            int width = 0;

            text.parent = parent;
            int textWidth = dimensionHelper.CalculateInnerWidth(text);
            text.parent = this;

            actionContainer.parent = parent;
            int actionWidth = dimensionHelper.CalculateOuterWidth(actionContainer);
            actionContainer.parent = this;

            width = Math.Max(textWidth, actionWidth);

            return width;
        }

        public int GetDynamicHeight()
        {
            int height = 0;

            text.parent = parent;
            int textHeight = dimensionHelper.CalculateInnerHeight(text);
            text.parent = this;

            height = textHeight;
            height += 3;

            return height;
        }
    }
}
