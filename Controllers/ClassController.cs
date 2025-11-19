using API.Entity;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using API.Dtos;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ClassController : ControllerBase
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ClassController(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll(string pageNumber="1", string pageSize = "10")
    {
      int number, size;
      if (int.TryParse(pageNumber, out number) && int.TryParse(pageSize, out size))
      {
        if (number <= 0 || number > 100)
        {
          number = 1;
        }
        if (size <= 0|| size > 100)
        {
          size = 10;
        }
        var query = _context.Classes
         .Include(c => c.lst_student) //Lấy danh sách sinh viên của mỗi lớp
         .AsQueryable();//Tạo truy vấn IQueryable để có thể áp dụng phân trang

        var totalItems = query.Count();//tổng số class
        var totalPages = (int)Math.Ceiling(totalItems / (double)size);//tổng số trang

        var classes = query
            .Skip((number - 1) * size)//bỏ qua các phần tử của các trang trước
            .Take(size)//lấy số phần tử của trang hiện tại
            .ToList();
        var result = _mapper.Map<List<ClassDto>>(classes);//Sử dụng AutoMapper để map từ classes sang ClassDto
        return Ok(result);
      }
      else
      {
        return BadRequest("pageNumber và pageSize không hợp lệ.");
      }
    }

    [HttpPost]
    public IActionResult Create(CreateClassDto model)
    {

      var newClass = new Class
      {
        name = model.name,
        lst_student = model.lst_student
      };

      _context.Classes.Add(newClass);
      _context.SaveChanges();

      var result = _mapper.Map<ClassDto>(newClass);//Sử dụng AutoMapper để map từ _class sang ClassDto

      return CreatedAtAction(nameof(GetById), new { id = newClass.id }, result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var _class = _context.Classes
      .Include(c => c.lst_student)
      .FirstOrDefault(c => c.id == id);//Tìm class theo id

      if (_class == null)
      {
        return NotFound();
      }
      var result = _mapper.Map<ClassDto>(_class);//Sử dụng AutoMapper để map từ _class sang ClassDto

      return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Class model)
    {
      var _class = _context.Classes
        .Include(c => c.lst_student)
        .SingleOrDefault(c => c.id == id);
      if (_class == null)
      {
        return NotFound();
      }
      _class.name = model.name;
      if (model.lst_student != null && model.lst_student.Count > 0)
      {
        _class.lst_student.Clear();//Xóa danh sách sinh viên hiện tại của classdto
        foreach (var student in model.lst_student)
        {
          _class.lst_student.Add(student);//Thêm danh sách sinh viên mới từ model vào classdto
        }
      }
      _context.SaveChanges();
      return Ok(new { message = "Cập nhật thành công" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var _class = _context.Classes.Find(id);
      if (_class == null)
      {
        return NotFound();
      }

      _context.Classes.Remove(_class);
      _context.SaveChanges();
      return Ok(new { message = "Xóa thành công" });
    }
  }
}