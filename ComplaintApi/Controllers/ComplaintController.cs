using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Complaint;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        IComplaintService _complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ComplaintDto model)
        {
            try
            {
                var complaint = await _complaintService.Create(model);
                return Ok(new
                {
                    ComplaintId = complaint.Id,
                    message = "Complaint Registered Successfully."
                });

            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var complaint = await _complaintService.GetAll();
            return Ok(complaint);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var complaint = await _complaintService.GetById(id);
            return Ok(complaint);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateComplaintDto model)
        {
            try
            {
                _complaintService.Update(model);
                return Ok(new { message = "Complaint Updated Successfully." });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _complaintService.Delete(id);
            return Ok(new { message = "Complaint Deleted Successfully." });
        }
    }
}
