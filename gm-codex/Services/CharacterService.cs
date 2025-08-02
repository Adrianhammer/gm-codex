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

    //Will later delegate prompting to ConsoleUI for flare or commands 
    public void CreateCharacter()
    {
        Console.WriteLine("Type 1 to create a character: ");
        string prompt = Console.ReadLine();
    }
}