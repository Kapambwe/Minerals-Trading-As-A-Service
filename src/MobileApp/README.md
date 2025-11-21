# Minerals Trading Mobile App

A .NET MAUI native mobile application for trading minerals on the Zambia Metal Exchange, specifically designed for buyers.

## Overview

This mobile application provides a comprehensive native platform for buyers to:
- Register and manage buyer profiles
- Complete KYC (Know Your Customer) processes
- Browse available minerals
- Execute trades
- Track warehouse operations
- Monitor settlements and inspections
- View trading dashboards and analytics

## Technology Stack

- **.NET MAUI 9.0**: Cross-platform mobile framework with native UI
- **XAML**: Native markup for UI definition
- **C# 12**: Programming language
- **Shell Navigation**: Native navigation pattern

## Supported Platforms

- **Android**: API 24 (Android 7.0) and above
- **iOS**: iOS 14.2 and above
- **macOS**: macOS 14.0 and above (via Mac Catalyst)
- **Windows**: Windows 10 version 1809 and above

## Project Structure

```
MobileApp/
├── Pages/               # Native XAML pages
│   ├── HomePage.xaml
│   ├── BuyersPage.xaml
│   ├── TradesPage.xaml
│   ├── DashboardPage.xaml
│   ├── WarehousesPage.xaml
│   ├── WarrantsPage.xaml
│   ├── InspectionsPage.xaml
│   └── SettlementsPage.xaml
├── Platforms/           # Platform-specific code
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
├── Resources/           # App resources (icons, splash screens, fonts)
├── App.xaml            # Application definition
├── AppShell.xaml       # Shell navigation structure
└── MauiProgram.cs      # App configuration and services
```

## Key Features

### Buyer Management
- Register new buyers
- Update buyer information
- Manage KYC documentation
- Track approval status

### Trade Execution
- View available minerals
- Execute buy orders
- Track trade status
- Monitor trade history

### Warehouse Operations
- View warehouse inventory
- Track mineral storage locations
- Monitor warehouse capacity

### Settlements & Inspections
- View settlement records
- Track inspection results
- Monitor compliance

### Dashboard
- Real-time market data
- Trading analytics
- Performance metrics

## Architecture

This app uses **Native .NET MAUI** with XAML pages, providing:
- Native performance with platform-specific controls
- Direct access to platform APIs
- Optimized UI for each platform
- Shell-based navigation

## Building the Application

### Prerequisites

1. **.NET 9.0 SDK** or later
2. **.NET MAUI workload**:
   ```bash
   dotnet workload install maui
   ```

### Platform-Specific Requirements

#### Android
- Android SDK (API 24+)
- Android Emulator or physical device

#### iOS/macOS
- macOS with Xcode 14+
- iOS Simulator or physical device

#### Windows
- Windows 10/11 with Visual Studio 2022
- Windows App SDK

### Build Commands

```bash
# Build for Android
dotnet build -f net9.0-android

# Build for iOS
dotnet build -f net9.0-ios

# Build for Mac Catalyst
dotnet build -f net9.0-maccatalyst

# Build for Windows
dotnet build -f net9.0-windows10.0.19041.0
```

### Run Commands

```bash
# Run on Android
dotnet run -f net9.0-android

# Run on iOS
dotnet run -f net9.0-ios

# Run on Mac Catalyst
dotnet run -f net9.0-maccatalyst

# Run on Windows
dotnet run -f net9.0-windows10.0.19041.0
```

## Configuration

The app uses mock services for development. To connect to a real backend:

1. Update service registrations in `MauiProgram.cs`
2. Replace mock services with HTTP-based services
3. Configure API endpoints in app settings

## Dependencies

- **Microsoft.Maui.Controls**: 9.0.10
- **Microsoft.Maui.Controls.Compatibility**: 9.0.10
- **Microsoft.Extensions.Logging.Debug**: 9.0.0
- **Platform.Trading.Management**: Local project reference

## Navigation

The app uses **Shell Navigation** with a flyout menu providing access to:
- Home
- Buyers
- Trades
- Dashboard
- Warehouses
- Warrants
- Inspections
- Settlements

## Development Notes

- Native XAML pages for optimal performance
- Shell-based navigation pattern
- Platform-specific UI optimizations available
- Direct access to native platform features

## Future Enhancements

- Full native UI implementation for each page
- Offline support with local database
- Push notifications for trade updates
- Biometric authentication
- QR code scanning for warehouse operations
- Advanced charting and analytics
- Multi-language support

## Conversion from Blazor Hybrid

This app was converted from a Blazor Hybrid architecture to Native MAUI for:
- Better performance
- Native platform controls
- Reduced dependencies
- Platform-specific optimizations

## License

[Add your license information here]

## Support

For issues and questions, please contact the development team or create an issue in the repository.
