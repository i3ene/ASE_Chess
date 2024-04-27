using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Views.Components.Styles;
using Client.Views.Components.Styles.Borders;
using Client.Views.Contents;

namespace Client.Views.Components
{
    public class CheckboxComponent : ButtonComponent
    {
        private bool check;

        public CheckboxComponent() : this(false) { }

        public CheckboxComponent(bool check) : base()
        {
            size.width = new StyleValue(StyleUnit.Fixed, 3);
            size.height = new StyleValue(StyleUnit.Fixed, 3);
            border.style = BorderStyle.Round;

            this.check = check;
            OnSelection += (button) => ToggleCheck();
            SetCheck(this.check);
        }

        private new void SetText(string text)
        {
            base.SetText(text);
        }

        private new void SetText(ContentString text)
        {
            base.SetText(text);
        }

        private new ContentString GetText()
        {
            return base.GetText();
        }

        public bool IsChecked()
        {
            return check;
        }

        public void SetCheck(bool check)
        {
            this.check = check;
            SetText(this.check ? "X" : " ");
            Update();
        }

        public bool ToggleCheck()
        {
            SetCheck(!check);
            return check;
        }
    }
}
