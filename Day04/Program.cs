using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    class Program
    {
        class Character
        {
            public char Char { get; set; }
            public int Count { get; set; }
        }
        public class Room
        {
            public string Crypted { get; set; }
            public bool IsDecoy { get; set; }
            public string Name { get; set; }
            public int Number { get; set; }
        }
        static char GetCharByShiftCount(Character c)
        {
            if (c.Char == ' ')
            {
                return ' ';
            }
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            int val = c.Count + alphabet.IndexOf(c.Char);
            return alphabet[val % alphabet.Length];
        }
        static void DecryptRoomName(Room room)
        {
            List<string> cryptedNames = room.Crypted.Split('-').ToList();
            cryptedNames.Remove(cryptedNames.Last());
            string name = string.Join(" ", cryptedNames.ToArray());
            char[] nameArray = name.ToCharArray();
            nameArray = name.ToCharArray().Select(c => GetCharByShiftCount(new Character() { Char = c, Count = room.Number })).ToArray();
            room.Name = new string(nameArray);
        }
        static void SetRoomNumberAndDecoyStatus(Room room)
        {
            List<Character> chars = new List<Character>();
            int indexOfNumber = 0;
            for (int i = 0; i < room.Crypted.Length; i++)
            {
                if ("0123456789".Contains(room.Crypted[i]))
                {
                    indexOfNumber = i;
                    break;
                }
                if (room.Crypted[i] == '-')
                {
                    continue;
                }
                if (!chars.Any(c => c.Char == room.Crypted[i]))
                {
                    chars.Add(new Character() { Char = room.Crypted[i], Count = room.Crypted.Where(c => c == room.Crypted[i]).Count() });
                }
            }

            string checksum = room.Crypted.Substring(room.Crypted.IndexOf('[') + 1, 5);

            room.IsDecoy = !(checksum == new string(chars.OrderBy(x => x.Count).ThenBy(x => 512 - x.Char).Reverse().Select(c => c.Char).ToArray()).Substring(0, 5));
            room.Number = Int32.Parse(room.Crypted.Substring(indexOfNumber, room.Crypted.IndexOf('[') - indexOfNumber));
        }
        static void Main(string[] args)
        {
            bool writeAll = false;
            List<string> roomStrings = File.ReadAllLines("Rooms.txt").ToList();
            List<Room> rooms = roomStrings.Select(sRoom => {
                Room room = new Room();
                room.Crypted = sRoom;
                SetRoomNumberAndDecoyStatus(room);
                DecryptRoomName(room);
                return room;
            }).ToList();
            rooms = rooms.OrderBy(room => room.IsDecoy).ThenBy(room => room.Name).ToList();
            Room correctRoom = rooms.First(r => r.Name.Contains("north"));
            Console.WriteLine($"Correct room: {correctRoom.Name} - {correctRoom.Number}");
            Console.WriteLine($"Room total excluding decoys = {rooms.Where(room => !room.IsDecoy).Select(r => r.Number).Sum()}");
            if (writeAll)
            {
                Console.WriteLine("-------------------------------------------");
                rooms.OrderBy(room => room.IsDecoy).ToList()
                    .ForEach(room => {
                        Console.WriteLine($"{room.Name} - {room.Number} {((room.IsDecoy) ? "(Decoy)" : "")}");
                    });
            }
            Console.ReadKey();
        }
    }
}
