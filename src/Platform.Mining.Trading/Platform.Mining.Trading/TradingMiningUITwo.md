3 — Frontend operator/compliance/admin screens
3.1 Market Operations Console

Purpose: control market state (open/close, declare auctions, halt trading), set circuit breakers.
Fields: current market status, scheduled events, pending halts, override logs.
Actions: start/stop session, schedule auction, emergency halt, replay market data.
Roles: Market Ops, Admin.

3.2 Surveillance / Market Abuse Monitoring

Purpose: detect spoofing, layering, wash trades, insider patterns.
Widgets: alert feed, suspicious-trade case management, automated pattern detection results, threshold tuning.
Actions: create case, assign investigator, freeze account, export evidence.
Roles: Compliance, Surveillance.

3.3 Clearing & Settlement Dashboard (backend UI)

Purpose: manage novation, settlement runs, netting, margin calls, default handling.
Fields: clearing members list, margin ledger, netting results, settlement obligations (per currency), scheduled settlements, outstanding fails.
Actions: initiate settlement, perform novation, issue margin call, close out default.
Roles: Clearing, Settlement.

3.4 Warehouse / Physical Ops Dashboard

Purpose: track inventories, QC, deliveries, reconciliations with trade records.
Fields: warehouse locations, totals by grade, receipts in/out, QC certificate repository, ownership ledger.
Actions: create receipt, attach QC document (upload), mark delivery complete, reconcile discrepancies.
Roles: Warehouse Manager, Ops, Clearing.

3.5 KYC / AML / User Onboarding

Purpose: manage participant onboarding and compliance checks.
Fields: entity profiles, docs (ID, incorporation, tax), risk scores, sanction lists check result, onboarding status.
Actions: approve/reject, request more docs, run PEP/sanctions checks, set account limits.
Roles: Compliance, Admin.

3.6 User Management & Permissions

Purpose: create users, roles, assign permissions, 2FA control.
Fields: user id, role, last login, MFA status, permitted instruments, API tokens.
Actions: create user, disable, reset password, grant/revoke permissions, issue API keys.
Roles: Admin, Security.

3.7 Audit Log & System Forensics

Purpose: immutable logs of actions, for regulator investigations.
Fields: timestamp, user, action, request/response payloads, session id.
Actions: search logs, export, attach to case.
Roles: Compliance, Admin, Regulator.

3.8 API & FIX Management Console

Purpose: manage external connectivity (FIX sessions, REST APIs, market data feeds).
Fields: session status, latency metrics, IP whitelists, message counts, sequence numbers.
Actions: reset sequence, throttle, create API key, manage entitlement.
Roles: Integration Engineer, Admin.

3.9 System Health / Monitoring (DevOps)

Purpose: realtime telemetry of matching engine, DB, network, job queues.
Widgets: CPU, memory, latency, throughput (tps), error rates, queue depths, SLA dashboards.
Actions: escalate alerts, restart services, view autoscale history.
Roles: DevOps, SRE, Admin.

3.10 Reconciliation & Finance

Purpose: reconcile trades to clearing, settlement, fees, invoicing, P&L close.
Fields: bank statements, internal ledger, settlement fails, fee schedules, FX rates.
Actions: run recon, flag mismatch, post journal, generate invoice, export for accounting.
Roles: Finance, Ops.