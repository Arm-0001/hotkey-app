using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using AutoIt;

public class TimeConverter
{
    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vlc);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    public const uint MOD_NONE = 0x0000; //(none)
    public const uint MOD_ALT = 0x0001; //ALT
    public const uint MOD_CONTROL = 0x0002; //CTRL
    public const uint MOD_SHIFT = 0x0004; //SHIFT
    public const uint MOD_WIN = 0x0008; //WINDOWS
    public const uint VK_G = 0x47; //G key

    public const int HOTKEY_ID = 9000;

    public void RegisterGlobalHotKey()
    {
        RegisterHotKey(IntPtr.Zero, HOTKEY_ID, MOD_CONTROL | MOD_WIN, VK_G);
    }

    public void UnregisterGlobalHotKey()
    {
        UnregisterHotKey(IntPtr.Zero, HOTKEY_ID);
    }

    public void CopySelectedText()
    {
        AutoItX.Send("^c"); // simulate CTRL+C
    }

    public void ConvertUnixTimestampAndDisplay()
    {
        Thread.Sleep(100);  // Add this line to introduce a small delay
        if (Clipboard.ContainsText())
        {
            string unixTimestampText = Clipboard.GetText();
            if (long.TryParse(unixTimestampText, out long unixTimestamp))
            {
                DateTime convertedDate = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
                ShowConvertedTime(convertedDate);
            }
            else
            {
                MessageBox.Show("Invalid Unix timestamp!");
            }
        }
        else
        {
            MessageBox.Show("Clipboard does not contain text!");
        }
    }

    public void ShowConvertedTime(DateTime convertedTime)
    {
        MessageBox.Show(convertedTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
