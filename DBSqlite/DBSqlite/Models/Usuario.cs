using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace DBSqlite.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int IdUsuario { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(80)]
        
        public int Edad { get; set; }
    }
}
