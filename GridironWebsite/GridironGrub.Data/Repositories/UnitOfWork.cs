using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridironGrub.Data.Repositories
{
    public class UnitOfWork
    {
        public CustomerRepository Customer { get; set; }
        public BraintreeRepository Payment { get; set; }
        public UserRepository User { get; set; }
        public AdminRepository Admin { get; set; }
        public VendorRepository Vendor { get; set; }
        public RunnerRepository Runner { get; set; }
        public ManagerRepository Manager { get; set; }

        public UnitOfWork()
        {
            Customer = new CustomerRepository();
            Payment = new BraintreeRepository();
            User = new UserRepository();
            Admin = new AdminRepository();
            Vendor = new VendorRepository();
            Runner = new RunnerRepository();
            Manager = new ManagerRepository();
        }
    }
}
