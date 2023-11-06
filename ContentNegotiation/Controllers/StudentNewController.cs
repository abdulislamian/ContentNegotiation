using AutoMapper;
using ContentNegotiation.Data;
using ContentNegotiation.Models;
using ContentNegotiation.Models.DTO;
using ContentNegotiation.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace JWTAuthentication.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class StudentNewController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StudentNewController(ApplicationDbContext _dbContext, IStudentRepository studentRepository, IMapper mapper,
             IHttpContextAccessor _httpContextAccessor)
        {
            dbContext = _dbContext;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            //_userManager = userManager;
            httpContextAccessor = _httpContextAccessor;
        }

        #region GellAllStudent
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await studentRepository.GetAllAsync();

            //Populate DTO with Domain Model (Student)
            var studentDTO = mapper.Map<List<StudentDTO>>(students);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Ok(studentDTO);
            }
            else
            {
                return View("GetAll", studentDTO);
            }
        }
        #endregion

        #region GetStudentById
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var studentDomain = await studentRepository.GetByIdAsync(id);
            if (studentDomain == null)
            {
                return NotFound();
            }

            var studentDTO = mapper.Map<StudentDTO>(studentDomain);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Ok(studentDTO);
            }
            else
            {
                return View("Details", studentDTO);
            }
        }
        #endregion

        #region CreateStudent
        //POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestDto AddStudentRequestDto)
        {
            var studentDomainModel = mapper.Map<Student>(AddStudentRequestDto);

            //Use Domain Model to create Region
            studentDomainModel = await studentRepository.CreateAsync(studentDomainModel);

            //Convert Back Region to regionDTO
            var studentDTO = mapper.Map<StudentDTO>(studentDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = studentDTO.Id }, studentDTO);
        }
        #endregion

        #region UpdateStudent
        //PUT: https://localhost:7296/api/student/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStudentRequestDTO updateStudentRequestDTO)
        {
            //Map DTO to Domain Model
            var studentDomainModel = mapper.Map<Student>(updateStudentRequestDTO);
            await studentRepository.UpdateAsync(id, studentDomainModel);
            if (studentDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTO
            var studentDTO = mapper.Map<StudentDTO>(studentDomainModel);
            return Ok(studentDTO);
        }
        #endregion

        #region DeleteStudent
        //Delete : https://localhost:7296/api/student/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var studentDomainModel = await studentRepository.DeleteAsync(id);
            if (studentDomainModel == null)
            {
                return NotFound();
            }

            //If want you can also return deleted Object Back
            var regionDTO = mapper.Map<StudentDTO>(studentDomainModel);
            return Ok(regionDTO);

        }
        #endregion
    }
}
