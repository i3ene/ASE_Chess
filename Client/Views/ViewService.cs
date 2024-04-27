using System.Text;
using Client.Views.Contents;

namespace Client.Views
{
    public class ViewService
    {
        public bool async;

        public ViewService()
        {
            async = false;
            Console.OutputEncoding = Encoding.UTF8;
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
            for (int i = Console.CursorTop; i < Console.WindowHeight; i++)
            {
                string line = "".PadRight(Console.WindowWidth);
                Console.WriteLine(line);
            }
            Console.SetCursorPosition(0, 0);
        }
    }
}
