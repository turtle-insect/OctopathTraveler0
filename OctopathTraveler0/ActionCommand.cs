using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OctopathTraveler0
{
	internal class ActionCommand : ICommand
	{
#pragma warning disable CS0067
		public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067

		private readonly Action<object?> mAction;
		public bool CanExecute(object? parameter) => true;
		public ActionCommand(Action<object?> action) => mAction = action;
		public void Execute(object? parameter) => mAction(parameter);
	}
}
