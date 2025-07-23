# E-CommerceAPI
## Links
- [Swagger](https://ecommerceapp-76db.onrender.com/swagger)
- [Health Check](https://ecommerceapp-76db.onrender.com/healthz)

## Auth
**You can use this login request:**
```json
{
  "username": "burakani2",
  "password": "123321"
}
```

### POST /auth/register
Register a new user

**Request:**
```json
{
  "username": "testuser",
  "password": "123456"
}
```

**Response:**
```json
"Registration successful."
```

---

### POST /auth/login
Authenticate and get JWT token

**Request:**
```json
{
  "username": "testuser",
  "password": "123456"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
```

---

## Products

### GET /products  
Returns product list  
🛡 Requires JWT authentication

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": "1",
      "name": "Product A",
      "price": 100,
      ..
    }
  ]
}
```

---

## Orders

### POST api/orders/create  
Initiates a preorder request  
🛡 Requires JWT authentication

**Request:**
```json
{
  "userId": "ef95931b-6160-417f-9a5d-9a86be814ffc"
}
```

**Response:**
```json
{
  "orderId"
}
```

---

### POST api/orders/{orderId}/complete  
Completes the order  
🛡 Requires JWT authentication

**Request:**
```json
{
  "orderId": "b8a1b249-56d4-44ae-a837-b3176cbb9349"
}
```

**Response:**
```json
{
  OK
}
```

---

## Authorization Usage

After obtaining a JWT token, include it in request headers like this:

```
Authorization: Bearer <token>
```

---

## Sample User Entity

```json
{
  "id": "guid",
  "username": "string",
  "passwordHash": "byte[]",
  "passwordSalt": "byte[]",
  "createdAt": "datetime"
}
```
