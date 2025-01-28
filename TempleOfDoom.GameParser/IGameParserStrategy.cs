namespace TempleOfDoom.JsonGameParser
{
    public interface IGameParserStrategy
    {
        DTOs.Game Parse(string path);
    }
}
