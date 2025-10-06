4 — Backend / operator service screens (more technical)

These are technical UIs to support run-time operations.

4.1 Matching Engine Admin

Purpose: tuning parameters, restart, match queue inspection.
Fields: matching rules, tick size, contract specs, current queues, unmatched orders.
Actions: apply param update (with approval), replay trades, emergency stop.
Roles: SRE, Market Ops.

4.2 Rule & Product Definition Editor

Purpose: create/edit contract specs (grades, lot sizes, tick sizes, delivery months, expiry rules).
Fields: product id, commodity type, grade spec, contract size, pricing unit, delivery rules.
Actions: create versioned product, deprecate, preview effect on instruments.
Roles: Product Managers, Market Ops.

4.3 Settlement Engine / Payment Gateway Integration

Purpose: configure banking rails (RTGS, SWIFT, local banks), settlement runs.
Fields: bank endpoints, settlement batches, FX conversion rules, timeout/retry.
Actions: trigger settlement, rollback, push SWIFT file.
Roles: Clearing, Finance, Integration.

4.4 Backtesting / Simulation Lab

Purpose: simulate stress, test new rules, provide market replay for surveillance.
Fields: historical market data selector, test scenarios, participant simulation.
Actions: run simulation, view metrics, export results.
Roles: Risk, Dev, Product.