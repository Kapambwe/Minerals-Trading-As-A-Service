# MiningTradingClientApp - .NET MAUI Application

## Overview

This application has been converted from a Blazor WebAssembly application to a native .NET MAUI (Multi-platform App UI) application with a **Robinhood-inspired trading interface**.

## What Changed

### From Blazor WebAssembly to .NET MAUI

- **Previous**: Blazor WebAssembly web application
- **Current**: Native cross-platform mobile/desktop application using .NET MAUI

### Key Features

#### 🎨 Robinhood-Style UI Design
- **Dark Theme**: Modern dark color scheme with green accents
- **Card-Based Layout**: Clean, minimalist card designs for mineral listings
- **Interactive Price Display**: Prominent pricing with positive/negative indicators
- **Smooth Navigation**: Bottom tab bar navigation similar to Robinhood mobile app
- **Touch-Friendly**: Large buttons and touch targets optimized for mobile

#### 📱 Platform Support
- **Android**: API 24+ (Android 7.0+)
- **iOS**: iOS 14.2+
- **macOS**: macOS 14.0+ (Mac Catalyst)
- **Windows**: Windows 10 1809+

#### 🎯 Application Features

1. **Home Page**
   - Portfolio summary with value and daily change
   - Quick action cards for navigation
   - Featured/trending minerals
   - About section

2. **Markets Page (Minerals Listing)**
   - Search functionality
   - Card-based mineral listings
   - Verification badges
   - Detailed mineral information
   - Direct "Buy Now" buttons

3. **Mineral Detail Page**
   - Large header image
   - Current price display
   - Detailed mineral information
   - Origin, seller, and weight details
   - Buy button

4. **Order Placement Page**
   - Order summary
   - Buyer information form
   - Quantity input with real-time total calculation
   - Confirm order button

5. **Order Confirmation Page**
   - Success confirmation
   - Order details summary
   - Navigation to markets or portfolio

6. **Portfolio Page**
   - Total portfolio value with change indicators
   - Holdings list with performance
   - Recent order history
   - Browse markets button

## Color Scheme (Robinhood-Inspired)

```
Primary Green:     #00C853
Dark Green:        #00A843
Red (Negative):    #FF3B30
Background Dark:   #1C1C1E
Background Medium: #2C2C2E
Background Light:  #3A3A3C
Text Primary:      #FFFFFF
Text Secondary:    #98989F
```

## Project Structure

```
MiningTradingClientApp/
├── App.xaml / App.xaml.cs           # Main application entry point
├── AppShell.xaml / AppShell.xaml.cs # Navigation shell with bottom tabs
├── MauiProgram.cs                   # App configuration and service registration
├── Models/                          # Data models
│   ├── Mineral.cs
│   └── OrderTracking.cs
├── Services/                        # Business logic services
│   ├── IMineralService.cs
│   ├── MockMineralService.cs
│   └── HttpMineralService.cs
├── Views/                           # XAML pages
│   ├── HomePage.xaml/cs
│   ├── MineralsPage.xaml/cs
│   ├── MineralDetailPage.xaml/cs
│   ├── OrderPage.xaml/cs
│   ├── OrderConfirmationPage.xaml/cs
│   └── PortfolioPage.xaml/cs
├── Resources/                       # App resources
│   ├── AppIcon/                     # App icon SVGs
│   ├── Splash/                      # Splash screen
│   ├── Images/                      # Image assets
│   ├── Fonts/                       # Custom fonts
│   └── Styles/                      # XAML styles
│       ├── Colors.xaml              # Color definitions
│       └── Styles.xaml              # Component styles
└── Platforms/                       # Platform-specific code
    ├── Android/
    ├── iOS/
    ├── MacCatalyst/
    └── Windows/
```

## Navigation

The app uses **Shell-based navigation** with a bottom tab bar containing:

1. **Home** - Dashboard and quick actions
2. **Markets** - Browse and search minerals
3. **Portfolio** - View holdings and order history

Additional pages are accessed via routing:
- Mineral Detail: `/mineraldetail?id={guid}`
- Order: `/order?id={guid}`
- Order Confirmation: `/orderconfirmation?mineralId={guid}&quantity={value}&buyer={name}`

## Technologies

- **.NET 9.0** - Latest .NET framework
- **.NET MAUI 9.0.10** - Cross-platform UI framework
- **CommunityToolkit.Maui 9.1.0** - Additional MAUI controls and helpers
- **C# 12** - Modern C# features
- **XAML** - Declarative UI markup

## Building the Application

### Prerequisites

1. **.NET 9.0 SDK** installed
2. **.NET MAUI workload** installed:
   ```bash
   dotnet workload install maui
   ```
3. **Platform SDKs**:
   - **Android**: Android SDK via Visual Studio or Android Studio
   - **iOS/macOS**: Xcode 14+ (macOS only)
   - **Windows**: Visual Studio 2022 with Windows App SDK

### Build Commands

```bash
# Restore packages
dotnet restore

# Build for specific platform
dotnet build -f net9.0-android
dotnet build -f net9.0-ios
dotnet build -f net9.0-maccatalyst
dotnet build -f net9.0-windows10.0.19041.0

# Run on emulator/simulator (requires platform setup)
dotnet run -f net9.0-android
```

### Known Limitations

⚠️ **Cannot build on Linux**: The .NET MAUI workload is not available for Linux. You must use Windows or macOS to build and run this application.

## Design Philosophy

### Robinhood-Inspired Elements

1. **Minimalist Interface**: Clean, distraction-free design focused on content
2. **Dark Theme**: Reduces eye strain and provides modern aesthetic
3. **Green for Positive**: Uses signature green color for positive changes and CTAs
4. **Card-Based Layout**: Each item is presented in a distinct, tappable card
5. **Bottom Tab Navigation**: Easy thumb access on mobile devices
6. **Large Typography**: Clear, readable text with strong hierarchy
7. **No Clutter**: Whitespace and breathing room between elements

### User Experience

- **Fast Navigation**: Bottom tabs and quick action cards
- **Clear Information Hierarchy**: Important info (prices, names) is prominent
- **Consistent Patterns**: Similar layouts across pages for predictability
- **Touch-Friendly**: All interactive elements are large enough for easy tapping
- **Real-time Feedback**: Instant updates when quantity or selections change

## Services

### IMineralService

Interface for mineral data operations:
- `GetAvailableMineralsAsync()` - Fetch all available minerals
- `SearchMineralsAsync(string searchTerm)` - Search minerals by term
- `GetOrderTrackingAsync()` - Get order history

### MockMineralService

Provides mock data for development and testing. Includes:
- 5 sample minerals (Emerald, Copper, Amethyst, Cobalt, Gold)
- 3 sample order tracking records
- Search functionality

### HttpMineralService

Placeholder for real HTTP API integration. Can be configured to connect to a backend service.

## Customization

### Changing Colors

Edit `/Resources/Styles/Colors.xaml` to change the color scheme:

```xml
<Color x:Key="RobinhoodGreen">#00C853</Color>
<Color x:Key="AccentColor">#00C853</Color>
<!-- Add or modify colors as needed -->
```

### Adding New Pages

1. Create XAML page in `/Views/`
2. Register in `MauiProgram.cs`:
   ```csharp
   builder.Services.AddTransient<YourPage>();
   ```
3. Register route in `AppShell.xaml.cs`:
   ```csharp
   Routing.RegisterRoute("yourpage", typeof(YourPage));
   ```

### Custom Fonts

1. Add font files to `/Resources/Fonts/`
2. Register in `MauiProgram.cs`:
   ```csharp
   fonts.AddFont("YourFont.ttf", "YourFontAlias");
   ```
3. Use in XAML:
   ```xml
   <Label FontFamily="YourFontAlias" />
   ```

## Testing

### Emulator/Simulator Testing

1. **Android Emulator**: Use Android Studio AVD Manager
2. **iOS Simulator**: Use Xcode Simulator (macOS only)
3. **Windows**: Run directly on Windows 10/11

### Device Testing

Connect physical device and run:
```bash
dotnet run -f net9.0-android
# or for iOS
dotnet run -f net9.0-ios
```

## Next Steps

### For Production

1. **Backend Integration**
   - Replace `MockMineralService` with `HttpMineralService`
   - Configure API endpoints
   - Add authentication

2. **Additional Features**
   - User authentication and registration
   - Real-time price updates
   - Push notifications
   - Offline support with local database
   - Payment integration
   - Order tracking with status updates

3. **Polish**
   - Add loading indicators
   - Improve error handling
   - Add data validation
   - Implement caching
   - Add analytics

4. **Store Deployment**
   - Google Play Store (Android)
   - Apple App Store (iOS)
   - Microsoft Store (Windows)

## Screenshots

(Screenshots would show the Robinhood-style dark theme with green accents, bottom tab navigation, and card-based mineral listings)

## License

[Your License Here]

## Support

For issues or questions, please refer to the main repository documentation.

---

**Note**: This is a native mobile/desktop application. It does not run in a web browser. Use the appropriate platform emulator or physical device for testing.
