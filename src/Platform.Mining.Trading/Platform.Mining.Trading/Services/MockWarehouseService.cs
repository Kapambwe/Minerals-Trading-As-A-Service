using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockWarehouseService : IWarehouseService
    {
        private List<WarehouseLocation> _locations = new();
        private List<WarehouseReceipt> _receipts = new();
        private List<QualityCertificate> _certificates = new();

        public MockWarehouseService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _locations = new List<WarehouseLocation>
            {
                new WarehouseLocation
                {
                    LocationId = "WH-001",
                    Name = "Lusaka Central Warehouse",
                    Address = "Industrial Road, Lusaka, Zambia",
                    Status = "Active",
                    TotalCapacity = 50000,
                    UsedCapacity = 32000
                },
                new WarehouseLocation
                {
                    LocationId = "WH-002",
                    Name = "Ndola Mining Facility",
                    Address = "Copperbelt Province, Ndola, Zambia",
                    Status = "Active",
                    TotalCapacity = 75000,
                    UsedCapacity = 48000
                },
                new WarehouseLocation
                {
                    LocationId = "WH-003",
                    Name = "Kitwe Storage Center",
                    Address = "Kitwe, Copperbelt, Zambia",
                    Status = "Active",
                    TotalCapacity = 40000,
                    UsedCapacity = 15000
                }
            };

            _receipts = new List<WarehouseReceipt>
            {
                new WarehouseReceipt
                {
                    ReceiptId = "WHR-001",
                    WarehouseLocation = "WH-001",
                    MineralType = "Copper",
                    Grade = "High Grade",
                    Quantity = 5000,
                    Owner = "First Quantum Minerals",
                    ReceivedDate = DateTime.Now.AddDays(-10),
                    Status = "Active",
                    QcCertificateId = "QC-001"
                },
                new WarehouseReceipt
                {
                    ReceiptId = "WHR-002",
                    WarehouseLocation = "WH-002",
                    MineralType = "Cobalt",
                    Grade = "Industrial Grade",
                    Quantity = 2500,
                    Owner = "KCM Trading",
                    ReceivedDate = DateTime.Now.AddDays(-5),
                    Status = "Active",
                    QcCertificateId = "QC-002"
                },
                new WarehouseReceipt
                {
                    ReceiptId = "WHR-003",
                    WarehouseLocation = "WH-001",
                    MineralType = "Emerald",
                    Grade = "Premium",
                    Quantity = 150,
                    Owner = "Gemfields Zambia",
                    ReceivedDate = DateTime.Now.AddDays(-15),
                    Status = "Released"
                }
            };

            _certificates = new List<QualityCertificate>
            {
                new QualityCertificate
                {
                    CertificateId = "QC-001",
                    ReceiptId = "WHR-001",
                    IssuedDate = DateTime.Now.AddDays(-10),
                    IssuedBy = "Zambia Bureau of Standards",
                    MineralType = "Copper",
                    Grade = "High Grade",
                    TestResults = new Dictionary<string, string>
                    {
                        { "Purity", "99.8%" },
                        { "Moisture Content", "0.2%" },
                        { "Impurities", "< 0.1%" }
                    },
                    Approved = true
                },
                new QualityCertificate
                {
                    CertificateId = "QC-002",
                    ReceiptId = "WHR-002",
                    IssuedDate = DateTime.Now.AddDays(-5),
                    IssuedBy = "Zambia Bureau of Standards",
                    MineralType = "Cobalt",
                    Grade = "Industrial Grade",
                    TestResults = new Dictionary<string, string>
                    {
                        { "Purity", "98.5%" },
                        { "Moisture Content", "0.3%" }
                    },
                    Approved = true
                }
            };
        }

        public async Task<List<WarehouseLocation>> GetWarehouseLocationsAsync()
        {
            await Task.Delay(100);
            return _locations;
        }

        public async Task<List<WarehouseReceipt>> GetWarehouseReceiptsAsync()
        {
            await Task.Delay(100);
            return _receipts;
        }

        public async Task<string> CreateReceiptAsync(WarehouseReceipt receipt)
        {
            await Task.Delay(150);
            receipt.ReceiptId = $"WHR-{_receipts.Count + 1:D3}";
            receipt.ReceivedDate = DateTime.Now;
            receipt.Status = "Active";
            _receipts.Add(receipt);
            return receipt.ReceiptId;
        }

        public async Task<bool> AttachQcCertificateAsync(string receiptId, QualityCertificate certificate)
        {
            await Task.Delay(150);
            var receipt = _receipts.FirstOrDefault(r => r.ReceiptId == receiptId);
            if (receipt != null)
            {
                certificate.CertificateId = $"QC-{_certificates.Count + 1:D3}";
                certificate.ReceiptId = receiptId;
                certificate.IssuedDate = DateTime.Now;
                _certificates.Add(certificate);
                receipt.QcCertificateId = certificate.CertificateId;
                return true;
            }
            return false;
        }

        public async Task<bool> MarkDeliveryCompleteAsync(string receiptId)
        {
            await Task.Delay(100);
            var receipt = _receipts.FirstOrDefault(r => r.ReceiptId == receiptId);
            if (receipt != null)
            {
                receipt.Status = "Released";
                return true;
            }
            return false;
        }

        public async Task<List<InventorySummary>> GetInventorySummaryAsync()
        {
            await Task.Delay(100);
            var summary = _receipts
                .Where(r => r.Status == "Active")
                .GroupBy(r => new { r.MineralType, r.Grade })
                .Select(g => new InventorySummary
                {
                    MineralType = g.Key.MineralType,
                    Grade = g.Key.Grade,
                    TotalQuantity = g.Sum(r => r.Quantity),
                    ReceiptCount = g.Count(),
                    AverageAge = (decimal)g.Average(r => (DateTime.Now - r.ReceivedDate).TotalDays)
                })
                .ToList();
            return summary;
        }

        public async Task<List<QualityCertificate>> GetQualityCertificatesAsync()
        {
            await Task.Delay(100);
            return _certificates;
        }
    }
}
