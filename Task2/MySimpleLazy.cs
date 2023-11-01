using System;
class MySimpleLazy<T> : ILazy<T>
{
    private Func<T> supplier;
    private T value;
    private bool isCreatedValue = false;

    public MySimpleLazy(Func<T> supplier){
        this.supplier = supplier;
    }

    public T Get()
    {
       if (!isCreatedValue){
        value = supplier();
        supplier = null;
        isCreatedValue = true;
       }
       return value;
    }
}