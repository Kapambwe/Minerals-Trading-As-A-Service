# Quick Start Guide - Minerals Trading Microservice

## Prerequisites

- .NET 10.0 SDK installed
- Visual Studio 2022 or VS Code (optional)
- REST Client (for testing HTTP files)

## Getting Started

### 1. Navigate to the Project Directory

```bash
cd C:\Dev\Minerals-Trading-As-A-Service\src\Services\Minerals.Trading.Service
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

Expected output: `Build succeeded`

### 4. Run Tests (Optional but Recommended)

```bash
dotnet test
```

Expected output: `Test summary: total: 26, failed: 0, succeeded: 26`

### 5. Run the API

```bash
dotnet run --project Minerals.Trading.Service.Api
```

The API will start on: **http://localhost:5248**

### 6. Test the API

#### Option A: Using Swagger UI
Open your browser and navigate to:
```
http://localhost:5248/openapi
```

#### Option B: Using the HTTP File
Open `Minerals.Trading.Service.Api.http` in Visual Studio or VS Code with REST Client extension.

#### Option C: Using curl

**Get all trades:**
```bash
curl http://localhost:5248/api/trades
```

**Create a buyer:**
```bash
curl -X POST http://localhost:5248/api/buyers \
  -H "Content-Type: application/json" \
  -d '{
    "companyName": "Test Mining Corp",
    "contactPerson": "John Doe",
    "email": "john@testmining.com",
    "phoneNumber": "+1-555-0123",
    "address": "123 Main St",
    "city": "Lusaka",
    "country": "Zambia"
  }'
```

## Common API Workflows

### Workflow 1: Register and Approve a Buyer

1. **Create Buyer**
```http
POST http://localhost:5248/api/buyers
Content-Type: application/json

{
  "companyName": "Example Mining Corp",
  "contactPerson": "John Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "+1-555-0123",
  "address": "123 Main St",
  "city": "Lusaka",
  "country": "Zambia",
  "companyRegistrationNumber": "REG123456",
  "taxIdentificationNumber": "TAX789012",
  "bankName": "Standard Bank",
  "bankAccountNumber": "1234567890",
  "swiftCode": "SWIFT123",
  "complianceOfficer": "Jane Smith"
}
```

2. **Note the returned ID, then approve the buyer**
```http
POST http://localhost:5248/api/buyers/{id}/approve
```

### Workflow 2: Create a Mineral Listing

1. **First, create and approve a seller** (similar to buyer workflow)

2. **Create Mineral Listing**
```http
POST http://localhost:5248/api/minerallistings
Content-Type: application/json

{
  "sellerId": "{seller-id-from-step-1}",
  "sellerCompanyName": "Zambian Copper Co",
  "metalType": "Copper",
  "quantityAvailable": 100.5,
  "pricePerTon": 9500.00,
  "originCountry": "Zambia",
  "qualityGrade": "Grade A",
  "notes": "High quality copper cathodes"
}
```

3. **View available listings**
```http
GET http://localhost:5248/api/minerallistings/available
```

### Workflow 3: Execute a Complete Trade

1. **Create a trade**
```http
POST http://localhost:5248/api/trades
Content-Type: application/json

{
  "buyerName": "Example Mining Corp",
  "sellerName": "Zambian Copper Co",
  "metalType": "Copper",
  "quantity": 50.0,
  "pricePerTon": 9500.00,
  "deliveryDate": "2025-06-01T00:00:00Z"
}
```

2. **Confirm the trade**
```http
POST http://localhost:5248/api/trades/{trade-id}/confirm
```

3. **Novate the trade (clearing house)**
```http
POST http://localhost:5248/api/trades/{trade-id}/novate
```

4. **Calculate initial margin**
```http
POST http://localhost:5248/api/margins/trade/{trade-id}/initial?marginPercentage=0.10
```

5. **Record a payment**
```http
POST http://localhost:5248/api/payments
Content-Type: application/json

{
  "tradeId": "{trade-id}",
  "amount": 475000.00,
  "paymentDate": "2025-01-15T10:00:00Z",
  "description": "Full payment for copper trade"
}
```

6. **Process settlement**

For Physical Delivery:
```http
POST http://localhost:5248/api/settlements/physical
Content-Type: application/json

{
  "tradeId": "{trade-id}",
  "warrantNumber": "WRN-20250101-ABC123",
  "warehouseLocation": "Lusaka Industrial Area"
}
```

Or for Cash Settlement:
```http
POST http://localhost:5248/api/settlements/cash
Content-Type: application/json

{
  "tradeId": "{trade-id}",
  "finalPrice": 9800.00
}
```

7. **Complete settlement**
```http
POST http://localhost:5248/api/settlements/{settlement-id}/complete
```

### Workflow 4: Warehouse and Warrant Management

1. **Register a warehouse**
```http
POST http://localhost:5248/api/warehouses
Content-Type: application/json

{
  "warehouseCode": "WH001",
  "operatorName": "Zambia Warehouse Ltd",
  "location": "Lusaka Industrial Area",
  "city": "Lusaka",
  "country": "Zambia",
  "storageCapacity": 10000.0,
  "currentStock": 0,
  "hasWeighingSystem": true,
  "hasQualityControl": true,
  "hasFinancialStabilityProof": true,
  "agreesToLMERules": true,
  "securityLevel": "High"
}
```

2. **Conduct inspection**
```http
POST http://localhost:5248/api/inspections
Content-Type: application/json

{
  "warehouseId": "{warehouse-id}",
  "warehouseCode": "WH001",
  "inspectorName": "John Inspector",
  "siteInspectionPassed": true,
  "weighingSystemVerified": true,
  "qualityControlVerified": true,
  "reportingSystemTested": true,
  "neutralityEnsured": true,
  "overallOutcome": "Passed"
}
```

3. **Approve warehouse**
```http
POST http://localhost:5248/api/warehouses/{warehouse-id}/approve
```

4. **Issue warrant**
```http
POST http://localhost:5248/api/warrants
Content-Type: application/json

{
  "tradeId": "{trade-id}",
  "tradeNumber": "TRD-20250101-ABC123",
  "warehouseId": "{warehouse-id}",
  "warehouseName": "Zambia Warehouse Ltd",
  "metalType": "Copper",
  "quantity": 50.0,
  "currentOwner": "Example Mining Corp",
  "qualityGrade": "Grade A",
  "lotNumber": "LOT-2025-001"
}
```

5. **Transfer warrant**
```http
POST http://localhost:5248/api/warrants/{warrant-id}/transfer
Content-Type: application/json

"New Owner Company Name"
```

## Useful Queries

### Get Trades by Status
```http
GET http://localhost:5248/api/trades/status/Pending
GET http://localhost:5248/api/trades/status/Confirmed
GET http://localhost:5248/api/trades/status/Novated
```

### Get Listings by Metal Type
```http
GET http://localhost:5248/api/minerallistings/metal-type/Copper
GET http://localhost:5248/api/minerallistings/metal-type/Gold
GET http://localhost:5248/api/minerallistings/metal-type/Cobalt
```

### Check Payment Status
```http
GET http://localhost:5248/api/payments/trade/{trade-id}/total
GET http://localhost:5248/api/payments/trade/{trade-id}/fully-paid
```

### View Margin Requirements
```http
GET http://localhost:5248/api/margins/trade/{trade-id}
GET http://localhost:5248/api/margins/trade/{trade-id}/total
```

## Troubleshooting

### Issue: Port 5248 already in use

**Solution:**
Change the port in `launchSettings.json` or use a different port:
```bash
dotnet run --project Minerals.Trading.Service.Api --urls "http://localhost:5249"
```

### Issue: Build fails with missing dependencies

**Solution:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### Issue: Tests fail

**Solution:**
This is unexpected. Check:
1. All packages are restored: `dotnet restore`
2. Build succeeds: `dotnet build`
3. View test output: `dotnet test --logger "console;verbosity=detailed"`

### Issue: API returns 404 for endpoints

**Solution:**
- Ensure the API is running
- Check the base URL (http://localhost:5248)
- Verify the endpoint path (e.g., /api/trades, not /trades)
- Check the HTTP verb (GET, POST, PUT, DELETE)

### Issue: Validation errors when creating entities

**Solution:**
Review the error message for specific validation failures:
- Ensure required fields are provided
- Check that prices are within acceptable ranges
- Verify that entities (buyers/sellers) are approved before trading
- Ensure quantities are positive and within limits

## Stopping the API

Press `Ctrl+C` in the terminal where the API is running.

## Next Steps

1. Explore all endpoints using the Swagger UI
2. Review the API documentation in `Minerals.Trading.Service.Api.http`
3. Check the business logic in the Manager classes
4. Review the model definitions in the Model project
5. Run tests to understand the expected behavior: `dotnet test`

## Support

For detailed documentation, see:
- `README.md` - Full API reference
- `IMPLEMENTATION_SUMMARY.md` - Implementation details
- `ARCHITECTURE.md` - Architecture overview
- `Minerals.Trading.Service.Api.http` - Complete API examples

## Metal Types Available

The system supports the following metal types:
- Copper
- Aluminum  
- Zinc
- Nickel
- Lead
- Tin
- Gold
- Cobalt

## Trade Status Flow

```
Pending → Confirmed → Novated → MarginCollected → Active → Settled → Completed
   ↓
Cancelled
```

## Settlement Types

1. **Physical Delivery** - Transfer of actual minerals using warehouse warrants
2. **Cash Settlement** - Financial settlement based on price differences

## Default Values

- Initial Margin: 10% of trade value (configurable)
- Listing Expiry: 30 days from creation (if not specified)
- Price Buffer: ±20% of market range for validation
- Max Quantity per Trade: 10,000 metric tons
- Min Trade Value: $1,000 USD

## Database

The API uses an **In-Memory Database** for development. All data is lost when the API stops. For production, configure a persistent database (SQL Server, PostgreSQL) in `Program.cs`.

## CORS

Currently configured with "AllowAll" policy for development. Restrict to specific origins for production in `Program.cs`.

Happy Trading! 🚀
