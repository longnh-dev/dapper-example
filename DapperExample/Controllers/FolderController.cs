using DapperExample.Handler;
using DapperExample.Sharedkernel;
using Domain.Handler;
using Microsoft.AspNetCore.Mvc;
using Sharedkernel.Helper;
using System.Data;
using System.Net;



namespace DapperExample.Controllers
{
    [ApiController]
    [Route("api/folders")]
    public class FolderController : ControllerBase
    {
        private readonly IFolderRepository _folderRepo;
        public FolderController(IFolderRepository folderRepo)
        {
            _folderRepo = folderRepo;
        }



        [HttpGet]
        public async Task<IActionResult> GetFolders()
        {
            try
            {
                var listFolderFromDatabase = await _folderRepo.GetAll();
                Dictionary<string, Folder> dic = new Dictionary<string, Folder>();
                foreach (var item in listFolderFromDatabase)
                {
                    dic.Add((item.ParentId == null ? "null" : item.ParentId) + (item.NextId == null ? "null" : item.NextId), item);
                }

                var treeRoot = TreeHelper.GenerateTree2(listFolderFromDatabase, dic, null, null).Reverse();
                return Ok(treeRoot);
            }
            catch (Exception ex)
            {
                //Magic number
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpGet("FlatFolders")]
        public async Task<IActionResult> GetFlatFolders()
        {
            var listFolderFromDatabase = await _folderRepo.GetAll();
            return Ok(listFolderFromDatabase);
        }



        [HttpPost]
        public async Task<Response> AddFolders(Domain.Handler.AddFolderRequest addFolderRequest)
        {
            try
            {
                var addFolderResponse = await _folderRepo.Add(addFolderRequest);
                return addFolderResponse;
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpDelete]
        public async Task<Response> DeleteFolders(IEnumerable<Folder> listDeleteFolder)
        {
            try
            {
                var deleteFolderResponse = await _folderRepo.Delete(listDeleteFolder);
                return deleteFolderResponse;
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpPut]
        public async Task<Response> MoveFolder(Domain.Handler.MoveFolderRequest moveFolderRequest)
        {
            try
            {
                var moveFolderResponse = await _folderRepo.Move(moveFolderRequest);
                return moveFolderResponse;
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, ex.Message);
            }
        }




    }
}
