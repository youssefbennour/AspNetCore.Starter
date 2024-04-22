using System.Collections;

namespace Starter.Common.ErrorHandling.Exceptions.Abstractions;

internal abstract class AppException : InvalidOperationException {
    protected private AppException(string message) : base(message) {

    }
    internal void UpsertToException(string key, string value) {
        this.Data[key] = value;
    }

    internal void ThrowIfContainsErrors() {
        if(this.Data.Count > 0) {
            throw this;
        }
    }
    internal void AddData(IDictionary dictionary) {
        if(dictionary != null) {
            foreach(DictionaryEntry item in dictionary) {
                this.Data.Add(item.Key, item.Value);
            }
        }
    }
}
