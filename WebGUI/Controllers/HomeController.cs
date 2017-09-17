using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Reflection;
using Domain.UsersDB;
using Domain.EquationsRelated;

namespace WebGUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.Clear();
            List<string> EquationNames = new List<string>();
            Type [] EqTypes = typeof(EquationMethods).Assembly.GetTypes();
            foreach (Type equation in EqTypes)
            {
                if (equation.Namespace == "Domain.Equations") EquationNames.Add(equation.Name);
            }
            Session["EquationNames"] = EquationNames;
            return View();
        }

        public ActionResult Equation(string EquationType)
        {
            Type MyType = typeof(EquationInitializeConditions);
            MethodInfo[] methodes = MyType.GetMethods();
            List<string> InitialCond = new List<string>();
            foreach (MethodInfo method in methodes)
            {
               if(method.ReturnType == typeof(double[])) InitialCond.Add(method.Name);
            }
            Session["InitialConditions"] = new SelectList(InitialCond);
            List<string> MethodsNames = new List<string>();
            if (EquationType != "None")
            {
                Type EqType = typeof(EquationMethods).Assembly.GetType(EquationType);
                MethodInfo[] methods = EqType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                foreach (MethodInfo method in methods)
                {
                    MethodsNames.Add(method.Name);
                }
            }
            Session["NumericalMethods"] = new SelectList(MethodsNames);
            
            EquationParameters Temporary = (EquationParameters) Session["TempEquation"];
            if (Temporary == null || Temporary.EquationName != EquationType)
            {
                Session["Image"] = false;
                Session["TempEquation"] = null;
                return View(new EquationParameters { EquationName = EquationType });
            }
            return View(Temporary);
        }

        [HttpPost]
        public ActionResult Equation(EquationParameters parames)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                Type EqType = typeof(EquationMethods).Assembly.GetType(parames.EquationName, true);
                MethodInfo[] methods = EqType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == parames.NumericalMethods) result = (string) method.Invoke(null, new object[] { parames });

                }
                Session["result"] = result;
                Session["TempEquation"] = parames;
                Session["Image"] = true;
            }
            return View(parames);
        }
    }
}