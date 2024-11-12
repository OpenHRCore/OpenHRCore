namespace OpenHRCore.Application.Common
{
    public class SearchRequest
    {
        public PaginationFilter Pagination { get; set; } = new();
        public List<SearchFilter> Filters { get; set; } = new();
        public List<SortFilter> Sorts { get; set; } = new();
    }

    public class PaginationFilter
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }

    public class SearchFilter
    {
        public string Field { get; set; } = default!;
        public SearchOperator Operator { get; set; } = SearchOperator.EqualsOp;
        public string Value { get; set; } = default!;
    }

    public class SortFilter 
    {
        public string Field { get; set; } = default!;
        public SortDirection Direction { get; set; } = SortDirection.Ascending;
    }

    public sealed class SearchOperator
    {
        public SearchOperator() { }
        private SearchOperator(string value) { Value = value; }
        public string Value { get; set; } = "equals";

        public static SearchOperator EqualsOp { get; } = new("equals");
        public static SearchOperator NotEquals { get; } = new("notequals");
        public static SearchOperator Contains { get; } = new("contains");
        public static SearchOperator GreaterThan { get; } = new("greaterthan");
        public static SearchOperator LessThan { get; } = new("lessthan");
        public static SearchOperator GreaterThanOrEqual { get; } = new("greaterthanorequal");
        public static SearchOperator LessThanOrEqual { get; } = new("lessthanorequal");
        public static SearchOperator Between { get; } = new("between");
        public static SearchOperator In { get; } = new("in");
        public static SearchOperator NotIn { get; } = new("notin");

        public override string ToString() => Value;
        public static bool operator ==(SearchOperator? left, SearchOperator? right) =>
            left?.Value == right?.Value;
        public static bool operator !=(SearchOperator? left, SearchOperator? right) =>
            !(left == right);
        public override bool Equals(object? obj) =>
            obj is SearchOperator other && Value.Equals(other.Value);
        public override int GetHashCode() => Value.GetHashCode();
    }

    public sealed class SortDirection
    {
        public SortDirection() { }
        private SortDirection(string value) { Value = value; }
        public string Value { get; set; } = "ascending";

        public static SortDirection Ascending { get; } = new("ascending");
        public static SortDirection Descending { get; } = new("descending");

        public override string ToString() => Value;
        public override bool Equals(object? obj) =>
            obj is SortDirection other && Value.Equals(other.Value);
        public override int GetHashCode() => Value.GetHashCode();
    }
}
