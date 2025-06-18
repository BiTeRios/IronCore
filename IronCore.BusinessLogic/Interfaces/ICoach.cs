using IronCore.Domain.Entities.Coach;
using System.Collections.Generic;

namespace IronCore.BusinessLogic.Interfaces
{
    public interface ICoach
    {
        List<CoachDTO> GetAllCoaches();
        CoachDTO GetCoachById(int id);
        bool CreateCoach(CoachDTO newCoach);
        bool DeleteCoach(int id);
        bool UpdateCoach(CoachDTO coach);
    }
}
