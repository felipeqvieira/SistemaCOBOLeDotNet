using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        // Configura o runtime para suportar alta densidade de pixels e estilos visuais modernos
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        // Dispara a janela principal unificada
        Application.Run(new FormMain());
    }
}