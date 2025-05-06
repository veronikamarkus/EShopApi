# E-Shop API

An ASP.NET Core Web API for managing products and orders in a small e-shop system, built with clean architecture, SOLID principles, and Docker-based deployment.

### Product API

- `GET /products` — List all products  
- `GET /products/{id}` — Get product by ID  
- `POST /products` — Create a new product  
- `PUT /products/{id}` — Update an existing product  
- `DELETE /products/{id}` — Delete a product  

### Order API

- `GET /orders` — List all orders  
- `GET /orders/{id}` — Get order by ID  
- `POST /orders` — Create a new order (with one or more products)  
- `DELETE /orders/{id}` — Cancel an order  

## Tech Stack

- ASP.NET Core 9.0
- PostgreSQL
- Entity Framework Core
- Docker + Docker Compose

## Running the Project with Docker

### 1. Clone the repository

```bash
git clone https://github.com/veronikamarkus/EshopApi.git
cd EshopApi
```

### 2. Create a .env file

```bash
cp .env.example .env
```

### 3. Build and run with Docker Compose
```bash
docker-compose up --build
```

### Ports Used in Docker
- API: http://localhost:8080
- PostgreSQL: localhost:5432

## Switching Cache

In appsettings.json file, set cache type to either "InMemory" or "FakeRedis":

"Cache": {
  "Type": "InMemory" // or "FakeRedis"
}

## 4. Request examples

Get all orders

```bash
curl -X GET http://localhost:8080/api/orders
```

Get order by ID

```bash
curl -X GET http://localhost:8080/api/orders/{id}
```

Create order

```bash
curl -X POST http://localhost:8080/api/orders \
     -H "Content-Type: application/json" \
     -d '{
          "items": [
            { "productId": "8f205a8b-99ac-4f82-a1b1-37c9b92ea31a", "quantity": 2 },
            { "productId": "6ed52c98-3211-4325-a78f-04a68a4e63c3", "quantity": 1 }
          ]
        }'
```

Delete order

```bash
curl -X DELETE http://localhost:8080/api/orders/{id}
```

Get all products

```bash
curl -X GET http://localhost:8080/api/products
```

Get product by Id

```bash
curl -X GET http://localhost:8080/api/products/{id}
```

Create product

```bash
curl -X POST http://localhost:8080/api/products \
     -H "Content-Type: application/json" \
     -d '{
          "name": "New Product",
          "price": 19.99
        }'
```

Update product

```bash
curl -X PUT http://localhost:8080/api/products/{id} \
     -H "Content-Type: application/json" \
     -d '{
          "id": "{id}",
          "name": "Updated Product Name",
          "price": 25.99
        }'
```

Delete product

```bash
curl -X DELETE http://localhost:8080/api/products/{id}

```
