using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FaceAttendanceSystem.Services
{
    // Builds a printable "who attended this lecture" attendance sheet as a PDF -
    // lecture in-charge, subject/course details, and a table listing every
    // registered student as either Present (with join time) or Not Attend.
    public static class AttendancePdfService
    {
        public static void GenerateAndSave(
            string filePath,
            Subject subject,
            Lecturer lecturerInCharge,
            DateTime forDate)
        {
            List<Student> allStudents = new StudentRepository().LoadAll()
                .OrderBy(s => s.RegNumber)
                .ToList();

            List<AttendanceRecord> todaysRecords = new AttendanceRepository()
                .GetByDate(forDate)
                .Where(r => r.SubjectCode.Equals(subject.SubjectCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Dictionary<string, AttendanceRecord> attendanceByStudent = todaysRecords
                .GroupBy(r => r.RegNumber, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

            int presentCount = attendanceByStudent.Count;
            int notAttendCount = allStudents.Count - presentCount;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(column =>
                    {
                        column.Item().Text("Sabaragamuwa University of Sri Lanka")
                            .FontSize(16).Bold().FontColor("#8D1538");
                        column.Item().Text("Faculty of Applied Sciences - Department of Physical Science and Technology")
                            .FontSize(10).FontColor(Colors.Grey.Darken2);
                        column.Item().PaddingTop(8).Text("Lecture Attendance Sheet")
                            .FontSize(13).Bold();
                        column.Item().PaddingTop(10).LineHorizontal(1).LineColor("#8D1538");
                    });

                    page.Content().PaddingTop(15).Column(column =>
                    {
                        column.Spacing(6);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Subject: ").Bold();
                                text.Span($"{subject.SubjectName}");
                            });
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Course Code: ").Bold();
                                text.Span(subject.SubjectCode);
                            });
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Lecture In-Charge: ").Bold();
                                text.Span($"{lecturerInCharge.FullName} ({lecturerInCharge.Position})");
                            });
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Date: ").Bold();
                                text.Span(forDate.ToString("dddd, dd MMMM yyyy"));
                            });
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Present: ").Bold().FontColor(Colors.Green.Darken2);
                                text.Span($"{presentCount}");
                            });
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Not Attend: ").Bold().FontColor(Colors.Red.Darken2);
                                text.Span($"{notAttendCount}");
                            });
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Total Registered: ").Bold();
                                text.Span($"{allStudents.Count}");
                            });
                        });

                        column.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(35);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderCellStyle).Text("#");
                                header.Cell().Element(HeaderCellStyle).Text("Reg Number");
                                header.Cell().Element(HeaderCellStyle).Text("Name");
                                header.Cell().Element(HeaderCellStyle).Text("Join Time");
                                header.Cell().Element(HeaderCellStyle).Text("Status");
                            });

                            for (int i = 0; i < allStudents.Count; i++)
                            {
                                Student student = allStudents[i];
                                bool attended = attendanceByStudent.TryGetValue(student.RegNumber, out AttendanceRecord? record);

                                table.Cell().Element(BodyCellStyle).Text($"{i + 1}");
                                table.Cell().Element(BodyCellStyle).Text(student.RegNumber);
                                table.Cell().Element(BodyCellStyle).Text(student.FullName);
                                table.Cell().Element(BodyCellStyle).Text(attended ? record!.TimeMarked.ToString("hh:mm tt") : "-");

                                table.Cell().Element(BodyCellStyle).Text(attended ? "Present" : "Not Attend")
                                    .FontColor(attended ? Colors.Green.Darken2 : Colors.Red.Darken2)
                                    .Bold();
                            }
                        });
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Generated by the Sabaragamuwa University Face Attendance System on ");
                        text.Span(DateTime.Now.ToString("dd MMM yyyy, hh:mm tt"));
                    });
                });
            })
            .GeneratePdf(filePath);
        }

        private static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background("#8D1538")
                .Padding(6)
                .DefaultTextStyle(x => x.FontColor(Colors.White).Bold());
        }

        private static IContainer BodyCellStyle(IContainer container)
        {
            return container
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(6);
        }
    }
}
