using ElmarakbyTest.Data;
using ElmarakbyTest.Models;
using Microsoft.AspNetCore.Hosting;

namespace ElmarakbyTest.Repository
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployerRepository(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment) 
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public int Add(EmployerModel model)
        {
            // saving file to image folder and get its path.
            string localFolder = "images/employers/";
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, localFolder);
            string filename = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            string filePath = Path.Combine("/", localFolder, filename);
            string serverFilename = Path.Combine(serverFolder, filename);
            if (!Directory.Exists(serverFolder)) {
                Directory.CreateDirectory(serverFolder);
            }
            model.Image.CopyTo(new FileStream(serverFilename, FileMode.Create));
            model.ImagePath = filePath;

            var employer = new Employer()
            {
                Name = model.Name,
                Title = model.Title,
                Salary = model.Salary,
                ImagePath = model.ImagePath
            };

            _dbContext.Employers.Add(employer);
            _dbContext.SaveChanges();
            return employer.Id;
        }

        public int Update(int id, EmployerModel model)
        {
            var employer = _dbContext.Employers.Find(id);
            if (employer == null) { return -1; }

            var oldImagePath = employer.ImagePath;

            // saving file to image folder and get its path.
            string localFolder = "images/employers/";
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, localFolder);
            string filename = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            string filePath = Path.Combine("/", localFolder, filename);
            string serverFilename = Path.Combine(serverFolder, filename);
            if (!Directory.Exists(serverFolder))
            {
                Directory.CreateDirectory(serverFolder);
            }
            model.Image.CopyTo(new FileStream(serverFilename, FileMode.Create));
            model.ImagePath = filePath;

            employer.Name = model.Name;
            employer.Title = model.Title;
            employer.Salary = model.Salary;
            employer.ImagePath = model.ImagePath;

            _dbContext.Employers.Update(employer);
            _dbContext.SaveChanges();

            // remove old image from the server.
            if (oldImagePath != null)
            {
                var oldServerForlder = Path.Combine(_webHostEnvironment.WebRootPath, oldImagePath);
                var file = new FileInfo(oldServerForlder);
                if (file.Exists) { file.Delete(); }
            }

            return 1;
        }

        public int Delete(int id) 
        {
            var employer = _dbContext.Employers.Find(id);
            if (employer == null) { return -1; }
            var oldImagePath = employer.ImagePath;

            _dbContext.Employers.Remove(employer);
            _dbContext.SaveChanges();

            // remove old image from the server.
            var oldServerForlder = Path.Combine(_webHostEnvironment.WebRootPath, oldImagePath);
            var file = new FileInfo(oldServerForlder);
            if (file.Exists) { file.Delete(); }

            return 1;
        }

        public List<EmployerModel> Get()
        {
            return _dbContext.Employers.Select(e => new EmployerModel
            {
                Id = e.Id,
                Name = e.Name,
                Title = e.Title,
                Salary = e.Salary,
                ImagePath = e.ImagePath
            }).ToList();
        }

        public EmployerModel GetById(int id)
        {
            return _dbContext.Employers.Where(e => e.Id == id).Select(e => new EmployerModel
            {
                Id = e.Id,
                Name = e.Name,
                Title = e.Title,
                Salary = e.Salary,
                ImagePath = e.ImagePath
            }).FirstOrDefault();
        }
    }
}
