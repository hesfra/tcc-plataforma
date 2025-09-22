CREATE TABLE Products (
    id UUID PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    price NUMERIC(18,2) NOT NULL
);