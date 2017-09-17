using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.UsersDB
{
    public class ExperimentsDB
    {
        public string User { get; set; }

        public byte [] Gif { get; set; }
        public string MimeType { get; set; }

        [Key]
        public virtual int EquationID { get; set; }
    }
}
