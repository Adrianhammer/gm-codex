using System;
namespace gm_codex.ConsoleUI;

public static class CreatePcUi
{
    public static (string Name, Race Race, string? EntityType, string? SubRace, Class CharacterClass, string? SubClass)? CreateCharacterCommand()
    {
        
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        
        Console.WriteLine("Race: ");
        var raceInput = Console.ReadLine();

        Console.WriteLine("Entity type (PC or NPC): ");
        string? entityType = Console.ReadLine();
        
        Console.WriteLine("Sub Race: ");
        string? subRace = Console.ReadLine();
        
        Console.WriteLine("Class: ");
        var classInput = Console.ReadLine();
        
        Console.WriteLine("Sub Class: ");
        var subClass = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name needs to be provided");
            return null;
        }
        
        if (string.IsNullOrWhiteSpace(raceInput) || !Enum.TryParse(raceInput, true, out Race parsedRace))
        {
            Console.WriteLine("Race needs to be of a valid race");
            return null;
        }

        if (string.IsNullOrWhiteSpace(classInput) ||
            !Enum.TryParse(classInput, true, out Class parsedClass))
        {
            Console.WriteLine("Class needs to be of a valid class");
            return null;
        }
        
        return (name, parsedRace, entityType, subRace, parsedClass, subClass);

    }
}