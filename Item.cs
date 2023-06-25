namespace PackerChallenge
{
    public class Item
    {
        public int Index { get; }
        public double Weight { get; }
        public decimal Cost { get; }

        public Item(int index, double weight, decimal cost)
        {
            Index = index;
            Weight = weight;
            Cost = cost;
        }
    }
}

