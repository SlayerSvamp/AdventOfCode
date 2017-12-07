using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    enum InitialState { Example, Listed, Real }
    class State
    {
        private int hash { get; set; }
        public List<Item> Items { get; set; }
        public int Floor { get; set; }
        public State Parent { get; set; } = null;
        public int LowestFloor { get { return Items.Min(x => x.Floor); } }
        public bool Done { get { return !Items.Any(x => x.Floor < 4); } }
        public IEnumerable<Item> ItemsOnFloor { get { return Items.Where(x => x.Floor == Floor); } }
        public Item this[Item item] { get { return Items.First(x => x.Atom == item.Atom && x.Type == item.Type); } }
        public Item this[Atom atom, ItemType type] { get { return Items.First(x => x.Atom == atom && x.Type == type); } }
        public State Clone()
        {
            return new State
            {
                Floor = Floor,
                Items = Items.Select(x => x.Clone()).ToList()
            };
        }
        public bool IsValid()
        {
            foreach (var chip in Items.Where(x => x.Type == ItemType.Microchip))
            {
                var generators = Items.Where(x => x.Type == ItemType.Generator && x.Floor == chip.Floor).ToList();
                if (generators.Any() && !generators.Any(x => x.Atom == chip.Atom))
                    return false;
            }

            return true;
        }
        public override int GetHashCode()
        {
            return hash > 0 ? hash
                            : (hash = Items.Where(x => x.Type == ItemType.Generator)
                                        .Select(x => x.GetHashCode() + this[x.Atom, ItemType.Microchip].GetHashCode())
                                        .OrderBy(x => x)
                                        .Aggregate(Floor - 1, (sum, hash) => hash + (sum << 4)));
        }
        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
        public List<State> GetPossibleStates()
        {
            var newStates = new List<State>();
            var movable = ItemsOnFloor.ToList();
            for (var i = 0; i < movable.Count; i++)
            {
                var item = movable[i];
                if (Floor < 4)
                {
                    //one item, move up
                    var oneUp = Clone();
                    oneUp.Parent = this;
                    oneUp.Floor++;
                    oneUp[item].Floor++;

                    //add one up
                    if (oneUp.IsValid())
                        newStates.Add(oneUp);

                    //two items, move up
                    for (int n = i + 1; n < movable.Count; n++)
                    {
                        var item2 = movable[n];
                        var twoUp = oneUp.Clone();
                        twoUp.Parent = this;
                        twoUp[item2].Floor++;

                        if (twoUp.IsValid())
                            newStates.Add(twoUp);
                    }
                }
                if (Floor > LowestFloor)
                {
                    //one item, move down
                    var oneDown = Clone();
                    oneDown.Parent = this;
                    oneDown.Floor--;
                    oneDown[item].Floor--;

                    if (oneDown.IsValid())
                        newStates.Add(oneDown);


                    //two items, move up
                    for (int n = i + 1; n < movable.Count; n++)
                    {
                        var item2 = movable[n];
                        var twoDown = oneDown.Clone();
                        twoDown.Parent = this;
                        twoDown[item2].Floor--;

                        if (twoDown.IsValid())
                            newStates.Add(twoDown);
                    }

                }
            }

            return newStates;
        }

        public static State InitialState(InitialState state)
        {
            switch (state)
            {
                case Day11.InitialState.Example:
                    return ExampleState();
                case Day11.InitialState.Listed:
                    return ListedState();
                case Day11.InitialState.Real:
                    return RealState();
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
        public static State ListedState()
        {
            var state = new State
            {
                Floor = 1,
                Items = new List<Item>
                {
                    //The first floor contains a promethium generator and a promethium-compatible microchip.
                    new Item { Floor = 1, Type = ItemType.Generator, Atom = Atom.Promethium },
                    new Item { Floor = 1, Type = ItemType.Microchip, Atom = Atom.Promethium },
                    //The second floor contains a cobalt generator, a curium generator, a ruthenium generator, and a plutonium generator.
                    new Item { Floor = 2, Type = ItemType.Generator, Atom = Atom.Cobalt },
                    new Item { Floor = 2, Type = ItemType.Generator, Atom = Atom.Curium },
                    new Item { Floor = 2, Type = ItemType.Generator, Atom = Atom.Ruthenium },
                    new Item { Floor = 2, Type = ItemType.Generator, Atom = Atom.Plutonium },
                    //The third floor contains a cobalt-compatible microchip, a curium-compatible microchip, a ruthenium-compatible microchip, and a plutonium-compatible microchip.
                    new Item { Floor = 3, Type = ItemType.Microchip, Atom = Atom.Cobalt },
                    new Item { Floor = 3, Type = ItemType.Microchip, Atom = Atom.Curium },
                    new Item { Floor = 3, Type = ItemType.Microchip, Atom = Atom.Ruthenium },
                    new Item { Floor = 3, Type = ItemType.Microchip, Atom = Atom.Plutonium },
                    //The fourth floor contains nothing relevant.
                }
            };
            return state;
        }
        public static State RealState()
        {
            var state = ListedState();
            state.Items.AddRange(new Item[]
            {
                new Item{Floor = 1, Type = ItemType.Generator, Atom = Atom.Elerium },
                new Item{Floor = 1, Type = ItemType.Microchip, Atom = Atom.Elerium },
                new Item{Floor = 1, Type = ItemType.Generator, Atom = Atom.Dilithium },
                new Item{Floor = 1, Type = ItemType.Microchip, Atom = Atom.Dilithium },
            });
            return state;
        }
        public static State ExampleState()
        {
            var state = new State
            {
                Floor = 1,
                Items = new List<Item>
                {
                    //The first floor contains a promethium generator and a promethium-compatible microchip.
                    new Item { Floor = 1, Type = ItemType.Microchip, Atom = Atom.Promethium },
                    new Item { Floor = 1, Type = ItemType.Microchip, Atom = Atom.Cobalt },
                    //The second floor contains a cobalt generator, a curium generator, a ruthenium generator, and a plutonium generator.
                    new Item { Floor = 2, Type = ItemType.Generator, Atom = Atom.Promethium },
                    //The third floor contains a cobalt-compatible microchip, a curium-compatible microchip, a ruthenium-compatible microchip, and a plutonium-compatible microchip.
                    new Item { Floor = 3, Type = ItemType.Generator, Atom = Atom.Cobalt }
                    //The fourth floor contains nothing relevant.
                }
            };
            return state;
        }
    }
}