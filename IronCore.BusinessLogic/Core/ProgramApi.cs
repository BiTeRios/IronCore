using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Program;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.Core
{
    public class ProgramApi
    {
        public List<ProgramDbModel> GetAllProgramsAPI()
        {
            using (var context = new ProgramContext())
            {
                return context.Programs.ToList();
            }
        }

        public ProgramDbModel GetProgramByIdAPI(int id)
        {
            using (var context = new ProgramContext())
            {
                return context.Programs.FirstOrDefault(p => p.Id == id);
            }
        }

        public void CreateProgramAPI(ProgramDbModel program)
        {
            using (var context = new ProgramContext())
            {
                context.Programs.Add(program);
                context.SaveChanges();
            }
        }

        public void UpdateProgramAPI(ProgramDbModel program)
        {
            using (var context = new ProgramContext())
            {
                context.Entry(program).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProgramAPI(int id)
        {
            using (var context = new ProgramContext())
            {
                var program = context.Programs.Find(id);
                if (program == null) throw new ArgumentException("Program not found");

                context.Programs.Remove(program);
                context.SaveChanges();
            }
        }
    }
}
