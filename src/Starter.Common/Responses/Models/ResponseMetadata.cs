using System.Text.Json.Serialization;
using Softylines.Contably.Common.Requests.Models;

namespace Softylines.Contably.Common.Responses.Models {
    public sealed class ResponseMetadata<T> where T : class{
        
        /// <summary>
        /// This constructor only serves for the purpose of deserialization.
        /// </summary>
        [JsonConstructor]
        private ResponseMetadata() { }
        
        public ResponseMetadata(QueryParameters parameters, int totalCount) {
            this.PageNumber = parameters.PageNumber;
            this.PageSize = parameters.PageSize;
            this.TotalCount = totalCount;
        }
        public ResponseMetadata(int pageNumber, int pageSize, int totalCount) {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
        }
                        
        public ResponseMetadata(IQueryable<T> source, int pageNumber, int pageSize) {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalCount = source.Count();
        }

        [JsonPropertyName("total_pages")]
        public int TotalPages => (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
        
        [JsonPropertyName("current_page")]
        public int PageNumber { get; set; }
        
        [JsonPropertyName("per_page")]
        public int PageSize { get; set; }

        [JsonPropertyName("next_page")]
        public int? NextPage =>
            PageNumber >= TotalPages ? null : PageNumber + 1;

        [JsonPropertyName("prev_page")]
        public int? PreviousPage => 
            PageNumber <= 1 ? null : PageNumber - 1;
    }
}
