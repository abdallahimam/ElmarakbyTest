using ElmarakbyTest.Models;
using ElmarakbyTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ElmarakbyTest.Controllers
{
    public class EmployersController : Controller
    {
        private readonly IEmployerRepository _employerRepository = null;
        
        public EmployersController(IEmployerRepository employerRepository) 
        {
            _employerRepository = employerRepository;
        }

        // GET: EmployersController
        [Route("~/")]
        [Route("~/home")]
        [Route("~/employers")]
        public ActionResult Index()
        {
            var employers = _employerRepository.Get();
            return View(employers);
        }

        // GET: EmployersController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
                return NotFound();

            var employer = _employerRepository.GetById(id);
            if (employer == null)
                return NotFound();

            return View(employer);
        }

        // GET: EmployersController/Create
        public ActionResult Create()
        {
            var model = new EmployerModel();
            return View(model);
        }

        // POST: EmployersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] EmployerModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    model.Id = _employerRepository.Add(model);
                    if (model.Id > 0)
                        return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        // GET: EmployersController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _employerRepository.GetById(id);
            return View(model);
        }

        // POST: EmployersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] EmployerModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    int result = _employerRepository.Update(id, model);

                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        // GET: EmployersController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _employerRepository.GetById(id);
            return View(model);
        }

        // POST: EmployersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] EmployerModel model)
        {
            if (id == model.Id)
            {
                var result = _employerRepository.Delete(id);
                if (result == 1)
                    return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Edit), new { Id = id });
        }
    }
}
