using System.ComponentModel.DataAnnotations;

string hiddenWord = "";
int guesses = 0;
string? userInput = "";
bool gameIsPlaying = false;
bool programRunning = true;
char[] incorrectGuesses = new char[26];

Random random = new Random();
string[] randomWords = { "commemorate", "concentrate", "freedom", "reveal", "despise", "relieve", "radiation", "simplicity", "sensitivity", "repetition", "impact", "cunning", "cellar", "dominant", "intervention", "familiar", "connection", "socialist", "revival", "dentist", "report", "temple", "hilarious", "spectrum", "photography", "assume", "improve", "agreement", "package", "psychology" };


while (programRunning)
{
    // Startup
    Console.Clear();
    Console.WriteLine("Welcome to hangman. Please select a game mode:\n1. Player vs Player\n2. Player vs Computer\n");
    userInput = Console.ReadLine();
    switch (userInput)
    {
        // Player vs Player
        case "1":
            Console.Clear();
            Console.WriteLine("Player vs Player\n\nPlayer 1, please enter a word to begin:");
            bool validEntry = false;
            guesses = 5;
            while (!validEntry)
            {
                userInput = Console.ReadLine();
                if (userInput != null && userInput != "")
                {
                    // Checks for user's word - it must be at least 5
                    // letters long and cannot contain any spaces
                    if (userInput.Length < 5)
                    {
                        Console.WriteLine("Your word must be at least 5 letters long.");
                    }
                    else if (userInput.Contains(' '))
                    {
                        Console.WriteLine("Your word cannot contain any spaces.");
                    }
                    else
                    {
                        userInput.ToLower().Trim();
                        hiddenWord = userInput;
                        Console.WriteLine("Your word has been saved! Press Enter to begin the game.");
                        Console.ReadLine();
                        validEntry = true;
                    }
                }
            }

            // Show '_'s in terminal in place of letters
            Console.Clear();
            char[] letters = hiddenWord.ToCharArray();
            char[] lettersCopy = new char[hiddenWord.Length];

            for (int i = 0; i < lettersCopy.Length; i++)
            {
                lettersCopy[i] = '_';
            }
            Console.WriteLine("\n\n");

            // Loop to take user's guess, then check for win conditions
            gameIsPlaying = true;
            while (gameIsPlaying)
            {
                Guess(letters, lettersCopy);
                CheckForWin(lettersCopy);
            }

            programRunning = PlayAgain();
            break;
        // Player vs Computer
        case "2":
            Console.Clear();
            Console.WriteLine("Player vs Computer\n\n");
            guesses = 5;
            hiddenWord = randomWords[random.Next(0, randomWords.Length)];
            

            // Show '_'s in terminal in place of letters
            Console.Clear();
            letters = hiddenWord.ToCharArray();
            lettersCopy = new char[hiddenWord.Length];

            for (int i = 0; i < lettersCopy.Length; i++)
            {
                lettersCopy[i] = '_';
            }
            Console.WriteLine("\n\n");

            // Loop to take user's guess, then check for win conditions
            gameIsPlaying = true;
            while (gameIsPlaying)
            {
                Guess(letters, lettersCopy);
                CheckForWin(lettersCopy);
            }

            programRunning = PlayAgain();
            break;
        default:
            Console.WriteLine("Please enter '1' or '2'.");
            Console.ReadLine();
            break;
    }
}

// Asks player for a letter to check
void Guess(char[] letters, char[] lettersCopy)
{
    bool validEntry = false;
    while (!validEntry)
    {
        Console.WriteLine("Please guess a letter.");
        WriteWord(lettersCopy);
        userInput = Console.ReadLine();
        if (userInput != null && userInput != "")
        {
            if (userInput.Length == 1)
            {
                CheckGuess(userInput, letters, lettersCopy);
                validEntry = true;
            }
            else
            {
                Console.WriteLine("\nOnly 1 letter at a time!");
            }
        }
    }
}

// Checks to see if letter guessed is in the word
// Also tracks guesses and ends game after guesses htis 0
void CheckGuess(string letter, char[] letters, char[] lettersCopy)
{
    if (hiddenWord.Contains(letter))
    {
        Console.WriteLine("\nThat guess is correct!");
        ShowLetter(Convert.ToChar(letter), letters, lettersCopy);
    }
    else
    {
        guesses--;
        if (guesses > 0)
        {
            Console.WriteLine($"\nThat guess is not correct. You have {(guesses > 1 ? guesses + " guesses remaining." : guesses + " guess remaining.")}");
            // Need to add code to show which letters have been guessed
        }
        else
        {
            Console.WriteLine($"Oh no! You've run out of guesses.\nThe word was {hiddenWord}.");
            gameIsPlaying = false;
        }
    }
}

// Changes '_' back to a letter if guessed correctly
void ShowLetter(char letter, char[] letters, char[] lettersCopy)
{
    for (int i = 0; i < letters.Length; i++)
    {
        if (letter == letters[i])
        {
            lettersCopy[i] = letter;
        }
    }
}

void WriteWord(char[] lettersCopy)
{
    Console.WriteLine();
    foreach (char letter in lettersCopy)
    {
        Console.Write(letter + " ");
    }
    Console.WriteLine("\n");
}

void CheckForWin(char[] lettersCopy)
{
    if (lettersCopy.Contains('_'))
    {
        return;
    }
    else
    {
        Console.WriteLine("Congratulations, you've guessed the word!");
        WriteWord(lettersCopy);
        gameIsPlaying = false;
    }
}

bool PlayAgain()
{
    Console.WriteLine("\nWould you like to play again? (Y / N)");
    bool validEntry = false;
    while (!validEntry)
    {
        userInput = Console.ReadLine();
        if (userInput.ToLower().Trim() == "y")
            return true;
        else if (userInput.ToLower().Trim() == "n")
            return false;
        else
            Console.WriteLine("Please enter 'Y' or 'N'.");
    }
    return false;
}