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
            size.height = new StyleValue(StyleUnit.Auto);

            text = new TextComponent();
            text.size.width = new StyleValue(StyleUnit.Auto);
            text.size.height = new StyleValue(StyleUnit.Auto);
            text.position.y = new StyleValue(StyleUnit.Auto);
            AddChild(text);

            actionContainer = new ListComponent(true);
            actionContainer.size.width = new StyleValue(StyleUnit.Auto);
            actionContainer.size.width = new StyleValue(StyleUnit.Fixed, 3);
            actionContainer.position.y = new StyleValue(StyleUnit.Auto);
            actionContainer.alignment.horizontal = HorizontalAlignment.Center;
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
                button.size.width = new StyleValue(StyleUnit.Fixed, action.Length + 2);
                button.size.height = new StyleValue(StyleUnit.Auto);
                button.position.x = new StyleValue(StyleUnit.Auto);

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
