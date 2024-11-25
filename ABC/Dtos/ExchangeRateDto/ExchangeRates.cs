namespace ABC.Dtos
{
    public class ExchangeRateDto
    {
        public CurrencyDto Currency { get; set; } // Nested currency object
        public decimal? Buy { get; set; } // Use nullable types to handle null values
        public decimal? Sell { get; set; }
    }

    public class CurrencyDto
    {
        public string Iso3 { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
    }

    public class ExchangeRateResponseDto
    {
        public ExchangeRateDataDto Data { get; set; }
        public PaginationDto Pagination { get; set; }
    }

    public class ExchangeRateDataDto
    {
        public List<ExchangeRatePayloadDto> Payload { get; set; }
    }

    public class ExchangeRatePayloadDto
    {
        public List<ExchangeRateDto> Rates { get; set; }
    }

    public class PaginationDto
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}

