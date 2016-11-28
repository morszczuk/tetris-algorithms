using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Tetris.Helpers
{
    public class MessageBox
    {
        public static async Task<MessageDialogResult> ShowMessage(Window window,string title,string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = (window as MetroWindow);
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;

            return await metroWindow.ShowMessageAsync(title, message, dialogStyle, metroWindow.MetroDialogOptions);
        }
    }
}
