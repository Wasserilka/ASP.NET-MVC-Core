using System;
using System.Windows;
using System.Windows.Input;

namespace Lesson_3
{
    public class MessageCommand : ICommand
    {
        internal string _message;

        public event EventHandler? CanExecuteChanged;

        internal string Message { get { return _message; } set { _message = value; } }

        internal MessageCommand(string Message) => _message = Message;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            MessageBox.Show($"{Message} - {parameter}");
        }
    }
}
