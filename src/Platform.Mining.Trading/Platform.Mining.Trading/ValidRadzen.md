
- `ReadOnly` (bool) - Make input read-only

#### 19. RadzenNumeric<TValue>
Numeric input with increment/decrement buttons.

**Properties:**
- `@bind-Value` (TValue) - Two-way binding (decimal, int, double?, decimal?)
- `Name` (string) - Input name attribute
- `Placeholder` (string) - Placeholder text
- `Style` (string) - CSS styling
- `Min` (TValue) - Minimum value (e.g., `0`, `1`)
- `Max` (TValue) - Maximum value (e.g., `14`, `100`)
- `Step` (TValue) - Increment/decrement step (e.g., `1`, `0.1`, `0.01`)
- `FormatString` (string) - Number format (e.g., `"{0:N2}"`)

**Example:**
```razor
<RadzenNumeric @bind-Value="@soilAnalysis.pH"
               Min="0"
               Max="14"
               Step="0.1"
               FormatString="{0:N2}" />
```

#### 20. RadzenDropDown<TValue>
Dropdown selection list.

**Properties:**
- `@bind-Value` (TValue) - Two-way binding for selected value
- `Data` (IEnumerable) - Data source for dropdown items
- `TextProperty` (string) - Property name for display text
- `ValueProperty` (string) - Property name for value
- `Placeholder` (string) - Placeholder text
- `Style` (string) - CSS styling
- `Change` (EventCallback) - Change event handler
- `Multiple` (bool) - Allow multiple selection

**Example:**
```razor
<RadzenDropDown @bind-Value="@selectedFieldId"
                Data="@fields"
                TextProperty="Name"
                ValueProperty="Id"
                Placeholder="Select a field"
                Style="width: 100%;" />
```

#### 21. RadzenDatePicker
Date picker component.

**Properties:**
- `@bind-Value` (DateTime or DateTime?) - Two-way binding for date value
- `Name` (string) - Input name attribute
- `Label` (string) - Label text
- `Style` (string) - CSS styling

#### 22. RadzenCheckBox<TValue>
Checkbox input.

**Properties:**
- `@bind-Value` (bool) - Two-way binding for checkbox state
- `Text` (string) - Label text
- `TValue` (Type) - Generic type for value
- `Disabled` (bool) - Disable checkbox

#### 23. RadzenRadioButtonList<TValue>
Radio button group.

**Properties:**
- `@bind-Value` (TValue) - Two-way binding for selected value
- `Data` (IEnumerable) - Data source for radio options
- `TextProperty` (string) - Property for display text
- `ValueProperty` (string) - Property for value

#### 24. RadzenSlider<TValue>
Slider input component.

**Properties:**
- `@bind-Value` (TValue) - Two-way binding for slider value
- `Min` (TValue) - Minimum value (e.g., `1`, `2017`)
- `Max` (TValue) - Maximum value (e.g., `10`, `2024`)
- `Step` (TValue) - Step increment (e.g., `1`)
- `Style` (string) - CSS styling

#### 25. RadzenUpload
File upload component.

**Properties:**
- `Accept` (string) - Accepted file types (e.g., `"image/*"`)
- `Multiple` (bool) - Allow multiple file selection
- `AllowedExtensions` (string) - Allowed file extensions (e.g., `".jpg,.pdf"`, `".pdf,.docx,.mp4"`)
- `Label` (string) - Upload label text
- `Style` (string) - CSS styling

### Button & Action Components

#### 26. RadzenButton
Button component with various styles.

**Properties:**
- `Text` (string) - Button label
- `Icon` (string) - Material icon name
- `Click` (EventCallback) - Click event handler
- `ButtonType` (enum ButtonType) - Type of button
  - `ButtonType.Submit`
  - `ButtonType.Button`
- `Size` (enum ButtonSize) - Button size
  - `ButtonSize.Small`
- `Variant` (enum Variant) - Button style variant
  - `Variant.Text`
  - `Variant.Outlined`
  - `Variant.Flat`
  - `Variant.Filled`
- `ButtonStyle` (enum ButtonStyle) - Button styling
  - `ButtonStyle.Primary`
  - `ButtonStyle.Secondary`
  - `ButtonStyle.Info`
  - `ButtonStyle.Danger`
  - `ButtonStyle.Light`
  - `ButtonStyle.Success`
  - `ButtonStyle.Warning`
- `Style` (string) - CSS styling
- `Disabled` (bool) - Disable button
- `Visible` (bool) - Show/hide button
- `Tooltip` (string) - Tooltip text

**Common Icons:**
- Actions: `"add"`, `"edit"`, `"delete"`, `"save"`, `"visibility"`, `"arrow_back"`, `"refresh"`, `"search"`
- Documents: `"picture_as_pdf"`, `"grid_on"`, `"print"`, `"download"`, `"attach_file"`, `"content_copy"`
- Status: `"check_circle_outline"`, `"cancel"`, `"notifications"`
- Medical: `"medical_services"`, `"vaccines"`, `"child_care"`
- Other: `"send"`, `"gps_fixed"`, `"link"`, `"photo_camera"`, `"more_vert"`, `"repeat"`, `"assignment"`, `"show_chart"`, `"add_circle"`

**Example:**
```razor
<RadzenButton Text="Save"
              Icon="save"
              ButtonStyle="ButtonStyle.Primary"
              Click="@SaveData" />

<RadzenButton Icon="visibility"
              Size="ButtonSize.Small"
              Click="@(() => ViewDetails(item))" />
```

#### 27. RadzenBadge
Badge for status indicators.

**Properties:**
- `Text` (string) - Badge text content
- `Variant` (enum Variant) - Badge style variant
  - `Variant.Outlined`
  - `Variant.Filled`
  - `Variant.Text`
  - `Variant.Flat`
- `BadgeStyle` (enum BadgeStyle) - Badge styling
  - `BadgeStyle.Success`
  - `BadgeStyle.Warning`
  - `BadgeStyle.Danger`
  - `BadgeStyle.Info`
  - `BadgeStyle.Primary`
  - `BadgeStyle.Secondary`
- `Style` (string) - CSS styling

**Example:**
```razor
@if (status == "Completed")
{
    <RadzenBadge BadgeStyle="BadgeStyle.Success" Text="Completed" />
}
else if (status == "Pending")
{
    <RadzenBadge BadgeStyle="BadgeStyle.Warning" Text="Pending" />
}
else
{
    <RadzenBadge BadgeStyle="BadgeStyle.Danger" Text="Overdue" />
}
```

### Form Components

#### 28. RadzenTemplateForm<TItem>
Form container with validation support.

**Properties:**
- `TItem` (Type) - Generic type for form data
- `Data` (TItem) - Form data object
- `Submit` (EventCallback<TItem>) - Submit event handler
- `Visible` (bool) - Show/hide form

**Example:**
```razor
<RadzenTemplateForm TItem="Farmer" Data="@farmer" Submit="@OnSubmit">
    <RadzenTextBox @bind-Value="@farmer.Name" Name="Name" />
    <RadzenRequiredValidator Component="Name" Text="Name is required" />

    <RadzenButton ButtonType="ButtonType.Submit" Text="Save" />
</RadzenTemplateForm>
```

### Validation Components

#### 29. RadzenRequiredValidator
Required field validator.

**Properties:**
- `Component` (string) - Component name to validate
- `Text` (string) - Validation error message
- `Style` (string) - CSS styling

#### 30. RadzenNumericValidator
Numeric range validator.

**Properties:**
- `Component` (string) - Component name to validate
- `Min` (decimal/int) - Minimum value (e.g., `0`)
- `Max` (decimal/int) - Maximum value (e.g., `14`, `100`)
- `Text` (string) - Validation error message

**Example:**
```razor
<RadzenNumeric @bind-Value="@analysis.pH" Name="pH" Min="0" Max="14" />
<RadzenNumericValidator Component="pH" Min="0" Max="14" Text="pH must be between 0-14" />
```

#### 31. RadzenLengthValidator
String length validator.

**Properties:**
- `Component` (string) - Component name to validate
- `Min` (int) - Minimum length (e.g., `10`)
- `Text` (string) - Validation error message

### Display Components

#### 32. RadzenIcon
Material icon display.

**Properties:**
- `Icon` (string) - Material icon name
- `Style` (string) - CSS styling
- `Class` (string) - CSS class

**Common Icons:**
- Agriculture: `"agriculture"`, `"grass"`, `"nutrition"`
- Animals: `"pets"`, `"family_restroom"`, `"crib"`, `"genetics"`
- Medical: `"vaccines"`, `"coronavirus"`, `"medical_services"`
- Status: `"warning"`, `"check_circle"`, `"info"`
- Science: `"science"`, `"analytics"`, `"terrain"`
- Actions: `"event"`, `"calendar"`, `"gps_fixed"`, `"map"`, `"pin_drop"`
- Other: `"groups"`, `"recommend"`, `"tips_and_updates"`, `"bug_report"`

**Example:**
```razor
<RadzenIcon Icon="agriculture" Style="font-size: 24px; color: #4CAF50;" />
<RadzenIcon Icon="warning" Class="text-danger" />
```

#### 33. RadzenLabel
Label component.

**Properties:**
- `Text` (string) - Label text
- `Style` (string) - CSS styling

#### 34. RadzenText
Text display component.

**Properties:**
- `Style` (string) - CSS styling

#### 35. RadzenProgressBar
Loading progress indicator.

**Example:**
```razor
@if (loading)
{
    <RadzenProgressBar />
}
```

#### 36. RadzenAlert
Alert message component.

**Properties:**
- `Text` (string) - Alert message
- `Severity` (enum AlertSeverity) - Alert level
  - `AlertSeverity.Warning`

#### 37. RadzenDivider
Visual separator.

**Properties:**
- `Style` (string) - CSS styling

### Dialog Components

#### 38. RadzenDialog
Modal dialog component.

**Properties:**
- `@bind-Visible` (bool) - Two-way binding for dialog visibility
- `Style` (string) - CSS styling (e.g., `"width: 800px;"`)

### Grid Layout Components

#### 39. RadzenRow
Bootstrap-style row container.

#### 40. RadzenColumn
Bootstrap-style column.

**Properties:**
- `Size` (int) - Column size (Bootstrap grid: `3`, `4`, `6`, `12`)

**Example:**
```razor
<div class="row">
    <div class="col-md-6">
        <RadzenCard>
            <!-- Content -->
        </RadzenCard>
    </div>
    <div class="col-md-6">
        <RadzenCard>
            <!-- Content -->
        </RadzenCard>
    </div>
</div>
```

## Common Property Patterns

### Data Binding
- **@bind-Value**: Two-way data binding on input components
- **Data**: Data source for grids, dropdowns, charts, lists

### Styling
- **Style**: Inline CSS styling (e.g., `"width: 100%"`, `"margin: 20px;"`)
- **Class**: CSS class names

### Events
- **Click**: Button click event handlers
- **Change**: Value change event handlers
- **Submit**: Form submission handlers

### Generic Types
- **TItem**: Generic type parameter for strongly-typed components
- **TValue**: Generic type for value binding

### Display Properties
- **Property/TextProperty/ValueProperty**: Property name binding for data display
- **FormatString**: Display format for numbers, dates, percentages
- **Title/Text/Label**: Display text content