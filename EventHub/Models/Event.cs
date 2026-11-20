using System;
using EventHub.Data;

namespace EventHub.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UniversityId { get; set; }
        public string Category { get; set; }      // Hackathon, Fest, Workshop, Sports, Cultural, Conference, Competition
        public string Mode { get; set; }          // Online / Offline / Hybrid
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public string Venue { get; set; }
        public decimal Fee { get; set; }
        public int Capacity { get; set; }
        public int AlreadyRegistered { get; set; }   // sample seed value
        public string Description { get; set; }
        public string Eligibility { get; set; }
        public string Prizes { get; set; }
        public string Schedule { get; set; }         // lines separated by '|'
        public string ContactEmail { get; set; }

        public University University { get { return DataStore.GetUniversity(UniversityId); } }
        public int SeatsLeft { get { return Math.Max(0, Capacity - AlreadyRegistered - DataStore.CountFor(Id)); } }
        public bool IsClosed { get { return DateTime.Today > RegistrationDeadline.Date || SeatsLeft == 0; } }
        public string[] ScheduleItems { get { return (Schedule ?? "").Split('|'); } }
    }
}
