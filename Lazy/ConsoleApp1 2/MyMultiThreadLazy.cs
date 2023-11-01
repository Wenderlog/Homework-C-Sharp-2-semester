using System;

class MyMultiThreadLazy<T> : ILazy<T>
{
    private Func<T>? supplier;
    private T? value;
    private volatile bool isCreatedValue = false;
    private volatile bool isFailed = false; 
    private Exception? exception; 
    private object lockObject = new object();

    public MyMultiThreadLazy(Func<T> supplier){
        this.supplier = supplier;
    }

    public T? Get()
    {
        if (!isCreatedValue) {
            lock(lockObject) {
                if (!isCreatedValue) {
                    try {
                        value = supplier();
                        supplier = null;
                        isCreatedValue = true;
                    }
                    catch (Exception ex) {
                        exception = ex; 
                        isFailed = true;
                    }
                }
            }
        }
        if (isFailed) {
            throw exception; 
        }
        return value;
    }
}
