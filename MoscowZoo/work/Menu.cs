using Spectre.Console;

namespace MoscowZoo;

public class Menu : IMenu
{
    public int ReadingMenu(string[] data)
    {
        Console.Clear();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите пункт меню:")
                .PageSize(10)
                .MoreChoicesText("[grey](Используйте стрелки для навигации)[/]")
                .AddChoices(data)
        );

        return Array.IndexOf(data, selection);
    }
}