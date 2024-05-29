using System.Text.Json.Serialization;
using Starter.Common.Requests.Models;

namespace Starter.Common.Responses.Models {
    public sealed class PaginatedList<T> where T: class {
        
        /// <summary>
        /// This constructor only serves for the purpose of deserialization.
        /// </summary>
        [JsonConstructor]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private PaginatedList(){}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        
        public PaginatedList(List<T> data, int pageNumber, int pageSize, int totalCount)
        {
            this.Data = data;
            this.Metadata = new ResponseMetadata<T>(pageNumber, pageSize, totalCount);
        }
        
        public PaginatedList(List<T> data, ResponseMetadata<T> metadata) {
            this.Data = data;
            this.Metadata = metadata;
        }

        public PaginatedList(List<T> data, QueryParameters parameters, int totalCount)
        {
            this.Data = data;
            this.Metadata = new ResponseMetadata<T>(parameters, totalCount);
        }

        public PaginatedList(IQueryable<T> source, QueryParameters parameters) {
            Metadata = new ResponseMetadata<T>(source, parameters.PageNumber, parameters.PageSize);

            this.Data = source.Paginate(parameters)
                .ToList();
        }

        public List<T> Data { get; set; } = [];
        public ResponseMetadata<T> Metadata { get; set; }
    }
}
