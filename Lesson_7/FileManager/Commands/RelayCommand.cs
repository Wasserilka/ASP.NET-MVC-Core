﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace FileManager.Commands
{
    internal class RelayCommand : ICommand
    {
        readonly internal Action<object> _execute;
        readonly internal Predicate<object> _canExecute;

        internal RelayCommand(Action<object> execute) : this(execute, null) { }

        internal RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute; _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) { _execute(parameter); }
    }
}
