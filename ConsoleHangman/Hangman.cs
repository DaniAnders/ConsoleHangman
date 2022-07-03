using System.Text;

namespace ConsoleHangman
{
    public class Hangman
    {
        private int guesses = 10;
        private bool won = false;
        private int  attempts = 0;

        private int Guesses { get => guesses; set => guesses = value; }
        private char[] Dashes { get; set; }
        private bool Won { get => won; set => won = value; }
        private int Attempts { get => attempts; set => attempts = value; }

        public char[] SecretWord { get; set; }

        private StringBuilder IncorrectLetters { get; set; }
        private StringBuilder CorrectLetters { get; set; }

        string path = " ";

        private string[] words = System.IO.File.ReadAllLines(path);
        private string[] Words { get => words; }



        public Hangman()
        {
            SecretWord = GetRandomWord();
            Dashes = new char[SecretWord.Length];
            IncorrectLetters = new StringBuilder();
            CorrectLetters = new StringBuilder();
        }


        public void PrintWelcome()
        {
            Console.WriteLine("\n ********** HANGMAN GAME ************ \n");
            Console.WriteLine("\nGuess a word:");
            Console.WriteLine("The word is chosen randomly,");
            Console.WriteLine("You can try guess a letter or entire word,");
            Console.WriteLine("You have 10 attempts,");
            Console.WriteLine("The words selected are names of fruits or vegetables");

        }
        

        public void PrintResult()
        {
            if(Attempts == 0)
            {
                Console.WriteLine("\nGOODBY!!!\n");
            }
            else if (Won)
            {
                Console.WriteLine("\nCONGRATULATIONS!!! \nYOU WON THE GAME\n");
            }
            else
            {
                Console.WriteLine("\nSORRY!!! \nYOU LOST THE GAME. TRY AGAIN\n");
            }
            Console.WriteLine($"The secret word is: {new string(SecretWord)}");
            Console.WriteLine($"You guessed: {CorrectLetters.ToString()}");
            Console.WriteLine($"The incorrect letters are: {IncorrectLetters}");
            Console.WriteLine($"\nThe number of atempts: {Attempts}");

            /* Asking the user he/she wants to play again */
             Console.WriteLine("\nDo want to play again?  ( Y ) or ( N )");
             char key = char.ToLower(Console.ReadKey().KeyChar);
             bool keep = key switch
             {
                 'y' => true,
                 _ => false
             };

             if (keep)
             {
                 HangmanClear();
                 Play();
             }


        }




        public void HangmanClear()
        {
            
            SecretWord = GetRandomWord();
            Dashes = new char[SecretWord.Length];
            IncorrectLetters.Clear();
            CorrectLetters.Clear();
            Guesses = 10;
            Attempts = 0;
            Won = false;

        }

        /* Printing the dashes */
        public void PrintDashes()
        {

            Console.WriteLine($"\nThe secret word contains {Dashes.Length} letters\n");
            for (int i = 0; i < Dashes.Length; i++)
            {
                Dashes[i] = '_';
            }

            foreach (char value in Dashes)
            {
                Console.Write(value + " ");
            }


        }

        /* Replacing the dashes with correct letters */
        public void UpdateDashes()
        {

            foreach (char element in Dashes)
            {
                Console.Write(element + " ");
            }
        }


        public char[] GetRandomWord()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(0, Words.Length - 1);
            char[] word = Words[randomIndex].ToCharArray();
            return word;
        }


        public bool IsValidInput(char letter)
        {
            bool isValid = false;
            if (char.IsLetter(letter))
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }



        /* Checking if the inputed word is the secret word */
        public bool GuessEntireWord(char[] inputedWord)
        {
            bool equal = true;
            if (inputedWord.Length != SecretWord.Length)
            {
                equal = false;
            }
            else
            {
                try
                {
                    for (int i = 0; i < SecretWord.Length; i++)
                    {
                        if (char.ToLower(SecretWord[i]) != char.ToLower(inputedWord[i]))
                        {
                            equal = false;

                        }
                        else
                        {
                            equal = true;

                        }
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
      

            }
            
            if (equal)
            {
                Won = true;
                Guesses--;
                Attempts++;
                CorrectLetters.Append(inputedWord);
    
            }
            else
            {
                Won = false;
                Guesses--;
                Attempts++;
                IncorrectLetters.Append(inputedWord);
                Console.WriteLine($"\nSorry! ( {new string(inputedWord)} )is NOT the secret word. Try again.\n");
                Console.WriteLine($"\nNumber of guesses: {Guesses}");
            }

            return equal;

        }




        public bool GuessCharInWord(char letter)
        {
            char[] wordToGuess = SecretWord;
            bool result = false;
            bool contains = wordToGuess.Contains(letter);

      
            if (!IsValidInput(letter))
            {
                Console.WriteLine($"\nINVALID INPUT! TRY AGAIN");
                Guesses--;
                Attempts++;
                result = false;
            }
            /* Adding a letter to dashes, when the user guesses a letter, update dashes with the inputed letter */
            else if (contains && !Dashes.Contains(letter))
            {
                try
                {
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i] == letter)
                        {
                            Dashes[i] = letter;
                        }

                    }

                } catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);

                }
                    Guesses--;
                    Attempts++;
                    CorrectLetters.Append(letter);
                    result = true;
                    Console.WriteLine($"\nYour guess was correct the letter ( {letter} ) is in the secret word.\n");
                    Console.WriteLine($"\nYou have {Guesses} guesses left.\n");


            }/* Checking the inputed letter has been entered already */
            else if(Dashes.Contains(letter) || IncorrectLetters.ToString().Contains(letter))
            {
                Guesses--;
                Attempts++;
                Console.WriteLine($"\nYou already entered the letter ( {letter} ). Try again!\n");
                Console.WriteLine($"\nYou have {Guesses} guesses left.\n");
        
               
            }/* Adding incorrect letters to string builder */
            else
            {
                Guesses--;
                Attempts++;
                IncorrectLetters.Append(letter);
                result = false;
                Console.WriteLine($"\nYour guess was incorrect the letter ( {letter} ) is not in the secret word.\n");
                Console.WriteLine($"\nYou have {Guesses} guesses left.\n");

            }

            return result;


        }




        public void Play()
        {
            PrintWelcome();
            PrintDashes();

            do
            {
                Console.WriteLine("\nPlease, selec an option below: \n");
                Console.WriteLine("1. Guess a letter");
                Console.WriteLine("2. Guess the entire word");
                Console.WriteLine("0. Quit");

                int.TryParse(Console.ReadLine(), out int option);

                switch (option)
                {
                    case 0:
                        Console.Clear();
                        Guesses = 0;
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\nPlease, enter your guess ( letter ): ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        char letter = char.ToLower(keyInfo.KeyChar);
                        GuessCharInWord(letter);
                        UpdateDashes();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Please, enter your guess( word ): ");
                        char[] inputedWord = Console.ReadLine().ToCharArray();
                        GuessEntireWord(inputedWord);
                        break;
                    default:
                        break;

                }

                if (Dashes.SequenceEqual(SecretWord))
                {
         
                    Won = true;
                  
                }


            } while (Guesses > 0 && Won == false);

            PrintResult();

           


        }






    }
}
