﻿using TempleOfDoom.GameParser.DTOs;

namespace TempleOfDoom.JsonGameParser.DTOs
{
    public class Room
    {
        public int id { get; set; }
        public string type { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Item[] items { get; set; }
        public Enemy[] enemies { get; set; }
        public Item[] specialFloorTiles { get; set; }
    }
}
