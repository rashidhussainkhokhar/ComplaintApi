using Application.Dtos.Complaint;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ComplaintService : IComplaintService
    {
        IRepository<Complaint> _complaintRepository;
        IMapper _mapper;
        public ComplaintService(IRepository<Complaint> complaintRepository, IMapper mapper)
        {
            _complaintRepository = complaintRepository;
            _mapper = mapper;
        }
        public async Task<Complaint> Create(ComplaintDto complaintDto)
        {
            var complaint = _mapper.Map<Complaint>(complaintDto);

            if (string.IsNullOrWhiteSpace(complaintDto.ComplaintType))
                throw new AppException("ComplaintType is required");
            if (string.IsNullOrWhiteSpace(complaintDto.ComplaintDescription))
                throw new AppException("ComplaintDescription is required");
            if (string.IsNullOrWhiteSpace(complaintDto.CustomerName))
                throw new AppException("CustomerName is required");
            if (string.IsNullOrWhiteSpace(complaintDto.CustomerContact))
                throw new AppException("CustomerContact is required");
            if (string.IsNullOrWhiteSpace(complaintDto.DateOfComplaint))
                throw new AppException("DateOfComplaint is required");

            await _complaintRepository.AddAsync(complaint);
            await _complaintRepository.Complete();
            return complaint;
        }

        public async void Delete(int id)
        {
            var complaint = await _complaintRepository.GetAsync(id);
            if (complaint != null)
            {
                await _complaintRepository.DeleteAsync(id);
                await _complaintRepository.Complete();
            }
        }

        public async Task<IEnumerable<Complaint>> GetAll()
        {
            var complaint = await _complaintRepository.GetAllListAsync(u => true);
            return complaint;
        }

        public async Task<Complaint> GetById(int id)
        {
            return await _complaintRepository.GetAsync(id);
        }

        public async void Update(UpdateComplaintDto complaintDto)
        {
            var complaint = await _complaintRepository.GetAsync(complaintDto.Id);

            if (complaint == null)
                throw new AppException("Complaint not found.");

            //update Values
            complaint.CustomerName = complaintDto.CustomerName;
            complaint.CustomerContact = complaintDto.CustomerContact;
            complaint.CustomerCity = complaintDto.CustomerCity;
            complaint.ComplaintType = complaintDto.ComplaintType;
            complaint.ComplaintDescription = complaintDto.ComplaintDescription;
            complaint.Status = complaintDto.Status;
            complaint.NameOfTechnician = complaintDto.NameOfTechnician;

            _complaintRepository.Update(complaint);
            await _complaintRepository.Complete();
        }
    }
}
