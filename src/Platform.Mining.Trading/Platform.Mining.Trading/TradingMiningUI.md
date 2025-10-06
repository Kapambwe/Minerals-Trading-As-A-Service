2 — Frontend trader-facing screens

These are the UI screens traders and brokers use daily.

2.1 Trading Dashboard (home)

Purpose: single-pane view of account, markets, news and alerts.
Widgets / fields: account balances (ZMW, USD), margin summary, top-of-book prices, personal P/L, open orders, open positions, alerts, trade blotter snapshot, news/ticker.
Actions: quick order entry, cancel all, view details, open deeper screens.
Roles: Trader, Broker.

2.2 Order Entry / Ticket

Purpose: submit new orders (limit, market, IOC, FOK, iceberg, block, auction entry).
Fields: instrument (mineral type, grade), contract month, direction (buy/sell), quantity (tonnes), unit price or price formula, order type, time-in-force, counterparty (if voice/broker-only), account, settlement instruction, delivery option (cash/physical), special instructions (quality tolerance).
Actions: validate (margin check), preview fees, place, amend, save template.
Roles: Trader, Broker, Market Maker.

2.3 Order Book (personal + market)

Purpose: show live orders — both user’s orders and full market depth (subject to permissions).
Views: personal orders grid, market depth ladder (price ladder), aggregated/individual orders.
Fields: order id, qty, filled, remaining, price, time, status, venue.
Actions: modify, cancel, split, attach algorithmic execution.
Roles: Trader, Market Maker.

2.4 Market Data / Price Ladder / Level 2

Purpose: live L1/L2/L3 data for instruments.
Components: price chart, order book ladder, last trades tape, implied pricing (spreads), historical time-series.
Actions: subscribe/unsubscribe, export, set alerts.
Roles: All market participants and data subscribers.

2.5 Trade Blotter / Trade History

Purpose: list executed trades with full detail for reconciliation.
Fields: trade id, buy/sell, qty, price, time, counterparty, clearing reference, fees, net value, settlement date, status.
Actions: download (CSV/PDF), request confirmation, dispute, view audit trail.
Roles: Trader, Broker, Clearing.

2.6 Positions & Portfolio

Purpose: aggregated positions by instrument, by account, P/L, margin usage.
Fields: instrument, open qty, average price, MTM, unrealized/realized P/L, realized P/L by date, required margin, collateral posted.
Actions: request margin call, post collateral, transfer positions (if allowed).
Roles: Trader, Risk.

2.7 Margin / Collateral Management

Purpose: show initial/variation margin, calls, collateral schedule, haircut rules.
Fields: margin requirement, current collateral, margin shortfall, eligible collateral types, collateral valuation.
Actions: post collateral, withdraw collateral, accept margin call.
Roles: Trader, Clearing, Risk.

2.8 Auction & LME-style Open Outcry / Electronic Auction UI

Purpose: place bids/offers during auctions or scheduled rings.
Fields: auction schedule, auction instruments, pre- and post-auction order lists, ring participants.
Actions: participate in auction, reveal/iceberg options, commit trade.
Roles: Traders, Market Operations.

2.9 Physical Delivery / Warehouse Receipts

Purpose: initiate/accept delivery, manage warehouse receipts and QC certificates.
Fields: warehouse id, receipt id, grade/specs, weight, storage dates, owner, lien status, quality report, delivery window.
Actions: request pickup/delivery, lock receipt, release receipt, dispute QC.
Roles: Warehouse Manager, Trader, Clearing.

2.10 Confirmations & Notifications

Purpose: show trade confirmations, emails/SMS notifications, regulatory reports.
Fields: confirmation docs (PDF), timestamps, counterparties, settlement instructions.
Actions: accept/acknowledge, print, archive.
Roles: Trader, Broker, Clearing, Regulator.

2.11 Research & Reporting (for traders)

Purpose: market analytics — supply/demand, inventory, seasonality, price models.
Widgets: charts, downloadable reports, volatility surfaces, implied forward curves.
Roles: Trader, Market Maker.