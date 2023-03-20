namespace DapperExample.Handler
{
    public class CompanyCreateModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }

    public class CompanyUpdateModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
