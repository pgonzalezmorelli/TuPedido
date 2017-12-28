using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TuPedido.Helpers;

namespace TuPedido.Repositories
{
    public class Database<T> : IDatabase<T> where T : new()
    {
        private readonly IFileHelper fileHelper;
        private readonly IConfiguration configuration;
        private readonly SQLiteAsyncConnection database;

        public Database(IFileHelper fileHelper, IConfiguration configuration)
        {
            this.fileHelper = fileHelper;
            this.configuration = configuration;

            database = new SQLiteAsyncConnection(this.fileHelper.GetLocalPath(this.configuration.Database.ConnectionString));
            database.CreateTableAsync<T>().Wait();
        }
        
        public async Task<int> Delete(T item)
        {
            return await database.DeleteAsync(item);
        }

        public async Task<T> First()
        {
            return await database.Table<T>().FirstOrDefaultAsync();
        }

        public async Task<int> Insert(T item)
        {
            return await database.InsertAsync(item);
        }

        public async Task<List<T>> Select()
        {
            return await database.Table<T>().ToListAsync();
        }

        public async Task<List<T>> Select(Expression<Func<T, bool>> filter)
        {
            return await database.Table<T>().Where(filter).ToListAsync();
        }

        public async Task<int> Update(T item)
        {
            return await database.UpdateAsync(item);
        }
    }
}
