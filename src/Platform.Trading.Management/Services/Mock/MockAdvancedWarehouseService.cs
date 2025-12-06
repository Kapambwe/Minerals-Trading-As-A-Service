using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.WarehouseManagement;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of electronic warehouse receipt service.
/// </summary>
public class MockWarehouseReceiptService : IWarehouseReceiptService
{
    private readonly List<ElectronicWarehouseReceipt> _receipts = new();

    public MockWarehouseReceiptService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _receipts.Add(new ElectronicWarehouseReceipt
        {
            Id = Guid.NewGuid().ToString(),
            ReceiptNumber = "EWR-2024-001",
            ReceiptType = "Negotiable",
            IssueDate = DateTime.UtcNow.AddMonths(-2),
            WarehouseId = "WH-001",
            WarehouseName = "Kitwe Copper Warehouse",
            WarehouseAddress = "Industrial Road, Kitwe, Copperbelt",
            WarehouseLicenseNumber = "WHL-2024-001",
            MetalType = MetalType.Copper,
            Quantity = 100,
            QualityGrade = "LME Grade A",
            LotNumber = "LOT-2024-001",
            CurrentOwnerId = "OWNER-001",
            CurrentOwnerName = "Glencore International",
            OwnershipDate = DateTime.UtcNow.AddMonths(-1),
            LegalStatus = "Valid",
            IsNegotiable = true,
            RegisteredWithRegulator = true,
            RegulatoryReference = "REG-2024-001",
            InsurancePolicyId = "INS-2024-001",
            InsuranceCurrent = true,
            StorageLocation = "Shed A, Bay 12",
            StorageRate = 0.50m
        });
    }

    public Task<ElectronicWarehouseReceipt?> GetReceiptAsync(string receiptNumber)
        => Task.FromResult(_receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber));

    public Task<List<ElectronicWarehouseReceipt>> GetAllReceiptsAsync()
        => Task.FromResult(_receipts.ToList());

    public Task<List<ElectronicWarehouseReceipt>> GetReceiptsByOwnerAsync(string ownerId)
        => Task.FromResult(_receipts.Where(r => r.CurrentOwnerId == ownerId).ToList());

    public Task<ElectronicWarehouseReceipt> IssueReceiptAsync(ElectronicWarehouseReceipt receipt)
    {
        receipt.Id = Guid.NewGuid().ToString();
        receipt.ReceiptNumber = $"EWR-{DateTime.UtcNow:yyyy}-{_receipts.Count + 1:D3}";
        receipt.IssueDate = DateTime.UtcNow;
        receipt.LegalStatus = "Valid";
        _receipts.Add(receipt);
        return Task.FromResult(receipt);
    }

    public Task<ElectronicWarehouseReceipt> TransferOwnershipAsync(string receiptNumber, string toOwnerId, string toOwnerName, string transferType)
    {
        var receipt = _receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber);
        if (receipt != null)
        {
            var transfer = new ReceiptOwnershipTransfer
            {
                Id = Guid.NewGuid().ToString(),
                TransferDate = DateTime.UtcNow,
                FromOwnerId = receipt.CurrentOwnerId,
                FromOwnerName = receipt.CurrentOwnerName,
                ToOwnerId = toOwnerId,
                ToOwnerName = toOwnerName,
                TransferType = transferType,
                VerifiedByWarehouse = true
            };
            receipt.OwnershipHistory.Add(transfer);
            receipt.CurrentOwnerId = toOwnerId;
            receipt.CurrentOwnerName = toOwnerName;
            receipt.OwnershipDate = DateTime.UtcNow;
        }
        return Task.FromResult(receipt ?? throw new InvalidOperationException("Receipt not found"));
    }

    public Task<ElectronicWarehouseReceipt> PledgeReceiptAsync(string receiptNumber, string pledgeeId, string pledgeType)
    {
        var receipt = _receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber);
        if (receipt != null)
        {
            receipt.IsEncumbered = true;
            receipt.EncumbranceType = pledgeType;
            receipt.EncumbranceHolder = pledgeeId;
        }
        return Task.FromResult(receipt ?? throw new InvalidOperationException("Receipt not found"));
    }

    public Task<ElectronicWarehouseReceipt> ReleaseEncumbranceAsync(string receiptNumber)
    {
        var receipt = _receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber);
        if (receipt != null)
        {
            receipt.IsEncumbered = false;
            receipt.EncumbranceType = null;
            receipt.EncumbranceHolder = null;
        }
        return Task.FromResult(receipt ?? throw new InvalidOperationException("Receipt not found"));
    }

    public Task<ElectronicWarehouseReceipt> RedeemReceiptAsync(string receiptNumber)
    {
        var receipt = _receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber);
        if (receipt != null)
        {
            receipt.IsRedeemed = true;
            receipt.RedemptionDate = DateTime.UtcNow;
            receipt.LegalStatus = "Redeemed";
        }
        return Task.FromResult(receipt ?? throw new InvalidOperationException("Receipt not found"));
    }

    public Task<ElectronicWarehouseReceipt> CancelReceiptAsync(string receiptNumber, string reason)
    {
        var receipt = _receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber);
        if (receipt != null)
        {
            receipt.LegalStatus = "Cancelled";
            receipt.Notes = reason;
        }
        return Task.FromResult(receipt ?? throw new InvalidOperationException("Receipt not found"));
    }

    public Task<bool> VerifySignatureAsync(string receiptNumber)
    {
        var receipt = _receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber);
        return Task.FromResult(receipt?.SignatureVerified ?? false);
    }

    public Task RegisterWithRegulatorAsync(string receiptNumber)
    {
        var receipt = _receipts.FirstOrDefault(r => r.ReceiptNumber == receiptNumber);
        if (receipt != null)
        {
            receipt.RegisteredWithRegulator = true;
            receipt.RegulatoryReference = $"REG-{DateTime.UtcNow:yyyyMMdd}-{receiptNumber}";
            receipt.RegistrationDate = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }
}

/// <summary>
/// Mock implementation of warehouse insurance service.
/// </summary>
public class MockWarehouseInsuranceService : IWarehouseInsuranceService
{
    private readonly List<WarehouseInsurance> _policies = new();

    public MockWarehouseInsuranceService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _policies.Add(new WarehouseInsurance
        {
            Id = Guid.NewGuid().ToString(),
            PolicyNumber = "INS-2024-001",
            PolicyType = "AllRisk",
            InsurerId = "INS-CO-001",
            InsurerName = "Madison General Insurance",
            PolicyStartDate = DateTime.UtcNow.AddMonths(-6),
            PolicyEndDate = DateTime.UtcNow.AddMonths(6),
            WarehouseId = "WH-001",
            WarehouseName = "Kitwe Copper Warehouse",
            MaximumCoverage = 50000000m,
            CurrentCoveredValue = 35000000m,
            Currency = "USD",
            Deductible = 50000m,
            AnnualPremium = 125000m,
            PremiumPaid = true,
            Status = "Active",
            CoveredPerils = new List<CoveredPeril>
            {
                new CoveredPeril { PerilCode = "FIRE", PerilName = "Fire", IsCovered = true },
                new CoveredPeril { PerilCode = "THEFT", PerilName = "Theft", IsCovered = true },
                new CoveredPeril { PerilCode = "FLOOD", PerilName = "Flood", IsCovered = true },
                new CoveredPeril { PerilCode = "DAMAGE", PerilName = "Malicious Damage", IsCovered = true }
            }
        });
    }

    public Task<WarehouseInsurance?> GetInsuranceAsync(string policyNumber)
        => Task.FromResult(_policies.FirstOrDefault(p => p.PolicyNumber == policyNumber));

    public Task<List<WarehouseInsurance>> GetAllInsuranceAsync()
        => Task.FromResult(_policies.ToList());

    public Task<WarehouseInsurance?> GetInsuranceByWarehouseAsync(string warehouseId)
        => Task.FromResult(_policies.FirstOrDefault(p => p.WarehouseId == warehouseId && p.Status == "Active"));

    public Task<WarehouseInsurance> CreateInsuranceAsync(WarehouseInsurance insurance)
    {
        insurance.Id = Guid.NewGuid().ToString();
        _policies.Add(insurance);
        return Task.FromResult(insurance);
    }

    public Task<WarehouseInsurance> UpdateInsuranceAsync(WarehouseInsurance insurance)
    {
        var existing = _policies.FindIndex(p => p.Id == insurance.Id);
        if (existing >= 0)
            _policies[existing] = insurance;
        return Task.FromResult(insurance);
    }

    public Task<WarehouseInsurance> RenewInsuranceAsync(string policyNumber)
    {
        var policy = _policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
        if (policy != null)
        {
            policy.PolicyStartDate = policy.PolicyEndDate;
            policy.PolicyEndDate = policy.PolicyEndDate.AddYears(1);
        }
        return Task.FromResult(policy ?? throw new InvalidOperationException("Policy not found"));
    }

    public Task<InsuranceClaim> FileClaimAsync(string policyNumber, InsuranceClaim claim)
    {
        var policy = _policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
        if (policy != null)
        {
            claim.Id = Guid.NewGuid().ToString();
            claim.ClaimNumber = $"CLM-{DateTime.UtcNow:yyyy}-{policy.Claims.Count + 1:D3}";
            claim.ClaimDate = DateTime.UtcNow;
            claim.Status = "Filed";
            policy.Claims.Add(claim);
        }
        return Task.FromResult(claim);
    }

    public Task<InsuranceClaim> UpdateClaimStatusAsync(string claimNumber, string status, decimal? approvedAmount = null)
    {
        foreach (var policy in _policies)
        {
            var claim = policy.Claims.FirstOrDefault(c => c.ClaimNumber == claimNumber);
            if (claim != null)
            {
                claim.Status = status;
                if (approvedAmount.HasValue)
                    claim.ApprovedAmount = approvedAmount;
                return Task.FromResult(claim);
            }
        }
        throw new InvalidOperationException("Claim not found");
    }

    public Task<List<WarehouseInsurance>> GetExpiringPoliciesAsync(int daysToExpiry)
    {
        var expiryDate = DateTime.UtcNow.AddDays(daysToExpiry);
        return Task.FromResult(_policies.Where(p => p.PolicyEndDate <= expiryDate && p.Status == "Active").ToList());
    }

    public Task<bool> VerifyInsuranceCoverageAsync(string warehouseId, decimal valueToCover)
    {
        var policy = _policies.FirstOrDefault(p => p.WarehouseId == warehouseId && p.IsActive);
        return Task.FromResult(policy != null && (policy.MaximumCoverage - policy.CurrentCoveredValue) >= valueToCover);
    }

    public Task<decimal> CalculatePremiumAsync(string warehouseId, decimal coveredValue)
        => Task.FromResult(coveredValue * 0.0025m); // 0.25% premium rate
}

/// <summary>
/// Mock implementation of load-out queue service.
/// </summary>
public class MockLoadOutQueueService : ILoadOutQueueService
{
    private readonly List<LoadOutQueue> _queue = new();

    public Task<LoadOutQueue?> GetQueueItemAsync(string queueNumber)
        => Task.FromResult(_queue.FirstOrDefault(q => q.QueueNumber == queueNumber));

    public Task<List<LoadOutQueue>> GetQueueByWarehouseAsync(string warehouseId)
        => Task.FromResult(_queue.Where(q => q.WarehouseId == warehouseId).OrderBy(q => q.QueuePosition).ToList());

    public Task<LoadOutQueue> CreateQueueRequestAsync(LoadOutQueue request)
    {
        request.Id = Guid.NewGuid().ToString();
        request.QueueNumber = $"LOQ-{DateTime.UtcNow:yyyyMMdd}-{_queue.Count + 1:D3}";
        request.RequestDate = DateTime.UtcNow;
        request.QueuePosition = _queue.Count(q => q.WarehouseId == request.WarehouseId && q.Status == "Queued") + 1;
        request.EstimatedWaitDays = request.QueuePosition * 2;
        request.EstimatedLoadOutDate = DateTime.UtcNow.AddDays(request.EstimatedWaitDays);
        request.Status = "Queued";
        _queue.Add(request);
        return Task.FromResult(request);
    }

    public Task<LoadOutQueue> UpdateQueuePositionAsync(string queueNumber, int newPosition)
    {
        var item = _queue.FirstOrDefault(q => q.QueueNumber == queueNumber);
        if (item != null)
        {
            item.QueuePosition = newPosition;
            item.EstimatedWaitDays = newPosition * 2;
            item.EstimatedLoadOutDate = DateTime.UtcNow.AddDays(item.EstimatedWaitDays);
        }
        return Task.FromResult(item ?? throw new InvalidOperationException("Queue item not found"));
    }

    public Task<LoadOutQueue> ScheduleLoadOutAsync(string queueNumber, DateTime scheduledDate, string timeSlot)
    {
        var item = _queue.FirstOrDefault(q => q.QueueNumber == queueNumber);
        if (item != null)
        {
            item.ScheduledDate = scheduledDate;
            item.ScheduledTimeSlot = timeSlot;
            item.Status = "Scheduled";
        }
        return Task.FromResult(item ?? throw new InvalidOperationException("Queue item not found"));
    }

    public Task<LoadOutQueue> CompleteLoadOutAsync(string queueNumber, decimal actualQuantity)
    {
        var item = _queue.FirstOrDefault(q => q.QueueNumber == queueNumber);
        if (item != null)
        {
            item.ActualLoadOutDate = DateTime.UtcNow;
            item.ActualLoadedQuantity = actualQuantity;
            item.Status = "Completed";
        }
        return Task.FromResult(item ?? throw new InvalidOperationException("Queue item not found"));
    }

    public Task<LoadOutQueue> CancelQueueRequestAsync(string queueNumber, string reason)
    {
        var item = _queue.FirstOrDefault(q => q.QueueNumber == queueNumber);
        if (item != null)
        {
            item.Status = "Cancelled";
            item.CancellationReason = reason;
        }
        return Task.FromResult(item ?? throw new InvalidOperationException("Queue item not found"));
    }

    public Task<int> GetEstimatedWaitDaysAsync(string warehouseId)
    {
        var queueLength = _queue.Count(q => q.WarehouseId == warehouseId && q.Status == "Queued");
        return Task.FromResult(queueLength * 2);
    }

    public Task<List<LoadOutQueue>> GetScheduledLoadOutsAsync(string warehouseId, DateTime date)
        => Task.FromResult(_queue.Where(q => q.WarehouseId == warehouseId && q.ScheduledDate?.Date == date.Date).ToList());
}

/// <summary>
/// Mock implementation of storage fee service.
/// </summary>
public class MockStorageFeeService : IStorageFeeService
{
    private readonly List<StorageFee> _invoices = new();

    public Task<StorageFee?> GetStorageFeeAsync(string invoiceNumber)
        => Task.FromResult(_invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber));

    public Task<List<StorageFee>> GetStorageFeesByHolderAsync(string warrantHolderId)
        => Task.FromResult(_invoices.Where(i => i.WarrantHolderId == warrantHolderId).ToList());

    public Task<List<StorageFee>> GenerateMonthlyInvoicesAsync(string billingPeriod)
    {
        var invoice = new StorageFee
        {
            Id = Guid.NewGuid().ToString(),
            InvoiceNumber = $"STG-{billingPeriod}-001",
            BillingPeriod = billingPeriod,
            WarehouseId = "WH-001",
            WarehouseName = "Kitwe Copper Warehouse",
            WarrantHolderId = "HOLDER-001",
            WarrantHolderName = "Glencore International",
            TotalQuantity = 500,
            TotalDays = 30,
            RatePerTonPerDay = 0.50m,
            SubTotal = 7500m,
            HandlingFee = 250m,
            InsuranceFee = 125m,
            GrossAmount = 7875m,
            NetAmount = 7875m,
            InvoiceDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            PaymentStatus = "Unpaid"
        };
        _invoices.Add(invoice);
        return Task.FromResult(new List<StorageFee> { invoice });
    }

    public Task<StorageFee> CalculateStorageFeeAsync(string warehouseId, string warrantHolderId, string billingPeriod)
    {
        return Task.FromResult(new StorageFee
        {
            TotalQuantity = 100,
            TotalDays = 30,
            RatePerTonPerDay = 0.50m,
            SubTotal = 1500m,
            NetAmount = 1500m
        });
    }

    public Task<StorageFee> RecordPaymentAsync(string invoiceNumber, decimal amount, string reference)
    {
        var invoice = _invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
        if (invoice != null)
        {
            invoice.AmountPaid = (invoice.AmountPaid ?? 0) + amount;
            invoice.PaymentDate = DateTime.UtcNow;
            invoice.PaymentReference = reference;
            invoice.PaymentStatus = invoice.AmountPaid >= invoice.NetAmount ? "Paid" : "PartiallyPaid";
        }
        return Task.FromResult(invoice ?? throw new InvalidOperationException("Invoice not found"));
    }

    public Task<List<StorageFee>> GetOverdueInvoicesAsync()
        => Task.FromResult(_invoices.Where(i => i.DueDate < DateTime.UtcNow && i.PaymentStatus != "Paid").ToList());

    public Task<decimal> GetStorageRateAsync(string warehouseId, MetalType metalType)
        => Task.FromResult(0.50m); // $0.50 per metric ton per day

    public Task ApplyLateFeeAsync(string invoiceNumber)
    {
        var invoice = _invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNumber);
        if (invoice != null && invoice.DueDate < DateTime.UtcNow)
        {
            invoice.IsOverdue = true;
            invoice.DaysOverdue = (DateTime.UtcNow - invoice.DueDate).Days;
            invoice.LateFee = invoice.NetAmount * 0.02m; // 2% late fee
        }
        return Task.CompletedTask;
    }
}
