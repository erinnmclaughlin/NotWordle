namespace NotWordle.Models;

public class Game
{
    private string Word { get; }
    public int MaxGuesses { get; } = 6;

    public List<GuessResult> PreviousGuesses { get; } = new();

    private Game()
    {
        Word = Words.GetRandomWord().ToUpper();
    }

    public static Game NewGame()
    {
        return new Game();
    }

    public void MakeGuess(string guess)
    {
        if (!Words.IsValid(guess))
            return;

        var result = GetResult(guess);
        PreviousGuesses.Add(result);
    }

    private GuessResult GetResult(string guess)
    {
        guess = guess.ToUpper();
        var index = 0;
        var letterPositions = new List<LetterPosition>();

        foreach (var letter in guess)
        {
            if (Word[index] == letter)
                letterPositions.Add(new(index, letter, LetterPositionResult.Correct));
            else if (Word.Contains(letter))
                letterPositions.Add(new(index, letter, LetterPositionResult.Misplaced));
            else
                letterPositions.Add(new(index, letter, LetterPositionResult.Incorrect));

            index++;
        }

        return new GuessResult(letterPositions);
    }
}
