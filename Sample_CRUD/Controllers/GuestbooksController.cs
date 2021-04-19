using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample_CRUD.Service;
using Sample_CRUD.ViewModels;


namespace Sample_CRUD.Controllers
{
    public class GuestbooksController : Controller
    {
        private readonly GuestbooksDBService GuestbookService = new GuestbooksDBService();


        // GET: Guestbooks
        public ActionResult Index()
        {
            //宣告一個新頁面的模型
            GuestbooksViewModel Data = new GuestbooksViewModel();
            // 從Service 中取得所需要的陣列資料
            Data.DataList = GuestbookService.GetDataList();
            //將頁面傳入View中
            return View(Data);
        }
    }
}