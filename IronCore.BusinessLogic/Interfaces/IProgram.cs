using IronCore.Domain.Entities.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.Interfaces
{
    public interface IProgram
    {
        List<ProgramDTO> GetAllPrograms();
        ProgramDTO GetProgramById(int id);
        bool CreateProgram(ProgramDTO newProgram);
        bool UpdateProgram(ProgramDTO updatedProgram);
        bool DeleteProgram(int id);
    }
}
