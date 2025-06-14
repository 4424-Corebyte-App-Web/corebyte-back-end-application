-- Insert test data for batch management
USE BatchDB;

-- Clear existing data (if any)
DELETE FROM Batches;

-- Insert test records that SHOULD appear in the query results
INSERT INTO Batches (Id, Name, Type, Status, Temperature, Amount, Total, Date, NLote)
VALUES 
-- Records that match the query criteria (Invoice, Pending, within date range)
(UUID(), 'Test Invoice 1', 'Invoice', 'Pending', '20C', '100', 1500.50, '2025-06-05T10:00:00', 'LOT001'),
(UUID(), 'Test Invoice 2', 'Invoice', 'Pending', '22C', '50', 750.25, '2025-06-08T14:30:00', 'LOT002'),
(UUID(), 'Test Invoice 3', 'Invoice', 'Pending', '21C', '75', 1125.75, '2025-06-03T09:15:00', 'LOT003'),

-- Records that should NOT appear (different dates, types, or status)
(UUID(), 'Outside Date Range', 'Invoice', 'Pending', '20C', '100', 1500.50, '2025-05-30T10:00:00', 'LOT004'),
(UUID(), 'Different Type', 'Receipt', 'Pending', '21C', '60', 900.00, '2025-06-05T10:00:00', 'LOT005'),
(UUID(), 'Different Status', 'Invoice', 'Completed', '22C', '80', 1200.00, '2025-06-07T10:00:00', 'LOT006'),

-- Edge case records
(UUID(), 'Start Date Edge', 'Invoice', 'Pending', '20C', '90', 1350.00, '2025-06-01T00:00:01', 'LOT007'),
(UUID(), 'End Date Edge', 'Invoice', 'Pending', '21C', '110', 1650.00, '2025-06-10T23:59:58', 'LOT008');

