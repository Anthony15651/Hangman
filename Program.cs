string hiddenWord = "hangman";
string userInput = "";

// Startup
Console.WriteLine("Welcome to hangman!\n\nPlease enter a word to begin:");
userInput = Console.Read().ToString();

Console.WriteLine($"Thank, you. Your word is: {userInput}");