using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableProject.Context;
using TableProject.Enum;
using TableProject.Services;
using TableProject.Services.ClassBuilder;

namespace TableProject.Controllers
{
    public class CreateTableController : Controller
    {
        private readonly ITableServices _tableServices;
        private static string _tableName;

        public CreateTableController()
        {
            _tableServices = new TableServices();
        }

        // GET: CreateTable
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateNewTable(string tableName, int fieldNum)
        {
            _tableName = tableName;
            TempData["count"] = fieldNum;

            foreach (string name in _tableServices.GetAllTableNames())
            {
                if (tableName == name)
                {
                    TempData["count"] = 0;
                    TempData["message"] = "این نام قبلا ثبت شده است!";
                }
            }


            return RedirectToAction("Index");
        }
        public ActionResult InsertNewTable(string[] fieldNames, FieldTypes[] fieldTypes)
        {
            MyClassBuilder builder = new MyClassBuilder(_tableName);
            var newClass = builder.CreateObject(MyClassBuilder.SetName(fieldNames), MyClassBuilder.SetTypes(fieldTypes));
            Type TP = builder.GetType();

            //کار نمی کنه!!!!!!!!!!!!!!!!
            using (OnlineDB db = new OnlineDB())
            {
                db.Type = TP;

                var config = new DbMigrationsConfiguration<OnlineDB> { AutomaticMigrationsEnabled = true };
                var migrator = new DbMigrator(config);
                
                migrator.Update();
            }
            return RedirectToAction("Index","Home");
        }
    }
}