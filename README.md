# 🌐 Sales Date Prediction API

Web API desarrollada en .NET 8 con **Clean Architecture** que proporciona servicios RESTful para el sistema de predicción de fechas de ventas basado en la base de datos StoreSample.

## 🏗️ Arquitectura Clean Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    SalesDatePrediction.API                  │ ◄─── Presentation Layer
│                  (Controllers + Program.cs)                 │
├─────────────────────────────────────────────────────────────┤
│               SalesDatePrediction.Application               │ ◄─── Application Layer
│              (Services, DTOs, Interfaces)                   │
├─────────────────────────────────────────────────────────────┤
│                SalesDatePrediction.Domain                   │ ◄─── Domain Layer
│                     (Entities)                              │
├─────────────────────────────────────────────────────────────┤
│             SalesDatePrediction.Infrastructure              │ ◄─── Infrastructure Layer
│              (Repositories, Data Context)                   │
└─────────────────────────────────────────────────────────────┘
```

## 📋 Prerrequisitos

- .NET 8.0 SDK
- SQL Server 2019+ o SQL Server Express
- Visual Studio 2022 o VS Code
- Base de datos StoreSample configurada

## 🚀 Instalación y Configuración

### 1. Clonar el repositorio
```bash
git clone [URL-del-repositorio]
cd SalesDatePrediction
```

### 2. Configurar la cadena de conexión
```json
// SalesDatePrediction.API/appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=StoreSample;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### 3. Restaurar paquetes para toda la solución
```bash
dotnet restore SalesDatePrediction.sln
```

### 4. Ejecutar la aplicación
```bash
cd SalesDatePrediction.API
dotnet run
```

La API estará disponible en: `https://localhost:7280` (HTTPS) o `http://localhost:5276` (HTTP)

## 📁 Estructura Real del Proyecto

```
SalesDatePrediction/
├── SalesDatePrediction.API/                    # 🎯 Presentation Layer
│   ├── Controllers/
│   │   ├── CustomersController.cs              # Predicciones y órdenes por cliente
│   │   ├── EmployeesController.cs              # Catálogo de empleados
│   │   ├── OrdersController.cs                 # Creación de órdenes
│   │   ├── ProductsController.cs               # Catálogo de productos
│   │   └── ShippersController.cs               # Catálogo de transportistas
│   ├── Program.cs                              # Configuración DI y middleware
│   ├── appsettings.json                        # Configuración principal
│   ├── appsettings.Testing.json                # Configuración para tests
│   └── SalesDatePrediction.API.csproj
│
├── SalesDatePrediction.Application/            # 🧠 Application Layer
│   ├── DTOs/
│   │   ├── CreateOrderDto.cs                   # DTO para crear órdenes
│   │   ├── CustomerPredictionDto.cs            # DTO de predicciones
│   │   ├── EmployeeDto.cs                      # DTO de empleados
│   │   ├── OrderDto.cs                         # DTO de órdenes
│   │   ├── ProductDto.cs                       # DTO de productos
│   │   └── ShipperDto.cs                       # DTO de transportistas
│   ├── Interfaces/                             # Contratos
│   │   ├── ICustomerRepository.cs
│   │   ├── ICustomerService.cs
│   │   ├── IEmployeeRepository.cs
│   │   ├── IEmployeeService.cs
│   │   ├── IOrderRepository.cs
│   │   ├── IOrderService.cs
│   │   ├── IProductRepository.cs
│   │   ├── IProductService.cs
│   │   ├── IShipperRepository.cs
│   │   └── IShipperService.cs
│   ├── Services/                               # Lógica de negocio
│   │   ├── CustomerService.cs
│   │   ├── EmployeeService.cs
│   │   ├── OrderService.cs
│   │   ├── ProductService.cs
│   │   └── ShipperService.cs
│   └── SalesDatePrediction.Application.csproj
│
├── SalesDatePrediction.Domain/                 # 💎 Domain Layer
│   ├── Entities/
│   │   ├── Category.cs                         # Categorías de productos
│   │   ├── CustOrder.cs                        # Vista de órdenes por cliente
│   │   ├── Customer.cs                         # Entidad principal de clientes
│   │   ├── CustomerPrediction.cs               # Vista de predicciones
│   │   ├── Employee.cs                         # Entidad de empleados
│   │   ├── GetEmployee.cs                      # Vista simplificada empleados
│   │   ├── GetProduct.cs                       # Vista simplificada productos
│   │   ├── GetShipper.cs                       # Vista simplificada transportistas
│   │   ├── Order.cs                            # Entidad principal de órdenes
│   │   ├── OrderDetail.cs                      # Detalles de órdenes
│   │   ├── OrderTotalsByYear.cs                # Vista totales por año
│   │   ├── OrderValue.cs                       # Vista valores de órdenes
│   │   ├── Product.cs                          # Entidad de productos
│   │   ├── Shipper.cs                          # Entidad de transportistas
│   │   └── Supplier.cs                         # Entidad de proveedores
│   └── SalesDatePrediction.Domain.csproj
│
├── SalesDatePrediction.Infrastructure/         # 🔧 Infrastructure Layer
│   ├── Data/
│   │   └── StoreSampleContext.cs               # DbContext con todas las entidades
│   ├── Repositories/
│   │   ├── CustomerRepository.cs               # Repositorio de clientes
│   │   ├── EmployeeRepository.cs               # Repositorio de empleados
│   │   ├── OrderRepository.cs                  # Repositorio de órdenes
│   │   ├── ProductRepository.cs                # Repositorio de productos
│   │   └── ShipperRepository.cs                # Repositorio de transportistas
│   └── SalesDatePrediction.Infrastructure.csproj
│
├── SalesDatePrediction.Tests/                  # 🧪 Testing Layer
│   ├── ApiIntegration/
│   │   └── ApiIntegrationTests.cs              # Tests end-to-end completos
│   ├── TestWebApplicationFactory.cs            # Factory para tests
│   └── SalesDatePrediction.Tests.csproj
│
└── SalesDatePrediction.sln                     # Solución principal
```

## 🔗 Endpoints API Implementados

### 👥 Customers Controller

#### `GET /api/customers/predictions`
**Función:** Obtiene todos los clientes con predicción de próxima compra usando la vista `CustomerPrediction`.

**Implementación:** Utiliza `StoreSampleContext.CustomerPredictions`

**Response:**
```json
[
  {
    "customerId": 1,
    "customerName": "Alfreds Futterkiste",
    "lastOrderDate": "2024-03-15T00:00:00",
    "nextPredictedOrder": "2024-04-20T00:00:00"
  }
]
```

#### `GET /api/customers/{customerId}/orders`
**Función:** Obtiene las órdenes de un cliente específico ordenadas por fecha descendente.

**Validaciones:** CustomerId debe ser > 0, retorna BadRequest si es inválido.

**Response:**
```json
[
  {
    "orderId": 10248,
    "requiredDate": "2024-03-25T00:00:00",
    "shippedDate": "2024-03-22T00:00:00",
    "shipName": "Vins et alcools Chevalier",
    "shipAddress": "59 rue de l'Abbaye",
    "shipCity": "Reims"
  }
]
```

### 👨‍💼 Employees Controller

#### `GET /api/employees`
**Función:** Obtiene todos los empleados usando la vista `GetEmployees`.

**Response:**
```json
[
  {
    "empId": 1,
    "fullName": "Nancy Davolio"
  }
]
```

### 🚚 Shippers Controller

#### `GET /api/shippers`
**Función:** Obtiene todos los transportistas usando la vista `GetShippers`.

**Response:**
```json
[
  {
    "shipperId": 1,
    "companyName": "Speedy Express"
  }
]
```

### 📦 Products Controller

#### `GET /api/products`
**Función:** Obtiene todos los productos usando la vista `GetProducts`.

**Response:**
```json
[
  {
    "productId": 1,
    "productName": "Chai"
  }
]
```

### 📋 Orders Controller

#### `POST /api/orders`
**Función:** Crea una nueva orden usando el stored procedure `sp_AddNewOrder`.

**Request Body (CreateOrderDto):**
```json
{
  "customerId": 1,
  "empId": 1,
  "shipperId": 1,
  "shipName": "Test Ship",
  "shipAddress": "Test Address",
  "shipCity": "Test City",
  "shipCountry": "USA",
  "orderDate": "2024-03-15T10:30:00",
  "requiredDate": "2024-03-25T10:30:00",
  "shippedDate": null,
  "freight": 10.50,
  "productId": 1,
  "unitPrice": 25.00,
  "quantity": 2,
  "discount": 0.1
}
```

**Validaciones implementadas:**
- Todos los campos requeridos tienen `[Required]`
- CustomerId, EmpId, ShipperId deben ser positivos
- UnitPrice y Quantity deben ser positivos
- Discount debe estar entre 0 y 1

## ⚙️ Configuración Real en Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

// Database Context
builder.Services.AddDbContext<StoreSampleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios (Infrastructure Layer)
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Servicios (Application Layer)
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IShipperService, ShipperService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// CORS para Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { } // Para testing
```

## 🗄️ Entity Framework - StoreSampleContext

El contexto mapea todas las entidades y vistas de la base de datos StoreSample:

### Tablas principales:
- **Customers** (Sales schema)
- **Orders** (Sales schema) 
- **OrderDetails** (Sales schema)
- **Employees** (HR schema)
- **Products** (Production schema)
- **Categories** (Production schema)
- **Suppliers** (Production schema)
- **Shippers** (Sales schema)

### Vistas utilizadas:
- **CustomerPrediction** (Sales schema) - Para predicciones
- **GetEmployees** (HR schema) - Empleados simplificados
- **GetShippers** (Sales schema) - Transportistas simplificados
- **GetProducts** (Production schema) - Productos simplificados
- **CustOrders** (Sales schema) - Órdenes por cliente
- **OrderValues** (Sales schema) - Valores de órdenes
- **OrderTotalsByYear** (Sales schema) - Totales anuales

## 🧪 Testing Configuración Real

### Archivos de test:
- **ApiIntegrationTests.cs**: Tests end-to-end con FluentAssertions
- **TestWebApplicationFactory.cs**: Factory para environment "Testing"

### Configuración de testing environment:
```json
// appsettings.Testing.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=StoreSample;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Ejecutar tests:
```bash
# Todos los tests
dotnet test

# Con cobertura
dotnet test --collect:"XPlat Code Coverage"

# Solo integración
dotnet test --filter "ApiIntegrationTests"
```

### Casos de test implementados:
- ✅ **GetCustomersWithPrediction**: Valida estructura y reglas de negocio
- ✅ **GetCustomerOrders**: Tests con IDs válidos e inválidos
- ✅ **GetAllEmployees**: Validación de estructura de empleados
- ✅ **GetAllShippers**: Validación de transportistas
- ✅ **GetAllProducts**: Validación de productos
- ✅ **CreateOrder**: Test completo de creación con validaciones

## 🔧 Dependencias Reales por Proyecto

### SalesDatePrediction.API
```xml
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.9" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
```

### SalesDatePrediction.Application
```xml
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.9" />
```

### SalesDatePrediction.Infrastructure
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.9" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.9" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.9" />
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.9" />
```

### SalesDatePrediction.Tests
```xml
<PackageReference Include="FluentAssertions" Version="8.7.0" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.20" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
<PackageReference Include="xunit" Version="2.9.3" />
<PackageReference Include="xunit.runner.visualstudio" Version="3.1.5" />
<PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.1" />
<PackageReference Include="coverlet.collector" Version="6.0.4" />
```

## 🏗️ Mapeo de Entidades a DTOs

### CustomerService - Predicciones:
```csharp
// Mapeo en CustomerService.GetCustomersWithPredictionAsync()
var customerDtos = customers.Select(c => new CustomerPredictionDto
{
    CustomerId = c.CustomerId,
    CustomerName = c.CustomerName,
    LastOrderDate = c.LastOrderDate,
    NextPredictedOrder = c.NextPredictedOrder,
});
```

### CustomerService - Órdenes:
```csharp
// Mapeo en CustomerService.GetCustomerOrdersAsync()
var orderDtos = orders.Select(o => new OrderDto
{
    OrderId = o.Orderid,
    RequiredDate = o.Requireddate,
    ShippedDate = o.Shippeddate,
    ShipName = o.Shipname,
    ShipAddress = o.Shipaddress,
    ShipCity = o.Shipcity
});
```

### OrderService - Crear orden:
```csharp
// Mapeo en OrderService.CreateOrderAsync()
var order = new Order
{
    Custid = createOrderDto.CustomerId,
    Empid = createOrderDto.EmpId,
    Shipperid = createOrderDto.ShipperId,
    // ... resto de propiedades
};

var orderDetail = new OrderDetail
{
    Productid = createOrderDto.ProductId,
    Unitprice = createOrderDto.UnitPrice,
    Qty = createOrderDto.Quantity,
    Discount = createOrderDto.Discount
};
```

## 🚀 Build y Despliegue

### Desarrollo:
```bash
cd SalesDatePrediction.API
dotnet run --environment Development
```

### Producción:
```bash
dotnet publish SalesDatePrediction.sln -c Release -o ./publish
cd publish
dotnet SalesDatePrediction.API.dll
```

## 📊 Swagger Documentation

Documentación interactiva disponible en:
- **Development**: `https://localhost:5001/swagger`
- **Configurado en**: `Program.cs` solo para environment Development

## 🔒 Manejo de Errores y Logging

### Implementado en todos los controladores:
```csharp
// Ejemplo de CustomerController
try
{
    var customers = await _customerService.GetCustomersWithPredictionAsync();
    return Ok(customers);
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error getting customers with prediction");
    return StatusCode(500, "Internal server error");
}
```

### Validaciones específicas:
- **ArgumentException**: Para parámetros inválidos (CustomerId <= 0)
- **ArgumentNullException**: Para objetos nulos
- **ModelState**: Validación automática de DTOs

## 📈 Características de la Arquitectura

### ✅ Principios SOLID implementados:
- **Single Responsibility**: Cada servicio y repositorio tiene una responsabilidad específica
- **Open/Closed**: Interfaces permiten extensión sin modificación
- **Liskov Substitution**: Implementaciones intercambiables
- **Interface Segregation**: Interfaces específicas por funcionalidad
- **Dependency Inversion**: Inyección de dependencias en Program.cs

### ✅ Clean Architecture benefits:
- **Testabilidad**: Factory pattern para integration tests
- **Mantenibilidad**: Separación clara de responsabilidades
- **Escalabilidad**: Fácil agregar nuevos endpoints y funcionalidades
- **Flexibilidad**: Cambiar implementaciones sin afectar otras capas

---

*API desarrollada con Clean Architecture, .NET 8, y Entity Framework Core 9.0*
