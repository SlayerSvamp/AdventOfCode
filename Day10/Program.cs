using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        interface IReceiver
        {
            int ID { get; set; }
            void AddValue(int value);
        }
        class Bot : IReceiver
        {
            public int ID { get; set; }
            public int? LowValue { get; set; } = null;
            public int? HighValue { get; set; } = null;
            public void AddValue(int value)
            {
                if (LowValue == null)
                {
                    LowValue = value;
                }
                else if (HighValue == null)
                {
                    if (LowValue < value)
                    {
                        HighValue = value;
                    }
                    else
                    {
                        HighValue = LowValue;
                        LowValue = value;
                    }
                }
                else
                {
                    throw new Exception($"bot {ID} recieved a value while holding two values");
                }
            }
            public void GiveValues(IReceiver low, IReceiver high)
            {
                low.AddValue((int)LowValue);
                high.AddValue((int)HighValue);
                //Value1 = null;
                //Value2 = null;
            }
        }
        class Output : IReceiver
        {
            public int ID { get; set; }
            public int Value { get; set; }
            public void AddValue(int value)
            {
                Value = value;
            }
        }
        enum ActionType { AddValue, GiveValues }
        enum ReceiverType { Output, Bot }
        class BotAction
        {
            public ActionType Type { get; set; }
            public int PrimaryBotID { get; set; }
            public int GivenValue { get; set; }
            public ReceiverType LowReceiverType { get; set; }
            public ReceiverType HighReceiverType { get; set; }
            public int LowReceiverID { get; set; }
            public int HighReceiverID { get; set; }
            public BotAction(string raw)
            {
                //0   1 2     3   4  5      6 7   8    9  10  11
                //bot X gives low to output Y and high to bot Z
                var segments = raw.Split(' ').ToList();
                if (segments[0] == "value")
                {
                    Type = ActionType.AddValue;
                    PrimaryBotID = int.Parse(segments[5]);
                    GivenValue = int.Parse(segments[1]);
                }
                else
                {
                    Type = ActionType.GiveValues;
                    PrimaryBotID = int.Parse(segments[1]);
                    LowReceiverType = (segments[5] == "bot") ? ReceiverType.Bot : ReceiverType.Output;
                    LowReceiverID = int.Parse(segments[6]);
                    HighReceiverType = (segments[10] == "bot") ? ReceiverType.Bot : ReceiverType.Output;
                    HighReceiverID = int.Parse(segments[11]);
                }
            }
        }
        static void Main(string[] args)
        {
            var actions = File.ReadAllLines("BotActions.txt").Select(line => new BotAction(line)).ToList();
            var completedActions = new List<BotAction>();
            var bots = new List<IReceiver>();
            var outputs = new List<IReceiver>();
            while (actions.Count > 0)
            {
                completedActions.ForEach(action => actions.Remove(action));
                actions.RemoveAll(a => completedActions.Contains(a));
                completedActions.Clear();
                actions.ForEach(action =>
                {
                    if (action.Type == ActionType.AddValue)
                    {
                        Bot bot = bots.SingleOrDefault(b => b.ID == action.PrimaryBotID) as Bot;
                        if (bot == null)
                        {
                            bot = new Bot() { ID = action.PrimaryBotID };
                            bots.Add(bot);
                        }
                        bot.AddValue(action.GivenValue);
                        completedActions.Add(action);
                    }
                    else
                    {
                        Bot bot = bots.SingleOrDefault(b => b.ID == action.PrimaryBotID) as Bot;
                        if (bot != null && bot.LowValue != null && bot.HighValue != null)
                        {
                            IReceiver low;
                            IReceiver high;
                            low = ((action.LowReceiverType == ReceiverType.Bot) ? bots : outputs).SingleOrDefault(receiver => receiver.ID == action.LowReceiverID);
                            if (low == null)
                            {
                                if (action.LowReceiverType == ReceiverType.Bot)
                                {
                                    low = new Bot() { ID = action.LowReceiverID };
                                    bots.Add(low as Bot);
                                }
                                else
                                {
                                    low = new Output() { ID = action.LowReceiverID };
                                    outputs.Add(low as Output);
                                }
                            }

                            high = ((action.HighReceiverType == ReceiverType.Bot) ? bots : outputs).SingleOrDefault(receiver => receiver.ID == action.HighReceiverID);
                            if (high == null)
                            {
                                if (action.HighReceiverType == ReceiverType.Bot)
                                {
                                    high = new Bot() { ID = action.HighReceiverID };
                                    bots.Add(high as Bot);
                                }
                                else
                                {
                                    high = new Output() { ID = action.HighReceiverID };
                                    outputs.Add(high as Output);
                                }
                            }

                            if (low != null && high != null)
                            {
                                bot.GiveValues(low, high);
                                completedActions.Add(action);
                            }
                        }

                    }
                });
            }
            Console.WriteLine($"outputs where ID[] => [0]*[1]*[2] = {outputs.Where(o => o.ID < 3).Select(o => (o as Output).Value).Aggregate((o, o2) => o * o2)}");
            Console.WriteLine($"Bot comparing 61 with 17 = {bots.ConvertAll(rec => rec as Bot).Single(bot => bot.LowValue == 17 && bot.HighValue == 61).ID}");

            Console.ReadKey();
        }
    }
}
