namespace Starter.Common.QueryModels {
    internal class QueryParameters {
        private const int maxPageSize = 100;
        internal int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        internal int PageSize {
            get {
                return _pageSize;
            }
            set {
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }

        internal string? searchTerm { get; set; }
    }
}
