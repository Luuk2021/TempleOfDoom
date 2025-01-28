using System.Text.Json;

namespace TempleOfDoom.JsonGameParser
{
    public class JsonGameParserStrategy : IGameParserStrategy
    {
        public DTOs.Game Parse(string path)
        {
            var gameDTO = JsonSerializer.Deserialize<DTOs.Game>(File.ReadAllText(path));

            if (gameDTO == null)
            {
                throw new Exception("Could not parse game file");
            }
            return gameDTO;
        }
    }
}
