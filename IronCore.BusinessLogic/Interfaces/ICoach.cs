using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.Domain.Entities.Coach;

namespace IronCore.BusinessLogic.Interfaces
{
    interface ICoach
    {
        CoachDbModel getInfoAboutCoach(int CoachID);
        bool deleteCoach(int CoachID);
        void addCoach(CoachDbModel coach);
        void modifyCoach(CoachDbModel coach);
    }
}
