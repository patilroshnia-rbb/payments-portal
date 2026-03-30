# 💳 Payments Portal

A full-stack **Payments Management Application** built using **Angular + .NET Core + SQL Server** with idempotent API design and clean architecture.

---

## 🚀 Tech Stack

### Frontend

* Angular (Standalone + Reactive Forms)
* TypeScript
* HTML/CSS

### Backend

* .NET 8 Web API
* Entity Framework Core


### Database

* SQL Server

---

## ✨ Features

* ✅ Create Payment
* ✅ Update Payment
* ✅ Delete Payment
* ✅ View Payments List
* ✅ Idempotent API (No duplicate transactions)
* ✅ Auto Reference Generator (PAY-YYYYMMDD-XXXX)


---

## 🔁 Sample Flow

1. Add USD 100
   → Generates: `PAY-20250910-0001`

2. Resubmit same `clientRequestId`
   → Returns same record (No duplicate)

3. Add EUR 250
   → Generates: `PAY-20250910-0002`

4. Edit/Delete as needed

---

## 🧠 Architecture

```text
Angular UI → .NET API → Service Layer → EF Core → SQL Server
```

* Controller → Handles HTTP requests
* Service → Business logic (idempotency, reference generation)
* DB → Stores transactions with unique constraint

---

## 🔐 Idempotency Design

* Each request includes a **ClientRequestId**
* Duplicate requests return the **same response**
* Prevents duplicate payments

---

## 🗄️ Database Schema

```sql
CREATE TABLE Payments (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Reference NVARCHAR(50),
    Amount DECIMAL(18,2),
    Currency NVARCHAR(10),
    CreatedAt DATETIME2,
    ClientRequestId NVARCHAR(100) UNIQUE
);
```

---

## ▶️ How to Run

### 🔹 Backend (.NET)

```bash
cd backend/PaymentsApi
dotnet run
```

API will run at:

```
https://localhost:7191
```

Swagger:

```
https://localhost:7191/swagger
```

---

### 🔹 Frontend (Angular)

```bash
cd frontend/payments-ui
ng serve
```

App will run at:

```
http://localhost:4200
```

---

## ⚙️ Configuration

### appsettings.json

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PaymentsDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## 📸 Screenshots

### Swagger API

<img width="1695" height="1027" alt="image" src="https://github.com/user-attachments/assets/006b2844-510c-4aae-9c98-cf616483af23" />


### UI

<img width="1859" height="694" alt="image" src="https://github.com/user-attachments/assets/f77def45-7bc1-4646-8688-0cdca9e6c08e" />

---

## 🧪 Validation

### Backend (FluentValidation)

* Amount > 0
* Currency must be valid
* ClientRequestId required

### Frontend (Angular)

* Required fields
* Amount > 0 validation

---

## 👩‍💻 Author

**Roshni Bhangale**



Give it a ⭐ on GitHub!
