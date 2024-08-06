namespace Companion.Console;
using Console = System.Console;
static class ConsoleUtility
{
// Repeat
    static string Repeat(string str, int times) {
        return string.Concat(Enumerable.Repeat(str, times));
    }

// Progress Bar
public static void ProgressBar(int progress, int total, int chunks = 30, ConsoleColor completeColour = ConsoleColor.Green,
        ConsoleColor remainingColour = ConsoleColor.Gray, string symbol = "■", bool showPercent = true)
    {
        Console.WriteLine();
        //Draw Blank Progress Bar
        Console.CursorLeft = 0;
        Console.Write("  [");
        Console.CursorLeft = chunks + 3;
        Console.Write("]");
        Console.CursorLeft = 3;
        float chunk = (float)chunks / total;

        // Chunk Calculations
        double completeRaw = Math.Ceiling((double)chunk * progress);
        int complete = (int)Math.Ceiling((double)chunk * progress);
        if (complete > chunks) complete = chunks;
        int remaining = chunks - complete;

        // Draw Progress
        Console.ForegroundColor = completeColour;
        Console.Write(Repeat(symbol, complete));
        Console.ForegroundColor = remainingColour;
        Console.Write(Repeat(symbol, remaining));

        // Show Percent
        Console.CursorLeft = chunks + 4;
        Console.ResetColor();
        if (showPercent)
        {
            int percent = (int)((float)progress / (float)total * 100);
            Console.Write($" {Repeat(" ", 3 - percent.ToString().Length)}{percent} %");
        }
        Console.WriteLine();
    }
}