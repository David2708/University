namespace universityApiBackend.Models.DataModels
{
    public class USerTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public TimeSpan validity { get; set; }
        public string RefreshToken { get; set; }
        public string EmailId { get; set; }
        public Guid GuidID { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
