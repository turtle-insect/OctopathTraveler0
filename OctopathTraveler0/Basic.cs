using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OctopathTraveler0
{
	internal class Basic
	{
		private readonly uint _moneyAddress = uint.MaxValue;
		public Basic()
		{
			var list = SaveData.Instance.FindAddress("Money", 0);
			if (list.Count == 0) return;

			_moneyAddress = list[0] + 31;
		}

		public uint Money
		{
			get => SaveData.Instance.ReadNumber(_moneyAddress, 4);
			set => SaveData.Instance.WriteNumber(_moneyAddress, 4, value);
		}
	}
}
