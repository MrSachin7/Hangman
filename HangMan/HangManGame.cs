using HangMan.HttpServices;

namespace HangMan;

public class HangManGame {
    private List<char> guessedChars;
    private int noOfTries;
    private RandomWordHttpClient randomWordGenerator;

    public HangManGame(int noOfTries) {
        this.noOfTries = noOfTries;
        guessedChars = new List<char>();
        randomWordGenerator = new RandomWordHttpClient();
    }

    public async Task play() {
        string? randomWord = await randomWordGenerator.GetRandomWordAsync();
        List<char> wordToDisplay = new();
        foreach (char c in randomWord) {
            wordToDisplay.Add('_');  // initialize the word to display with all '_'
        }

        while (noOfTries > 0) {
            Console.WriteLine($"Guess the following word : \n The word has {randomWord.Length} chars");
            wordToDisplay.ForEach(Console.Write);
            Console.WriteLine("\n Your letter ??");
            // ask for a char
            string? guessedFromConsole = Console.ReadLine();
            // convert the string into char
            Char guess = guessedFromConsole[0];

            if (guessedChars.Contains(guess)) {
                Console.WriteLine("You have already guessed this char, Try another");
            }
            else {
                guessedChars.Add(guessedFromConsole[0]); // keeping track of guessed letters
                if (randomWord.Contains(guess)) {
                    int noOfOccurence =
                        randomWord.Count(c => c.Equals(guess)); // how many times the letter is in the word
                    int index = -1;  // -1 because index+1 is used ...
                    for (int i = 0; i < noOfOccurence; i++) {
                        index = randomWord.IndexOf(guess, index+1);      // index+1 to start from the next char after found
                        wordToDisplay[index] = guess; // replacing the '_' with the actual letter
                    }
                }
                else {
                    Console.WriteLine("Wrong guess");
                    noOfTries--;
                    Console.WriteLine($"Number of tries remaining : {noOfTries}");
                }
                Console.WriteLine($"Guessed letters so far are : ");
                foreach (char guessedChar in guessedChars) {
                    Console.Write(guessedChar+ ",");
                }
                Console.WriteLine("\nResult :");                                      

                if ((new string(wordToDisplay.ToArray())).Equals(randomWord)) {
                    Console.WriteLine("YOU WON ......CONGRATS");
                    break;
                }

                if (noOfTries==0) {
                    Console.WriteLine("YOU LOST .... YOU ARE HANGED");
                    Console.WriteLine($"The word was : {randomWord}");

                }


            }
        }
    }
}