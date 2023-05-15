using Demo.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.PL.Helpers;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        // Inheritance : DepartmentController is a Controller 
        // Aggregation : DepartmentController has a DepartmentRepository


        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepositories _departmentRepositories;
        
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        // [HttpGet] --> By Default
        public async Task<IActionResult> Index(string SearchValue)
        {

            /// // Binding Is One Way Binding At MVC .
            /// // 1.View Data Is Dictionary Object Introduced at ( ASP.NET FrameWork 3.5 )
            /// //        => It helps us to transfer the data from controller[Action] to view 
            /// ViewData["Message"] = "Hello From View Data";

            /// 2.View Bag Is Dynamic Property Introduced at ( ASP.NET FrameWork 4.0 based on dynamic Keyword )
            ///        => It helps us to transfer the data from controller[Action] to view 
            /// ViewBag.Message = "Hello View Bag";

            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
            {

                 employees = await _unitOfWork.EmployeeRepository.GetAll();

            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.SearchEmployeeByName(SearchValue);
               
            }

            var mappedEmp =  _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);


            return View(mappedEmp);
        }

        //  /Employee/Create
        public IActionResult Create()
        {
            // ViewData["Departments"]= _departmentRepositories.GetAll();
            //ViewBag.Departments = _departmentRepositories.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)   // Server Side Validation  --> [ Back End Validation ] 
            {
                // Manual Mapping  
                ///var employee = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Address = employeeVM.Address,
                ///    Age = employeeVM.Age,
                ///    DepartmentId = employeeVM.DepartmentId,
                ///    Email = employeeVM.Email,
                ///    IsActive = employeeVM.IsActive,
                ///    HireDate = employeeVM.HireDate,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    Salary = employeeVM.Salary,
                ///};

                //  Employee employee = (Employee) employeeVM;
                employeeVM.ImageUrl = DocumentSettings.UploadFile(employeeVM.Image, "images");


                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                await _unitOfWork.EmployeeRepository.Add(mappedEmp);

               int count=  await _unitOfWork.Complete();


                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Successfully";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);

        }

        // [HttpGet] --> By Default
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();   // State = 400
            }
            var employee = await _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (employee is null)
            {
                return NotFound();
            }
            DocumentSettings.DeleteFile("Images", employee.ImageUrl);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(viewName, mappedEmp);
        }

        public Task<IActionResult> Edit(int? id)
        {
            return Details(id, "Edit");
            ///{
            ///    if(id is null)
            ///    {
            ///        return BadRequest();
            ///    }
            ///    var employee = _employeeRepository.GetEmployee(id.Value);
            ///    if(employee is null)
            ///    {
            ///        return NotFound();
            ///    }
            ///    return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id is null || id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    employeeVM.ImageUrl = DocumentSettings.UploadFile(employeeVM.Image, "images");
    
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                   await _unitOfWork.EmployeeRepository.Update(mappedEmp);
                   await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    // 1.Log Error 
                    // 2.Friendly Message 

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);

        }

        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id is null || id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    await _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                     var count= await _unitOfWork.Complete();
                    if(count>0 )
                        DocumentSettings.DeleteFile(mappedEmp.ImageUrl,"Images");

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }

            return View(employeeVM);


        }






    }
}
