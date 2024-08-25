using Microsoft.AspNetCore.SignalR;
using SignalRBlazorSSR.Data;
using SignalRBlazorSSR.Services;

namespace SignalRBlazorSSR.Hubs;

public class EmployeeHub : Hub
{
    public async Task RefreshEmployees(List<Employee> employees, int count)
    {
        await Clients.All.SendAsync("RefreshEmployees", employees, count);
    }
}
