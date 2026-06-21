# 🎓 SmartCampusAI – AI-Powered Attendance System

## Tech Stack
- C# .NET 6 WinForms + ADO.NET
- SQL Server (Express or full)
- OpenCvSharp4 (Haar Cascade + LBPH Face Recognition)
- Newtonsoft.Json

---

## ⚡ Quick Setup

### Step 1 – Database
1. Open **SQL Server Management Studio (SSMS)**
2. Open `Database/SmartCampusAI.sql`
3. Execute the entire script
4. Database `SmartCampusAI` is created with seed data

### Step 2 – Connection String
Edit `Data/DBConnection.cs` and update:
```csharp
// Windows Auth (default):
"Server=.\\SQLEXPRESS;Database=SmartCampusAI;Trusted_Connection=True;TrustServerCertificate=True;"

// SQL Server Auth:
"Server=YOUR_SERVER;Database=SmartCampusAI;User Id=sa;Password=YOUR_PASS;TrustServerCertificate=True;"
```

### Step 3 – Haar Cascade XML Files
Download from OpenCV official repo and place in `Resources/` folder:
- `haarcascade_frontalface_default.xml`
- `haarcascade_eye.xml` (optional)

Direct download URL:
https://github.com/opencv/opencv/tree/master/data/haarcascades

### Step 4 – Build & Run
```bash
cd SmartCampusAI.WinForms
dotnet restore
dotnet build
dotnet run
```

Or open `SmartCampusAI.sln` in Visual Studio 2022 and press F5.

---

## 🔐 Default Login Credentials
| Username | Password     | Role  |
|----------|-------------|-------|
| admin    | Admin@123   | Admin |
| teacher  | Teacher@123 | User  |
| staff    | Staff@123   | User  |

---

## 🤖 Face Recognition Workflow

### 1. Register Student Faces
1. Go to **Face Recognition** form
2. Click **Register Face** mode
3. Select a student from dropdown
4. Start camera → click **Capture Sample** 20 times (vary angles)
5. Progress bar fills to 100%

### 2. Train the AI Model
- Click **🧠 Train AI Model**
- Wait for "Trained with N samples" message
- Training uses LBPH (Local Binary Pattern Histogram)

### 3. Auto Attendance
1. Click **Recognize Face** mode
2. Point camera at students
3. Face is detected (green rectangle)
4. If confidence > 80% → attendance auto-marked in SQL Server
5. Duplicate attendance for same day is prevented

### 4. Manual Fallback
- Click **✏ Manual Attendance Fallback** if camera fails
- Select student + status manually

---

## 📁 Project Structure
```
SmartCampusAI/
├── Database/
│   └── SmartCampusAI.sql          ← Run this first
├── SmartCampusAI.WinForms/
│   ├── Forms/
│   │   ├── LoginForm.cs
│   │   ├── DashboardForm.cs
│   │   ├── StudentForm.cs
│   │   ├── UserForm.cs
│   │   ├── AttendanceForm.cs
│   │   └── FaceRecognitionForm.cs ← AI face recognition
│   ├── Models/
│   │   ├── Student.cs
│   │   ├── User.cs
│   │   ├── Attendance.cs
│   │   └── FaceData.cs
│   ├── Data/
│   │   ├── DBConnection.cs
│   │   ├── StudentRepository.cs
│   │   ├── UserRepository.cs
│   │   ├── AttendanceRepository.cs
│   │   └── FaceRepository.cs
│   ├── Services/
│   │   ├── CameraService.cs       ← Webcam / DroidCam integration
│   │   ├── FaceRecognitionService.cs ← LBPH AI logic
│   │   └── AttendanceService.cs
│   ├── Utils/
│   │   ├── SessionManager.cs
│   │   ├── Validators.cs
│   │   └── Helpers.cs
│   └── Styles/
│       ├── Theme.cs
│       └── ColorPalette.cs
```

---

## 📷 DroidCam Setup (Phone as Webcam)
1. Install DroidCam on Android / iPhone
2. Install DroidCam Windows client
3. Connect phone and PC on same Wi-Fi
4. In Face Recognition form, set **Camera Index = 1** (or 2)
5. Click Start Camera

---

## 🗄 Database Tables
| Table      | Purpose                              |
|------------|--------------------------------------|
| Users      | Login accounts with roles            |
| Students   | Student records                      |
| FaceData   | LBP face encodings + image paths     |
| Attendance | Daily attendance with confidence %   |

---

## ⚙ NuGet Packages (auto-restored)
```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.0" />
<PackageReference Include="OpenCvSharp4" Version="4.9.0.20240103" />
<PackageReference Include="OpenCvSharp4.runtime.win" Version="4.9.0.20240103" />
<PackageReference Include="OpenCvSharp4.Extensions" Version="4.9.0.20240103" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
```

---

## 🛠 Troubleshooting
| Problem | Fix |
|---------|-----|
| Camera not opening | Check camera index (0=built-in, 1=USB/DroidCam) |
| "Haar cascade not found" | Place XML files in `Resources/` folder in project root |
| SQL connection error | Update connection string in `DBConnection.cs` |
| Low recognition accuracy | Capture more samples (30+), ensure good lighting |
| UI freezing | Camera runs on background Task; ensure .NET 6+ |
