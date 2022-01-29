namespace NotWordle
{
    public enum LetterPositionResult { Correct, Misplaced, Incorrect }

    public record GuessResult(List<LetterPosition> LetterPositions);
    public record LetterPosition(int Position, char Letter, LetterPositionResult Result);

    public class GameManager
    {
        public string Word { get; private set; }
        public int GuessCount { get; private set; }
        public int MaxGuesses { get; private set; } = 6;
        public bool IsCorrect { get; private set; }

        public bool GameOver => IsCorrect || GuessCount >= MaxGuesses;
        public int GuessesLeft => MaxGuesses - GuessCount;
        public List<GuessResult> PreviousGuesses { get; } = new();

        private GameManager()
        {
            Word = Words.GetRandomWord().ToUpper();
        }

        public static GameManager NewGame()
        {
            return new GameManager();
        }

        public bool MakeGuess(string guess)
        {
            guess = guess.ToLower();

            if (!Words.Values.Contains(guess))
                return false;

            var result = GetResult(guess);
            PreviousGuesses.Add(result);

            GuessCount++;
            IsCorrect = Word == guess.ToUpper();
            return true;
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
}
