using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockBuyerService : IBuyerService
{
    private readonly List<Buyer> _buyers;

    public MockBuyerService()
    {
        _buyers = new List<Buyer>
        {
            new Buyer
            {
                Id = "BUY001",
                CompanyName = "Dubai Metals Trading LLC",
                ContactPerson = "Ahmed Al-Mansoori",
                Email = "ahmed.almansoori@dubaimetals.ae",
                PhoneNumber = "+971 50 123 4567",
                Address = "Sheikh Zayed Road, Dubai",
                City = "Dubai",
                Country = "United Arab Emirates",
                RegistrationDate = DateTime.Now.AddYears(-2),
                IsApproved = true,
                Status = "KYC Approved",
                CompanyRegistrationNumber = "DM-TRD-001",
                TaxIdentificationNumber = "UAE123456789",
                BankName = "Emirates NBD",
                BankAccountNumber = "AE123456789012345678901",
                SwiftCode = "EBILAEAD",
                ComplianceOfficer = "Fatima Khan",
                LastKYCReviewDate = DateTime.Now.AddMonths(-3),
                KYCStatus = "Approved",
                Notes = "Specializes in copper and aluminum imports."
            },
            new Buyer
            {
                Id = "BUY002",
                CompanyName = "Zambia Copper Buyers PLC",
                ContactPerson = "Chanda Mwape",
                Email = "chanda.mwape@zambiacopper.zm",
                PhoneNumber = "+260 97 123 4567",
                Address = "Independence Avenue, Lusaka",
                City = "Lusaka",
                Country = "Zambia",
                RegistrationDate = DateTime.Now.AddYears(-1),
                IsApproved = true,
                Status = "KYC Approved",
                CompanyRegistrationNumber = "ZCB-LUS-002",
                TaxIdentificationNumber = "ZMB987654321",
                BankName = "Zanaco Bank",
                BankAccountNumber = "ZM987654321098765432109",
                SwiftCode = "ZNCOZMLU",
                ComplianceOfficer = "David Banda",
                LastKYCReviewDate = DateTime.Now.AddMonths(-6),
                KYCStatus = "Approved",
                Notes = "Major local buyer for domestic processing."
            },
            new Buyer
            {
                Id = "BUY003",
                CompanyName = "Swiss Global Commodities AG",
                ContactPerson = "Hans Müller",
                Email = "hans.muller@swissglobal.ch",
                PhoneNumber = "+41 79 123 4567",
                Address = "Bahnhofstrasse 10, Zürich",
                City = "Zürich",
                Country = "Switzerland",
                RegistrationDate = DateTime.Now.AddMonths(-6),
                IsApproved = false,
                Status = "Pending KYC",
                CompanyRegistrationNumber = "SGC-AG-003",
                TaxIdentificationNumber = "CHE123456789",
                BankName = "UBS AG",
                BankAccountNumber = "CH123456789012345678901",
                SwiftCode = "UBSWCHZH",
                ComplianceOfficer = "Maria Schneider",
                LastKYCReviewDate = DateTime.Now.AddDays(-10),
                KYCStatus = "Pending",
                Notes = "New applicant, awaiting final KYC review."
            },
            new Buyer
            {
                Id = "BUY004",
                CompanyName = "African Minerals Corp",
                ContactPerson = "Nadia Khan",
                Email = "nadia.khan@africanminerals.za",
                PhoneNumber = "+27 82 111 2222",
                Address = "Sandton Drive, Johannesburg",
                City = "Johannesburg",
                Country = "South Africa",
                RegistrationDate = DateTime.Now.AddMonths(-10),
                IsApproved = true,
                Status = "KYC Approved",
                CompanyRegistrationNumber = "AMC-JHB-004",
                TaxIdentificationNumber = "ZAF111222333",
                BankName = "Standard Bank",
                BankAccountNumber = "ZA111222333444555666777",
                SwiftCode = "SBZAZAJJ",
                ComplianceOfficer = "Thabo Mbeki",
                LastKYCReviewDate = DateTime.Now.AddMonths(-2),
                KYCStatus = "Approved",
                Notes = "Actively trades in various base metals."
            }
        };
    }

    public Task<IEnumerable<Buyer>> GetAllBuyersAsync()
    {
        return Task.FromResult<IEnumerable<Buyer>>(_buyers);
    }

    public Task<Buyer?> GetBuyerByIdAsync(string id)
    {
        var buyer = _buyers.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(buyer);
    }

    public Task<Buyer> CreateBuyerAsync(Buyer buyer)
    {
        buyer.Id = $"BUY{_buyers.Count + 1:D3}";
        buyer.RegistrationDate = DateTime.Now;
        buyer.Status = "Pending KYC";
        buyer.KYCStatus = "Pending";
        _buyers.Add(buyer);
        return Task.FromResult(buyer);
    }

    public Task<Buyer> UpdateBuyerAsync(Buyer buyer)
    {
        var existingBuyer = _buyers.FirstOrDefault(b => b.Id == buyer.Id);
        if (existingBuyer != null)
        {
            var index = _buyers.IndexOf(existingBuyer);
            _buyers[index] = buyer;
        }
        return Task.FromResult(buyer);
    }

    public Task<bool> DeleteBuyerAsync(string id)
    {
        var buyer = _buyers.FirstOrDefault(b => b.Id == id);
        if (buyer != null)
        {
            _buyers.Remove(buyer);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<Buyer> ApproveBuyerAsync(string buyerId)
    {
        var buyer = _buyers.FirstOrDefault(b => b.Id == buyerId);
        if (buyer != null)
        {
            buyer.IsApproved = true;
            buyer.Status = "Approved";
        }
        return Task.FromResult(buyer!); // Return the updated buyer
    }

    public Task<Buyer> UpdateKYCStatusAsync(string buyerId, string kycStatus)
    {
        var buyer = _buyers.FirstOrDefault(b => b.Id == buyerId);
        if (buyer != null)
        {
            buyer.KYCStatus = kycStatus;
            if (kycStatus == "Approved")
            {
                buyer.Status = "KYC Approved";
            }
            else if (kycStatus == "Rejected")
            {
                buyer.Status = "KYC Rejected";
            }
            buyer.LastKYCReviewDate = DateTime.Now;
        }
        return Task.FromResult(buyer!); // Return the updated buyer
    }
}
