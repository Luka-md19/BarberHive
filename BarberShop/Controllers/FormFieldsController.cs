using AutoMapper;
using BarberShop.Contract;
using BarberShop.Exceptions;
using BarberShop.Models.FormFieldDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFieldsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFormFieldRepository _formFieldRepository;

        public FormFieldsController(IMapper mapper, IFormFieldRepository formFieldRepository)
        {
            _mapper = mapper;
            _formFieldRepository = formFieldRepository;
        }

        // GET: api/FormFields/GetAll
        [HttpGet("GetAll")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<IEnumerable<GetFormFieldDto>>> GetFormFields()
        {
            var formFields = await _formFieldRepository.GetAllAsync(Guid.NewGuid());
            if (formFields == null || !formFields.Any())
            {
                return NotFound("No form fields found.");
            }
            return Ok(formFields);
        }

        // GET: api/FormFields/5
        [HttpGet("{id}")]
        [AllowAnonymous] // This can be removed if authorization is required
        public async Task<ActionResult<GetFormFieldDto>> GetFormField(int id)
        {
            var formFieldDto = await _formFieldRepository.GetAsync(id, Guid.NewGuid());
            if (formFieldDto == null)
            {
                return NotFound($"No form field found with ID {id}.");
            }
            return Ok(formFieldDto);
        }

        // PUT: api/FormFields/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormField(int id, UpdateFormFieldDto updateFormFieldDto)
        {
            if (id != updateFormFieldDto.Id)
            {
                return BadRequest("Mismatched Form Field ID.");
            }

            try
            {
                await _formFieldRepository.UpdateAsync(id, updateFormFieldDto, Guid.NewGuid());
            }
            catch (NotFoundException)
            {
                return NotFound($"No form field found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the form field: " + ex.Message);
            }

            return NoContent();
        }

        // POST: api/FormFields
        [HttpPost]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<ActionResult<GetFormFieldDto>> PostFormField(CreateFormFieldDto createFormFieldDto)
        {
            var formFieldDto = await _formFieldRepository.AddAsync<CreateFormFieldDto, GetFormFieldDto>(createFormFieldDto, Guid.NewGuid());
            return CreatedAtAction(nameof(GetFormField), new { id = formFieldDto.Id }, formFieldDto);
        }

        // DELETE: api/FormFields/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")] // Uncomment if needed
        public async Task<IActionResult> DeleteFormField(int id)
        {
            try
            {
                await _formFieldRepository.DeleteAsync(id, Guid.NewGuid());
            }
            catch (NotFoundException)
            {
                return NotFound($"No form field found with ID {id}.");
            }

            return NoContent();
        }
    }
}
