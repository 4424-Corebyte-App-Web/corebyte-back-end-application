CREATE TABLE batches (
    id CHAR(36) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    type VARCHAR(100) NOT NULL,
    status VARCHAR(50) NOT NULL,
    temperature DOUBLE NOT NULL,
    amount VARCHAR(50) NOT NULL,
    total DECIMAL(15,2) NOT NULL,
    date DATETIME NOT NULL,
    nLote VARCHAR(50) NOT NULL
);
