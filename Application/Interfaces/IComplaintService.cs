using Application.Dtos.Complaint;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComplaintService
    {
        Task<Complaint> Create(ComplaintDto complaintDto);
        Task<IEnumerable<Complaint>> GetAll();
        Task<Complaint> GetById(int id);
        void Update(UpdateComplaintDto userUpdateDto);
        void Delete(int id);
    }
}
