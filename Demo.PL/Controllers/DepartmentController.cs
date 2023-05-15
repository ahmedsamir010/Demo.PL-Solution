using Microsoft.AspNetCore.Mvc;
using Demo.BLL.Repositories;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using Demo.PL.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // Inheritance : DepartmentController is a Controller 
        // Aggregation : DepartmentController has a DepartmentRepository


        //private readonly IDepartmentRepositories _departmentRepositories;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            //_departmentRepositories = departmentRepositories;
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // Binding Is One Way Binding At MVC .
            // 1.View Data Is Dictionary Object Introduced at ( ASP.NET FrameWork 3.5 )
            //        => It helps us to transfer the data from controller[Action] to view 
            ViewData["Message1"] = "Hello View Data From Employee";


            // 2.View Bag Is Dynamic Property Introduced at ( ASP.NET FrameWork 4.0 based on dynamic Keyword )
            //        => It helps us to transfer the data from controller[Action] to view 

            ViewBag.Message = "Hello View Bag From Department";


            var departments = await _unitOfWork.DepartmentRepositories.GetAll();

            var mappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);


            return View(mappedDepartment);
        }
        //[HttpGet] By Default 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid) // Server Side Validation  [ Back End Validation ]
            {
                // 3. TempData
                var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentViewModel);

                await _unitOfWork.DepartmentRepositories.Add(mappedDepartment);
                var count = await _unitOfWork.Complete();

                if (count > 0)
                {
                    TempData["Message2"] = "Department Is Creadted Successfully";
                }

                return RedirectToAction(nameof(Index));
            }

            return View(departmentViewModel);
        }


        // Department/Details/1
        // Department/Details
        //[HttpGet] By Default
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();   // State = 400
            }

            var department = await _unitOfWork.DepartmentRepositories.GetById(id.Value);

            if (department is null)
            {
                return NotFound();
            }
            var mappedDepartment = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, mappedDepartment);

        }

        // [HttpGet]  --> By Default 
        public Task<IActionResult> Edit(int? id)
        {
            return Details(id, "Edit");
            ///if (id is null)
            ///{
            ///    return BadRequest();   // State = 400
            ///}
            ///var department = _departmentRepositories.GetDepartment(id.Value);
            ///if (department is null)
            ///{
            ///    return NotFound();
            ///}
            ///return View(department);

        }
        [HttpPost]
        public async Task<IActionResult> Edit([FormRoute] int? id, DepartmentViewModel departmentViewModel)
        {
            if (id != departmentViewModel.Id)
                return BadRequest();
            if (ModelState.IsValid)

            {
                try
                {
                    var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentViewModel);

                    _unitOfWork.DepartmentRepositories.Update(mappedDepartment);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    // 1.Log Error 
                    // 2.Friendly Message 

                    ModelState.AddModelError(string.Empty, Ex.Message);

                }
            }

            return View(departmentViewModel);
        }

        // [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, DepartmentViewModel departmentViewModel)
        {
            if (id is null || id != departmentViewModel.Id)
            {
                return BadRequest();  // 400
            }
            try
            {
                var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentViewModel);
                await _unitOfWork.DepartmentRepositories.Delete(mappedDepartment);
                _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(departmentViewModel);
        }



    }
}
