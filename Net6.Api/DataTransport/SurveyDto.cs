namespace Net6.Api.DataTransport
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Answers { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
