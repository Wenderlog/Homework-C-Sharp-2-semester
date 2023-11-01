using System;
public interface ILazy<T> { 
    T? Get(); 
}
