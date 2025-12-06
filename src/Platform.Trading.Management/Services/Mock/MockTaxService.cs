using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Tax;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of ITaxService for development and testing.
/// </summary>
public class MockTaxService : ITaxService
{
    private readonly List<TaxCalculation> _taxCalculations = new();
    private readonly List<TaxRateConfiguration> _taxRates = new();
    private readonly List<ExportPermit> _exportPermits = new();
    private readonly List<BozTransaction> _bozTransactions = new();

    public MockTaxService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed tax rate configurations
        _taxRates.AddRange(new[]
        {
            // Copper tax rates
            new TaxRateConfiguration
            {
                MetalType = MetalType.Copper,
                TaxType = "MineralRoyalty",
                TaxName = "Copper Mineral Royalty",
                Rate = 5.5m, // 5.5% for copper
                EffectiveFrom = new DateTime(2023, 1, 1),
                IsActive = true,
                LegalReference = "Mines and Minerals Development Act 2015",
                GazetteReference = "SI No. 89 of 2022"
            },
            new TaxRateConfiguration
            {
                MetalType = MetalType.Copper,
                TaxType = "ExportLevy",
                TaxName = "Copper Export Levy",
                Rate = 10m, // 10% for unprocessed copper
                EffectiveFrom = new DateTime(2023, 1, 1),
                IsActive = true,
                LegalReference = "Export Duty Act"
            },
            new TaxRateConfiguration
            {
                MetalType = MetalType.Copper,
                TaxType = "WithholdingTax",
                TaxName = "Withholding Tax on Exports",
                Rate = 15m,
                EffectiveFrom = new DateTime(2023, 1, 1),
                IsActive = true,
                LegalReference = "Income Tax Act Cap 323"
            },
            // Cobalt tax rates
            new TaxRateConfiguration
            {
                MetalType = MetalType.Cobalt,
                TaxType = "MineralRoyalty",
                TaxName = "Cobalt Mineral Royalty",
                Rate = 8m, // Higher rate for cobalt
                EffectiveFrom = new DateTime(2023, 1, 1),
                IsActive = true,
                LegalReference = "Mines and Minerals Development Act 2015"
            }
        });

        // Seed sample tax calculation
        _taxCalculations.Add(new TaxCalculation
        {
            TradeId = "trade-001",
            TradeNumber = "TRD-2024-001",
            MetalType = MetalType.Copper,
            Quantity = 100,
            TradeValue = 852500, // $8,525/ton * 100 tons
            Currency = "USD",
            ExchangeRateToZmw = 26.5m,
            TradeValueZmw = 22591250,
            SellerId = "seller-001",
            SellerName = "Zambia Mining Corp",
            BuyerId = "buyer-001",
            BuyerName = "Copper Trading Ltd",
            TaxComponents = new List<TaxComponent>
            {
                new TaxComponent
                {
                    TaxType = "MineralRoyalty",
                    TaxName = "Copper Mineral Royalty",
                    TaxRate = 5.5m,
                    TaxableAmount = 22591250,
                    TaxAmount = 1242519m,
                    Currency = "ZMW",
                    LegalReference = "SI No. 89 of 2022"
                },
                new TaxComponent
                {
                    TaxType = "ExportLevy",
                    TaxName = "Copper Export Levy",
                    TaxRate = 10m,
                    TaxableAmount = 22591250,
                    TaxAmount = 2259125,
                    Currency = "ZMW"
                }
            },
            TotalTaxAmount = 3501644,
            TotalTaxCurrency = "ZMW",
            TotalTaxAmountUsd = 132136m,
            Status = "Calculated",
            DueDate = DateTime.Now.AddDays(30)
        });

        // Seed export permit
        _exportPermits.Add(new ExportPermit
        {
            PermitNumber = "EXP-2024-00123",
            ApplicationDate = DateTime.Now.AddDays(-10),
            IssueDate = DateTime.Now.AddDays(-5),
            ExpiryDate = DateTime.Now.AddMonths(3),
            ApplicantId = "seller-001",
            ApplicantName = "Zambia Mining Corp",
            ApplicantMiningLicense = "ML-ZM-2024-0123",
            MetalType = MetalType.Copper,
            Quantity = 500,
            QualityGrade = "Grade A",
            DestinationCountry = "China",
            DestinationPort = "Shanghai",
            BuyerName = "Shanghai Metals Trading Co.",
            DeclaredValue = 4262500,
            Currency = "USD",
            TaxClearanceVerified = true,
            TaxClearanceNumber = "TCC-2024-56789",
            EnvironmentalClearanceVerified = true,
            ZemaCertificateNumber = "ZEMA-EC-2024-001",
            MiningLicenseVerified = true,
            Status = "Approved",
            ReviewedBy = "Export Control Officer",
            ReviewDate = DateTime.Now.AddDays(-5),
            ApprovalAuthority = "Ministry of Commerce and Trade"
        });

        // Seed BOZ transaction
        _bozTransactions.Add(new BozTransaction
        {
            ReportingReference = "BOZ-2024-00567",
            TransactionDate = DateTime.Now.AddDays(-3),
            TransactionType = "Export",
            TradeId = "trade-001",
            TradeNumber = "TRD-2024-001",
            TransactionAmount = 852500,
            TransactionCurrency = "USD",
            ExchangeRate = 26.5m,
            ZmwEquivalent = 22591250,
            LocalPartyId = "seller-001",
            LocalPartyName = "Zambia Mining Corp",
            ForeignPartyName = "Copper Trading Ltd",
            ForeignPartyCountry = "United Kingdom",
            ForeignBankName = "Barclays Bank UK",
            ForeignBankSwiftCode = "BARCGB22",
            MetalType = MetalType.Copper,
            Quantity = 100,
            Status = "Submitted",
            SubmissionDate = DateTime.Now.AddDays(-2),
            BozAcknowledgementNumber = "BOZ-ACK-2024-001234",
            AcknowledgementDate = DateTime.Now.AddDays(-1),
            IsLargeTransaction = true,
            ThresholdAmount = 100000
        });
    }

    // Tax Calculations
    public Task<IEnumerable<TaxCalculation>> GetAllTaxCalculationsAsync()
        => Task.FromResult<IEnumerable<TaxCalculation>>(_taxCalculations);

    public Task<TaxCalculation?> GetTaxCalculationByIdAsync(string id)
        => Task.FromResult(_taxCalculations.FirstOrDefault(t => t.Id == id));

    public Task<TaxCalculation?> GetTaxCalculationByTradeAsync(string tradeId)
        => Task.FromResult(_taxCalculations.FirstOrDefault(t => t.TradeId == tradeId));

    public Task<TaxCalculation> CalculateTaxAsync(string tradeId)
    {
        // In production, this would fetch trade details and calculate taxes
        var calculation = new TaxCalculation
        {
            TradeId = tradeId,
            TradeNumber = $"TRD-{tradeId}",
            CalculationDate = DateTime.Now,
            MetalType = MetalType.Copper,
            Quantity = 50,
            TradeValue = 426250,
            Currency = "USD",
            ExchangeRateToZmw = 26.5m,
            TradeValueZmw = 11295625,
            TaxComponents = new List<TaxComponent>
            {
                new TaxComponent
                {
                    TaxType = "MineralRoyalty",
                    TaxName = "Copper Mineral Royalty",
                    TaxRate = 5.5m,
                    TaxableAmount = 11295625,
                    TaxAmount = 621259m,
                    Currency = "ZMW"
                }
            },
            TotalTaxAmount = 621259,
            TotalTaxCurrency = "ZMW",
            Status = "Calculated",
            DueDate = DateTime.Now.AddDays(30)
        };
        _taxCalculations.Add(calculation);
        return Task.FromResult(calculation);
    }

    public Task<TaxCalculation> UpdateTaxCalculationAsync(TaxCalculation calculation)
    {
        var existing = _taxCalculations.FirstOrDefault(t => t.Id == calculation.Id);
        if (existing != null)
        {
            _taxCalculations.Remove(existing);
            _taxCalculations.Add(calculation);
        }
        return Task.FromResult(calculation);
    }

    public Task<TaxCalculation> SubmitTaxToZraAsync(string id)
    {
        var calculation = _taxCalculations.FirstOrDefault(t => t.Id == id);
        if (calculation != null)
        {
            calculation.Status = "Submitted";
            calculation.ZraAssessmentNumber = $"ZRA-{DateTime.Now:yyyyMMdd}-{new Random().Next(10000, 99999)}";
            calculation.ZraAssessmentDate = DateTime.Now;
        }
        return Task.FromResult(calculation!);
    }

    public Task<TaxCalculation> RecordTaxPaymentAsync(string id, string paymentReference, DateTime paymentDate)
    {
        var calculation = _taxCalculations.FirstOrDefault(t => t.Id == id);
        if (calculation != null)
        {
            calculation.Status = "Paid";
            calculation.PaymentReference = paymentReference;
            calculation.PaymentDate = paymentDate;
        }
        return Task.FromResult(calculation!);
    }

    // Tax Rate Configuration
    public Task<IEnumerable<TaxRateConfiguration>> GetTaxRateConfigurationsAsync(MetalType? metalType = null)
    {
        var query = _taxRates.AsEnumerable();
        if (metalType.HasValue)
            query = query.Where(r => r.MetalType == metalType.Value);
        return Task.FromResult(query.Where(r => r.IsActive));
    }

    public Task<TaxRateConfiguration?> GetTaxRateConfigurationByIdAsync(string id)
        => Task.FromResult(_taxRates.FirstOrDefault(r => r.Id == id));

    public Task<TaxRateConfiguration> CreateTaxRateConfigurationAsync(TaxRateConfiguration config)
    {
        config.Id = Guid.NewGuid().ToString();
        _taxRates.Add(config);
        return Task.FromResult(config);
    }

    public Task<TaxRateConfiguration> UpdateTaxRateConfigurationAsync(TaxRateConfiguration config)
    {
        var existing = _taxRates.FirstOrDefault(r => r.Id == config.Id);
        if (existing != null)
        {
            _taxRates.Remove(existing);
            _taxRates.Add(config);
        }
        return Task.FromResult(config);
    }

    public Task<decimal> GetCurrentTaxRateAsync(MetalType metalType, string taxType)
    {
        var rate = _taxRates.FirstOrDefault(r => 
            r.MetalType == metalType && r.TaxType == taxType && r.IsActive);
        return Task.FromResult(rate?.Rate ?? 0);
    }

    // Export Permits
    public Task<IEnumerable<ExportPermit>> GetAllExportPermitsAsync()
        => Task.FromResult<IEnumerable<ExportPermit>>(_exportPermits);

    public Task<ExportPermit?> GetExportPermitByIdAsync(string id)
        => Task.FromResult(_exportPermits.FirstOrDefault(p => p.Id == id));

    public Task<IEnumerable<ExportPermit>> GetExportPermitsByApplicantAsync(string applicantId)
        => Task.FromResult<IEnumerable<ExportPermit>>(_exportPermits.Where(p => p.ApplicantId == applicantId));

    public Task<IEnumerable<ExportPermit>> GetExportPermitsByStatusAsync(string status)
        => Task.FromResult<IEnumerable<ExportPermit>>(_exportPermits.Where(p => p.Status == status));

    public Task<ExportPermit> CreateExportPermitAsync(ExportPermit permit)
    {
        permit.Id = Guid.NewGuid().ToString();
        permit.PermitNumber = $"EXP-{DateTime.Now:yyyyMMdd}-{_exportPermits.Count + 1:D5}";
        permit.ApplicationDate = DateTime.Now;
        permit.Status = "Pending";
        _exportPermits.Add(permit);
        return Task.FromResult(permit);
    }

    public Task<ExportPermit> UpdateExportPermitAsync(ExportPermit permit)
    {
        var existing = _exportPermits.FirstOrDefault(p => p.Id == permit.Id);
        if (existing != null)
        {
            _exportPermits.Remove(existing);
            _exportPermits.Add(permit);
        }
        return Task.FromResult(permit);
    }

    public Task<ExportPermit> ApproveExportPermitAsync(string id, string approvedBy)
    {
        var permit = _exportPermits.FirstOrDefault(p => p.Id == id);
        if (permit != null)
        {
            permit.Status = "Approved";
            permit.ReviewedBy = approvedBy;
            permit.ReviewDate = DateTime.Now;
            permit.IssueDate = DateTime.Now;
            permit.ExpiryDate = DateTime.Now.AddMonths(3);
        }
        return Task.FromResult(permit!);
    }

    public Task<ExportPermit> RejectExportPermitAsync(string id, string rejectedBy, string reason)
    {
        var permit = _exportPermits.FirstOrDefault(p => p.Id == id);
        if (permit != null)
        {
            permit.Status = "Rejected";
            permit.ReviewedBy = rejectedBy;
            permit.ReviewDate = DateTime.Now;
            permit.RejectionReason = reason;
        }
        return Task.FromResult(permit!);
    }

    // Bank of Zambia Reporting
    public Task<IEnumerable<BozTransaction>> GetAllBozTransactionsAsync()
        => Task.FromResult<IEnumerable<BozTransaction>>(_bozTransactions);

    public Task<BozTransaction?> GetBozTransactionByIdAsync(string id)
        => Task.FromResult(_bozTransactions.FirstOrDefault(t => t.Id == id));

    public Task<IEnumerable<BozTransaction>> GetBozTransactionsByStatusAsync(string status)
        => Task.FromResult<IEnumerable<BozTransaction>>(_bozTransactions.Where(t => t.Status == status));

    public Task<BozTransaction> CreateBozTransactionAsync(BozTransaction transaction)
    {
        transaction.Id = Guid.NewGuid().ToString();
        transaction.ReportingReference = $"BOZ-{DateTime.Now:yyyyMMdd}-{_bozTransactions.Count + 1:D5}";
        transaction.ReportingDate = DateTime.Now;
        transaction.Status = "Pending";
        
        // Check if large transaction
        if (transaction.TransactionAmount >= 100000)
        {
            transaction.IsLargeTransaction = true;
            transaction.ThresholdAmount = 100000;
        }
        
        _bozTransactions.Add(transaction);
        return Task.FromResult(transaction);
    }

    public Task<BozTransaction> SubmitToBozAsync(string id)
    {
        var transaction = _bozTransactions.FirstOrDefault(t => t.Id == id);
        if (transaction != null)
        {
            transaction.Status = "Submitted";
            transaction.SubmissionDate = DateTime.Now;
        }
        return Task.FromResult(transaction!);
    }

    public Task<IEnumerable<BozTransaction>> GetLargeTransactionsAsync(decimal threshold)
        => Task.FromResult<IEnumerable<BozTransaction>>(
            _bozTransactions.Where(t => t.TransactionAmount >= threshold));
}
