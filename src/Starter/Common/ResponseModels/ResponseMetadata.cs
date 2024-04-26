using System.Text.Json.Serialization;

namespace Starter.Common.ApiResponses {
    internal class ResponseMetadata<T> {
        internal ResponseMetadata(int currentPage, int pageSize, int totalCount) {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
        }

        internal ResponseMetadata(IQueryable<T> source, int currentPage, int pageSize) {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalCount = source.Count();
        }

        [JsonPropertyName("total_pages")]
        internal int TotalPages {
            get => (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
        }

        [JsonPropertyName("total_count")]
        internal int TotalCount { get; set; }
        [JsonPropertyName("current_page")]
        internal int CurrentPage { get; init; }
        [JsonPropertyName("per_page")]
        internal int PageSize { get; init; }
        [JsonPropertyName("next_page")]
        internal int NextPage { get; set; }
        [JsonPropertyName("prev_page")]
        internal int PreviousPage { get; set; }
        [JsonPropertyName("next_page_link")]
        internal string? NextpageLink { get; set; }
        [JsonPropertyName("prev_page_link")]
        internal string? PreviousPageLink { get; set; }
        [JsonPropertyName("first_page_link")]
        internal string? FirstPageLink { get; set; }
        [JsonPropertyName("last_page_link")]
        internal string? LastPageLink { get; set; }

        /// <summary>
        /// Defines the base route.
        /// </summary>
        [JsonPropertyName("path")]
        internal string? Path { get; set; }
    }
}
