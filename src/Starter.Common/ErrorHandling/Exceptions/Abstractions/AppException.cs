using System.Collections;

namespace Starter.Common.ErrorHandling.Exceptions.Abstractions;

public abstract class AppException : InvalidOperationException {
    private protected AppException(string message) : base(message) {

    }
    internal void UpsertToException(string key, string value) {
        this.Data[key] = value;
    }

    internal void ThrowIfContainsErrors() {
        if(this.Data.Count > 0) {
            throw this;
        }
    }
    internal void AddData(IDictionary? dictionary)
    {
        if (dictionary == null) return;
        foreach(DictionaryEntry item in dictionary) {
            this.Data.Add(item.Key, item.Value);
        }
    }
}
