using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStores.GlobalModels
{
	public partial class TreeModel<TKey, TValue>
	{
		public TKey key { get; set; }
		public TValue Value { get; set; }
		public List<TreeModel<TKey, TValue>> Branch { get; set; }
	}
}
