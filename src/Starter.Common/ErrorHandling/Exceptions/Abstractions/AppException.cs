using System.Collections;

namespace Starter.Common.ErrorHandling.Exceptions.Abstractions;

internal interface IAppException;

public abstract class AppException<TDerived> :
    Exception, IAppException where TDerived : AppException<TDerived> 
{
    private protected AppException(string message) : base(message) {

    }
    public TDerived UpsertToException(string key, string value) {
        this.Data[key] = value;
        return (TDerived)this;
    }

    public TDerived ThrowIfContainsErrors() {
        if(this.Data.Count > 0) {
            throw this;
        }

        return (TDerived)this;
    }
    
    public TDerived AddData(IDictionary? dictionary)
    {
        if (dictionary == null) return (TDerived)this;
        foreach(DictionaryEntry item in dictionary) {
            this.Data.Add(item.Key, item.Value);
        }

        return (TDerived)this;
    }
}
