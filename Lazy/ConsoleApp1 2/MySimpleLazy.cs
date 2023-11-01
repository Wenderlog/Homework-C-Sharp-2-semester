using System;

class MySimpleLazy<T> : ILazy<T>
{
    private Func<T>? supplier;
    private T? value;
    private bool isCreatedValue = false;
    private Exception? exception; 

    public MySimpleLazy(Func<T> supplier){
        this.supplier = supplier;
    }

    public T? Get()
    {
        if (!isCreatedValue){
            try {
                value = supplier();
                supplier = null;
                isCreatedValue = true;
            }
            catch (Exception ex) {
                exception = ex; 
            }
        }
        if (exception != null) {
            throw exception; 
        }
        return value;
    }
}
