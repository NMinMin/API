using API.Entity;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AutoMapper;
using API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  //api/hello
  public class StudentController : ControllerBase
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public StudentController(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
    {
      if (pageNumber <= 0)
      {
        pageNumber = 1;
      }
      if (pageSize <= 0)
      {
        pageSize = 10;
      }
      var query = _context.Students
        .Include(s => s._class); //Lấy thông tin lớp học của mỗi sinh viên
      var totalItems = query.Count();//tổng số student
      var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);//tổng số trang
      var students = query
          .Skip((pageNumber - 1) * pageSize)//bỏ qua các phần tử của các trang trước
          .Take(pageSize)//lấy số phần tử của trang hiện tại
          .ToList();
      var result = _mapper.Map<List<StudentDto>>(students);//Sử dụng AutoMapper để map từ students sang StudentDto
      return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(StudentModel model)
    {
      var student = _mapper.Map<Student>(model);//Sử dụng AutoMapper để map từ model sang Student

      _context.Students.Add(student);
      _context.SaveChanges();

      var result = _mapper.Map<StudentDto>(student);//Sử dụng AutoMapper để map từ student sang StudentDto

      return CreatedAtAction(nameof(GetById), new { id = student.id }, result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var student = _context.Students.Find(id);
      if (student == null)
      {
        return NotFound();
      }
      var result1 = new
      {
        id = student.id,
        name = student.name,
        dateofbirth = student.dateofbirth.ToString("dd-MM-yyyy"),
        class_id = student.class_id,
        class_name = student._class.name
      };
      var result = _mapper.Map<StudentDto>(result1);//Sử dụng AutoMapper để map từ result1 sang StudentDto

      return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Student model)
    {
      var user = _context.Students.Find(id);
      if (user == null)
      {
        return NotFound();
      }
      user.name = model.name;
      user.dateofbirth = model.dateofbirth;
      user.class_id = model.class_id;
      user._class = model._class;
      _context.SaveChanges();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var user = _context.Students.Find(id);
      if (user == null)
      {
        return NotFound();
      }

      _context.Students.Remove(user);
      _context.SaveChanges();
      return NoContent();
    }
  }
}