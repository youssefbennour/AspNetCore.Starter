namespace Starter.Common.ApiResponses {
    internal class PaginatedList<T> {
        internal PaginatedList(List<T> data, ResponseMetadata<T> metadata) {
            this.Data = data;
            this.Metadata = metadata;
        }

        internal PaginatedList(List<T> data, ResponseMetadata<T> metadata, string message) {
            this.Data = data;
            this.Metadata = metadata;
            this.Message = message;
        }

        internal PaginatedList(IQueryable<T> source, int currentPage, int pageSize) {
            Metadata = new ResponseMetadata<T>(source, currentPage, pageSize);

            var skip = (currentPage - 1) * pageSize;
            this.Data = source.Skip(skip)
                .Take(pageSize)
                .ToList();

            this.Metadata.TotalCount = source.Count();
        }

        internal string Message { get; private set; } = default!;
        internal List<T> Data { get; private set; }
        internal ResponseMetadata<T> Metadata { get; private set; }

    }
}
