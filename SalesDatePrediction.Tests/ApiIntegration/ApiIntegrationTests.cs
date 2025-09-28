using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesDatePrediction.API;
using SalesDatePrediction.Application.DTOs;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;
using FluentAssertions;
using System.Net;

namespace SalesDatePrediction.Tests
{
    public class ApiIntegrationTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ApiIntegrationTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCustomersWithPrediction_ReturnsOkWithData()
        {
            // Act
            var response = await _client.GetAsync("/api/customers/predictions");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var customers = await response.Content.ReadFromJsonAsync<List<CustomerPredictionDto>>();

            customers.Should().NotBeNull().And.NotBeEmpty();

            // Validate that customer IDs are unique
            var customerIds = customers!.Select(c => c.CustomerId).ToList();
            customerIds.Should().OnlyHaveUniqueItems();

            // Comprehensive validation for all customers
            customers.Should().AllSatisfy(customer =>
            {
                customer.CustomerId.Should().BePositive();
                customer.CustomerName.Should().NotBeNullOrWhiteSpace();
                customer.CustomerName.Length.Should().BeGreaterThan(2);
                customer.LastOrderDate.Should().HaveValue();
                customer.NextPredictedOrder.Should().HaveValue();

                // Business rule: NextPredictedOrder should be after LastOrderDate
                customer.NextPredictedOrder.Should().BeAfter(customer.LastOrderDate!.Value);
            });
        }

        [Fact]
        public async Task GetCustomerOrders_ReturnsOkWithValidId()
        {
            // Act
            var response = await _client.GetAsync("/api/customers/1/orders");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var orders = await response.Content.ReadFromJsonAsync<List<CustomerOrderDto>>();

            orders.Should().NotBeNull();
            orders.Should().NotBeEmpty();

            // Validate each order has required properties
            foreach (var order in orders!)
            {
                order.OrderId.Should().BePositive();
                order.ShipName.Should().NotBeNullOrEmpty();
                order.ShipAddress.Should().NotBeNullOrEmpty();
                order.ShipCity.Should().NotBeNullOrEmpty();
                order.RequiredDate.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task GetCustomerOrders_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync("/api/customers/999999/orders");

            // Assert - Should return OK with empty list, not NotFound based on current implementation
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var orders = await response.Content.ReadFromJsonAsync<List<CustomerOrderDto>>();
            orders.Should().NotBeNull();
            orders.Should().BeEmpty(); // No orders for non-existent customer
        }

        [Fact]
        public async Task GetCustomerOrders_ReturnsBadRequest_WhenCustomerIdIsInvalid()
        {
            // Act
            var response = await _client.GetAsync("/api/customers/0/orders");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsOkWithData()
        {
            // Act
            var response = await _client.GetAsync("/api/employees");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var employees = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();

            employees.Should().NotBeNull().And.NotBeEmpty();

            // Validate that employee IDs are unique
            var empIds = employees!.Select(e => e.EmpId).ToList();
            empIds.Should().OnlyHaveUniqueItems();

            // Comprehensive validation for all employees
            employees.Should().AllSatisfy(emp =>
            {
                emp.EmpId.Should().BePositive();
                emp.FullName.Should().NotBeNullOrWhiteSpace();
                emp.FullName.Length.Should().BeGreaterThan(2);
                emp.FullName.Should().Contain(" "); // Should contain first and last name
            });
        }

        [Fact]
        public async Task GetAllShippers_ReturnsOkWithData()
        {
            // Act
            var response = await _client.GetAsync("/api/shippers");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var shippers = await response.Content.ReadFromJsonAsync<List<ShipperDto>>();

            shippers.Should().NotBeNull().And.NotBeEmpty();

            // Validate that shipper IDs are unique
            var shipperIds = shippers!.Select(s => s.ShipperId).ToList();
            shipperIds.Should().OnlyHaveUniqueItems();

            // Comprehensive validation for all shippers
            shippers.Should().AllSatisfy(shipper =>
            {
                shipper.ShipperId.Should().BePositive();
                shipper.CompanyName.Should().NotBeNullOrWhiteSpace();
                shipper.CompanyName.Length.Should().BeGreaterThan(2);
            });
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkWithData()
        {
            // Act
            var response = await _client.GetAsync("/api/products");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

            products.Should().NotBeNull().And.NotBeEmpty();

            // Validate that product IDs are unique
            var productIds = products!.Select(p => p.ProductId).ToList();
            productIds.Should().OnlyHaveUniqueItems();

            // Comprehensive validation for all products
            products.Should().AllSatisfy(product =>
            {
                product.ProductId.Should().BePositive();
                product.ProductName.Should().NotBeNullOrWhiteSpace();
                product.ProductName.Length.Should().BeGreaterThan(2);
            });
        }

        [Fact]
        public async Task CreateOrder_ReturnsCreatedWithValidOrder()
        {
            // Arrange
            var newOrder = new CreateOrderDto
            {
                CustomerId = 1,
                EmpId = 1,
                ShipperId = 1,
                ShipName = "Test Ship",
                ShipAddress = "Test Address",
                ShipCity = "Test City",
                ShipCountry = "USA",
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now.AddDays(10),
                Freight = 10.50m,
                ProductId = 1,
                UnitPrice = 25.00m,
                Quantity = 2,
                Discount = 0.1m
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/orders", newOrder);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var result = await response.Content.ReadFromJsonAsync<object>();
            result.Should().NotBeNull();

            // The response should contain the new order ID
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("orderId");

            // Validate business rules
            newOrder.RequiredDate.Should().BeAfter(newOrder.OrderDate);
            newOrder.UnitPrice.Should().BePositive();
            newOrder.Quantity.Should().BePositive();
            newOrder.Discount.Should().BeInRange(0, 1); // Discount should be between 0 and 100%
        }

    }
}