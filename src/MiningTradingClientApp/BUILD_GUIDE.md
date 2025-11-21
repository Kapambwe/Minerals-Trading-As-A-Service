# Build Guide - MiningTradingClientApp (.NET MAUI)

## Quick Start

This is a **.NET MAUI** application, not a web application. It must be built and run on a supported platform.

## Prerequisites

### Required

1. **.NET 9.0 SDK**
   ```bash
   # Download from: https://dotnet.microsoft.com/download/dotnet/9.0
   # Verify installation:
   dotnet --version
   ```

2. **.NET MAUI Workload**
   ```bash
   dotnet workload install maui
   ```

### Platform-Specific Requirements

#### For Android Development

- **Android SDK** (API 24+)
- Install via:
  - Visual Studio 2022 (Windows/Mac)
  - Android Studio
  - Command line: `dotnet workload install android`

#### For iOS/macOS Development

- **macOS only**
- **Xcode 14+**
- Install from Mac App Store
- Accept license: `sudo xcodebuild -license accept`

#### For Windows Development

- **Windows 10/11**
- **Visual Studio 2022** with:
  - .NET Multi-platform App UI development workload
  - Windows App SDK

## Building

### 1. Restore Dependencies

```bash
cd src/MiningTradingClientApp
dotnet restore
```

### 2. Build for Specific Platform

#### Android
```bash
dotnet build -f net9.0-android
```

#### iOS (macOS only)
```bash
dotnet build -f net9.0-ios
```

#### macOS (Mac Catalyst)
```bash
dotnet build -f net9.0-maccatalyst
```

#### Windows
```bash
dotnet build -f net9.0-windows10.0.19041.0
```

## Running

### Android Emulator

1. Create/start emulator via Android Studio AVD Manager
2. Run:
   ```bash
   dotnet run -f net9.0-android
   ```

### iOS Simulator (macOS only)

```bash
dotnet run -f net9.0-ios
```

### Windows

```bash
dotnet run -f net9.0-windows10.0.19041.0
```

### Physical Devices

#### Android
1. Enable Developer Options on device
2. Enable USB Debugging
3. Connect device
4. Run: `dotnet run -f net9.0-android`

#### iOS (macOS only)
1. Connect device
2. Trust computer on device
3. Run: `dotnet run -f net9.0-ios`

## Visual Studio

### Opening in Visual Studio 2022

1. Open `MiningTradingClientApp.csproj`
2. Select target framework in dropdown
3. Select device/emulator
4. Press F5 to debug

### Visual Studio for Mac

1. Open `MiningTradingClientApp.csproj`
2. Select target (iOS/macOS/Android)
3. Select device/simulator
4. Click Run

## Troubleshooting

### "MAUI workload not found"

**Solution:**
```bash
dotnet workload install maui
```

### "Android SDK not found"

**Solution:**
- Install Android SDK via Visual Studio or Android Studio
- Set `ANDROID_HOME` environment variable

### "Xcode not found" (macOS)

**Solution:**
```bash
# Install Xcode from Mac App Store
sudo xcode-select --switch /Applications/Xcode.app
sudo xcodebuild -license accept
```

### "Cannot build on Linux"

**Reason:** .NET MAUI workload is not available for Linux.

**Solution:** Use Windows or macOS for development.

### Build Errors

**Clean and rebuild:**
```bash
dotnet clean
dotnet restore
dotnet build -f [target-framework]
```

### Emulator Issues

**Android:**
- Ensure emulator is running before build
- Check `adb devices` to see connected devices

**iOS:**
- Open Xcode and check simulator list
- Boot simulator manually if needed

## Project Structure

```
MiningTradingClientApp/
├── App.xaml/cs              # App entry point
├── AppShell.xaml/cs         # Navigation
├── MauiProgram.cs           # Configuration
├── Models/                  # Data models
├── Services/                # Business logic
├── Views/                   # UI pages (XAML)
├── Resources/               # Assets
│   ├── AppIcon/            # App icons
│   ├── Splash/             # Splash screen
│   ├── Images/             # Images
│   ├── Fonts/              # Fonts
│   └── Styles/             # XAML styles
└── Platforms/              # Platform code
    ├── Android/
    ├── iOS/
    ├── MacCatalyst/
    └── Windows/
```

## Development Workflow

### Hot Reload

MAUI supports hot reload:
1. Run app in debug mode
2. Make changes to XAML/C#
3. Save file
4. Changes apply automatically

**Note:** Not all changes support hot reload. Some require rebuild.

### Debugging

- Set breakpoints in code-behind files
- Use Debug Console for logging
- Inspect variables in Watch window
- Use `Debug.WriteLine()` for logging

### Testing Changes

1. Make code changes
2. Save files
3. If hot reload works, see changes immediately
4. If not, stop and rebuild
5. Test on emulator/simulator
6. Test on physical device before release

## Performance Tips

### Faster Builds

1. **Use specific framework:** Don't build all platforms
   ```bash
   dotnet build -f net9.0-android  # Just Android
   ```

2. **Incremental builds:** Don't clean unless necessary

3. **Disable unused platforms:** Remove from `<TargetFrameworks>` in .csproj

### Faster Emulator

1. **Use hardware acceleration:**
   - Android: Enable HAXM/KVM
   - iOS: Included by default

2. **Use physical device** when possible

3. **Keep emulator running** between builds

## Deployment

### Android (Google Play)

1. Build release version:
   ```bash
   dotnet publish -f net9.0-android -c Release
   ```
2. Sign APK/AAB
3. Upload to Google Play Console

### iOS (App Store)

1. Build archive in Xcode
2. Submit to App Store Connect
3. Wait for review

### Windows (Microsoft Store)

1. Build MSIX package
2. Submit to Partner Center

### macOS (Mac App Store)

1. Build PKG/DMG
2. Submit to App Store Connect

## CI/CD Considerations

### GitHub Actions

**⚠️ Important:** Linux runners cannot build MAUI apps.

**Solutions:**
1. Use Windows runners (`windows-latest`)
2. Use macOS runners (`macos-latest`) for iOS
3. Use self-hosted runners with SDK installed

**Example workflow:**
```yaml
jobs:
  build-android:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: 9.0.x
      - name: Install MAUI
        run: dotnet workload install maui
      - name: Build
        run: dotnet build src/MiningTradingClientApp -f net9.0-android
```

## Getting Help

### Resources

- [.NET MAUI Documentation](https://docs.microsoft.com/dotnet/maui/)
- [MAUI GitHub](https://github.com/dotnet/maui)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/.net-maui)

### Common Commands

```bash
# Check installed workloads
dotnet workload list

# Update workloads
dotnet workload update

# Uninstall workload
dotnet workload uninstall maui

# List available SDKs
dotnet --list-sdks

# Check project info
dotnet msbuild -getProperty:TargetFrameworks
```

## Next Steps

1. ✅ Install prerequisites
2. ✅ Build for your platform
3. ✅ Run on emulator
4. ✅ Test on device
5. 🔄 Start developing!

---

**Need Help?** Check the README.md and CONVERSION_NOTES.md for more information.
