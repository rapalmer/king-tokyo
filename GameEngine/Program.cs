using System;
using System.Windows.Forms;
using GameEngine.Views;

namespace GameEngine
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            StartUp();
        }

        [STAThread]
        public static void StartUp(bool login = true)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form form;
            if (login)
            {
                form = new LoginForm();
            }
            else
            {
                form = new MainMenuForm();
            }
            form.Show();
            Application.Run();
        }

        [STAThread]
        public static void Run()
        {
            using (var game = new Engine())
                game.Run();
        }
    }
#endif
}