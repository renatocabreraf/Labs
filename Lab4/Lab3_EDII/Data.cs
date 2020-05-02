
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3_EDII
{
	public class Data
	{
		private static Data instance = null;
		public static Data Instance
		{
			get
			{
				if (instance == null) instance = new Data();
				return instance;
			}
		}

		public List<CompressionsCollection> archivos = new List<CompressionsCollection>();
	}
}
