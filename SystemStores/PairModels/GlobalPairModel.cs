using System.Collections.Generic;

namespace SystemStores.PairModels
{
    public  partial struct KeyValuePairModel<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }

    public partial struct KeyValueIdentityPair<Tkey, TValue, TIdentity>
    {
        public Tkey Key { get; set; }
        public TValue Value { get; set; }
        public TIdentity Identity { get; set; }
    }
	
}
