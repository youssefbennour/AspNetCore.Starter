using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Starter.Common.Requests.Models {
    public class QueryParameters {
        private const int MaxPageSize = 100;
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 10;
        
        [FromQuery(Name = "page")]
        [DefaultValue(DefaultPage)]
        public int PageNumber { get; set; } = DefaultPage;
        
        private int pageSize = DefaultPageSize;

        [FromQuery(Name = "per_page")]
        [DefaultValue(DefaultPageSize)]
        public int PageSize
        {
            get => pageSize;
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        [FromQuery(Name = "search_term")]
        public string? SearchTerm { get; set; }
    }
}
