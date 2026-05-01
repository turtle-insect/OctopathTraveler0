namespace OctopathTraveler0
{
	internal class SaveData
	{
		public static SaveData Instance { get; } = new SaveData();
		private String _filename = String.Empty;
		private Byte[] _buffer = [];

		private SaveData() { }

		public bool Open(String filename)
		{
			if (System.IO.File.Exists(filename) == false) return false;

			Byte[] buffer = System.IO.File.ReadAllBytes(filename);
			int length = BitConverter.ToInt32(buffer);
			Oodle oodle = new();
			_buffer = oodle.Decompress(buffer[8..], length);
			_filename = filename;
			return true;
		}

		public bool Save()
		{
			if (String.IsNullOrEmpty(_filename)) return false;
			if (_buffer.Length == 0) return false;

			Backup();

			Oodle oodle = new();
			Byte[] comp = oodle.Compress(_buffer);
			Byte[] buffer = [
				.. BitConverter.GetBytes(_buffer.Length),
				.. BitConverter.GetBytes(0),
				.. comp
			];

			System.IO.File.WriteAllBytes(_filename, buffer);
			return true;
		}

		public void Import(String filename)
		{
			if (_buffer.Length == 0) return;
			if (System.IO.File.Exists(filename) == false) return;

			_buffer = System.IO.File.ReadAllBytes(filename);
		}

		public void Export(String filename)
		{
			if (_buffer.Length == 0) return;

			System.IO.File.WriteAllBytes(filename, _buffer);
		}

		public uint ReadNumber(uint address, uint size)
		{
			if (_buffer.Length == 0) return 0;
			if (address + size > _buffer.Length) return 0;

			uint result = 0;
			for (int i = 0; i < size; i++)
			{
				result += (uint)_buffer[address + i] << (i * 8);
			}
			return result;
		}

		public void WriteNumber(uint address, uint size, uint value)
		{
			if (_buffer.Length == 0) return;
			if (address + size > _buffer.Length) return;

			for (uint i = 0; i < size; i++)
			{
				_buffer[address + i] = (Byte)(value & 0xFF);
				value >>= 8;
			}
		}

		public List<uint> FindAddress(String name, uint index)
		{
			List<uint> result = new List<uint>();
			if (_buffer.Length == 0) return result;
			if (_buffer.Length < name.Length) return result;

			uint max = (uint)_buffer.Length - (uint)name.Length + 1;
			for (; index < max; index++)
			{
				if (_buffer[index] != name[0]) continue;

				int len = 1;
				for (; len < name.Length; len++)
				{
					if (_buffer[index + len] != name[len]) break;
				}
				if (len >= name.Length) result.Add(index);
				index += (uint)len;
			}
			return result;
		}

		private void Backup()
		{
			DateTime now = DateTime.Now;
			String path = System.IO.Directory.GetCurrentDirectory();
			path = System.IO.Path.Combine(path, "backup");
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
			path = System.IO.Path.Combine(path, $"{now:yyyy-MM-dd HH-mm-ss} {System.IO.Path.GetFileName(_filename)}");
			System.IO.File.Copy(_filename, path, true);
		}
	}
}
