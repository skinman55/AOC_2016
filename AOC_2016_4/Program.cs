using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2016_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var rd = new RoomDecoder();
            rd.Run();
        }
    }

    class RoomDecoder
    {
        private List<Room> _allRooms = new List<Room>();

        public void Run()
        {
            var input = File.ReadAllLines("input.txt");
            foreach (var line in input)
            {
                //_allRooms.Add(new Room(line));
                var r = new Room(line);
                if (r.IsRealRoom())
                {
                    var name = r.DecryptRoomName();
                    if (name.Contains("north"))
                    {
                        Console.WriteLine(r.SectorId);                        
                    }
                    
                }
            }

            //var room = new Room("qzmt-zixmtkozy-ivhz-343[asdf]");
            //Console.WriteLine(room.DecryptRoomName());
            //var sectors = _allRooms.Where(x => x.IsRealRoom()).Select(x => x.SectorId).Sum();

            //Console.WriteLine(sectors);
            Console.ReadLine();
        }
    }

    class Room
    {
        public int SectorId { get; private set; }
        public string Name { get; private set; }
        private string _checksum;
        public Room(string encryptedID)
        {
            ParseRoomID(encryptedID);
        }

        private void ParseRoomID(string encryptedID)
        {
            var data = encryptedID;
            var sectorIndex = data.LastIndexOf("-");
            Name = data.Substring(0, sectorIndex);
            data = data.Remove(0, sectorIndex+1);
            var checksumIndex = data.LastIndexOf("[");
            SectorId = int.Parse(data.Substring(0, checksumIndex));
            data = data.Remove(0, checksumIndex);
            _checksum = data.Substring(1, data.IndexOf("]")-1);

        }

        public bool IsRealRoom()
        {
            var ccs = CalculateCheckSum();
            return _checksum == ccs;
        }

        private string CalculateCheckSum()
        {
            var chars = new List<char>();
            var sortedChars = string.Concat(Name.OrderBy(c => c)).Where(c => c != '-').GroupBy(c => c).OrderByDescending(x => x.Count()).ToList();

            var sb = new StringBuilder();
            for (var x = 0; x < 5; x++)
            {
                sb.Append(sortedChars[x].Key);
            }

            return sb.ToString();
        }

        public string DecryptRoomName()
        {
            var sb = new StringBuilder();
            foreach (var c in Name.ToCharArray())
            {
                if (c == '-')
                {
                    sb.Append(" ");
                }
                else
                {
                    sb.Append(ShiftCharacter(c));
                }
            }
            return sb.ToString();
        }

        private readonly List<char>_allCharacters = new List<Char>
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z'
        };

        private char ShiftCharacter(char c)
        {
            if (c == 'v')
            {
                
            }
            var idx = _allCharacters.IndexOf(c) - 1;
            for (var x = 0; x <= SectorId; x++)
            {
                idx++;
                if (idx >= 26)
                {
                    idx = 0;
                }
                
            }
            return _allCharacters[idx];
        }
    }
}
