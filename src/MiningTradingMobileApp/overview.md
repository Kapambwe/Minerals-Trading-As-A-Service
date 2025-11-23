# eShop ClientApp - Project Overview

## Project Description

This is a .NET MAUI (Multi-platform App UI) client application for the eShop system. It's a cross-platform mobile and desktop application that provides an e-commerce shopping experience for the AdventureWorks brand, featuring catalog browsing, shopping cart functionality, order management, and user authentication.

## Technology Stack

- **.NET MAUI** - Cross-platform framework
- **C# 12** - Primary programming language
- **XAML** - UI markup language
- **MVVM Pattern** - Model-View-ViewModel architecture
- **CommunityToolkit.Maui** - Enhanced MAUI controls and utilities
- **CommunityToolkit.Mvvm** - MVVM helpers and source generators
- **gRPC** - For backend communication
- **IdentityModel.OidcClient** - OAuth/OpenID Connect authentication

## Project Structure

### Root Files

- **App.xaml / App.xaml.cs** - Application entry point and global resources
- **AppShell.xaml / AppShell.xaml.cs** - Shell navigation structure with tab bar
- **MauiProgram.cs** - Dependency injection and service registration
- **AppActions.cs** - App shortcuts and actions
- **ClientApp.csproj** - Project configuration file
- **GlobalUsings.cs** - Global using statements
- **GlobalSuppressions.cs** - Code analysis suppressions

### Folder Structure

#### **/Animations**
Contains custom animation classes for UI transitions:
- **Base/**
  - `AnimationBase.cs` - Base animation class
  - `EasingType.cs` - Easing function types
- `FadeToAnimation.cs` - Fade transition animations
- `StoryBoard.cs` - Animation sequencing

#### **/Controls**
Custom reusable MAUI controls:
- `AddBasketButton.xaml/.cs` - Custom button for adding items to cart
- `CustomTabbedPage.cs` - Custom tabbed page implementation
- `ToggleButton.cs` - Toggle button control

#### **/Converters**
Value converters for data binding:
- `DoesNotHaveCountConverter.cs` - Checks if collection has no items
- `DoubleConverter.cs` - Double value conversion
- `FirstValidationErrorConverter.cs` - Extracts first validation error
- `HasCountConverter.cs` - Checks if collection has items
- `ItemsToHeightConverter.cs` - Calculates height based on item count
- `WebNavigatedEventArgsConverter.cs` - Web navigation event converter
- `WebNavigatingEventArgsConverter.cs` - Web navigating event converter

#### **/Effects**
Platform-specific visual effects (if any)

#### **/Exceptions**
Custom exception classes for the application

#### **/Extensions**
Extension methods for various types

#### **/Helpers**
Utility and helper classes

#### **/Messages**
Message classes for communication between components

#### **/Models**
Data models organized by domain:

**Basket:**
- `BasketItem.cs` - Shopping cart item model
- `CustomerBasket.cs` - Customer shopping basket model

**Catalog:**
- `CatalogBrand.cs` - Product brand model
- `CatalogItem.cs` - Product item model
- `CatalogRoot.cs` - Catalog root container
- `CatalogType.cs` - Product type/category model

**Campaign:**
- `Campaign.cs` - Marketing campaign model
- `CampaignItem.cs` - Campaign item model
- `CampaignRoot.cs` - Campaign root container

**Location:**
- `Location.cs` - Location model
- `Position.cs` - Geographic position
- `GeolocationError.cs` - Geolocation error handling
- `GeolocationException.cs` - Geolocation exceptions

**Orders:**
- `Order.cs` - Order model
- `OrderItem.cs` - Order line item
- `OrderCheckout.cs` - Checkout data
- `CardType.cs` - Payment card types
- `CancelOrderCommand.cs` - Cancel order command

**User:**
- `UserInfo.cs` - User information
- `UserToken.cs` - Authentication token
- `Address.cs` - User address
- `PaymentInfo.cs` - Payment information
- `LogoutParameter.cs` - Logout parameters

**Other:**
- `Permission.cs` - Permission model
- `PermissionStatus.cs` - Permission status
- `TabParameter.cs` - Tab navigation parameters

#### **/Platforms**
Platform-specific code for Android, iOS, MacCatalyst, and Windows

#### **/Properties**
Assembly properties and configurations

#### **/Resources**
Application resources:

**Fonts:**
- FontAwesome icons (Regular and Solid)
- Montserrat font family (Bold and Regular)

**Images:**
- Product images
- Icons and UI elements
- Brand assets

**Styles:**
- `Colors.xaml` - Color definitions and themes
- `Styles.xaml` - Reusable UI styles

#### **/Services**
Service layer implementing business logic:

**App Environment:**
- `IAppEnvironmentService.cs` / `AppEnvironmentService.cs` - Environment configuration

**Basket:**
- `IBasketService.cs` - Basket service interface
- `BasketService.cs` - Production basket service (gRPC)
- `BasketMockService.cs` - Mock basket service for testing
- `Basket.cs` / `BasketGrpc.cs` - gRPC generated code

**Catalog:**
- `ICatalogService.cs` - Catalog service interface
- `CatalogService.cs` - Production catalog service
- `CatalogMockService.cs` - Mock catalog service

**Identity:**
- `IIdentityService.cs` - Identity service interface
- `IdentityService.cs` - OAuth/OIDC authentication service
- `IdentityMockService.cs` - Mock identity service
- `AuthorizeRequest.cs` - Authorization request model

**Location:**
- `ILocationService.cs` / `LocationService.cs` - Geolocation services

**Navigation:**
- `INavigationService.cs` / `MauiNavigationService.cs` - Page navigation

**Orders:**
- `IOrderService.cs` - Order service interface
- `OrderService.cs` - Production order service
- `OrderMockService.cs` - Mock order service

**Request Provider:**
- `IRequestProvider.cs` / `RequestProvider.cs` - HTTP client wrapper
- `HttpRequestExceptionEx.cs` - HTTP exception handling

**Settings:**
- `ISettingsService.cs` / `SettingsService.cs` - App settings management

**Theme:**
- `ITheme.cs` / `Theme.shared.cs` - Theme management

**Dialog:**
- `IDialogService.cs` / `DialogService.cs` - Dialog/alert services

**Other:**
- `IFixUriService.cs` / `FixUriService.cs` - URI resolution
- `IOpenUrlService.cs` / `OpenUrlService.cs` - External URL handling
- `EShopJsonSerializerContext.cs` - JSON serialization context
- `Common.cs` - Common service utilities

#### **/Triggers**
XAML triggers for UI behavior:
- Custom trigger actions for animations and events

#### **/Validations**
Validation rules and validators for user input

#### **/ViewModels**
View models implementing MVVM pattern:
- `MainViewModel.cs` - Main application view model
- `LoginViewModel.cs` - Login page view model
- `BasketViewModel.cs` - Shopping cart view model
- `CatalogViewModel.cs` - Product catalog view model
- `CatalogItemViewModel.cs` - Individual product view model
- `CheckoutViewModel.cs` - Checkout process view model
- `MapViewModel.cs` - Map/location view model
- `ProfileViewModel.cs` - User profile and orders view model
- `OrderDetailViewModel.cs` - Order details view model
- `SettingsViewModel.cs` - App settings view model
- `SelectionViewModel.cs` - Generic selection view model
- `ObservableCollectionEx.cs` - Extended observable collection
- **Base/**
  - `IViewModelBase.cs` - View model interface
  - `ViewModelBase.cs` - Base view model class

#### **/Views**
XAML views (pages):
- `LoginView.xaml/.cs` - Login and authentication page
- `CatalogView.xaml/.cs` - Product catalog listing
- `CatalogItemView.xaml/.cs` - Product detail page
- `BasketView.xaml/.cs` - Shopping cart page
- `CheckoutView.xaml/.cs` - Checkout page
- `ProfileView.xaml/.cs` - User profile and orders
- `OrderDetailView.xaml/.cs` - Order details page
- `MapView.xaml/.cs` - Location/map view
- `SettingsView.xaml/.cs` - App settings page
- `FiltersView.xaml/.cs` - Product filter options
- `CustomNavigationView.xaml/.cs` - Custom navigation
- `ContentPageBase.cs` - Base page class
- `BadgeView.cs` - Badge UI element
- `MauiAuthenticationBrowser.cs` - OAuth browser implementation

**Templates/** - Data templates for list items:
- `ProductTemplate.xaml/.cs` - Product list item template
- `BasketItemTemplate.xaml/.cs` - Cart item template
- `OrderTemplate.xaml/.cs` - Order list item template
- `OrderItemTemplate.xaml/.cs` - Order detail item template
- `CampaignTemplate.xaml/.cs` - Campaign item template

## MAUI XAML Components Used

### Layout Components

- **Grid** - Primary layout container with rows and columns
- **StackLayout** / **VerticalStackLayout** / **HorizontalStackLayout** - Stack-based layouts
- **FlexLayout** - Flexible box layout for responsive designs
- **ScrollView** - Scrollable content container
- **Border** - Container with border and rounded corners
- **ContentView** - Reusable content container

### Navigation Components

- **Shell** - Application shell with built-in navigation
- **TabBar** - Bottom tab navigation
- **FlyoutItem** - Navigation flyout items
- **ShellContent** - Shell content pages
- **BackButtonBehavior** - Back button customization

### Collection Components

- **CollectionView** - Efficient data-bound list display with selection
- **BindableLayout** - Dynamic item generation in layouts

### Input Components

- **Button** - Standard button control
- **ImageButton** - Image-based button
- **Entry** - Text input field (likely in checkout/login forms)

### Display Components

- **Label** - Text display
- **Image** - Image display with various aspect modes
- **BoxView** - Colored rectangular box
- **Ellipse** - Circular/elliptical shape
- **ActivityIndicator** - Loading spinner

### Data Binding Features

- **DataTemplate** - Templates for data items
- **ResourceDictionary** - Resource definitions
- **Style** - Reusable styling
- **Setter** - Property setters in styles
- **Trigger** / **DataTrigger** / **EventTrigger** - Conditional UI changes
- **VisualStateManager** - Visual state management
- **Binding** - Data binding expressions
- **StaticResource** / **DynamicResource** - Resource references
- **x:DataType** - Compiled bindings for performance

### CommunityToolkit.Maui Components

- **Expander** - Expandable/collapsible content (used in filters)
- **StatusBarBehavior** - Status bar customization

### Platform-Specific Features

- **OnPlatform** - Platform-specific values
- **OnIdiom** - Device idiom-specific values
- **Maps** - Map controls (iOS, Android, MacCatalyst)

### Custom Components

- **AddBasketButton** - Custom add to cart button
- **CustomTabbedPage** - Custom tabbed navigation
- **ToggleButton** - Toggle switch button
- **ContentPageBase** - Custom base page with common functionality

## Architecture Pattern

The application follows the **MVVM (Model-View-ViewModel)** pattern:

1. **Models** - Data structures representing business entities
2. **Views** - XAML-based UI pages and controls
3. **ViewModels** - Presentation logic and data binding intermediary

### Key Features

- **Dependency Injection** - All services registered in `MauiProgram.cs`
- **Service Abstraction** - Interface-based services with mock implementations for testing
- **Navigation Service** - Centralized navigation management
- **Settings Service** - Persistent application settings
- **Identity Service** - OAuth/OIDC authentication
- **gRPC Communication** - Efficient backend communication for basket operations
- **REST API** - HTTP-based services for catalog and orders
- **Animation System** - Custom animation framework
- **Value Converters** - Data transformation for bindings
- **Validation** - Input validation framework
- **Theming** - Dark/light mode support

## Supported Platforms

- **Android**
- **iOS**
- **MacCatalyst**
- **Windows**

## Key NuGet Packages

- `Microsoft.Maui.Controls` (v9.0.30) - Core MAUI framework
- `Microsoft.Maui.Controls.Maps` (v9.0.30) - Map support
- `CommunityToolkit.Maui` (v9.1.1) - Additional MAUI controls
- `CommunityToolkit.Mvvm` (v8.3.2) - MVVM helpers
- `IdentityModel.OidcClient` (v6.0.0) - OAuth/OIDC authentication
- `Grpc.Net.Client` (v2.67.0) - gRPC client
- `Google.Protobuf` (v3.29.3) - Protocol buffers
- `Microsoft.Extensions.Logging.Debug` (v9.0.0) - Debug logging

## Development Features

- **Mock Services** - Toggle between real and mock services for development
- **Debug Handlers** - Certificate validation bypass for local development
- **Logging** - Debug logging enabled in development builds
- **Hot Reload** - MAUI hot reload support for rapid development

## Application Flow

1. **Login** - User authentication via OAuth/OIDC or mock mode
2. **Catalog** - Browse products with filters by brand and type
3. **Product Details** - View individual product information
4. **Basket** - Add products to shopping cart
5. **Checkout** - Complete purchase with shipping and payment info
6. **Orders** - View order history in profile
7. **Map** - View store locations (mobile platforms)
8. **Settings** - Configure app preferences

## Notable Implementation Details

- Shell-based navigation with tab bar for main sections
- Custom animations for smooth page transitions
- Responsive design using FlexLayout and adaptive layouts
- Platform-specific code handling for iOS, Android, MacCatalyst, and Windows
- Compiled bindings (x:DataType) for improved performance
- Visual state management for interactive UI elements
- Custom font integration (FontAwesome, Montserrat)
- Theme support with light/dark mode
