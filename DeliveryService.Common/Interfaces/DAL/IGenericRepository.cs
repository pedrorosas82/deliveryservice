using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.DAL
{
    public interface IGenericRepository<DTO> where DTO: class
    {
        DTO Get(int dtoId);
        IEnumerable<DTO> ListAll();
        void Save(DTO dto);
        void Delete(int dtoId);
    }
}
