using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryService.Identity.DAL.Entities
{
    public class DeliveryServiceRole : IdentityRole
    {
        public DeliveryServiceRole() : base() { }
        public DeliveryServiceRole(string name) : base(name) { }
    }
}