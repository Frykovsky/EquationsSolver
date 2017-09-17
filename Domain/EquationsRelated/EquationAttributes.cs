using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.EquationsRelated
{
    [AttributeUsage(AttributeTargets.Property)]
    class EquationCourantAttribute : ValidationAttribute
    {
        private readonly string dr;
        private readonly string dt;
        private readonly string courant;
        private readonly string equationname;
        public EquationCourantAttribute(string varstep, string timestep, string equation, string shoulduse)
        {
            equationname = equation;
            dr = varstep;
            dt = timestep;
            courant = shoulduse;
        }

        protected override ValidationResult IsValid (object value, ValidationContext valcontext)
        {
            var courantmodel = valcontext.ObjectType.GetProperty(courant);
            var courantvalue = courantmodel.GetValue(valcontext.ObjectInstance, null);
            if ((bool)courantvalue)
            {
                var namemodel = valcontext.ObjectType.GetProperty(equationname);
                var drmodel = valcontext.ObjectType.GetProperty(dr);
                var dtmodel = valcontext.ObjectType.GetProperty(dt);
                var namevalue = namemodel.GetValue(valcontext.ObjectInstance, null);
                var drvalue = drmodel.GetValue(valcontext.ObjectInstance, null);
                var dtvalue = dtmodel.GetValue(valcontext.ObjectInstance, null);
                double courantnumber = 0.0;
                if (namevalue.ToString() == "Domain.Equations.Advection")
                {
                    courantnumber = (double) value * (double) dtvalue / (double) drvalue;
                } else if (namevalue.ToString() == "Domain.Equations.Diffusion")
                {
                    courantnumber = 2.0 * (double)value * (double)dtvalue / ((double)drvalue*(double)drvalue);
                }
                if ( courantnumber > 1.0) return new ValidationResult("The stability condition does not hold, try to decrease the coeffitient");
                else return ValidationResult.Success;
            } else
            {
                return ValidationResult.Success;
            }
            
            
        }
    }
}
