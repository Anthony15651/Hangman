using System.ComponentModel.DataAnnotations;

string hiddenWord = "";
int guesses = 5;
string? userInput = "";
bool gameIsPlaying = false;
bool programRunning = true;


while (programRunning)
{
    // Startup
    Console.Clear();
    Console.WriteLine("Welcome to hangman!\n\nPlease enter a word to begin:");
    bool validEntry = false;
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
        }
        else
        {
            Console.WriteLine("Oh no! You've run out of guesses.");
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