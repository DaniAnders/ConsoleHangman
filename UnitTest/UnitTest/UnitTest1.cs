namespace UnitTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestValidInput()
    {
        char letter = 'a';
        bool res = Hangman.IsValidInput(letter);
        Assert.true(res);
    }


    


}
