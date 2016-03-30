using HandymanTools.Common.Models;

namespace HandymanTools.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        int AddCustomer(Customer dto);

        Customer GetCustomer(int CustomerId); 
    }
}
