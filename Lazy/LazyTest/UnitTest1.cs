namespace LazyTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMySimpleLazyWithNull()
    {
        Func<int> supplier = null;
        ILazy<int> lazy = new MySimpleLazy<int>(supplier);

        Assert.Throws<NullReferenceException>(() => lazy.Get());
    }

    [TestMethod]
    public void TestMySimpleLazy()
    {
        Func<int> supplier = () => 10;
        ILazy<int> lazy = new MySimpleLazy<int>(supplier);

        int result1 = lazy.Get();
        int result2 = lazy.Get();

        Assert.AreEqual(10, result1);
        Assert.AreEqual(10, result2);

        Assert.AreEqual(1, counter);
    }

    [TestMethod]
     public void TestMyThreadLazyWithNull()
    {
        Func<int> supplier = null;
        ILazy<int> lazy = new MyThreadLazy<int>(supplier);

        Assert.Throws<NullReferenceException>(() => lazy.Get());
    }

    [TestMethod]
    public void TestMyThreadLazyThreadSafety()
    {
        Func<int> supplier = () =>
        {
            Thread.Sleep(10);
            counter++;
            return 10;
        };

        ILazy<int> lazy = new MyThreadLazy<int>(supplier);

        int numThreads = 10;
        Task<int>[] tasks = new Task<int>[numThreads];

        for (int i = 0; i < numThreads; i++)
        {
            tasks[i] = Task.Run(() => lazy.Get());
        }

        Task.WhenAll(tasks).Wait();
        int result = lazy.Get();

        foreach (int taskResult in tasks.Select(t => t.Result))
        {
            Assert.AreEqual(10, taskResult);
        }

        Assert.AreEqual(1, counter);
    }
}