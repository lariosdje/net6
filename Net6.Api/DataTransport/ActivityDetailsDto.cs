﻿namespace Net6.Api.DataTransport
{
    public class ActivityDetailsDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }

        public ICollection<SurveyDto> Surveys { get; set; }
        public PropertyDto Property { get; set; }
    }
}
