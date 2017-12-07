namespace Day11
{
    enum Atom
    {
        Promethium,
        Cobalt,
        Curium,
        Ruthenium,
        Plutonium,
        Elerium,
        Dilithium
    }
    enum ItemType
    {
        Microchip,
        Generator
    }
    class Item
    {
        public ItemType Type { get; set; }
        public Atom Atom { get; set; }
        public int Floor { get; set; }
        public Item Clone()
        {
            return new Item
            {
                Type = Type,
                Atom = Atom,
                Floor = Floor
            };
        }
        public override int GetHashCode()
        {
            return (Floor - 1) << (Type == ItemType.Generator ? 2 : 0);
        }
        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
    }
}