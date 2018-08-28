using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.DAL
{
    public interface IGenericRepository<DTO> where DTO : class
    {
        IEnumerable<DTO> ListAll();
        DTO Get(int entityId);

        void Save(DTO model);
        void Delete(int entityId);
    }
}
