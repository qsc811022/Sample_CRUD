using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sample_CRUD.Models;
using Sample_CRUD.Service;
using Sample_CRUD.ViewModels;


namespace Sample_CRUD.Controllers
{
    public class GuestbooksController : Controller
    {
        private readonly GuestbooksDBService GuestbookService = new GuestbooksDBService();


        // GET: Guestbooks
        public ActionResult Index(string Search)
        {
            //宣告一個新頁面的模型
            GuestbooksViewModel Data = new GuestbooksViewModel();
            // 從Service 中取得所需要的陣列資料
            Data.Search = Search;
            Data.DataList = GuestbookService.GetDataList(Data.Search);
            //將頁面傳入View中
            return View(Data);
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include ="Name,Content")]Guestbook Data)
        {

            //使用Service 來新增一筆資料
            GuestbookService.InsertGuestbooks(Data);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            //取得頁面所需資料,由Service取得
            Guestbook Data = GuestbookService.GetDataById(Id);
            //將資料傳入 View 中
            return View(Data);
        }
        
        [HttpPost]
        public ActionResult Edit(int  Id,[Bind(Include ="Name,Content")] Guestbook UpdateData)
        {
            if (GuestbookService.CheckUpdate(Id))
            {
                //編號設置修改中
                UpdateData.Id=Id;
                //使用 Service 來修改資料
                GuestbookService.UpdateGuestbooks(UpdateData);
                //重新導向頁面至開始頁面
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //回覆留言頁面要根據傳入編號來回覆的資料
        public ActionResult Reply(int Id)
        {
            Guestbook Data = GuestbookService.GetDataById(Id);

            return View(Data);

        }
        [HttpPost]
        //使用Bind的include來定義接受的欄位，用來避免傳入其他不相干值
        public ActionResult Reply(int Id, [Bind(Include ="Reply,ReplyTime")] Guestbook ReplyData)
        {
            if (GuestbookService.CheckUpdate(Id))
            {
                //將編號設定
                ReplyData.Id = Id;
                GuestbookService.ReplyGuestbooks(ReplyData);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int Id)
        {
            //Service 來刪除資料
            GuestbookService.DeleteGuestbooks(Id);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index");
        }


    }
}