using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public Customer Customer { get; set; }
        public string FormActionName { get; set; }
        public List<MembershipType> MembershipTypes { get; set; }
    }
}