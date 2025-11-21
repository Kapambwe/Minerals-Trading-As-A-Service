# Order Placement Feature - Usage Guide

## Overview

The Order Placement feature provides a dedicated mobile interface for buyers to browse available minerals and place orders directly from the mobile app. This feature is built using .NET MAUI XAML and provides a native mobile experience across all supported platforms.

## Accessing the Order Placement Page

### Method 1: From Home Page (Quick Access)
1. Launch the Minerals Trading Mobile App
2. On the home page, tap the prominent **"🛒 Place Mineral Order"** button
3. You'll be taken directly to the Order Placement page

### Method 2: From Navigation Menu
1. Open the app's navigation menu (flyout)
   - Swipe from left edge of screen, or
   - Tap the hamburger menu icon (≡) in the top-left corner
2. Tap **"Place Order"** from the menu
3. You'll be taken to the Order Placement page

## How to Place an Order

### Step 1: Browse Available Minerals

When you open the Order Placement page, you'll see a list of all available minerals. Each listing shows:

- **Seller Company Name**: The company selling the mineral
- **Metal Type**: Type of mineral (Copper, Gold, Cobalt, etc.)
- **Quality Grade**: Quality specification (e.g., "Grade A Cathodes", "99.99% Pure Gold")
- **Available Quantity**: Amount available in Metric Tons (MT)
- **Price per MT**: Cost per metric ton in USD
- **Notes**: Additional information about the mineral

**Example Listing:**
```
Konkola Copper Mines PLC                    Copper
Quality: Grade A Cathodes
Available: 1000 MT                          $8,800.00/MT
High purity copper cathodes, ready for immediate shipment.
```

### Step 2: Select a Mineral

1. Scroll through the available minerals
2. Tap on the mineral you want to order
3. The order form will appear below

### Step 3: Enter Order Details

Once you've selected a mineral, fill in the order form:

#### 3a. Order Quantity
- **Manual Entry**: Type the quantity in the text field
- **Increment/Decrement**: Use the + and - buttons to adjust quantity
- The system will automatically validate that your quantity doesn't exceed available stock
- You'll see "Maximum available: X MT" to help you stay within limits

#### 3b. Buyer Company Name
- Enter your company name in the text field
- This is required to place the order

#### 3c. Preferred Delivery Date
- Tap the date picker to select your preferred delivery date
- The default is set to 30 days from today
- You can only select future dates

#### 3d. Order Notes (Optional)
- Add any special instructions or notes in the text area
- Examples: "Urgent delivery", "Split shipment", "Certificate required"

### Step 4: Review Your Order

As you enter the quantity, you'll see a real-time calculation of:

**Order Summary**
```
Total: $X,XXX,XXX.XX
```

This shows the total cost (Quantity × Price per MT)

### Step 5: Place the Order

1. Review all your order details
2. Tap the **"Place Order"** button (green button on the right)
3. A confirmation dialog will appear showing:
   - Quantity
   - Metal type
   - Price per MT
   - Total cost
4. Tap **"Yes, Place Order"** to confirm or **"Cancel"** to go back

### Step 6: Order Confirmation

After successfully placing your order:
- You'll see a success message with your Order Number (e.g., "ORD20240121143022")
- The total value will be displayed
- You'll be automatically redirected to the Trades page to track your order

## Order Form Actions

### Clear Button
- Tap **"Clear"** (gray button on the left) to reset the form
- This will:
  - Clear the quantity field
  - Clear the buyer name
  - Clear order notes
  - Reset delivery date to default
  - Deselect the current mineral

### Quantity Controls
- **+ Button**: Increases quantity by 1 MT (up to available maximum)
- **- Button**: Decreases quantity by 1 MT (minimum 0)
- **Direct Entry**: Type any valid number in the quantity field

## Validation & Error Handling

The order system includes several validations:

### Quantity Validation
- ❌ **Error**: "Please enter a valid quantity"
  - **Solution**: Enter a quantity greater than 0
  
- ❌ **Error**: "Quantity exceeds available amount (X MT)"
  - **Solution**: Reduce your order quantity to match available stock

### Buyer Information Validation
- ❌ **Error**: "Please enter your company name"
  - **Solution**: Fill in the Buyer Company Name field

### Mineral Selection Validation
- ❌ **Error**: "Please select a mineral first"
  - **Solution**: Select a mineral from the available listings

## Order Status Tracking

After placing an order:
1. Go to the **Trades** page (from navigation menu)
2. Find your order using the Order Number
3. Track the status of your order:
   - **Pending**: Order submitted, awaiting processing
   - **Confirmed**: Order confirmed by seller
   - **In Transit**: Minerals are being shipped
   - **Delivered**: Order completed

## Available Minerals (Sample Data)

The mobile app comes with sample mineral listings for testing:

### Listing 1: Copper Cathodes
- **Seller**: Konkola Copper Mines PLC
- **Type**: Copper
- **Grade**: Grade A Cathodes
- **Available**: 1,000 MT
- **Price**: $8,800.00/MT

### Listing 2: Pure Gold
- **Seller**: Kansanshi Mining PLC
- **Type**: Gold
- **Grade**: 99.99% Pure Gold
- **Available**: 50 MT
- **Price**: $65,000,000.00/MT

### Listing 3: Cobalt Hydroxide
- **Seller**: Konkola Copper Mines PLC
- **Type**: Cobalt
- **Grade**: Cobalt Hydroxide
- **Available**: 200 MT
- **Price**: $30,000.00/MT

### Listing 4: Copper Concentrates
- **Seller**: Mufumbwe Small Scale Miners Cooperative
- **Type**: Copper
- **Grade**: Copper Concentrates
- **Available**: 50 MT
- **Price**: $8,750.00/MT

## Tips for Best Experience

### 1. Check Available Quantity
Always verify the available quantity before entering your order quantity to avoid validation errors.

### 2. Use Quick Controls
Use the +/- buttons for quick quantity adjustments, especially on mobile devices where typing can be slower.

### 3. Save Order Numbers
Take note of your order number after placing an order. You'll need it to track your order status.

### 4. Plan Delivery Dates
Consider processing and shipping times when selecting your delivery date. The default 30-day period is recommended.

### 5. Add Detailed Notes
Use the notes field to communicate any special requirements to the seller.

## Technical Details

### Service Integration
The Order Placement page integrates with:
- **IMineralListingService**: Fetches available minerals
- **ITradeService**: Creates trade records from orders

### Data Flow
1. User selects mineral → MineralListing data loaded
2. User fills form → Validation performed client-side
3. User confirms → Trade object created
4. Trade submitted → ITradeService.CreateTradeAsync()
5. Success → Navigate to Trades page

### Order to Trade Mapping
When you place an order, it creates a Trade with:
- **Trade Number**: Auto-generated (ORDYYYYMMDDHHmmss)
- **Status**: Set to "Pending"
- **Buyer Name**: From your input
- **Seller Name**: From selected mineral listing
- **Metal Type**: From selected mineral
- **Quantity**: From your input
- **Price/Total**: Calculated from mineral pricing
- **Delivery Date**: From your selection

## Troubleshooting

### Issue: Minerals not loading
**Symptoms**: "Loading available minerals..." message doesn't disappear
**Solution**: 
- Check network connection (if using live backend)
- Restart the app
- Verify mock services are registered in MauiProgram.cs

### Issue: Order form doesn't appear
**Symptoms**: Tapping a mineral doesn't show the order form
**Solution**:
- Ensure you tapped on the mineral listing card (not just anywhere)
- Try selecting a different mineral
- Restart the app if issue persists

### Issue: Order button not working
**Symptoms**: Nothing happens when tapping "Place Order"
**Solution**:
- Check that all required fields are filled (quantity, buyer name)
- Verify quantity is within available limits
- Check app logs for error messages

## Future Enhancements

Planned improvements for the order placement feature:
- [ ] Order history view
- [ ] Saved buyer profiles
- [ ] Favorite minerals
- [ ] Price alerts
- [ ] Bulk ordering
- [ ] Order templates
- [ ] Multi-currency support
- [ ] Real-time inventory updates
- [ ] Push notifications for order status

## Support

For assistance with the Order Placement feature:
1. Check this guide first
2. Review the README.md for general app information
3. See GETTING_STARTED.md for setup instructions
4. Contact support or create an issue in the repository

---

**Enjoy seamless mineral ordering with our mobile app! 🛒📱**
