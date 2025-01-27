using System.Text.Json;
using System;
using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.JsonGameParser.DTOs;
using TempleOfDoom.GameLogic.Models.Decorators.Door;
using TempleOfDoom.GameLogic.Models.Interfaces;

namespace TempleOfDoom.JsonGameParser
{
    public class GameParser
    {
        private LocatableFactory _locatableFactory;
        public GameLogic.Models.Game Parse(string path)
        {
            _locatableFactory = new LocatableFactory();
            var gameDTO = JsonSerializer.Deserialize<DTOs.Game>(File.ReadAllText(path));

            if (gameDTO == null)
            {
                throw new Exception("Could not parse game file");
            }

            var rooms = new List<GameLogic.Models.Room>();

            foreach (var roomDTO in gameDTO.rooms)
            {
                rooms.Add(BuildRoom(roomDTO));
            }

            //foreach (var connection in gameDTO.connections)
            //{
            //    var doors = BuildDoors(connection);
            //    var room1 = rooms.First(r => r.Id == connection.room1);
            //    var room2 = rooms.First(r => r.Id == connection.room2);
            //    room1.AddLocatable(doors[0]);
            //    room2.AddLocatable(doors[1]);
            //}
            return new GameLogic.Models.Game(rooms, null);
        }

        //private List<IDoor> BuildDoors(DTOs.Connection connection)
        //{

        //}

        private GameLogic.Models.Room BuildRoom(DTOs.Room roomDTO)
        {
            var room = new GameLogic.Models.Room(roomDTO.id);

            for (int x = 0; x < roomDTO.width; x++)
            {
                room.AddLocatable(new Wall((x, 0)));
                room.AddLocatable(new Wall((x, roomDTO.height - 1)));
            }

            for (int y = 0; y < roomDTO.height; y++)
            {
                room.AddLocatable(new Wall((0, y)));
                room.AddLocatable(new Wall((roomDTO.width - 1, y)));
            }

            if (roomDTO.items == null)
                return room;

            foreach (var item in roomDTO.items)
            {
                var name = item.type.ToLower().Replace(" ", "");
                ICollidable collidable = new BaseCollidable((item.x, item.y));

                switch (name)
                {
                    case "sankarastone":
                        collidable = new SankaraStone(collidable);
                        break;
                    case "key":
                        collidable = new Key(collidable, item.color);
                        break;
                    case "pressureplate":
                        collidable = new PressurePlate(collidable);
                        break;
                    case "boobytrap":
                        collidable = new BoobyTrap(collidable, item.damage);
                        break;
                    case "disappearingboobytrap":
                        collidable = (ICollidable)new DisappearingBoobyTrap(collidable, item.damage);
                        break;
                }
                room.AddLocatable(collidable);
            }

            return room;
        }
    }
}
