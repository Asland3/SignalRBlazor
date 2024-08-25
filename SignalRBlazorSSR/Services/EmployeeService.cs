using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRBlazorSSR.Data;
using SignalRBlazorSSR.Hubs;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace SignalRBlazorSSR.Services;

public class EmployeeService
{
    private readonly IHubContext<EmployeeHub> _context;
    private AppDbContext _dbContext = new AppDbContext();
    private readonly SqlTableDependency<Employee> _dependency;
    private readonly string _connectionString;

    public EmployeeService(IHubContext<EmployeeHub> context)
    {
        _context = context;
        _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=SignalRBlazorSSR;Trusted_Connection=True;MultipleActiveResultSets=true";
        _dependency = new SqlTableDependency<Employee>(_connectionString, "Employees");
        _dependency.OnChanged += DependencyOnChanged;
        _dependency.Start();
    }

    private async void DependencyOnChanged(object sender, RecordChangedEventArgs<Employee> e)
    {
        var employees = await GetAllGetEmployees();
        var count = await CountEmployees();
        await _context.Clients.All.SendAsync("RefreshEmployees", employees, count);
    }

    public async Task<List<Employee>> GetAllGetEmployees()
    {
        return await _dbContext.Employees.AsNoTracking().ToListAsync();
    }

    public async Task<int> CountEmployees()
    {
        return await _dbContext.Employees.CountAsync();
    }

    // public async Task<Employee> GetEmployee(int id)
    // {
    //     return await _context.Employees.FindAsync(id);
    // }
    //
    // public async Task<Employee> AddEmployee(Employee employee)
    // {
    //     _context.Employees.Add(employee);
    //     await _context.SaveChangesAsync();
    //     return employee;
    // }
    //
    // public async Task<Employee> UpdateEmployee(Employee employee)
    // {
    //     _context.Entry(employee).State = EntityState.Modified;
    //     await _context.SaveChangesAsync();
    //     return employee;
    // }
    //
    // public async Task<Employee> DeleteEmployee(int id)
    // {
    //     var employee = await _context.Employees.FindAsync(id);
    //     if (employee == null)
    //     {
    //         return null;
    //     }
    //
    //     _context.Employees.Remove(employee);
    //     await _context.SaveChangesAsync();
    //     return employee;
    // }
}
