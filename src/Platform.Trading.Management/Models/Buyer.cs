namespace Platform.Trading.Management.Models;

public class Buyer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CompanyName { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public bool IsApproved { get; set; } = false;
    public string Status { get; set; } = "Pending KYC"; // e.g., Pending KYC, KYC Approved, Rejected

    // KYC Information
    public string CompanyRegistrationNumber { get; set; } = string.Empty;
    public string TaxIdentificationNumber { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BankAccountNumber { get; set; } = string.Empty;
    public string SwiftCode { get; set; } = string.Empty;
    public string ComplianceOfficer { get; set; } = string.Empty;
    public DateTime LastKYCReviewDate { get; set; } = DateTime.Now;
    public string KYCStatus { get; set; } = "Pending"; // e.g., Pending, Reviewed, Approved, Rejected
    public string? Notes { get; set; }
}
