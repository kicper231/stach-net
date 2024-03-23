//namespace Infrastructure
//{
//    public class FakeCustomerRepository : ICustomerRepository
//    {
//        private Dictionary<int, Customer> customers;

// public FakeCustomerRepository() { var customers = new List<Customer> { new Customer { Id = 1,
// FirstName = "John", LastName = "Smith"}, new Customer { Id = 2, FirstName = "Bob", LastName =
// "Smith"}, new Customer { Id = 3, FirstName = "Kate", LastName = "Smith"}, };

// this.customers = customers.ToDictionary(p=>p.Id); }

// public List<Customer> GetAll() { return customers.Values.ToList(); }

// public Customer GetById(int id) { customers.TryGetValue(id, out var customer); return customer; }

// public void Add(Customer customer) { if (customers.ContainsKey(customer.Id)) { throw new
// InvalidOperationException("Klient o tym ID ju¿ istnieje."); }

// customers[customer.Id] = customer; } }

//}