namespace CodeChallenge.Model
{
    public class Customer
    {
        public string? id { get; set; }
        public string? salutation { get; set; } = "Ms.";
        public string? initials { get; set; } = "A.";
        public string? firstname { get; set; } = "Dummy";
        public string? firstname_ascii { get; set; } = "Dummy";
        public string? gender { get; set; } = "m";
        public string? firstname_country_rank { get; set; } = "7866";
        public string? firstname_country_frequency { get; set; } = "45";
        public string? lastname { get; set; } = "Dummy";
        public string? lastname_ascii { get; set; } = "Dummy";
        public string? lastname_country_rank { get; set; } = "7866";
        public string? lastname_country_frequency { get; set; } = "45";
        public string? email { get; set; } = "Dummy@dummy.com";
        public string? password { get; set; } = "Dummy";
        public string? country_code { get; set; } = "US";
        public string? country_code_alpha { get; set; } = "USA";
        public string? country_name { get; set; } = "United States";
        public string? primary_language_code { get; set; } = "en";
        public string? primary_language { get; set; } = "English";
        public decimal balance { get; set; } = 111;
        public string? phone_Number { get; set; } = "+1 518-531-9333";
        public string? currency { get; set; } = "USD";
    }
}
