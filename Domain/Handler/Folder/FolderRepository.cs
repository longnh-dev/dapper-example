using Dapper;
using DapperExample.Sharedkernel;
using Infracstructure.Context;
using Sharedkernel.Helper;
using System.Net;

namespace Domain.Handler
{
    public class FolderRepository : IFolderRepository
    {
        private readonly DapperContext _context;
        public FolderRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<Response> Add(AddFolderRequest request)
        {
            try
            {
                var connection = _context.GetConnectionInstance();
                int rowUpdated = 0;
                //TODO : Chỗ này chưa đúng, phần này phải tách ra chỗ khác. không viết ở đây
                int count = request.ListFolderAdd.Count();
                for (int i = 0; i < count; i++)
                {
                    request.ListFolderAdd.ElementAt(i).NextId = i < count - 1 ? request.ListFolderAdd.ElementAt(i + 1).Id : request.NextFolder?.Id;
                }
                var sqlInsert = "INSERT INTO FolderTree VALUES ( @Id, @NextId, @ParentId, @FolderName, @Level, @isFile)";
                foreach (var folderNew in request.ListFolderAdd.Reverse())
                {
                    var sqlUpdate = "UPDATE FolderTree SET NextId = @Id";
                    if (folderNew.NextId != null)
                    {
                        sqlUpdate += " WHERE NextId = @NextId AND ParentId = @ParentId";
                    }
                    else
                    {
                        sqlUpdate += " WHERE NextId IS NULL AND ParentId = @ParentId";
                    }
                    rowUpdated += connection.Execute(sqlUpdate, folderNew);



                    rowUpdated += connection.Execute(sqlInsert, folderNew);
                }
                //TODO: Chỗ này chưa đúng. chưa bắt hết các trường hợp, vẫn còn magic string. Created cần cho vào file string i18N
                if (rowUpdated > 0)
                    return new Response(HttpStatusCode.OK, "Created!");
                else
                    return new Response(HttpStatusCode.BadRequest, "Have an exception");
            }
            catch(Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<Response> Delete(IEnumerable<Folder> listFolderDelete)
        {
            var connection = _context.GetConnectionInstance();
            int rowDeleted = 0;
            var sqlUpdateItemBeforeDeleted = "Update FolderTree SET NextId = @NextId WHERE NextId = @Id";
            var sqlDelete = "DELETE FROM FolderTree WHERE Id = @Id";
            foreach (var item in listFolderDelete)
            {
                connection.Execute(sqlUpdateItemBeforeDeleted, item);
                rowDeleted += connection.Execute(sqlDelete, item);
            }
            if (rowDeleted > 0)
                return new Response(HttpStatusCode.OK, "Deleted!");
            else
                return new Response(HttpStatusCode.BadRequest, "Have an exception");
        }

        public async Task<IEnumerable<Folder>> GetAll()
        {
            try
            {
                var connection = _context.GetConnectionInstance();
                var query = "SELECT * FROM FolderTree WHERE Level = 0 OR Level = 1";

                var listFolder = await connection.QueryAsync<Folder>(query);

                return listFolder;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public Task<Response<Folder>> GetById(string id)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<Response> Move(MoveFolderRequest request)
        {
            var connection = _context.GetConnectionInstance();
            int rowUpdated = 0;
            int count = request.ListFolderMove.Count();
            for (int i = 0; i < count; i++)
            {
                request.ListFolderMove.ElementAt(i).NextId = i < count - 1 ? request.ListFolderMove.ElementAt(i + 1).Id : request.NextFolder?.Id;
            }
            var sqlSelectOldItem = "SELECT * FROM FolderTree WHERE Id = @Id";
            var sqlUpdateOldPreviousItem = "UPDATE FolderTree SET NextId = @NextId WHERE NextId = @Id";
            var sqlUpdateItem = "UPDATE FolderTree SET NextId = @NextId, ParentId = @ParentId WHERE Id = @Id";
            var sqlUpdateNewPreviousItem = "UPDATE FolderTree SET NextId = @Id  WHERE (@NextId IS NULL OR   NextId = @NextId) AND (@ParentId IS NULL OR  ParentId = @ParentId)";

            foreach (var folderNew in request.ListFolderMove.Reverse())
            {
                var folderOld = connection.Query<Folder>(sqlSelectOldItem, folderNew).FirstOrDefault();
                rowUpdated += connection.Execute(sqlUpdateOldPreviousItem, folderOld);
             
                    //sqlUpdateNewPreviousItem += "UPDATE FolderTree SET NextId = @Id  WHERE NextId = @NextId AND (@ParentId IS NULL OR  ParentId = @ParentId)";
                
                rowUpdated += connection.Execute(sqlUpdateNewPreviousItem, folderNew);
                rowUpdated += connection.Execute(sqlUpdateItem, folderNew);
            }
            if (rowUpdated > 0)
                return new Response(HttpStatusCode.OK, "Updated!");
            else
                return new Response(HttpStatusCode.BadRequest, "Have an exception");
        }
    }
}
