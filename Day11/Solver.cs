using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Solver
    {
        public List<State> SavedState { get; set; }
        List<State> States { get; set; } = new List<State>();
        State CurrentState { get { return States.Last(); } }
        public class Pair
        {
            public int MicrochipFloor { get; set; }
            public int GeneratorFloor { get; set; }
            public bool OnSameFloor { get { return MicrochipFloor == GeneratorFloor; } }
            public Pair(int microchipFloor, int generatorFloor)
            {
                MicrochipFloor = microchipFloor;
                GeneratorFloor = generatorFloor;
            }
        }
        public class State
        {
            public List<Pair> Pairs { get; set; } = new List<Pair>();
            public int ElevatorFloor { get; set; }
            public bool CanMoveUp { get { return ElevatorFloor < 4; } }
            public bool CanMoveDown { get { return ElevatorFloor > 1; } }
            public bool ProblemSolved { get { return Pairs.All(pair => pair.GeneratorFloor == 4 && pair.MicrochipFloor == 4); } }
            public State()
            {
            }
            public List<State> PossibleStates
            {
                get
                {
                    List<State> possible = new List<State>();
                    if (CanMoveUp)
                    {
                        //one microchip up
                        Pairs.Where(pair => pair.MicrochipFloor == ElevatorFloor).ToList().ForEach(pair =>
                        {
                            if (pair.GeneratorFloor == ElevatorFloor + 1 || Pairs.All(p2 => p2.GeneratorFloor != (ElevatorFloor + 1)))
                            {
                                var pairs = new List<Pair>();
                                pairs.AddRange(Pairs.Select(p2 => new Pair(p2.MicrochipFloor, p2.GeneratorFloor)));

                                pairs.Last(p2 => p2.MicrochipFloor == pair.MicrochipFloor && p2.GeneratorFloor == pair.GeneratorFloor).MicrochipFloor++;
                                var state = new State() { Pairs = new List<Pair>()};
                                possible.Add(new State() { });
                            }
                        });
                        //one generator up
                        //two microchips up
                        //two generators up
                    }
                    //one pair up
                    //one microchip down
                    //one generator down
                    //two microchips down
                    //two generators down
                    //one pair down
                    return possible;
                }
            }
        }
        public void TryAll()
        {
            if (States.Last().ProblemSolved)
            {
                if (SavedState != null || States.Count < SavedState.Count)
                {
                    SavedState.AddRange(States);
                    return;
                }
            }
            CurrentState.PossibleStates.ForEach(state =>
            {
            });
        }
        public Solver()
        {
            States.Add(new State()
            {
                Pairs = new List<Pair> { new Pair(1, 1), new Pair(3, 2), new Pair(3, 2), new Pair(3, 2), new Pair(3, 2) },
                ElevatorFloor = 1
            });
        }
    }
}
