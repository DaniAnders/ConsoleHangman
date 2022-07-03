using System.Text;
using ConsoleHangman;
namespace ConsoleHangman.Unit_Tests;

[TestClass]
public class UnitTest1
{

    Hangman hangman = new Hangman();
    char[] secretWord;


    [TestMethod]
    [DataRow('a')]
    [DataRow('z')]
    [DataRow('A')]
    public void TestValidInput(char letter)
    {
      
        bool res = hangman.IsValidInput(letter);
        Assert.IsTrue(res);
    }


    [TestMethod]
    [DataRow(' ')]
    [DataRow('5')]
    [DataRow('*')]
    [DataRow('$')]
    public void TestInvalidInput(char letter)
    {

        bool res = hangman.IsValidInput(letter);
        Assert.IsFalse(res);
    }


    [TestMethod]
    public void TestGuessEntireWord()
    {
        char[] secretWord = hangman.SecretWord;
        char[] inputedWord = secretWord;
        bool res = hangman.GuessEntireWord(inputedWord);

        Assert.IsTrue(res);

    }

    [TestMethod]
    public void TestFailToGuessEntireWord()
    {
        char[] secretWord = hangman.SecretWord;
        char[] inputedWord = { 'a', 'b', 'c' };
        bool res = hangman.GuessEntireWord(inputedWord);

        Assert.IsFalse(res);

    }

    [TestMethod]
    public void TestGuessCharInWord()
    {
        char[] secretWord = hangman.SecretWord;
        char letter = secretWord[0];
        bool res = hangman.GuessCharInWord(letter);

        Assert.IsTrue(res);

    }

    [TestMethod]
    public void TestFailGuessChar()
    {
        char[] secretWord = hangman.SecretWord;
        char letter = 'Z';
        bool res = hangman.GuessCharInWord(letter);

        Assert.IsFalse(res);

    }


}
