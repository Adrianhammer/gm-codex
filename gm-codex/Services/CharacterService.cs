using gm_codex.ConsoleUI;
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
        var characterData = CreateCharacterUi.CreateCharacterCommand();
        if (characterData == null)
        {
            Console.WriteLine("ERROR: Character creation aborted due to invalid input.");
            return;
        }

        var (name, race, subRace, characterClass, subClass) = characterData.Value;
        
        var character = new Character
        {
            Name = name,
            Race = race,
            SubRace = subRace,
            CharacterClass = characterClass,
            SubClass = subClass
        };
        
        _repository.InsertCharacter(character);
        Console.WriteLine("Character created");
    }
}