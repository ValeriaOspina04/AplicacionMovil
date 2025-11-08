using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DBSqlite.Models;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace DBSqlite.Data
{
    public class SQLiteHelper 
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(String dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Usuario>().Wait();
        }


        public Task<int> GuardarUsuarioAsync(Usuario Est)
        {
            if (Est.IdUsuario == 0)
            {
                return db.UpdateAsync(Est);

            }
            else
            {
                return db.InsertAsync(Est);
            }

        }
        //para visuaizar todos los estudiantes que estan en la base de datos
        public Task<List<Usuario>> GetUsuarioAsync()
        {
            return db.Table<Usuario>().ToListAsync();
        }


        //recuperar todos los estudiantes por el ID
        public Task<Usuario> GetUsuarioByIdAsync(int IdUsuario)
        {
            return db.Table<Usuario>().Where(a => a.IdUsuario == IdUsuario).FirstOrDefaultAsync();
        }
    }
}
