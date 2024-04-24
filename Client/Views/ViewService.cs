using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Views.Contents;

namespace Client.Views
{
    public class ViewService
    {
        public bool async;

        public ViewService()
        {
            async = false;
        }

        public Task Display(IViewable view)
        {
            return Task.Factory.StartNew(() =>
            {
                bool needsUpdate = true;
                view.OnUpdate += () => needsUpdate = true;
                while (true)
                {
                    if (needsUpdate)
                    {
                        Task display = Task.Factory.StartNew(() => DisplayContent(view));
                        needsUpdate = false;
                        if (!async) display.Wait();
                    }
                    Thread.Sleep(33);
                }
            });
        }

        private void DisplayContent(IViewable view)
        {
            ContentCanvas canvas = view.View();
            Console.SetCursorPosition(0, 0);
            foreach (ContentString row in canvas.GetRows())
            {
                ContentString line = row.PadRight(Console.WindowWidth);
                Console.WriteLine(line);
            }
        }
    }
}
