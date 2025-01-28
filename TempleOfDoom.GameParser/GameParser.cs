using TempleOfDoom.GameLogic.Models;
using TempleOfDoom.GameLogic.Services;
using TempleOfDoom.JsonGameParser.DTOs;
using TempleOfDoom.GameLogic.Models.Interfaces;
using TempleOfDoom.GameLogic.Models.Door;
using TempleOfDoom.GameParser.DTOs;
using TempleOfDoom.GameLogic.Models.Adapters;
using Room = TempleOfDoom.GameLogic.Models.Room;
using TempleOfDoom.GameLogic.Models.Decorators.Door;

namespace TempleOfDoom.JsonGameParser
{
    public class GameParser
    {
        private LocatableFactory _locatableFactory;
        private IGameParserStrategy _strategy;

        public GameParser(LocatableFactory locatableFactory, IGameParserStrategy strategy)
        {
            _locatableFactory = locatableFactory;
            _strategy = strategy;
        }

        public GameLogic.Models.Game Parse(string path)
        {
            var gameDTO = _strategy.Parse(path);

            var rooms = new List<GameLogic.Models.Room>();

            foreach (var roomDTO in gameDTO.rooms)
            {
                rooms.Add(BuildRoom(roomDTO, gameDTO.connections));
            }
            var playerDTO = gameDTO.player;

            ICollidable collidable = new BaseCollidable((playerDTO.startX, playerDTO.startY));

            var player = (GameLogic.Models.Player)_locatableFactory.CreateLocatable("player", [collidable, playerDTO.lives]);

            var startRoom = rooms.First(r => r.Id == playerDTO.startRoomId);

            startRoom.AddLocatable(player);

            AddDoors(rooms, gameDTO.connections);

            return new GameLogic.Models.Game(rooms, player);
        }

        private void AddDoors(IEnumerable<GameLogic.Models.Room> rooms, IEnumerable<Connection> connections)
        {
            var roomLookup = rooms.ToDictionary(r => r.Id);

            foreach (var connection in connections)
            {
                if (roomLookup.TryGetValue(connection.EAST, out var eastRoom) &&
                    roomLookup.TryGetValue(connection.WEST, out var westRoom))
                {
                    ProcessDoorPair(
                        eastRoom, westRoom,
                        (0, eastRoom.Height / 2),
                        (westRoom.Width, westRoom.Height / 2),
                        connection,
                        false
                    );
                }

                if (roomLookup.TryGetValue(connection.NORTH, out var northRoom) &&
                    roomLookup.TryGetValue(connection.SOUTH, out var southRoom))
                {
                    ProcessDoorPair(
                        southRoom, northRoom,
                        (southRoom.Width / 2, 0),
                        (northRoom.Width / 2, northRoom.Height),
                        connection,
                        true
                    );
                }
            }
        }

        private void ProcessDoorPair(
            GameLogic.Models.Room sourceRoom,
            GameLogic.Models.Room targetRoom,
            (int x, int y) sourcePosition,
            (int x, int y) targetPosition,
            Connection connection,
            bool horizontal)
        {
            RemoveWallsAtPosition(sourceRoom, sourcePosition);
            RemoveWallsAtPosition(targetRoom, targetPosition);

            var sourcePosToGoTo = sourcePosition;
            var targetPosToGoTo = targetPosition;

            if (!horizontal)
            {
                sourcePosToGoTo = (sourcePosition.x + 1, sourcePosition.y);
                targetPosToGoTo = (targetPosition.x - 1, targetPosition.y);
            }
            else
            {
                sourcePosToGoTo = (sourcePosition.x, sourcePosition.y + 1);
                targetPosToGoTo = (targetPosition.x, targetPosition.y - 1);
            }

            IDoor baseDoor = new BaseDoor(sourcePosition, targetRoom.Id, targetPosToGoTo);
            IDoor reverseDoor = new BaseDoor(targetPosition, sourceRoom.Id, sourcePosToGoTo);

            foreach (var doorDTO in connection.doors.Reverse())
            {
                baseDoor = DecorateDoor(baseDoor, doorDTO, sourceRoom, horizontal);
            }

            foreach (var doorDTO in connection.doors.Reverse())
            {
                reverseDoor = DecorateDoor(reverseDoor, doorDTO, targetRoom, horizontal);
            }
            sourceRoom.AddLocatable(baseDoor);
            targetRoom.AddLocatable(reverseDoor);
        }

        private void RemoveWallsAtPosition(GameLogic.Models.Room room, (int x, int y) position)
        {
            var wallsToRemove = room.GetLocatables().OfType<Wall>().Where(l => l.Position == position).ToArray();
            foreach (var wall in wallsToRemove)
            {
                room.RemoveLocatable(wall);
            }
        }

        private IDoor DecorateDoor(IDoor door, DTOs.Door doorDTO, GameLogic.Models.Room room, bool isHorizontal)
        {
            var name = doorDTO.type.ToLower().Replace(" ", "");

            List<object> arguments = [door];

            if (name == nameof(Colored).ToLower())
                arguments.Add(isHorizontal);

            if (name == nameof(Toggle).ToLower() || name == nameof(Switched).ToLower())
            {
                arguments.Add(room);
            }

            if (name == nameof(OpenOnStonesInRoom).ToLower())
            {
                arguments.Add(room);
                if (IsDefaultValue(doorDTO.no_of_stones))
                    arguments.Add(doorDTO.no_of_stones);
            }

            var properties = doorDTO.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == nameof(doorDTO.type))
                    continue;

                var value = property.GetValue(doorDTO);
                if (value != null && !IsDefaultValue(value))
                {
                    arguments.Add(value);
                }
            }
            return (IDoor)_locatableFactory.CreateLocatable(name, arguments.ToArray());
        }

        private GameLogic.Models.Room BuildRoom(DTOs.Room roomDTO, IEnumerable<Connection> connections)
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

            if (roomDTO.items != null)
            {
                foreach (var item in roomDTO.items)
                {
                    room.AddLocatable(CreateLocatableFromItem(item));
                }
            }

            if (roomDTO.enemies != null)
            {
                foreach (var enemy in roomDTO.enemies)
                {
                    room.AddLocatable(CreateLocatableFromEnemy(enemy));
                }
            }

            if (roomDTO.specialFloorTiles != null)
            {
                foreach (var specialFloorTile in roomDTO.specialFloorTiles)
                {
                    if (specialFloorTile.type == "wall")
                        room.AddLocatable(new Wall((specialFloorTile.x,specialFloorTile.y)));
                    if (specialFloorTile.type == "innerdoor")
                    {
                        IDoor door = new BaseDoor((specialFloorTile.x, specialFloorTile.y), roomDTO.id, (specialFloorTile.x, specialFloorTile.y - 1));
                        door = new TogglePosition(door, (specialFloorTile.x, specialFloorTile.y + 1));
                        room.AddLocatable(BuildWithin(door, room, connections));
                    }
                }
            }
            return room;
        }

        private IDoor BuildWithin(IDoor door, Room room, IEnumerable<Connection> connections)
        {
            var connection = connections.First(c => c.within == room.Id);

            foreach (var doorDTO in connection.doors)
            {
                door = DecorateDoor(door, doorDTO, room, true);
            }
            return door;
        }

        private ILocatable CreateLocatableFromItem(Item item)
        {
            var name = item.type.ToLower().Replace(" ", "");
            var collidable = new BaseCollidable((item.x, item.y));
            var arguments = new List<object> { collidable };

            var properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == nameof(item.type) || property.Name == nameof(item.x) || property.Name == nameof(item.y))
                    continue;

                var value = property.GetValue(item);
                if (value != null && !IsDefaultValue(value))
                {
                    arguments.Add(value);
                }
            }

            return _locatableFactory.CreateLocatable(name, arguments.ToArray());
        }

        private ILocatable CreateLocatableFromEnemy(Enemy enemy)
        {
            var name = enemy.type.ToLower().Replace(" ", "");
            var collidable = new BaseCollidable((enemy.x, enemy.y));
            var arguments = new List<object>{
                collidable,
                (enemy.minX, enemy.minY),
                (enemy.maxX, enemy.maxY)
            };

            return _locatableFactory.CreateLocatable(name, arguments.ToArray());
        }

        private bool IsDefaultValue(object value)
        {
            if (value is int intValue && intValue == 0)
                return true;

            if (value is double doubleValue && doubleValue == 0.0)
                return true;

            if (value is float floatValue && floatValue == 0.0f)
                return true;

            if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
                return true;

            return false;
        }
    }
}
