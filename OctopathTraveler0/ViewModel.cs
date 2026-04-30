using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Input;

namespace OctopathTraveler0
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public ICommand OpenFileCommand { get; }
		public ICommand SaveFileCommand { get; }
		public ICommand ImportFileCommand { get; }
		public ICommand ExportFileCommand { get; }

		public Basic Basic { get; private set; } = new();
		

		public ViewModel()
		{
			OpenFileCommand = new ActionCommand(OpenFile);
			SaveFileCommand = new ActionCommand(SaveFile);
			ImportFileCommand = new ActionCommand(ImportFile);
			ExportFileCommand = new ActionCommand(ExportFile);
		}

		public void Initialize()
		{
			Basic = new();
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Basic)));
		}

		private void OpenFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance.Open(dlg.FileName);
			Initialize();
		}

		private void SaveFile(object? parameter)
		{
			SaveData.Instance.Save();
		}

		private void ImportFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance.Import(dlg.FileName);
		}

		private void ExportFile(object? parameter)
		{
			SaveFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance.Export(dlg.FileName);
		}
	}
}
