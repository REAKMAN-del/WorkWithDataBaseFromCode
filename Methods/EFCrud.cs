using DataBase.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Methods
{
    public static class EFCrud
    {
        public static void ShowPcBuilds()
        {
            using (var db = new PCAssembly())
            {
                var pcBuilds = db.PC_Builds.ToList();

                foreach(var pcBuild in pcBuilds)
                {
                    Console.WriteLine($"ID: {pcBuild.BuildID}. {pcBuild.BuildName} (Цена: {pcBuild.TotalPrice} USD) [Собран: {pcBuild.IsAssembled}]");
                }
            }
        }

        public static void AddPcBuild(string name, decimal price)
        {
            using(var db = new PCAssembly())
            {
                var b = new PС_Build
                {
                    BuildName = name,
                    TotalPrice = price,
                    CreatedDate = DateTime.Now,
                    IsAssembled = false
                };

                db.PC_Builds.Add(b);
                db.SaveChanges();

                Console.WriteLine("Добавлена сборка с ID " + b.BuildID);
            }
        }

        public static void CompletePcBuild(int id)
        {
            using (var db = new PCAssembly())
            {
                var build = db.PC_Builds.Find(id);

                if(build != null)
                {
                    build.IsAssembled = true;

                    db.Entry(build).Property(x => x.IsAssembled).IsModified = true;

                    db.SaveChanges();
                    Console.WriteLine("Статус успешно обновлен в базе!");
                }
            }
        }

        public static void DeletePcBuild(int id)
        {
            using( var db = new PCAssembly())
            {
                var build = db.PC_Builds.Find(id);

                if(build != null)
                {
                    db.PC_Builds.Remove(build);
                    db.SaveChanges();

                    Console.WriteLine("Проект удален.");
                }
            }
        }
    }
}
