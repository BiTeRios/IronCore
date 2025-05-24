using IronCore.BusinessLogic.Interfaces;
using IronCore.Domain.Entities.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCore.BusinessLogic.Core;
using IronCore.BusinessLogic.DBModel;
using IronCore.Domain.Entities.Product;
using System.IO;

namespace IronCore.BusinessLogic.BL
{
    public class CoachBL : ICoach
    {
        private readonly CoachContext ctx = new CoachContext();

        public CoachDbModel getInfoAboutCoach(int id) => ctx.Coaches.Find(id);
        public IEnumerable<CoachDbModel> getAllCoaches() => ctx.Coaches.ToList();
        public bool deleteCoach(int id)
        {
            var c = ctx.Coaches.Find(id);
            if (c is null) return false;
            ctx.Coaches.Remove(c);
            ctx.SaveChanges();
            return true;
        }

        //public void SaveFromViewModel(CoachViewModel vm, Stream photoStream, string fileName)
        //{
        //    // 1) Сохраняем файл, если он есть
        //    if (photoStream != null && photoStream.Length > 0)
        //    {
        //        var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/Trainers");
        //        Directory.CreateDirectory(folder);
        //        var path = Path.Combine(folder, fileName);
        //        using (var fs = File.Create(path))
        //            photoStream.CopyTo(fs);
        //        vm.ImagePath = "/Images/Trainers/" + fileName;
        //    }

        //    // 2) Маппим ViewModel → доменную сущность
        //    var coach = new CoachCl
        //    {
        //        ID = vm.ID,
        //        FullName = vm.FullName,
        //        Qualification = vm.Qualification,
        //        Specialization = vm.Specialization,
        //        ExperienceTime = vm.ExperienceTime,
        //        Bio = vm.Bio,
        //        Testimonials = vm.Testimonials,
        //        TelegramUrl = vm.TelegramUrl,
        //        InstagramUrl = vm.InstagramUrl,
        //        SteamUrl = vm.SteamUrl,
        //        RegistrationDate = vm.RegistrationDate,
        //        ImagePath = vm.ImagePath
        //    };
        //}

        public void addCoach(CoachDbModel coach)
        {
            ctx.Coaches.Add(coach);
            ctx.SaveChanges();
        }

        public void modifyCoach(CoachDbModel coach)
        {
            var current = ctx.Coaches.Find(coach.Id);
            if (current is null) return;
            ctx.Entry(current).CurrentValues.SetValues(coach);
            ctx.SaveChanges();
        }
    }

}
