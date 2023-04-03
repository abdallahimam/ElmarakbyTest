using ElmarakbyTest.Models;

namespace ElmarakbyTest.Repository
{
    public interface IEmployerRepository
    {
        public int Add(EmployerModel model);
        public int Update(int id, EmployerModel model);
        public int Delete(int id);
        public List<EmployerModel> Get();
        public EmployerModel GetById(int id);
    }

}
