using gm_codex.Data;
using gm_codex.Models;

namespace gm_codex.Services;

public class CharacterService
{
    private readonly CharacterRepository _repository;

    public CharacterService(CharacterRepository repository)
    {
        _repository = repository;
    }
    public void CreateCharacter()
    {
        Console.WriteLine("Type 1 to create a character: ");
        string prompt = Console.ReadLine();
        
        if (prompt != "1")
        {
            Console.WriteLine("Cancelled");
            return;
        }
        
        Console.WriteLine("Name: ");
        string name = Console.ReadLine();
        
        Console.WriteLine("Race: ");
        string raceInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(raceInput) || !Enum.TryParse(raceInput, true, out Race parsedRace))
        {
            Console.WriteLine("Race needs to be of a valid race");
            return;
        }
        
        Console.WriteLine("Sub Race: ");
        string subRace = Console.ReadLine();
        
        Console.WriteLine("Class: ");
        string characterClassInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(characterClassInput) ||
            !Enum.TryParse(characterClassInput, true, out Class parsedClass))
        {
            Console.WriteLine("Class needs to be of a valid class");
            return;
        }
        
        Console.WriteLine("Sub Class: ");
        string characterSubClass = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(raceInput) ||
            string.IsNullOrWhiteSpace(characterClassInput))
        {
            Console.WriteLine("Name, Race and Class needs to be provided");
            return;
        }
        
        var character = new Character
        {
            Name = name,
            Race = parsedRace,
            SubRace = subRace,
            CharacterClass = parsedClass,
            SubClass = characterSubClass
        };
        
        _repository.InsertCharacter(character);
        Console.WriteLine("Character created");
    }
}