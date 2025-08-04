namespace gm_codex.ConsoleUI;

public class CreateCharacterUi
{
    public void CreateCharacterCommand()
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
        
        
    }
}