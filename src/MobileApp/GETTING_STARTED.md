# Getting Started with Minerals Trading Mobile App

This guide will help you set up, build, and run the Minerals Trading Mobile App on your development machine.

## Prerequisites

### Required Software

1. **.NET 9.0 SDK** or later
   - Download from: https://dotnet.microsoft.com/download
   - Verify installation: `dotnet --version`

2. **.NET MAUI Workload**
   ```bash
   dotnet workload install maui
   ```

### Platform-Specific Requirements

#### For Android Development
- **Windows, macOS, or Linux**
- Android SDK (automatically installed with MAUI workload)
- Android Emulator or physical Android device
- Minimum Android version: 7.0 (API 24)

#### For iOS Development
- **macOS only** (requires Xcode)
- Xcode 14 or later
- iOS Simulator or physical iOS device
- Minimum iOS version: 14.2

#### For macOS Development
- **macOS only**
- Xcode 14 or later
- Minimum macOS version: 14.0

#### For Windows Development
- **Windows 10/11** (version 1809 or later)
- Visual Studio 2022 (recommended) or VS Code
- Windows App SDK

## Development Environment Setup

### Option 1: Visual Studio 2022 (Recommended for Windows/Mac)

1. **Install Visual Studio 2022**
   - Download from: https://visualstudio.microsoft.com/

2. **Select Workloads**
   - .NET Multi-platform App UI development
   - Mobile development with .NET (for Android)
   - iOS development (macOS only)

3. **Open the Solution**
   - Open `Platform.Mining.Trading.sln` or `MineralsTradingMobileApp.csproj`
   - Right-click on MobileApp project → Set as Startup Project

### Option 2: Visual Studio Code

1. **Install VS Code**
   - Download from: https://code.visualstudio.com/

2. **Install Extensions**
   - C# Dev Kit
   - .NET MAUI (optional but helpful)

3. **Open the Project**
   ```bash
   cd src/MobileApp
   code .
   ```

### Option 3: Command Line

Works on Windows, macOS, and Linux (for project setup and Android)

## Building the Application

### From Command Line

```bash
# Navigate to project directory
cd src/MobileApp

# Restore dependencies
dotnet restore

# Build for specific platform
dotnet build -f net9.0-android      # Android
dotnet build -f net9.0-ios          # iOS (macOS only)
dotnet build -f net9.0-maccatalyst  # macOS (macOS only)
dotnet build -f net9.0-windows10.0.19041.0  # Windows (Windows only)
```

### From Visual Studio

1. Select target platform in dropdown (Android, iOS, Windows)
2. Click Build → Build Solution (or press Ctrl+Shift+B / Cmd+Shift+B)

## Running the Application

### Android

#### Using Emulator
```bash
# List available emulators
dotnet build -f net9.0-android -t:Run

# Or use Visual Studio:
# 1. Select "Android Emulator" as target
# 2. Press F5 or click Debug
```

#### Using Physical Device
1. Enable Developer Options on Android device
2. Enable USB Debugging
3. Connect device via USB
4. Trust the computer on device
5. Run:
   ```bash
   dotnet build -f net9.0-android -t:Run
   ```

### iOS (macOS only)

#### Using Simulator
```bash
dotnet build -f net9.0-ios -t:Run
```

#### Using Physical Device
1. Register device in Apple Developer Portal
2. Create provisioning profile
3. Connect device via USB
4. Trust the computer on device
5. Run from Visual Studio or Xcode

### Windows

```bash
dotnet build -f net9.0-windows10.0.19041.0 -t:Run
```

Or press F5 in Visual Studio

### macOS (Mac Catalyst)

```bash
dotnet build -f net9.0-maccatalyst -t:Run
```

## Project Structure Overview

```
MobileApp/
├── Components/          # Blazor components
│   ├── Layout/         # MainLayout, NavMenu
│   ├── Pages/          # Page components
│   └── Routes.razor    # Routing
├── Platforms/          # Platform-specific code
├── Resources/          # Images, icons, styles
├── wwwroot/           # Static web assets
└── MauiProgram.cs     # App configuration
```

## Key Files to Know

- **MauiProgram.cs**: App initialization, service registration
- **App.xaml/cs**: Application lifecycle
- **MainPage.xaml**: Main page with BlazorWebView
- **Components/Routes.razor**: Route definitions
- **Components/Layout/NavMenu.razor**: Navigation menu
- **Components/Pages/*.razor**: Page components

## Configuration

### Connecting to Real Backend

1. Update service registrations in `MauiProgram.cs`:
   ```csharp
   // Replace mock services with HTTP services
   builder.Services.AddScoped<IBuyerService, HttpBuyerService>();
   // Add HttpClient
   builder.Services.AddHttpClient();
   ```

2. Configure API endpoint:
   ```csharp
   builder.Services.AddScoped(sp => new HttpClient 
   { 
       BaseAddress = new Uri("https://your-api.com/") 
   });
   ```

### App Settings

Modify app properties in `MineralsTradingMobileApp.csproj`:
- ApplicationTitle
- ApplicationId
- ApplicationDisplayVersion
- ApplicationVersion

## Debugging

### Enable Debug Logging

Already enabled in debug mode:
```csharp
#if DEBUG
    builder.Services.AddBlazorWebViewDeveloperTools();
    builder.Logging.AddDebug();
#endif
```

### View Logs

- **Android**: Use Android Logcat (View → Tool Windows → Logcat in VS)
- **iOS**: Use Xcode Console
- **Windows**: Use Visual Studio Output window
- **Console**: Check debug output in terminal

### Debugging Blazor Components

1. Set breakpoints in .razor.cs files or @code blocks
2. Press F5 to start debugging
3. Debug as you would a normal .NET app

### Hot Reload

Both Visual Studio and VS Code support hot reload for MAUI apps:
- Make changes to XAML or C# files
- Save the file
- App updates automatically (in most cases)

## Common Issues & Solutions

### Issue: MAUI workload not found
```
Solution: dotnet workload install maui
```

### Issue: Android SDK not found
```
Solution: Set ANDROID_HOME environment variable
export ANDROID_HOME=$HOME/Library/Android/sdk  # macOS/Linux
set ANDROID_HOME=C:\Android\sdk               # Windows
```

### Issue: iOS build fails (code signing)
```
Solution: 
1. Open Xcode
2. Xcode → Preferences → Accounts
3. Add your Apple ID
4. Download certificates
```

### Issue: Cannot resolve Platform.Trading.Management
```
Solution: Ensure Platform.Trading.Management project builds successfully
cd ../Platform.Trading.Management
dotnet build
```

### Issue: Radzen components not showing correctly
```
Solution: Check that Radzen CSS is loaded in index.html
<link rel="stylesheet" href="_content/Radzen.Blazor/css/material-base.css">
```

## Testing the App

### Manual Testing Checklist

- [ ] App launches successfully
- [ ] Home page displays correctly
- [ ] Navigation menu works
- [ ] Can navigate to Buyers page
- [ ] Can navigate to Trades page
- [ ] Can navigate to Dashboard page
- [ ] Can navigate to other pages
- [ ] Components from Platform.Trading.Management render correctly
- [ ] Radzen components work as expected
- [ ] App responds to orientation changes (mobile)

### Performance Testing

Monitor:
- App startup time
- Page navigation speed
- Component render time
- Memory usage
- Battery consumption (on device)

## Next Steps

1. **Customize the App**
   - Update app icon and splash screen
   - Modify colors and themes
   - Add custom fonts

2. **Connect to Backend**
   - Implement HTTP services
   - Configure authentication
   - Handle API errors

3. **Add Features**
   - Push notifications
   - Offline support
   - Biometric authentication
   - QR code scanning

4. **Deploy**
   - Create release builds
   - Submit to app stores
   - Set up CI/CD pipeline

## Resources

- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [Blazor Hybrid Documentation](https://docs.microsoft.com/aspnet/core/blazor/hybrid/)
- [Radzen Blazor Components](https://blazor.radzen.com/)
- [MAUI GitHub Repository](https://github.com/dotnet/maui)

## Support

For issues specific to this project:
- Check PROJECT_OVERVIEW.md for architecture details
- Check README.md for feature documentation
- Create an issue in the GitHub repository

## License

[Add license information]

---

**Happy Coding! 🚀**
