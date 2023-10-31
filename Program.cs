string hiddenWord = "";
int guesses = 5;
string? userInput = "";

// Startup
// Console.Clear();
Console.WriteLine("Welcome to hangman!\n\nPlease enter a word to begin:");
userInput = Console.ReadLine();
Console.WriteLine("Your word has been saved! Press Enter to begin the game.");
Console.ReadLine();



// Return '_'s to terminal in place of letters
// Console.Clear();
hiddenWord = userInput;
char[] letters = userInput.ToCharArray();
char[] lettersCopy = new char[userInput.Length];

for (int i = 0; i < lettersCopy.Length; i++)
{
    lettersCopy[i] = '_';
}

foreach (char letter in lettersCopy)
{
    Console.Write(letter + " ");
}
Console.WriteLine("\n\n");


// Guess();
// foreach (char letter in lettersCopy)
// {
//     Console.Write(letter + " ");
// }
// Console.WriteLine();
// Guess();
// foreach (char letter in lettersCopy)
// {
//     Console.Write(letter + " ");
// }



// Asks player for a letter to check
void Guess()
{
    bool validEntry = false;
    while (!validEntry)
    {
        Console.WriteLine("Please guess a letter.\n");
        userInput = Console.ReadLine();
        if (userInput != null && userInput != "")
        {
            if (userInput.Length == 1)
            {
                validEntry = CheckGuess(userInput);
            }
            else
            {
                Console.WriteLine("\nOnly 1 letter at a time!");
            }
        }
    }
}

// Checks to see if letter guessed is in the word
bool CheckGuess(string letter)
{
    if (hiddenWord.Contains(letter))
    {
        Console.WriteLine("Guess is correct!");
        ShowLetter(Convert.ToChar(letter));
        return true;
    }
    guesses--;
    Console.WriteLine($"\nGuess is not correct. You have {guesses} guesses remaining.");
    return false;
}

// Changes '_' back to a letter if guessed correctly
void ShowLetter(char letter)
{
    for (int i = 0; i < letters.Length; i++)
    {
        if (letter == letters[i])
        {
            lettersCopy[i] = letter;
        }
    }
}