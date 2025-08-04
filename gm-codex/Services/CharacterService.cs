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
        string race = Console.ReadLine();
        Console.WriteLine("Sub Race: ");
        string subRace = Console.ReadLine();
        Console.WriteLine("Class: ");
        string characterClass = Console.ReadLine();
        Console.WriteLine("Sub Class: ");
        string characterSubClass = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(race) ||
            string.IsNullOrWhiteSpace(characterClass))
        {
            Console.WriteLine("Name, Race and Class needs to be provided");
            return;
        }
        
        var character = new Character
        {
            Name = name,
            Race = race,
            SubRace = subRace,
            CharacterClass = characterClass,
            SubClass = characterSubClass
        };
        
        _repository.InsertCharacter(character);
        Console.WriteLine("Character created");
    }
}