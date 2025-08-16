using FirstProject.Data;
using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeData _dbContext;

        public EmployeeController(EmployeeData dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var model = _dbContext.Employees.OrderBy(x => x.Id);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = _dbContext.Employees.Find(id);
            _dbContext.Employees.Remove(model);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(EmployeeModels employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Picture != null)
                {
                    string fileName = Path.GetFileName(employee.Picture.FileName);
                    string extension = Path.GetExtension(fileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pictures", employee.Name + extension);

                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("Picture", "Only .jpg, .png, .jpeg file are allowed");
                        return View(employee);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        employee.Picture.CopyTo(stream);
                    }
                    employee.PicturePath = "/Pictures/" + employee.Name + extension;

                    _dbContext.Employees.Add(employee);
                    if (_dbContext.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage
                        ));
                        ModelState.AddModelError(" ", message);
                        return View(employee);
                    }
                }
                return View();
            }
            return View(employee);
        }


        public ActionResult Edit(int id)
        {
            var model = _dbContext.Employees.Find(id);

            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(EmployeeModels employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Picture != null)
                {
                    string fileName = Path.GetFileName(employee.Picture.FileName);
                    string extension = Path.GetExtension(fileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", employee.Name + extension);

                    if (extension != ".jpg" && extension != ".png" && extension != "jpeg")
                    {
                        ModelState.AddModelError("Picture", "Only .jpg, .png, .jpeg file are allowed");
                        return View(employee);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        employee.Picture.CopyTo(stream);
                    }
                    employee.PicturePath = "/Pictures/" + employee.Name + extension;

                }

                else
                {
                    employee.PicturePath = employee.PicturePath;
                }

                _dbContext.Employees.Update(employee);
                if (_dbContext.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage
                    ));
                    ModelState.AddModelError(" ", message);
                    return View(employee);
                }
            }

            return View(employee);
        }


    }

}
