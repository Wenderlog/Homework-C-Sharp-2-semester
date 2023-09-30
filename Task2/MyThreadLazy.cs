using System;

class MyThreadLazy<T> : ILazy<T>
{
    private Func<T> supplier;
    private T value;
    private bool isCreatedValue = false;
    private object lockObject = new object();

    public MyThreadLazy(Func<T> supplier){
        this.supplier = supplier;
    }

    public T Get()
    {
       if (!isCreatedValue){
        lock(lockObject){
             if (!isCreatedValue){
                value = supplier();
                supplier = null;
                isCreatedValue = true;
        }
        }
       }
       return value;
    }
}