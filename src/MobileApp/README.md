# Minerals Trading Mobile App

A .NET MAUI Blazor Hybrid mobile application for trading minerals on the Zambia Metal Exchange, specifically designed for buyers.

## Overview

This mobile application provides a comprehensive platform for buyers to:
- Register and manage buyer profiles
- Complete KYC (Know Your Customer) processes
- Browse available minerals
- Execute trades
- Track warehouse operations
- Monitor settlements and inspections
- View trading dashboards and analytics

## Technology Stack

- **.NET MAUI 9.0**: Cross-platform mobile framework
- **Blazor Hybrid**: Reuses existing Razor components from Platform.Trading.Management
- **Radzen Blazor**: UI component library
- **C# 12**: Programming language

## Supported Platforms

- **Android**: API 24 (Android 7.0) and above
- **iOS**: iOS 14.2 and above
- **macOS**: macOS 14.0 and above (via Mac Catalyst)
- **Windows**: Windows 10 version 1809 and above

## Project Structure

```
MobileApp/
├── Components/
│   ├── Layout/          # Layout components (MainLayout, NavMenu)
│   ├── Pages/           # Page components (Home, Buyers, Trades, etc.)
│   └── Routes.razor     # Routing configuration
├── Platforms/           # Platform-specific code
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
├── Resources/           # App resources (icons, splash screens, fonts)
├── wwwroot/            # Static web assets
├── App.xaml            # Application definition
├── MainPage.xaml       # Main page with BlazorWebView
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
- **Microsoft.AspNetCore.Components.WebView.Maui**: 9.0.10
- **Radzen.Blazor**: 5.9.0
- **Platform.Trading.Management**: Local project reference

## Component Reuse

This mobile app reuses existing Razor components from the `Platform.Trading.Management` project:
- BuyerManagement.razor
- TradeManagement.razor
- TradingDashboard.razor
- WarehouseManagement.razor
- WarrantManagement.razor
- InspectionManagement.razor
- SettlementManagement.razor

All dialogs and data models are also shared, ensuring consistency across web and mobile platforms.

## Development Notes

- The app uses BlazorWebView to host Blazor components in a native mobile shell
- Navigation is handled through Blazor Router
- UI styling uses Radzen Blazor components and Bootstrap
- Platform-specific features can be accessed through MAUI APIs

## Future Enhancements

- Offline support
- Push notifications for trade updates
- Biometric authentication
- QR code scanning for warehouse operations
- Advanced charting and analytics
- Multi-language support

## License

[Add your license information here]

## Support

For issues and questions, please contact the development team or create an issue in the repository.
