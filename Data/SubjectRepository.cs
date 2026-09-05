using System.Xml.Linq;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Data
{
    public class SubjectRepository
    {
        public SubjectRepository()
        {
            if (!File.Exists(FilePaths.SubjectsFilePath))
            {
                new XElement("subjects").Save(FilePaths.SubjectsFilePath);
                SeedFromHandbook();
            }
        }

        // Pre-loads the subjects actually listed in the Faculty of Applied Sciences
        // handbook (FAS-SHB) for the Department of Physical Science and Technology.
        // Years 1-2 are the common core every PST student takes. Years 3-4 vary a lot
        // by major (Physics / Chemical Technology / Applied Physics / Computer
        // Science & Technology); the Applied-Physics-track compulsory courses are
        // seeded as a starting point - add/remove the rest here in Manage Subjects.
        private void SeedFromHandbook()
        {
            var seedSubjects = new List<Subject>
            {
                // Year 1, Semester 1
                new() { SubjectCode = "PST 11201", SubjectName = "Mechanics and Properties of Matter", Year = 1, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 11202", SubjectName = "Introduction to Electricity and Magnetism", Year = 1, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 11103", SubjectName = "Physics Laboratory 1-I", Year = 1, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 11204", SubjectName = "General Chemistry", Year = 1, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 11205", SubjectName = "Fundamentals of Organic Chemistry", Year = 1, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 11106", SubjectName = "Inorganic Chemistry Laboratory I", Year = 1, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 11107", SubjectName = "Structured Programming", Year = 1, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 11208", SubjectName = "Computer Hardware and Software", Year = 1, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 11109", SubjectName = "Computer Laboratory 1-I", Year = 1, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 11210", SubjectName = "Calculus and Differential Equations", Year = 1, Semester = 1, Credits = 2 },

                // Year 1, Semester 2
                new() { SubjectCode = "PST 12201", SubjectName = "Physics of Heat and Waves", Year = 1, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 12102", SubjectName = "Semi-Conductor Physics", Year = 1, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 12103", SubjectName = "AC Theory & Circuits", Year = 1, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 12104", SubjectName = "Physics Laboratory 1-II", Year = 1, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 12205", SubjectName = "Fundamentals of Physical Chemistry", Year = 1, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 12206", SubjectName = "Fundamentals of Analytical Chemistry", Year = 1, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 12107", SubjectName = "Organic Chemistry Laboratory I", Year = 1, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 12108", SubjectName = "Object Oriented Programming", Year = 1, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 12209", SubjectName = "Fundamentals of Statistics", Year = 1, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 12110", SubjectName = "Computer Laboratory 1-II", Year = 1, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 12211", SubjectName = "Database Management Systems", Year = 1, Semester = 2, Credits = 2 },

                // Year 2, Semester 1
                new() { SubjectCode = "PST 21201", SubjectName = "Electronics", Year = 2, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 21202", SubjectName = "Geometrical and Physical Optics", Year = 2, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 21103", SubjectName = "Physics Laboratory 2-I", Year = 2, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 21204", SubjectName = "Organic Chemistry", Year = 2, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 21205", SubjectName = "Industrial Chemistry and Technology I (Organic)", Year = 2, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 21106", SubjectName = "Organic Chemistry Laboratory II", Year = 2, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 21207", SubjectName = "Data Structures & Algorithms", Year = 2, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 21208", SubjectName = "Computer Architecture and Assembly Language", Year = 2, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 21209", SubjectName = "Statistics for Experimental Analysis", Year = 2, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 21110", SubjectName = "Computer Laboratory 2-I", Year = 2, Semester = 1, Credits = 1 },

                // Year 2, Semester 2
                new() { SubjectCode = "PST 22201", SubjectName = "Physics of Electromagnetic Radiation and Introduction to Laser", Year = 2, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 22202", SubjectName = "Quantum Physics, Atomic & Nuclear Physics", Year = 2, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 22103", SubjectName = "Physics Laboratory 2-II", Year = 2, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 22204", SubjectName = "Chemistry of Elements", Year = 2, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 22205", SubjectName = "Physical Chemistry", Year = 2, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 22106", SubjectName = "Inorganic Chemistry Laboratory II", Year = 2, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 22208", SubjectName = "Software Engineering", Year = 2, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 22209", SubjectName = "Statistical Methodology", Year = 2, Semester = 2, Credits = 2 },
                new() { SubjectCode = "PST 22110", SubjectName = "Computer Laboratory 2-II", Year = 2, Semester = 2, Credits = 1 },
                new() { SubjectCode = "PST 22211", SubjectName = "Operating Systems", Year = 2, Semester = 2, Credits = 2 },

                // Year 3, Semester 1 (Applied Physics track compulsory courses - starting point)
                new() { SubjectCode = "PST 31201", SubjectName = "Solid State Physics", Year = 3, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 31202", SubjectName = "Nuclear Physics & Applications", Year = 3, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 31203", SubjectName = "Quantum Mechanics", Year = 3, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 31104", SubjectName = "Material Physics", Year = 3, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 31205", SubjectName = "Special Relativity", Year = 3, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 31206", SubjectName = "Optical Fiber & Telecommunication", Year = 3, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 31108", SubjectName = "Physics Laboratory 3-I", Year = 3, Semester = 1, Credits = 1 },
                new() { SubjectCode = "PST 31211", SubjectName = "Mathematical Programming", Year = 3, Semester = 1, Credits = 2 },

                // Year 4, Semester 1 (Applied Physics track compulsory courses - starting point)
                new() { SubjectCode = "PST 41202", SubjectName = "Computational Physics", Year = 4, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 41204", SubjectName = "Remote Sensing & GIS", Year = 4, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 41205", SubjectName = "Geophysics", Year = 4, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 41206", SubjectName = "Medical and BioPhysics", Year = 4, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 41211", SubjectName = "Data Reduction & Analysis", Year = 4, Semester = 1, Credits = 2 },
                new() { SubjectCode = "PST 41216", SubjectName = "Classical Mechanics", Year = 4, Semester = 1, Credits = 2 },
            };

            foreach (Subject subject in seedSubjects)
            {
                Add(subject);
            }
        }

        public List<Subject> LoadAll()
        {
            XElement root = XElement.Load(FilePaths.SubjectsFilePath);
            return root.Elements("subject").Select(e => new Subject
            {
                SubjectCode = (string?)e.Element("subjectCode") ?? string.Empty,
                SubjectName = (string?)e.Element("subjectName") ?? string.Empty,
                Year = (int?)e.Element("year") ?? 1,
                Semester = (int?)e.Element("semester") ?? 1,
                Credits = (int?)e.Element("credits") ?? 3
            }).ToList();
        }

        public bool SubjectCodeExists(string subjectCode)
        {
            return LoadAll().Any(s => s.SubjectCode.Equals(subjectCode, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Subject subject)
        {
            XElement root = XElement.Load(FilePaths.SubjectsFilePath);
            root.Add(new XElement("subject",
                new XElement("subjectCode", subject.SubjectCode),
                new XElement("subjectName", subject.SubjectName),
                new XElement("year", subject.Year),
                new XElement("semester", subject.Semester),
                new XElement("credits", subject.Credits)
            ));
            root.Save(FilePaths.SubjectsFilePath);
        }

        public void Update(Subject subject)
        {
            XElement root = XElement.Load(FilePaths.SubjectsFilePath);
            XElement? existing = root.Elements("subject")
                .FirstOrDefault(e => (string?)e.Element("subjectCode") == subject.SubjectCode);

            if (existing != null)
            {
                existing.SetElementValue("subjectName", subject.SubjectName);
                existing.SetElementValue("year", subject.Year);
                existing.SetElementValue("semester", subject.Semester);
                existing.SetElementValue("credits", subject.Credits);
                root.Save(FilePaths.SubjectsFilePath);
            }
        }

        public void Delete(string subjectCode)
        {
            XElement root = XElement.Load(FilePaths.SubjectsFilePath);
            root.Elements("subject")
                .Where(e => (string?)e.Element("subjectCode") == subjectCode)
                .ToList()
                .ForEach(e => e.Remove());
            root.Save(FilePaths.SubjectsFilePath);
        }
    }
}
