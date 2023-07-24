// Program.cs
using System;
using System.Windows.Forms;

public class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        TimeConverter timeConverter = new TimeConverter();
        timeConverter.RegisterGlobalHotKey();

        Application.AddMessageFilter(new MessageFilter(timeConverter));
        Application.Run();

        timeConverter.UnregisterGlobalHotKey();
    }
}

public class MessageFilter : IMessageFilter
{
    private const int WM_HOTKEY = 0x0312;
    private TimeConverter _timeConverter;

    public MessageFilter(TimeConverter timeConverter)
    {
        _timeConverter = timeConverter;
    }

    public bool PreFilterMessage(ref Message m)
    {
        if (m.Msg == WM_HOTKEY)
        {
            _timeConverter.CopySelectedText();
            Thread.Sleep(500); // Wait a bit for the clipboard to get the text
            _timeConverter.ConvertUnixTimestampAndDisplay();
        }
        return false;
    }
}