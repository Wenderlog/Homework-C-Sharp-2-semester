namespace rjrgjkrtn;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMySimpleLazyWithNull()
    {
         Func<int> supplier = null;
        ILazy<int> lazy = new MySimpleLazy<int>(supplier);

        Assert.Throws<NullReferenceException>(() => lazy.Get());
    }
    [Test]
    public void TestMySimpleLazy()
    {
        Func<int> supplier = () => 10;
        ILazy<int> lazy = new MySimpleLazy<int>(supplier);

        Assert.AreEqual(10,lazy.Get());
    }
    [Test]
    public void TestMyThreadLazy()
    {
        Func<int> supplier = () => 10;
        ILazy<int> lazy = new MyThreadLazy<int>(supplier);

        Assert.AreEqual(10,lazy.Get());
    }
    [Test]
    public void TestMyThreadLazyWithNull()
    {
         Func<int> supplier = null;
        ILazy<int> lazy = new MyThreadLazy<int>(supplier);

        Assert.Throws<NullReferenceException>(() => lazy.Get());
    }
     [Test]
    public void TestMySimpleLazyThreadSafety()
    {
       Func<int> supplier = () =>{
        Thread.Sleep(10);
        return 10;
       };
       ILazy<int> lazy = new MySimpleLazy<int>(supplier);
       int numThread = 10;
       Task<int>[] tasks = new Task<int>[numThread];

       for (int i = 0; i < numThread; i++){
        tasks[i] = Task.Run(() => lazy.Get());
       }

       Task.WhenAll(tasks).Wait();
       Assert.AreEqual(10, lazy.Get());
    }
     [Test]
    public void TestMyThreadLazyThreadSafet()
    {
        Func<int> supplier = () =>{
        Thread.Sleep(10);
        return 10;
       };
       ILazy<int> lazy = new MyThreadLazy<int>(supplier);
       int numThread = 10;
       Task<int>[] tasks = new Task<int>[numThread];

       for (int i = 0; i < numThread; i++){
        tasks[i] = Task.Run(() => lazy.Get());
       }

       Task.WhenAll(tasks).Wait();
       Assert.AreEqual(10, lazy.Get());
    }

}