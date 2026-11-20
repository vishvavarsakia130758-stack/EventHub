using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EventHub.Models;

namespace EventHub.Data
{
    // In-memory store (same idea as the static list in the reference ProductController).
    // All data below is SAMPLE data - replace with real events or move to Entity Framework later.
    public static class DataStore
    {
        static readonly object Sync = new object();

        public static List<University> Universities = new List<University>
        {
            new University{Id=1,Name="Indian Institute of Technology Bombay",ShortName="IIT Bombay",City="Mumbai",State="Maharashtra",Type="IIT",Established=1958,Website="https://www.iitb.ac.in",Description="One of India's top engineering institutes, with large student-run tech and cultural festivals."},
            new University{Id=2,Name="Indian Institute of Technology Delhi",ShortName="IIT Delhi",City="New Delhi",State="Delhi",Type="IIT",Established=1961,Website="https://home.iitd.ac.in",Description="Research-driven institute with active tech and cultural societies."},
            new University{Id=3,Name="Indian Institute of Technology Madras",ShortName="IIT Madras",City="Chennai",State="Tamil Nadu",Type="IIT",Established=1959,Website="https://www.iitm.ac.in",Description="Known for Shaastra and Saarang, among the largest student-run fests."},
            new University{Id=4,Name="Indian Institute of Science",ShortName="IISc Bangalore",City="Bengaluru",State="Karnataka",Type="Deemed",Established=1909,Website="https://iisc.ac.in",Description="India's premier institute for advanced science and research."},
            new University{Id=5,Name="BITS Pilani",ShortName="BITS Pilani",City="Pilani",State="Rajasthan",Type="Deemed",Established=1964,Website="https://www.bits-pilani.ac.in",Description="Famous for APOGEE, OASIS and a strong startup culture."},
            new University{Id=6,Name="University of Delhi",ShortName="DU",City="New Delhi",State="Delhi",Type="Central",Established=1922,Website="https://www.du.ac.in",Description="Large central university with dozens of colleges and vibrant cultural circuits."},
            new University{Id=7,Name="Anna University",ShortName="Anna Univ",City="Chennai",State="Tamil Nadu",Type="State",Established=1978,Website="https://www.annauniv.edu",Description="Leading technical university of Tamil Nadu."},
            new University{Id=8,Name="Jadavpur University",ShortName="JU",City="Kolkata",State="West Bengal",Type="State",Established=1955,Website="https://jadavpuruniversity.in",Description="Renowned for engineering, arts and a lively campus culture."},
            new University{Id=9,Name="Banaras Hindu University",ShortName="BHU",City="Varanasi",State="Uttar Pradesh",Type="Central",Established=1916,Website="https://www.bhu.ac.in",Description="One of Asia's largest residential universities."},
            new University{Id=10,Name="Savitribai Phule Pune University",ShortName="SPPU",City="Pune",State="Maharashtra",Type="State",Established=1949,Website="https://www.unipune.ac.in",Description="Major academic hub of western Maharashtra."},
            new University{Id=11,Name="Marwadi University",ShortName="Marwadi Univ",City="Rajkot",State="Gujarat",Type="Private",Established=2016,Website="https://www.marwadiuniversity.ac.in",Description="Private university in Rajkot with strong engineering and management programs."},
            new University{Id=12,Name="University of Hyderabad",ShortName="UoH",City="Hyderabad",State="Telangana",Type="Central",Established=1974,Website="https://uohyd.ac.in",Description="Central university known for sciences, humanities and social sciences."},
            new University{Id=13,Name="Panjab University",ShortName="PU",City="Chandigarh",State="Chandigarh",Type="State",Established=1882,Website="https://puchd.ac.in",Description="One of the oldest universities in North India."},
            new University{Id=14,Name="Manipal Academy of Higher Education",ShortName="MAHE",City="Manipal",State="Karnataka",Type="Deemed",Established=1953,Website="https://manipal.edu",Description="Large deemed university with a lively student event calendar."}
        };

        public static List<Event> Events = BuildEvents();
        // Registrations now live in the SQLite database. This property keeps the old
        // "DataStore.Registrations" usage working (returns a fresh snapshot from the DB).
        public static List<Registration> Registrations
        {
            get
            {
                using (var db = new AppDbContext())
                    return db.Registrations.AsNoTracking().ToList();
            }
        }

        static List<Event> BuildEvents()
        {
            DateTime t = DateTime.Today;
            Func<int, DateTime> d = n => t.AddDays(n);
            return new List<Event>
            {
                new Event{Id=1,Title="CodeStorm 36-Hour Hackathon",UniversityId=1,Category="Hackathon",Mode="Offline",StartDate=d(21),EndDate=d(23),RegistrationDeadline=d(14),Venue="Convocation Hall, Powai",Fee=500,Capacity=300,AlreadyRegistered=212,Description="Build a working product in 36 hours around themes of fintech, climate and healthcare. Mentors from industry will guide teams.",Eligibility="Any UG/PG student in India. Teams of 2 to 4.",Prizes="Rs. 3,00,000 prize pool + internship interviews",Schedule="Day 1, 10 AM: Opening and problem statements|Day 1, 12 PM: Hacking begins|Day 2, 6 PM: Mentor round|Day 3, 10 AM: Final demos|Day 3, 4 PM: Prize ceremony",ContactEmail="hack@example.edu"},
                new Event{Id=2,Title="Shaastra Robotics Challenge",UniversityId=3,Category="Competition",Mode="Offline",StartDate=d(35),EndDate=d(37),RegistrationDeadline=d(25),Venue="CLT Complex, IIT Madras",Fee=800,Capacity=150,AlreadyRegistered=60,Description="Design autonomous bots that navigate a dynamic arena. Three rounds: line follow, obstacle course and a head-to-head knockout.",Eligibility="UG students. Teams of up to 4.",Prizes="Rs. 1,50,000 + trophies",Schedule="Day 1: Inspection and qualifiers|Day 2: Obstacle course|Day 3: Knockouts and finals",ContactEmail="robotics@example.edu"},
                new Event{Id=3,Title="Annual Cultural Fest: Rangotsav",UniversityId=6,Category="Cultural",Mode="Offline",StartDate=d(28),EndDate=d(30),RegistrationDeadline=d(20),Venue="North Campus, Delhi",Fee=0,Capacity=2000,AlreadyRegistered=1180,Description="Three days of dance, music, theatre and street play competitions with open-mic nights and a food street.",Eligibility="Open to students from all colleges. College ID required.",Prizes="Trophies and cash awards across 20+ events",Schedule="Day 1: Inauguration and street plays|Day 2: Dance and music|Day 3: Battle of Bands and closing night",ContactEmail="rangotsav@example.edu"},
                new Event{Id=4,Title="Intro to Machine Learning Workshop",UniversityId=4,Category="Workshop",Mode="Hybrid",StartDate=d(10),EndDate=d(11),RegistrationDeadline=d(7),Venue="CSA Auditorium + Live stream",Fee=250,Capacity=120,AlreadyRegistered=95,Description="Hands-on two-day workshop covering regression, classification and a mini project in Python. Certificate on completion.",Eligibility="UG/PG students with basic Python knowledge.",Prizes="Participation certificate",Schedule="Day 1: Python and NumPy refresher, regression|Day 2: Classification and mini project",ContactEmail="mlworkshop@example.edu"},
                new Event{Id=5,Title="Inter-University Cricket Cup",UniversityId=5,Category="Sports",Mode="Offline",StartDate=d(45),EndDate=d(50),RegistrationDeadline=d(30),Venue="BITS Cricket Ground",Fee=3000,Capacity=32,AlreadyRegistered=21,Description="T20 knockout tournament for university teams. Floodlit semi-finals and final.",Eligibility="One team (15 players) per university.",Prizes="Rs. 1,00,000 winner, Rs. 50,000 runner-up",Schedule="Day 1-4: League and knockouts|Day 5: Semi-finals|Day 6: Final",ContactEmail="sports@example.edu"},
                new Event{Id=6,Title="TechVision National Conference",UniversityId=7,Category="Conference",Mode="Hybrid",StartDate=d(40),EndDate=d(41),RegistrationDeadline=d(33),Venue="Main Auditorium, Guindy",Fee=400,Capacity=500,AlreadyRegistered=140,Description="Talks and paper presentations on AI, IoT and sustainable computing. Best papers get published in the proceedings.",Eligibility="UG, PG and research scholars.",Prizes="Best paper awards and publication",Schedule="Day 1: Keynotes and paper tracks|Day 2: Panel discussion and awards",ContactEmail="techvision@example.edu"},
                new Event{Id=7,Title="Startup Pitch Fest",UniversityId=11,Category="Competition",Mode="Offline",StartDate=d(18),EndDate=d(18),RegistrationDeadline=d(12),Venue="Seminar Hall, Marwadi University",Fee=100,Capacity=200,AlreadyRegistered=88,Description="Pitch your idea to a panel of founders and investors. Top teams get incubation support and mentoring.",Eligibility="Student founders from any college.",Prizes="Seed grant up to Rs. 2,00,000 + incubation",Schedule="10 AM: Registration|11 AM: Pitch rounds|3 PM: Finalists on stage|5 PM: Results",ContactEmail="startup@example.edu"},
                new Event{Id=8,Title="Kolkata Literary and Debate Meet",UniversityId=8,Category="Cultural",Mode="Offline",StartDate=d(26),EndDate=d(27),RegistrationDeadline=d(19),Venue="Arts Faculty Hall",Fee=50,Capacity=250,AlreadyRegistered=70,Description="Parliamentary debate, extempore, poetry slam and quiz in English, Hindi and Bengali.",Eligibility="All students.",Prizes="Medals and books",Schedule="Day 1: Debate and extempore|Day 2: Poetry slam and quiz",ContactEmail="litmeet@example.edu"},
                new Event{Id=9,Title="Web3 and Cloud Bootcamp",UniversityId=10,Category="Workshop",Mode="Online",StartDate=d(8),EndDate=d(9),RegistrationDeadline=d(5),Venue="Online (link sent after registration)",Fee=0,Capacity=1000,AlreadyRegistered=430,Description="Free weekend bootcamp on cloud basics and smart contracts with live Q&A.",Eligibility="Any student with a laptop.",Prizes="E-certificate",Schedule="Sat 10 AM: Cloud fundamentals|Sun 10 AM: Smart contract basics",ContactEmail="bootcamp@example.edu"},
                new Event{Id=10,Title="Kashi Yuva Mahotsav",UniversityId=9,Category="Fest",Mode="Offline",StartDate=d(55),EndDate=d(57),RegistrationDeadline=d(45),Venue="Central Lawns, BHU",Fee=0,Capacity=3000,AlreadyRegistered=900,Description="Festival of art, classical music, science exhibits and student-run stalls.",Eligibility="Open to all students.",Prizes="Awards in art, music and science exhibit categories",Schedule="Day 1: Exhibits open|Day 2: Music evenings|Day 3: Closing ceremony",ContactEmail="mahotsav@example.edu"},
                new Event{Id=11,Title="Data Science Case Competition",UniversityId=12,Category="Competition",Mode="Online",StartDate=d(15),EndDate=d(17),RegistrationDeadline=d(10),Venue="Online",Fee=150,Capacity=400,AlreadyRegistered=399,Description="Solve a real-world dataset challenge over a weekend and present insights to judges.",Eligibility="UG/PG students. Teams of 1 to 3.",Prizes="Rs. 75,000 + certificates",Schedule="Fri: Problem release|Sat: Mentor office hours|Sun: Submission and presentations",ContactEmail="datasci@example.edu"},
                new Event{Id=12,Title="Inter-College Football League",UniversityId=13,Category="Sports",Mode="Offline",StartDate=d(33),EndDate=d(38),RegistrationDeadline=d(24),Venue="PU Sports Complex",Fee=1500,Capacity=24,AlreadyRegistered=10,Description="Round-robin and knockout football tournament for college teams.",Eligibility="Teams of 11 + 5 substitutes.",Prizes="Rs. 60,000 and trophies",Schedule="Day 1-4: Group stage|Day 5: Semi-finals|Day 6: Final",ContactEmail="football@example.edu"},
                new Event{Id=13,Title="MedTech Innovation Summit",UniversityId=14,Category="Conference",Mode="Offline",StartDate=d(60),EndDate=d(61),RegistrationDeadline=d(50),Venue="Dr. TMA Pai Hall",Fee=600,Capacity=400,AlreadyRegistered=120,Description="Medical devices, health-tech and biotech talks plus a student prototype showcase.",Eligibility="Students of engineering, medicine and allied sciences.",Prizes="Prototype grants and mentorship",Schedule="Day 1: Talks and panels|Day 2: Prototype showcase and awards",ContactEmail="medtech@example.edu"},
                new Event{Id=14,Title="Open Mic and Music Night",UniversityId=12,Category="Cultural",Mode="Offline",StartDate=d(5),EndDate=d(5),RegistrationDeadline=d(3),Venue="Amphitheatre, UoH",Fee=0,Capacity=150,AlreadyRegistered=64,Description="Sing, perform stand-up or play an instrument. Slots are first come, first served.",Eligibility="All students.",Prizes="Feature slot at the next fest",Schedule="6 PM: Doors open|7 PM: Performances|10 PM: Closing jam",ContactEmail="openmic@example.edu"}
            };
        }

        public static University GetUniversity(int id) { return Universities.FirstOrDefault(u => u.Id == id); }
        public static Event GetEvent(int id) { return Events.FirstOrDefault(e => e.Id == id); }

        public static int CountFor(int eventId)
        {
            lock (Sync)
            {
                using (var db = new AppDbContext())
                    return db.Registrations.Count(r => r.EventId == eventId && r.Status == "Confirmed");
            }
        }

        // Returns null on success, otherwise an error message.
        public static string TryRegister(Registration r)
        {
            lock (Sync)
            {
                var ev = GetEvent(r.EventId);
                if (ev == null) return "Event not found.";
                if (DateTime.Today > ev.RegistrationDeadline.Date) return "Registration for this event has closed.";
                if (ev.SeatsLeft == 0) return "This event is full.";

                using (var db = new AppDbContext())
                {
                    string email = r.Email.Trim();
                    if (db.Registrations.Any(x => x.EventId == r.EventId && x.Status == "Confirmed" && x.Email == email))
                        return "This email is already registered for this event.";

                    using (var tx = db.Database.BeginTransaction())
                    {
                        r.Id = 0;
                        r.Email = email;
                        r.RegisteredOn = DateTime.Now;
                        r.Status = "Confirmed";
                        db.Registrations.Add(r);
                        db.SaveChanges();                       // database assigns the Id

                        r.Code = "EH" + (100000 + (r.Id * 7919) % 900000);
                        db.SaveChanges();
                        tx.Commit();
                    }
                    return null;
                }
            }
        }

        public static bool Cancel(string code, string email)
        {
            lock (Sync)
            {
                using (var db = new AppDbContext())
                {
                    email = (email ?? "").Trim();
                    var r = db.Registrations.FirstOrDefault(x => x.Code == code && x.Email == email);
                    if (r == null) return false;
                    r.Status = "Cancelled";
                    db.SaveChanges();
                    return true;
                }
            }
        }
    }
}
