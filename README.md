# Lanka AI Campus Attendance System

C# .NET 8 WinForms application that marks student attendance automatically by
recognizing faces through a webcam (built with DlibDotNet + OpenCvSharp).

## ⚠️ Required before you can build

This project needs **two Dlib model files** that are too large to include in
this zip. Download both (they are public files from the official dlib model
repository) and place them in the `FaceAttendanceSystem\Models\` folder:

1. `shape_predictor_68_face_landmarks.dat`
2. `dlib_face_recognition_resnet_model_v1.dat`

Get them from: https://github.com/davisking/dlib-models
(each is inside a `.bz2` archive — extract with 7-Zip to get the plain `.dat` file)

After extracting, your folder should look like:
```
FaceAttendanceSystem\
  Models\
    shape_predictor_68_face_landmarks.dat
    dlib_face_recognition_resnet_model_v1.dat
```

## 📧 Setting up confirmation emails (optional but recommended)

The app sends two kinds of emails: to lecturers when they register, and to
students when their attendance is marked. If you don't configure this, the
app still works perfectly — it just quietly skips sending emails.

To turn emails on:
1. Run the app once (so `XML Local Data Files\email-settings.xml` gets created)
2. Close the app and open that file in Notepad
3. Fill in a real sender account, e.g. for Gmail:
   - `senderEmail`: your Gmail address
   - `senderPassword`: a Gmail **App Password** (not your normal password —
     create one at https://myaccount.google.com/apppasswords, requires
     2-Step Verification to be turned on)
   - Leave `smtpHost` as `smtp.gmail.com` and `smtpPort` as `587`
4. Save the file and restart the app

## How to open & run
1. Open `FaceAttendanceSystem.sln` in Visual Studio 2022
2. Let NuGet restore the packages (DlibDotNet, OpenCvSharp4)
3. Place the two model files as above
4. Build (Ctrl+Shift+B) and Run (F5)
5. Allow camera access if Windows prompts you

## How it works
- **Register Student**: capture the student's face from the webcam, enter
  their registration number / name / class, and save. A 128-value face
  "descriptor" is computed with Dlib's ResNet recognition model and stored
  in `XML Local Data Files\students.xml`.
- **Take Attendance**: the webcam runs continuously; every ~0.5s a frame is
  checked, a face descriptor is computed, and compared (Euclidean distance)
  against every registered student. A match within the recognition threshold
  marks that student "Present" for today in `attendance.xml` (each student
  can only be marked once per day).
- **Attendance Records**: pick a date to see everyone marked present that day.

## ⚠️ Honest note about this build
Unlike the earlier bug-fixes on your existing project, this is **brand-new
code** that uses a webcam and machine-learning models, and I don't have a
Windows machine or a webcam here to actually compile and run it myself. I
followed the standard, documented DlibDotNet face-recognition pattern
carefully, but there's a realistic chance the very first build throws a
small error (e.g. an API name mismatch) since I couldn't verify it end to end.
If that happens, send me the exact error message/screenshot and I'll fix it
immediately.
