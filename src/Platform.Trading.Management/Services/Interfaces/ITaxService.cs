using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Tax;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for tax calculation and regulatory reporting operations.
/// </summary>
public interface ITaxService
{
    // Tax Calculations
    Task<IEnumerable<TaxCalculation>> GetAllTaxCalculationsAsync();
    Task<TaxCalculation?> GetTaxCalculationByIdAsync(string id);
    Task<TaxCalculation?> GetTaxCalculationByTradeAsync(string tradeId);
    Task<TaxCalculation> CalculateTaxAsync(string tradeId);
    Task<TaxCalculation> UpdateTaxCalculationAsync(TaxCalculation calculation);
    Task<TaxCalculation> SubmitTaxToZraAsync(string id);
    Task<TaxCalculation> RecordTaxPaymentAsync(string id, string paymentReference, DateTime paymentDate);
    
    // Tax Rate Configuration
    Task<IEnumerable<TaxRateConfiguration>> GetTaxRateConfigurationsAsync(MetalType? metalType = null);
    Task<TaxRateConfiguration?> GetTaxRateConfigurationByIdAsync(string id);
    Task<TaxRateConfiguration> CreateTaxRateConfigurationAsync(TaxRateConfiguration config);
    Task<TaxRateConfiguration> UpdateTaxRateConfigurationAsync(TaxRateConfiguration config);
    Task<decimal> GetCurrentTaxRateAsync(MetalType metalType, string taxType);
    
    // Export Permits
    Task<IEnumerable<ExportPermit>> GetAllExportPermitsAsync();
    Task<ExportPermit?> GetExportPermitByIdAsync(string id);
    Task<IEnumerable<ExportPermit>> GetExportPermitsByApplicantAsync(string applicantId);
    Task<IEnumerable<ExportPermit>> GetExportPermitsByStatusAsync(string status);
    Task<ExportPermit> CreateExportPermitAsync(ExportPermit permit);
    Task<ExportPermit> UpdateExportPermitAsync(ExportPermit permit);
    Task<ExportPermit> ApproveExportPermitAsync(string id, string approvedBy);
    Task<ExportPermit> RejectExportPermitAsync(string id, string rejectedBy, string reason);
    
    // Bank of Zambia Reporting
    Task<IEnumerable<BozTransaction>> GetAllBozTransactionsAsync();
    Task<BozTransaction?> GetBozTransactionByIdAsync(string id);
    Task<IEnumerable<BozTransaction>> GetBozTransactionsByStatusAsync(string status);
    Task<BozTransaction> CreateBozTransactionAsync(BozTransaction transaction);
    Task<BozTransaction> SubmitToBozAsync(string id);
    Task<IEnumerable<BozTransaction>> GetLargeTransactionsAsync(decimal threshold);
}
