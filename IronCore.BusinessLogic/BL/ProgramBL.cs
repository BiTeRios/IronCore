using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic.BL
{
    public class ProgramBL : ProgramApi, IProgram
    {
        public List<ProgramDTO> GetAllPrograms()
        {
            return GetAllProgramsAPI().Select(MapToDTO).ToList();
        }

        public ProgramDTO GetProgramById(int id)
        {
            var db = GetProgramByIdAPI(id);
            return db != null ? MapToDTO(db) : null;
        }

        public bool CreateProgram(ProgramDTO newProgram)
        {
            if (string.IsNullOrWhiteSpace(newProgram.Title) || string.IsNullOrWhiteSpace(newProgram.Description))
                return false;

            var db = MapToDb(newProgram);
            CreateProgramAPI(db);
            return true;
        }

        public bool UpdateProgram(ProgramDTO updatedProgram)
        {
            if (updatedProgram == null || updatedProgram.Id <= 0)
                return false;

            var db = MapToDb(updatedProgram);
            UpdateProgramAPI(db);
            return true;
        }

        public bool DeleteProgram(int id)
        {
            if (id <= 0) return false;

            try
            {
                DeleteProgramAPI(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ProgramDTO MapToDTO(ProgramDbModel db)
        {
            return new ProgramDTO
            {
                Id = db.Id,
                Title = db.Title,
                Description = db.Description,
                Duration = db.Duration,
                Trainer = db.Trainer,
                SuitableFor = db.SuitableFor
            };
        }

        private ProgramDbModel MapToDb(ProgramDTO dto)
        {
            return new ProgramDbModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                Duration = dto.Duration,
                Trainer = dto.Trainer,
                SuitableFor = dto.SuitableFor
            };
        }
    }
}
