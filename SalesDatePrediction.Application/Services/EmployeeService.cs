using Microsoft.Extensions.Logging;
using SalesDatePrediction.Application.DTOs;
using SalesDatePrediction.Application.Interfaces;

namespace SalesDatePrediction.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            try
            {
                _logger.LogInformation("Getting all employees");

                var employees = await _employeeRepository.GetAllEmployeesAsync();

                // Mapear entidades a DTOs
                var employeeDtos = employees.Select(e => new EmployeeDto
                {
                    EmpId = e.Empid,
                    FullName = e.FullName
                });

                _logger.LogInformation("Retrieved {Count} employees", employeeDtos.Count());

                return employeeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all employees");
                throw;
            }
        }
    }
}