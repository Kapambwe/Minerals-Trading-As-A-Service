using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockSellerService : ISellerService
{
    private readonly List<Seller> _sellers;

    public MockSellerService()
    {
        _sellers = new List<Seller>
        {
            new Seller
            {
                Id = "SEL001",
                CompanyName = "Konkola Copper Mines PLC",
                ContactPerson = "Mwansa Bwalya",
                Email = "mwansa.bwalya@kcm.zm",
                PhoneNumber = "+260 96 111 2222",
                Address = "Chingola Road, Chililabombwe",
                City = "Chililabombwe",
                Country = "Zambia",
                RegistrationDate = DateTime.Now.AddYears(-5),
                IsApproved = true,
                Status = "KYC Approved",
                CompanyRegistrationNumber = "KCM-ZMB-001",
                TaxIdentificationNumber = "ZMB123456789",
                BankName = "Standard Chartered Zambia",
                BankAccountNumber = "ZM123456789012345678901",
                SwiftCode = "SCBLZMLU",
                ComplianceOfficer = "Lungu Phiri",
                LastKYCReviewDate = DateTime.Now.AddMonths(-1),
                KYCStatus = "Approved",
                Notes = "Large-scale copper mining company.",
                MineralsSold = new List<MetalType> { MetalType.Copper },
                ProductionCapacityPerMonth = 15000
            },
            new Seller
            {
                Id = "SEL002",
                CompanyName = "Zambia Gold Company Ltd.",
                ContactPerson = "Nelly Banda",
                Email = "nelly.banda@zambiagold.zm",
                PhoneNumber = "+260 97 222 3333",
                Address = "Cairo Road, Lusaka",
                City = "Lusaka",
                Country = "Zambia",
                RegistrationDate = DateTime.Now.AddYears(-2),
                IsApproved = true,
                Status = "KYC Approved",
                CompanyRegistrationNumber = "ZGC-LUS-002",
                TaxIdentificationNumber = "ZMB987654321",
                BankName = "Atlas Mara Bank",
                BankAccountNumber = "ZM987654321098765432109",
                SwiftCode = "ATMAZMLU",
                ComplianceOfficer = "Chola Mumba",
                LastKYCReviewDate = DateTime.Now.AddMonths(-2),
                KYCStatus = "Approved",
                Notes = "State-owned gold mining and trading company.",
                MineralsSold = new List<MetalType> { MetalType.Gold },
                ProductionCapacityPerMonth = 500
            },
            new Seller
            {
                Id = "SEL003",
                CompanyName = "Mufumbwe Small Scale Miners Cooperative",
                ContactPerson = "Joseph Tembo",
                Email = "joseph.tembo@mufumbwemines.zm",
                PhoneNumber = "+260 95 333 4444",
                Address = "Mufumbwe District Office",
                City = "Mufumbwe",
                Country = "Zambia",
                RegistrationDate = DateTime.Now.AddMonths(-8),
                IsApproved = false,
                Status = "Pending KYC",
                CompanyRegistrationNumber = "MSMC-ZMB-003",
                TaxIdentificationNumber = "ZMB456789012",
                BankName = "National Commercial Bank",
                BankAccountNumber = "ZM456789012345678901234",
                SwiftCode = "NCBZMLU",
                ComplianceOfficer = "Grace Zulu",
                LastKYCReviewDate = DateTime.Now.AddDays(-5),
                KYCStatus = "Pending",
                Notes = "Cooperative of artisanal and small-scale miners. Awaiting full KYC.",
                MineralsSold = new List<MetalType> { MetalType.Copper, MetalType.Cobalt },
                ProductionCapacityPerMonth = 100
            },
            new Seller
            {
                Id = "SEL004",
                CompanyName = "Kansanshi Mining PLC",
                ContactPerson = "Peter Lungu",
                Email = "peter.lungu@kansanshi.zm",
                PhoneNumber = "+260 97 444 5555",
                Address = "Solwezi Mine Site",
                City = "Solwezi",
                Country = "Zambia",
                RegistrationDate = DateTime.Now.AddYears(-7),
                IsApproved = true,
                Status = "KYC Approved",
                CompanyRegistrationNumber = "KMP-ZMB-004",
                TaxIdentificationNumber = "ZMB789012345",
                BankName = "FNB Zambia",
                BankAccountNumber = "ZM789012345678901234567",
                SwiftCode = "FIRNZMLX",
                ComplianceOfficer = "Susan Mwale",
                LastKYCReviewDate = DateTime.Now.AddMonths(-3),
                KYCStatus = "Approved",
                Notes = "One of the largest copper mines in Africa.",
                MineralsSold = new List<MetalType> { MetalType.Copper, MetalType.Gold },
                ProductionCapacityPerMonth = 20000
            }
        };
    }

    public Task<IEnumerable<Seller>> GetAllSellersAsync()
    {
        return Task.FromResult<IEnumerable<Seller>>(_sellers);
    }

    public Task<Seller?> GetSellerByIdAsync(string id)
    {
        var seller = _sellers.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(seller);
    }

    public Task<Seller> CreateSellerAsync(Seller seller)
    {
        seller.Id = $"SEL{_sellers.Count + 1:D3}";
        seller.RegistrationDate = DateTime.Now;
        seller.Status = "Pending KYC";
        seller.KYCStatus = "Pending";
        _sellers.Add(seller);
        return Task.FromResult(seller);
    }

    public Task<Seller> UpdateSellerAsync(Seller seller)
    {
        var existingSeller = _sellers.FirstOrDefault(s => s.Id == seller.Id);
        if (existingSeller != null)
        {
            var index = _sellers.IndexOf(existingSeller);
            _sellers[index] = seller;
        }
        return Task.FromResult(seller);
    }

    public Task<bool> DeleteSellerAsync(string id)
    {
        var seller = _sellers.FirstOrDefault(s => s.Id == id);
        if (seller != null)
        {
            _sellers.Remove(seller);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<Seller> ApproveSellerAsync(string sellerId)
    {
        var seller = _sellers.FirstOrDefault(s => s.Id == sellerId);
        if (seller != null)
        {
            seller.IsApproved = true;
            seller.Status = "Approved";
        }
        return Task.FromResult(seller!); // Return the updated seller
    }

    public Task<Seller> UpdateKYCStatusAsync(string sellerId, string kycStatus)
    {
        var seller = _sellers.FirstOrDefault(s => s.Id == sellerId);
        if (seller != null)
        {
            seller.KYCStatus = kycStatus;
            if (kycStatus == "Approved")
            {
                seller.Status = "KYC Approved";
            }
            else if (kycStatus == "Rejected")
            {
                seller.Status = "KYC Rejected";
            }
            seller.LastKYCReviewDate = DateTime.Now;
        }
        return Task.FromResult(seller!); // Return the updated seller
    }
}
