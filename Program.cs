using System.ComponentModel.DataAnnotations;
using CrypticWizard.RandomWordGenerator;

string hiddenWord = "";
string? userInput;
bool gameIsPlaying;
int guesses = 0;
bool programRunning = true;
char[] userGuesses = new char[0];

WordGenerator wordGenerator = new WordGenerator();

// Hardcoded words for computer - not needed since I'm using a random word generator
// Random random = new Random();
// string[] randomWords = { "commemorate", "concentrate", "freedom", "reveal", "despise", "relieve", "radiation", "simplicity", "sensitivity", "repetition", "impact", "cunning", "cellar", "dominant", "intervention", "familiar", "connection", "socialist", "revival", "dentist", "report", "temple", "hilarious", "spectrum", "photography", "assume", "improve", "agreement", "package", "psychology" };


while (programRunning)
{
    // Startup
    bool validEntry;

    Console.Clear();
    Console.WriteLine("Welcome to hangman. Please select a game mode:\n1. Player vs Player\n2. Player vs Computer\n");
    userInput = Console.ReadLine();
    switch (userInput)
    {
        // Player vs Player ---------------------------------------------------------------------------------
        case "1":
            Console.Clear();
            Console.WriteLine("Player vs Player\nPlayer 1, please enter a word to begin:");
            validEntry = false;
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
                        userInput = userInput.ToLower().Trim();
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
        // Player vs Computer -------------------------------------------------------------------------------
        case "2":
            Console.Clear();
            Console.WriteLine("Player vs Computer\n");
            guesses = 5;
            bool validSelection = false;

            while (!validSelection)
            {
                // Difficulty selection
                Console.WriteLine("Please select a difficulty:\n1. Easy\n2. Medium\n3. Hard\n");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Easy");
                        validSelection = true;

                        do
                        {
                            hiddenWord = wordGenerator.GetWord();
                        } while (hiddenWord.Length < 10 || !CheckWord(hiddenWord)); // Set the size of easy words here
                        
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Medium");
                        validSelection = true;

                        do
                        {
                            hiddenWord = wordGenerator.GetWord();
                        } while (hiddenWord.Length < 7 || hiddenWord.Length > 9 || !CheckWord(hiddenWord)); // Set the size of medium words here

                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Hard");
                        validSelection = true;

                        do
                        {
                            hiddenWord = wordGenerator.GetWord();
                        } while (hiddenWord.Length < 4 || hiddenWord.Length > 6 || !CheckWord(hiddenWord)); // Set the size of hard words here

                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("You must enter a number with a cooresponding menu option.");
                        Console.ReadLine();
                        break;
                }
            }

            // Use this code when using the hardcoded words
            // hiddenWord = randomWords[random.Next(0, randomWords.Length)];


            // Show '_'s in terminal in place of letters
            letters = hiddenWord.ToCharArray();
            lettersCopy = new char[hiddenWord.Length];

            for (int i = 0; i < lettersCopy.Length; i++)
            {
                lettersCopy[i] = '_';
            }

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
            Console.Clear();
            Console.WriteLine("You must enter a number with a corresponding menu option.");
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
        DisplayGuesses();
        WriteWord(lettersCopy);
        userInput = Console.ReadLine();
        if (userInput != null && userInput != "")
        {
            userInput = userInput.ToLower().Trim();
            if (userInput.Length != 1)
            {
                Console.WriteLine("\nOnly 1 letter at a time!");
            }
            else if (!char.IsLetter(userInput, 0))
            {
                Console.WriteLine("Your guess must be a letter.");
            }
            else if (userGuesses.Contains(Convert.ToChar(userInput)))
            {
                Console.WriteLine("You've already guessed that letter!");
            }
            else
            {
                AddGuess(userInput);
                CheckGuess(userInput, letters, lettersCopy);
                validEntry = true;
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

// Writes lettersCopy array to console
void WriteWord(char[] lettersCopy)
{
    Console.WriteLine();
    foreach (char letter in lettersCopy)
    {
        Console.Write(letter + " ");
    }
    Console.WriteLine("\n");
}

// Checks array to see if all letters have been guessed
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

// Asks user if they'd like to play again, and clears guess array
bool PlayAgain()
{
    Console.WriteLine("\nWould you like to play again? (Y / N)");
    bool validEntry = false;
    while (!validEntry)
    {
        userInput = Console.ReadLine();
        if (userInput.ToLower().Trim() == "y")
        {
            Array.Resize(ref userGuesses, 0);
            return true;
        }
        else if (userInput.ToLower().Trim() == "n")
        {
            return false;
        }
        else
        {
            Console.WriteLine("Please enter 'Y' or 'N'.");
        }
    }
    return false;
}

// Adds user's guess to previous guesses
void AddGuess(string letter)
{
    Array.Resize(ref userGuesses, userGuesses.Length + 1);
    userGuesses[userGuesses.Length - 1] = Convert.ToChar(letter);
}

// Displays the users previous guesses
void DisplayGuesses()
{
    if (userGuesses.Length > 0)
    {
        Console.WriteLine("Guesses so far:");
        foreach (char letter in userGuesses)
        {
            Console.Write(letter + " ");
        }
        Console.WriteLine();
    }
}

// Checks randomly generated word to make sure it only contains letters
bool CheckWord(string hiddenWord)
{
    char[] letters = hiddenWord.ToCharArray();

    foreach (char letter in letters)
    {
        if (!char.IsLetter(letter))
            return false;
    }

    return true;
}